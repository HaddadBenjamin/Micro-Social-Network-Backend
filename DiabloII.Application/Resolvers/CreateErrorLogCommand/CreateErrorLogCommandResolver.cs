using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace DiabloII.Application.Resolvers.CreateErrorLogCommand
{
    public class CreateErrorLogCommandResolver : ICreateErrorLogCommandResolver
    {
        private readonly ExceptionContext _exceptionContext;

        private readonly HttpStatusCode _httpResponseStatus;

        public CreateErrorLogCommandResolver(ExceptionContext exceptionContext, HttpStatusCode httpResponseStatus)
        {
            _exceptionContext = exceptionContext;
            _httpResponseStatus = httpResponseStatus;
        }

        public Domain.Commands.ErrorLogs.CreateErrorLogCommand Resolve()
        {
            var errorLogContentObject = CreateTheErrorLogContentObject();
            var errorLogContent = JsonConvert.SerializeObject(errorLogContentObject, Formatting.Indented);

            return new Domain.Commands.ErrorLogs.CreateErrorLogCommand
            {
                Id = Guid.NewGuid(),
                CreationDateUtc = DateTime.UtcNow,
                Content = errorLogContent
            };
        }

        private dynamic CreateTheErrorLogContentObject() => new
        {
            Exception = CreateTheExceptionObject(),
            Request = CreateTheHttpObject()
        };

        private dynamic CreateTheExceptionObject()
        {
            var exception = _exceptionContext.Exception.Demystify();
            var innerException = exception.InnerException;
            var innerExceptionObject = innerException != null ? new
            {
                Message = exception.Message,
                Source = exception.Source,
                Data = exception.Data,
                StackTrace = exception.StackTrace.Split(Environment.NewLine),
            } : null;

            return new
            {
                Message = exception.Message,
                Source = exception.Source,
                Data = exception.Data,
                StackTrace = exception.StackTrace.Split(Environment.NewLine),
                InnerException = innerExceptionObject
            };
        }

        private dynamic CreateTheHttpObject() => new
        {
            Request = CreateTheHttpRequestObject(),
            Response = CreateTheHttpResponseObject()
        };

        private dynamic CreateTheHttpResponseObject() => new
        {
            HttpStatus = _httpResponseStatus
        };

        private dynamic CreateTheHttpRequestObject()
        {
            var httpRequest = _exceptionContext.HttpContext.Request;
            var requestUrl = httpRequest.GetDisplayUrl();
            var requestHeaders = httpRequest.Headers.Select(header =>
            {
                var headerValue = string.Join(',', header.Value);

                return $"{header.Key} : {headerValue}";
            });
            var requestBody = string.Empty;

            try
            {
                using (var streamReader = new StreamReader(httpRequest.Body))
                    requestBody = streamReader.ReadToEnd();

                return new
                {
                    RequestUrl = requestUrl,
                    RequestBody = requestBody,
                    RequestHeaders = requestHeaders,
                };
            }
            catch (Exception)
            {
                return new
                {
                    RequestUrl = requestUrl,
                    RequestBody = requestBody,
                    RequestHeaders = requestHeaders,
                };
            }
        }
    }
}
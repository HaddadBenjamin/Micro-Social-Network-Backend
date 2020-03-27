using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.ErrorLogs.Models;
using DiabloII.Items.Api.Exceptions;
using DiabloII.Items.Api.Services.ErrorLogs;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace DiabloII.Items.Api
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        private static readonly Dictionary<string, HttpStatusCode?> _exceptionTypeNameToHttpStatusMapper = new Dictionary<string, HttpStatusCode?>()
        {
            {nameof(BadRequestException), HttpStatusCode.BadRequest}
        };

        public override void OnException(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception;
            var exceptionTypeName = exception.GetType().Name;
            var evaluatedResponseHttpStatus = _exceptionTypeNameToHttpStatusMapper.GetValueOrDefault(exceptionTypeName);
            var responseHttpStatus = evaluatedResponseHttpStatus ?? HttpStatusCode.InternalServerError;

            SetExceptionResult(exceptionContext, exception, responseHttpStatus);

            exceptionContext.ExceptionHandled = true;

            var errorLogService = (IErrorLogsService)exceptionContext.HttpContext.RequestServices.GetService(typeof(IErrorLogsService));
            var errorLogCreator = new ErrorLoggerCreator(exceptionContext, responseHttpStatus);
            var errorLog = errorLogCreator.Create();

            errorLogService.Log(errorLog);
        }

        private static void SetExceptionResult(ExceptionContext exceptionContext, Exception exception, HttpStatusCode code) =>
        exceptionContext.Result = new JsonResult(exception)
        {
            StatusCode = (int)code
        };
    }

    public class ErrorLoggerCreator
    {

        private readonly ExceptionContext _exceptionContext;

        private readonly HttpStatusCode _httpResponseStatus;

        public ErrorLoggerCreator(ExceptionContext exceptionContext, HttpStatusCode httpResponseStatus)
        {
            _exceptionContext = exceptionContext;
            _httpResponseStatus = httpResponseStatus;
        }

        public ErrorLog Create()
        {
            var errorLogContentObject = CreateTheErrorLogContentObject();
            var errorLogContent = JsonConvert.SerializeObject(errorLogContentObject, Formatting.Indented);

            return new ErrorLog
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
            using (var streamReader = new StreamReader(httpRequest.Body))
            {
                var requestBody = streamReader.ReadToEnd();
                var requestUrl = httpRequest.GetDisplayUrl();
                var requestHeaders = httpRequest.Headers.Select(header =>
                {
                    var headerValue = string.Join(',', header.Value);

                    return $"{header.Key} : {headerValue}";
                });

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
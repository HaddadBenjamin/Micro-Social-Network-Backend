using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.DbContext.Suggestions;
using DiabloII.Items.Api.Exceptions;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DiabloII.Items.Api
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        private static Dictionary<string, HttpStatusCode?> _exceptionTypeNameToHttpStatusMapper = new Dictionary<string, HttpStatusCode?>()
        {
            {nameof(BadRequestException), HttpStatusCode.BadRequest}
        };

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var exceptionTypeName = exception.GetType().Name;
            var evaluatedHttpStatus = _exceptionTypeNameToHttpStatusMapper.GetValueOrDefault(exceptionTypeName);
            var httpStatus = evaluatedHttpStatus ?? HttpStatusCode.InternalServerError;

            SetExceptionResult(context, exception, httpStatus);

            context.ExceptionHandled = true;

            LogExceptionInDatabase(context);
        }

        private static void SetExceptionResult(ExceptionContext context, Exception exception, HttpStatusCode code) =>
            context.Result = new JsonResult(exception)
            {
                StatusCode = (int)code
            };

        private void LogExceptionInDatabase(ExceptionContext context)
        {
            var errorLog = CreateTheErrorLog(context);
            var dbContext = (ApplicationDbContext)context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext));

            dbContext.ErrorLogs.Add(errorLog);
            dbContext.SaveChanges();
        }

        private ErrorLog CreateTheErrorLog(ExceptionContext exceptionContext)
        {
            var errorLogContentObject = CreateTheErrorLogContentObject(exceptionContext);
            var errorLogContent = JsonConvert.SerializeObject(errorLogContentObject, Formatting.Indented);

            return new ErrorLog
            {
                Id = Guid.NewGuid(),
                CreationDateUtc = DateTime.UtcNow,
                Content = errorLogContent
            };
        }

        private static dynamic CreateTheErrorLogContentObject(ExceptionContext exceptionContext) => new
        {
            Exception = CreateTheExceptionObject(exceptionContext),
            Request = CreateTheRequestObject(exceptionContext)
        };

        private static dynamic CreateTheExceptionObject(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception.Demystify();
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

        private static dynamic CreateTheRequestObject(ExceptionContext exceptionContext)
        {
            var httpRequest = exceptionContext.HttpContext.Request;
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
                    RequestHeaders = requestHeaders
                };
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            { nameof(BadRequestException), HttpStatusCode.BadRequest }
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

        private static void SetExceptionResult(ExceptionContext context, Exception exception, HttpStatusCode code) => context.Result = new JsonResult(exception)
        {
            StatusCode = (int)code
        };

        private void LogExceptionInDatabase(ExceptionContext context)
        {
            var applicationLog = CreateApplicationLog(context);
            var dbContext = (ApplicationDbContext)context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext));

            dbContext.Logs.Add(applicationLog);
            dbContext.SaveChanges();
        }

        private ApplicationLog CreateApplicationLog(ExceptionContext context)
        {
            var endpointUrl = context.HttpContext.Request.GetDisplayUrl();
            var requestHeaders = context.HttpContext.Request.Headers.Select(header => new { header.Key, header.Value });
            var exceptionDemystified = context.Exception.Demystify();
            var logMessage = JsonConvert.SerializeObject(new
            {
                Id = Guid.NewGuid(),
                Exception = exceptionDemystified,
                EndpointUrl = endpointUrl,
                RequestHeaders = requestHeaders,
            }, Formatting.Indented);
           
            return new ApplicationLog
            {
                CreationDateUtc = DateTime.UtcNow,
                Level = LogLevel.Error,
                Message = logMessage
            };
        }
    }
}
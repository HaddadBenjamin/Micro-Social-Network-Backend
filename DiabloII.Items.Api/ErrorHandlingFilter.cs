using System;
using System.Collections.Generic;
using System.Net;
using DiabloII.Items.Api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DiabloII.Items.Api
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        private static Dictionary<string, HttpStatusCode?> ExceptionTypeNameToHttpStatusMapper = new Dictionary<string, HttpStatusCode?>()
        {
            { nameof(BadRequestException), HttpStatusCode.BadRequest }
        };

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var exceptionTypeName = exception.GetType().Name;
            var evaluatedHttpStatus = ExceptionTypeNameToHttpStatusMapper.GetValueOrDefault(exceptionTypeName);
            var httpStatus = evaluatedHttpStatus ?? HttpStatusCode.InternalServerError;

            SetExceptionResult(context, exception, httpStatus);

            context.ExceptionHandled = true;
        }

        private static void SetExceptionResult(ExceptionContext context, Exception exception, HttpStatusCode code) => context.Result = new JsonResult(exception)
        {
            StatusCode = (int)code
        };
    }
}
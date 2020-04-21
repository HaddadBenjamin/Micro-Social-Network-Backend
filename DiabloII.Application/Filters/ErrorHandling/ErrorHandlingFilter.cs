using System;
using System.Collections.Generic;
using System.Net;
using DiabloII.Domain.Exceptions;
using DiabloII.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DiabloII.Application.Filters.ErrorHandling
{
    public class ErrorHandlingFilter : ExceptionFilterAttribute
    {
        private static readonly Dictionary<string, HttpStatusCode?> _exceptionTypeNameToHttpStatusMapper = new Dictionary<string, HttpStatusCode?>()
        {
            { nameof(BadRequestException), HttpStatusCode.BadRequest },
            { nameof(UnauthorizedException), HttpStatusCode.Unauthorized },
            { nameof(AlreadyExistsException), HttpStatusCode.Forbidden },
            { nameof(NotFoundException), HttpStatusCode.NotFound },
        };

        public override void OnException(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception;
            var exceptionTypeName = exception.GetType().Name;
            var evaluatedResponseHttpStatus = _exceptionTypeNameToHttpStatusMapper.GetValueOrDefault(exceptionTypeName);
            var responseHttpStatus = evaluatedResponseHttpStatus ?? HttpStatusCode.InternalServerError;

            SetExceptionResult(exceptionContext, exception, responseHttpStatus);

            exceptionContext.ExceptionHandled = true;

            var errorLogCommandHandler = (IErrorLogCommandHandler)exceptionContext.HttpContext.RequestServices.GetService(typeof(IErrorLogCommandHandler));
            var errorLogCreator = new ErrorLoggerCreator(exceptionContext, responseHttpStatus);
            var errorLog = errorLogCreator.Create();

            errorLogCommandHandler.Create(errorLog);
        }

        private static void SetExceptionResult(ExceptionContext exceptionContext, Exception exception, HttpStatusCode code) =>
            exceptionContext.Result = new JsonResult(exception)
            {
                StatusCode = (int)code
            };
    }
}
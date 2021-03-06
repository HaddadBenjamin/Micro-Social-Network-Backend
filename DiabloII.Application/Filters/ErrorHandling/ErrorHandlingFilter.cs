﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using DiabloII.Application.Resolvers.Implementations.CreateErrorLogCommand;
using DiabloII.Domain.Exceptions;
using MediatR;
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

        public override async Task OnExceptionAsync(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception;
            var exceptionTypeName = exception.GetType().Name;
            var evaluatedResponseHttpStatus = _exceptionTypeNameToHttpStatusMapper.GetValueOrDefault(exceptionTypeName);
            var responseHttpStatus = evaluatedResponseHttpStatus ?? HttpStatusCode.InternalServerError;

            SetExceptionResult(exceptionContext, exception, responseHttpStatus);

            exceptionContext.ExceptionHandled = true;

            var mediator = (IMediator)exceptionContext.HttpContext.RequestServices.GetService(typeof(IMediator));
            var commandResolver = new CreateErrorLogCommandResolver(exceptionContext, responseHttpStatus);
            var command = commandResolver.Resolve();

            await mediator.Send(command);
        }

        private static void SetExceptionResult(ExceptionContext exceptionContext, Exception exception, HttpStatusCode code) =>
            exceptionContext.Result = new JsonResult(exception)
            {
                StatusCode = (int)code
            };
    }
}
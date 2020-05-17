using Halcyon.HAL;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DiabloII.Application.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static CreatedResult CreatedByUsingTheRequestRoute(this ControllerBase controller, [ActionResultObjectValue] object value) => controller
            .Created(controller.HttpContext.Request.Path.Value, value);
    }
}
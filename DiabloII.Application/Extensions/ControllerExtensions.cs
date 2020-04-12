using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DiabloII.Application.Extensions
{
    public static class ControllerExtensions
    {
        public static CreatedResult CreatedByUsingTheRequestRoute(this Controller controller, [ActionResultObjectValue] object value) => controller
            .Created(controller.HttpContext.Request.Path.Value, value);
    }
}
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

        public static string GetUrl(this ControllerBase controller, string subUrl = null)
        {
            var baseUrl = controller.Request.GetDisplayUrl();

            subUrl = subUrl is null ? string.Empty : $"/{subUrl}";

            var url = $"{baseUrl}{subUrl}";

            return url;
        }

        public static HALResponse ToHalResponse(this ControllerBase controller, object model)
        {
            var url = controller.Request.GetDisplayUrl();
            var halResponse = new HALResponse(model, new HALModelConfig
            {
                LinkBase = url,
                ForceHAL = false
            });

            return halResponse;
        }
    }
}
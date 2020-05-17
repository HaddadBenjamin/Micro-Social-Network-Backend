using Halcyon.HAL;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Services.Hals
{
    public class BaseHalService
    {
        protected static HALResponse ToHalResponse(ControllerBase controller, object model)
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
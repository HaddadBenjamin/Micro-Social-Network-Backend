using System.Net.Http;
using DiabloII.Application.Extensions;
using DiabloII.Application.Responses.Suggestions;
using Microsoft.AspNetCore.Mvc;
using Halcyon.HAL;

namespace DiabloII.Application.Services.Hals
{
    public static class SuggestionHalDecorator
    {
        public static HALResponse DecorateSuggestionLinks(SuggestionDto dto, ControllerBase controller)
        {
            var halResponse = controller.ToHalResponse(dto);

            return halResponse.AddLink(controller, "suggestion_create", HttpMethod.Post);
        }
    }
}

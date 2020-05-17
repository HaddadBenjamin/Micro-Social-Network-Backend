using System.Net.Http;
using DiabloII.Application.Extensions;
using DiabloII.Application.Responses.Suggestions;
using Microsoft.AspNetCore.Mvc;
using Halcyon.HAL;

namespace DiabloII.Application.Services.Hals
{
    public class SuggestionHalService : BaseHalService
    {
        public HALResponse AddLinks(SuggestionDto dto, ControllerBase controller)
        {
            var halResponse = ToHalResponse(controller, dto);

            return halResponse.AddLink(controller, "suggestion_create", HttpMethod.Post);
        }
    }
}

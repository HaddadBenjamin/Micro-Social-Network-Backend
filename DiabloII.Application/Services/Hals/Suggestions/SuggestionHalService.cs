using System.Net.Http;
using DiabloII.Application.Extensions;
using DiabloII.Application.Responses.Suggestions;
using Halcyon.HAL;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Services.Hals.Suggestions
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

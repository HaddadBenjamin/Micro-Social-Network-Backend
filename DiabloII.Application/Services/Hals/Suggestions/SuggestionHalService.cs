using System.Net.Http;
using DiabloII.Application.Extensions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Services.UserIdResolver;
using Halcyon.HAL;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Services.Hals.Suggestions
{
    public class SuggestionHalService : BaseHalService, ISuggestionHalService
    {
        private readonly string _userId;

        public SuggestionHalService(IUserIdResolverService userIdResolver) => _userId = userIdResolver.Resolve();

        public HALResponse AddLinks(SuggestionDto dto, ControllerBase controller)
        {
            var halResponse = ToHalResponse(controller, dto);
            var canEditSuggestion = _userId == dto.CreatedBy;

            halResponse.AddLink(controller, "suggestion_create", HttpMethod.Post);

            if (canEditSuggestion)
                halResponse.AddLink(controller, "suggestion_delete", HttpMethod.Delete);

            return halResponse;
        }
    }
}

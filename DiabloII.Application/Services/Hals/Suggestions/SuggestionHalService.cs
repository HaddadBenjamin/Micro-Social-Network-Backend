using System.Net.Http;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Services.UserIdResolver;
using Halcyon.HAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Services.Hals.Suggestions
{
    public class SuggestionHalService : BaseHalService, ISuggestionHalService
    {
        private readonly string _userId;

        public SuggestionHalService(IUserIdResolverService userIdResolver, IHttpContextAccessor httpContextAccessor) : base (httpContextAccessor) =>
            _userId = userIdResolver.Resolve();

        public HALResponse AddLinks(SuggestionDto dto, ControllerBase controller)
        {
            var halResponse = ToHalResponse(dto);
            var canEditSuggestion = _userId == dto.CreatedBy;

            AddLink(halResponse, "suggestion_create", HttpMethod.Post);

            if (canEditSuggestion)
                AddLink(halResponse, "suggestion_delete", HttpMethod.Delete);

            return halResponse;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using DiabloII.Application.Mappers.Suggestions;
using DiabloII.Application.Resolvers.Implementations.UserId;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Application.Services.Hals.Bases;
using Halcyon.HAL;
using Microsoft.AspNetCore.Http;

namespace DiabloII.Application.Services.Hals.Domains.Suggestions
{
    public class SuggestionHalService : BaseHalService, ISuggestionHalService
    {
        private readonly string _userId;
        private static readonly string _domain = "suggestions";

        public SuggestionHalService(IUserIdResolver userIdResolver, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor) =>
            _userId = userIdResolver.Resolve();

        public HALResponse AddLinks(IReadOnlyCollection<HALResponse> halResponses)
        {
            var responses = new HalResponses
            {
                Elements = halResponses
            };
            var halResponse = ToHalResponse(responses);

            AddLink(halResponse, "suggestion_create", HttpMethod.Post, _domain);

            return halResponse;
        }

        public HALResponse AddLinks(SuggestionDto suggestion)
        {
            var suggestionHalResponse = SuggestionDtoToHalLayer.Map(suggestion, this);
            var canEditSuggestion = _userId == suggestion.CreatedBy;
            var halResponse = ToHalResponse(suggestionHalResponse);
            var baseUrl = $"{_domain}/{suggestion.Id}";

            AddLink(halResponse, "comment_create", HttpMethod.Post, $"{baseUrl}/comments");

            if (canEditSuggestion)
                AddLink(halResponse, "suggestion_delete", HttpMethod.Delete, baseUrl);
            else
                AddLink(halResponse, "vote_create", HttpMethod.Post, $"{baseUrl}/votes");


            return halResponse;
        }

        public HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId)
        {
            var halResponse = ToHalResponse(comment);
            var canEditComment = _userId == comment.CreatedBy;

            if (canEditComment)
                AddLink(halResponse, "comment_delete", HttpMethod.Delete, $"{_domain}/{suggestionId}/comments/{comment.Id}");

            return halResponse;
        }
    }
}

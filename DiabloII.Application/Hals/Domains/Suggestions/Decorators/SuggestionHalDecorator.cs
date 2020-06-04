using System;
using System.Collections.Generic;
using System.Net.Http;
using DiabloII.Application.Hals.Bases;
using DiabloII.Application.Hals.Domains.Suggestions.Rules;
using DiabloII.Application.Mappers.Suggestions;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Suggestions;
using Halcyon.HAL;
using Microsoft.AspNetCore.Http;

namespace DiabloII.Application.Hals.Domains.Suggestions.Decorators
{
    public class SuggestionHalDecorator : BaseHalDecorator, ISuggestionHalDecorator
    {
        private readonly ISuggestionHalRules _halRules;
        private static readonly string _domain = "suggestions";

        public SuggestionHalDecorator(IHttpContextAccessor httpContextAccessor, ISuggestionHalRules halRules) : base(httpContextAccessor) =>
            _halRules = halRules;

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
            var halResponse = ToHalResponse(suggestionHalResponse);
            var baseUrl = $"{_domain}/{suggestion.Id}";

            AddLink(halResponse, "comment_create", HttpMethod.Post, $"{baseUrl}/comments");

            if (_halRules.CanEditASuggestion(suggestion))
                AddLink(halResponse, "suggestion_delete", HttpMethod.Delete, baseUrl);

            if (_halRules.CanAddAVote(suggestion))
                AddLink(halResponse, "vote_create", HttpMethod.Post, $"{baseUrl}/votes");

            return halResponse;
        }

        public HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId)
        {
            var halResponse = ToHalResponse(comment);

            if (_halRules.CanEditAComment(comment))
                AddLink(halResponse, "comment_delete", HttpMethod.Delete, $"{_domain}/{suggestionId}/comments/{comment.Id}");

            return halResponse;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
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

        private readonly ISuggestionUrlExtractor _urlExtractor;

        public SuggestionHalDecorator(IHttpContextAccessor httpContextAccessor, ISuggestionHalRules halRules, ISuggestionUrlExtractor urlExtractor) : base(httpContextAccessor)
        {
            _halRules = halRules;
            _urlExtractor = urlExtractor;
        }

        public HALResponse AddLinks(IReadOnlyCollection<HALResponse> halResponses)
        {
            var responses = new HalResponses
            {
                Elements = halResponses
            };
            var halResponse = ToHalResponse(responses);

            AddLink(halResponse, "suggestion_create", HttpMethod.Post, _urlExtractor.Create());

            return halResponse;
        }

        public HALResponse AddLinks(SuggestionDto suggestion)
        {
            var suggestionHalResponse = SuggestionDtoToHalLayer.Map(suggestion, this);
            var halResponse = ToHalResponse(suggestionHalResponse);

            AddLink(halResponse, "comment_create", HttpMethod.Post, _urlExtractor.CreateComment(suggestion.Id));

            if (_halRules.CanEditASuggestion(suggestion))
                AddLink(halResponse, "suggestion_delete", HttpMethod.Delete, _urlExtractor.Delete(suggestion.Id));

            if (_halRules.CanAddAVote(suggestion))
                AddLink(halResponse, "vote_create", HttpMethod.Post, _urlExtractor.CreateVote(suggestion.Id));

            return halResponse;
        }

        public HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId)
        {
            var halResponse = ToHalResponse(comment);

            if (_halRules.CanEditAComment(comment))
                AddLink(halResponse, "comment_delete", HttpMethod.Delete, _urlExtractor.DeleteComment(suggestionId, comment.Id));

            return halResponse;
        }
    }
}

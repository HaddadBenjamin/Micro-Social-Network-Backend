using System;
using System.Collections.Generic;
using System.Net.Http;
using DiabloII.Application.Mappers.Suggestions;
using DiabloII.Application.Migrations;
using DiabloII.Application.Resolvers.UserId;
using DiabloII.Application.Responses;
using DiabloII.Application.Responses.Suggestions;
using Halcyon.HAL;
using Microsoft.AspNetCore.Http;

namespace DiabloII.Application.Services.Hals.Suggestions
{
    public class SuggestionHalService : BaseHalService, ISuggestionHalService
    {
        private readonly string _userId;

        public SuggestionHalService(IUserIdResolver userIdResolver, IHttpContextAccessor httpContextAccessor) : base (httpContextAccessor) =>
            _userId = userIdResolver.Resolve();

        public HALResponse AddLinks(IReadOnlyCollection<HALResponse> halResponses)
        {
            var responses = new HalResponses
            {
                Elements = halResponses
            };
            var halResponse = ToHalResponse(responses);

            AddLink(halResponse, "suggestion_create", HttpMethod.Post);

            return halResponse;
        }

        public HALResponse AddLinks(SuggestionDto suggestion)
        {
            var suggestionHalResponse = SuggestionDtoToHalLayer.Map(suggestion, this);
            var canEditSuggestion = _userId == suggestion.CreatedBy;
            var halResponse = ToHalResponse(suggestionHalResponse);
           
            AddLink(halResponse, "comment_create", HttpMethod.Post, $"{suggestion.Id}/comments");

            if (canEditSuggestion)
                AddLink(halResponse, "suggestion_delete", HttpMethod.Delete, suggestion.Id.ToString());
            else
                AddLink(halResponse, "vote_create", HttpMethod.Post, $"{suggestion.Id}/votes");


            return halResponse;
        }

        public HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId)
        {
            var halResponse = ToHalResponse(comment);
            var canEditComment = _userId == comment.CreatedBy;


            if (canEditComment)
                AddLink(halResponse, "comment_delete", HttpMethod.Delete, $"{suggestionId}/comments/{comment.Id}");

            return halResponse;
        }
    }
}

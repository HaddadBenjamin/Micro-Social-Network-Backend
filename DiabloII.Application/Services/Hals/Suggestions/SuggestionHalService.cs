using System;
using System.Linq;
using System.Net.Http;
using AutoMapper;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Services.UserIdResolver;
using Halcyon.HAL;
using Microsoft.AspNetCore.Http;

namespace DiabloII.Application.Services.Hals.Suggestions
{
    public class SuggestionHalService : BaseHalService, ISuggestionHalService
    {
        private readonly IMapper _mapper;
        private readonly string _userId;

        public SuggestionHalService(IUserIdResolverService userIdResolver, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base (httpContextAccessor)
        {
            _mapper = mapper;
            _userId = userIdResolver.Resolve();
        }

        public HALResponse AddLinks(SuggestionDto suggestion)
        {
            var suggestionHalResponse = ToSuggestionHalResponse(suggestion);
            var canEditSuggestion = _userId == suggestion.CreatedBy;
            var halResponse = ToHalResponse(suggestionHalResponse);

            AddLink(halResponse, "suggestion_create", HttpMethod.Post);

            if (canEditSuggestion)
                AddLink(halResponse, "suggestion_delete", HttpMethod.Delete, suggestion.Id.ToString());

            return halResponse;
        }

        // This logic mapping should be extracted, in better case should go in configuration of automapper.
        private SuggestionHalResponse ToSuggestionHalResponse(SuggestionDto suggestion)
        {
            var suggestionHalResponse = _mapper.Map<SuggestionHalResponse>(suggestion);

            suggestionHalResponse.Votes = suggestion.Votes.Select(vote => AddLinks(vote, suggestion.Id)).ToList();
            suggestionHalResponse.Comments = suggestion.Comments.Select(comment => AddLinks(comment, suggestion.Id)).ToList();

            return suggestionHalResponse;
        }

        private HALResponse AddLinks(SuggestionVoteDto vote, Guid suggestionId)
        {
            var halResponse = ToHalResponse(vote);
            var canVote = _userId != vote.CreatedBy;
            var subUrl = $"{suggestionId}/votes";

            if (canVote)
                AddLink(halResponse, "vote_create", HttpMethod.Post, subUrl);

            return halResponse;
        }

        private HALResponse AddLinks(SuggestionCommentDto comment, Guid suggestionId)
        {
            var halResponse = ToHalResponse(comment);
            var canEditComment = _userId == comment.CreatedBy;
            var subUrl = $"{suggestionId}/comments";

            AddLink(halResponse, "comment_create", HttpMethod.Post, subUrl);

            if (canEditComment)
                AddLink(halResponse, "comment_delete", HttpMethod.Delete, $"{subUrl}/{comment.Id}");

            return halResponse;
        }
    }
}

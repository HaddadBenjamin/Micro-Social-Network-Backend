using System;
using System.Collections.Generic;
using DiabloII.Items.Api.DbContext.Suggestions.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace DiabloII.Items.Api.Services.Suggestions
{
    public interface ISuggestionRepository
    {
        #region Read
        IIncludableQueryable<Suggestion, ICollection<SuggestionComment>> GetQueryableSuggestions();
        
        IReadOnlyCollection<Suggestion> GetAll();

        Suggestion GetFirstSuggestion(Guid suggestionId);

        Suggestion GetUserSuggestion(Guid suggestionId, string userIp);

        SuggestionVote GetUserVoteOrDefault(Suggestion suggestion, string userIp);

        SuggestionComment GetUserComment(Suggestion suggestion, Guid commentId, string userIp);

        bool DoesSuggestionExists(Guid suggestionId);

        bool DoesSuggestionExists(Guid suggestionId, string userIp);

        bool DoesSuggestionContentIsUnique(string suggestionContent);

        bool DoesUserCommentExists(Guid suggestionId, Guid commentId, string userIp);
        #endregion

        #region Write
        void AddVote(Suggestion suggestion, SuggestionVote suggestionVote);

        Suggestion AddComment(Guid suggestionId, SuggestionComment suggestionComment);

        void RemoveSuggestion(Guid suggestionId, string userIp);

        void RemoveVote(Suggestion suggestion, SuggestionVote suggestionVote);

        Suggestion RemoveComment(Guid suggestionId, Guid commentId, string userIp);

        void RemoveComment(Suggestion suggestion, SuggestionComment suggestionComment);
        #endregion
    }
}
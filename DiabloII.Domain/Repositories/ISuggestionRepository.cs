using System;
using System.Collections.Generic;
using DiabloII.Domain.Models.Suggestions;

namespace DiabloII.Domain.Repositories
{
    public interface ISuggestionRepository
    {
        #region Read
        IReadOnlyCollection<Suggestion> GetAll();

        Suggestion GetFirstSuggestion(Guid suggestionId);

        Suggestion GetUserSuggestion(Guid suggestionId, string userId);

        SuggestionVote GetUserVoteOrDefault(Suggestion suggestion, string userId);

        SuggestionComment GetUserComment(Suggestion suggestion, Guid commentId, string userId);

        bool DoesSuggestionExists(Guid suggestionId);

        bool IsOwnerOfTheSuggestion(Guid suggestionId, string userId);

        bool DoesCommentExists(Guid commentId);

        bool IsOwnerOfTheComment(Guid suggestionId, Guid commentId, string userId);

        bool DoesSuggestionContentIsUnique(string suggestionContent);
        #endregion

        #region Write
        void AddVote(Suggestion suggestion, SuggestionVote suggestionVote);

        Suggestion AddComment(Guid suggestionId, SuggestionComment suggestionComment);

        void RemoveUserSuggestion(Guid suggestionId, string userId);

        void RemoveVote(Suggestion suggestion, SuggestionVote suggestionVote);

        Suggestion RemoveUserComment(Guid suggestionId, Guid commentId, string userId);

        void RemoveComment(Suggestion suggestion, SuggestionComment suggestionComment);
        #endregion
    }
}
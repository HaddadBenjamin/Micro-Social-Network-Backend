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

        Suggestion GetUserSuggestion(Guid suggestionId, string userIp);

        SuggestionVote GetUserVoteOrDefault(Suggestion suggestion, string userIp);

        SuggestionComment GetUserComment(Suggestion suggestion, Guid commentId, string userIp);

        bool DoesSuggestionExists(Guid suggestionId);

        bool DoesSuggestionExists(Guid suggestionId, string userIp);

        bool DoesCommentExists(Guid commentId);

        bool DoesUserCommentExists(Guid suggestionId, Guid commentId, string userIp);

        bool DoesSuggestionContentIsUnique(string suggestionContent);
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
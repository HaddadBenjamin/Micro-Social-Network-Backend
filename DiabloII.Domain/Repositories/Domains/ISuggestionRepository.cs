using System;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories.Bases;

namespace DiabloII.Domain.Repositories.Domains
{
    public interface ISuggestionRepository :
        IRepositoryGetAll<Suggestion>,
        IRepositoryGet<Suggestion, Guid>
    {
        #region Read
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
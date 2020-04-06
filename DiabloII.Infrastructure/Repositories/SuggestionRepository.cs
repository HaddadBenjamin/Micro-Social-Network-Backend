using System;
using System.Collections.Generic;
using System.Linq;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Repositories;
using DiabloII.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DiabloII.Infrastructure.Repositories
{
    public class SuggestionRepository : ISuggestionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SuggestionRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        #region Read
        public IIncludableQueryable<Suggestion, ICollection<SuggestionComment>> GetQueryableSuggestions() => _dbContext.Suggestions
            .Include(suggestion => suggestion.Votes)
            .Include(suggestion => suggestion.Comments);

        public IReadOnlyCollection<Suggestion> GetAll() => GetQueryableSuggestions().ToList();

        public Suggestion GetFirstSuggestion(Guid suggestionId) =>
            GetQueryableSuggestions().First(vote => vote.Id == suggestionId);

        public Suggestion GetUserSuggestion(Guid suggestionId, string userId) =>
            GetQueryableSuggestions().FirstOrDefault(suggestion => suggestion.Id == suggestionId && suggestion.CreatedBy == userId);

        public SuggestionVote GetUserVoteOrDefault(Suggestion suggestion, string userId) => suggestion
            .Votes
            .FirstOrDefault(vote => vote.CreatedBy == userId);

        public SuggestionComment GetUserComment(Suggestion suggestion, Guid commentId, string userId) => suggestion
            .Comments
            .First(comment => comment.CreatedBy == userId && comment.Id == commentId);

        public bool DoesSuggestionExists(Guid suggestionId) =>
            _dbContext.Suggestions.Any(suggestion => suggestion.Id == suggestionId);
       
        public bool IsOwnerOfTheSuggestion(Guid suggestionId, string userId) => _dbContext.Suggestions.Any(suggestion => suggestion.Id == suggestionId && suggestion.CreatedBy == userId);

        public bool DoesCommentExists(Guid commentId) =>
            _dbContext.SuggestionComments.Any(comment => comment.Id == commentId);

        public bool IsOwnerOfTheComment(Guid suggestionId, Guid commentId, string userId) => _dbContext.Suggestions
            .Any(suggestion => suggestion.Id == suggestionId &&
                               suggestion.Comments.Any(comment => comment.Id == commentId && comment.CreatedBy == userId));

        public bool DoesSuggestionContentIsUnique(string suggestionContent) =>
            _dbContext.Suggestions.All(suggestion => suggestion.Content != suggestionContent);
        #endregion

        #region Write
        public void AddVote(Suggestion suggestion, SuggestionVote suggestionVote)
        {
            suggestion.Votes.Add(suggestionVote);
            _dbContext.SuggestionVotes.Add(suggestionVote);
        }

        public Suggestion AddComment(Guid suggestionId, SuggestionComment suggestionComment)
        {
            var suggestion = GetFirstSuggestion(suggestionId);

            suggestion.Comments.Add(suggestionComment);
            _dbContext.SuggestionComments.Add(suggestionComment);

            return suggestion;
        }

        public void RemoveUserSuggestion(Guid suggestionId, string userId)
        {
            var userSuggestion = GetUserSuggestion(suggestionId, userId);

            _dbContext.Remove(userSuggestion);
        }

        public void RemoveVote(Suggestion suggestion, SuggestionVote suggestionVote)
        {
            suggestion.Votes.Remove(suggestionVote);
            _dbContext.SuggestionVotes.Remove(suggestionVote);
        }

        public Suggestion RemoveUserComment(Guid suggestionId, Guid commentId, string userId)
        {
            var suggestion = GetFirstSuggestion(suggestionId);
            var userComment = GetUserComment(suggestion, commentId, userId);

            RemoveComment(suggestion, userComment);

            return suggestion;
        }

        public void RemoveComment(Suggestion suggestion, SuggestionComment suggestionComment)
        {
            suggestion.Comments.Remove(suggestionComment);
            _dbContext.SuggestionComments.Remove(suggestionComment);
        }
        #endregion
    }
}
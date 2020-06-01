using System;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Domain.Validations.Suggestions
{
    public class CommonSuggestionRepositoryValidationContext
    {
        public ISuggestionRepository Repository { get; }

        public Guid Id { get; set; }

        public Guid CommentId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public CommonSuggestionRepositoryValidationContext(ISuggestionRepository repository) => Repository = repository;

        public CommonSuggestionRepositoryValidationContext(ISuggestionRepository repository, Guid id)
        {
            Id = id;
            Repository = repository;
        }
    }
}
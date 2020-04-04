using System;
using DiabloII.Items.Api.Repositories.Suggestions;

namespace DiabloII.Items.Api.Validations.Suggestions
{
    public class SuggestionDbContextValidationContext
    {
        public ISuggestionRepository Repository { get; }
      
        public Guid Id { get; set; }
        
        public Guid CommentId { get; set; }

        public string Content { get; set; }

        public string Ip { get; set; }

        public SuggestionDbContextValidationContext(ISuggestionRepository repository) => Repository = repository;

        public SuggestionDbContextValidationContext(ISuggestionRepository repository, Guid id)
        {
            Id = id;
            Repository = repository;
        }
    }
}
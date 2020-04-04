using System;
using DiabloII.Items.Api.Repositories.Suggestions;

namespace DiabloII.Items.Api.Vallidations.Suggestions
{
    public class SuggestionDbContextValidationContext
    {
        public ISuggestionRepository Repository { get; }
      
        public Guid Id { get; set; }

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
using System;
using DiabloII.Items.Api.DbContext;

namespace DiabloII.Items.Api.Validators.Suggestions
{
    public class SuggestionDbContextValidatorContext
    {
        public ApplicationDbContext DbContext { get; }
      
        public Guid Id { get; }

        public string Content { get; set; }

        public string Ip { get; set; }

        public SuggestionDbContextValidatorContext(ApplicationDbContext dbContext) => DbContext = dbContext;

        public SuggestionDbContextValidatorContext(ApplicationDbContext dbContext, Guid id)
        {
            Id = id;
            DbContext = dbContext;
        }
    }
}
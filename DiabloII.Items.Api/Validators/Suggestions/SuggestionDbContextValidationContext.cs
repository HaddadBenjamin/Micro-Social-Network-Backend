using System;
using DiabloII.Items.Api.DbContext;

namespace DiabloII.Items.Api.Validators.Suggestions
{
    public class SuggestionDbContextValidationContext
    {
        public ApplicationDbContext DbContext { get; }
      
        public Guid Id { get; }

        public string Content { get; set; }

        public string Ip { get; set; }

        public SuggestionDbContextValidationContext(ApplicationDbContext dbContext) => DbContext = dbContext;

        public SuggestionDbContextValidationContext(ApplicationDbContext dbContext, Guid id)
        {
            Id = id;
            DbContext = dbContext;
        }
    }
}
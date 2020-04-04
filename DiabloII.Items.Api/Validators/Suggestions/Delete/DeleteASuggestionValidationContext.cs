using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Delete
{
    public class DeleteASuggestionValidationContext
    {
        public DeleteASuggestionDto Dto { get; set; }

        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidationContext DbContextValidationContext { get; set; }

        public DeleteASuggestionValidationContext(DeleteASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidationContext = new SuggestionDbContextValidationContext(dbContext, dto.Id)
            {
                Ip = dto.Ip
            };
        }
    }
}
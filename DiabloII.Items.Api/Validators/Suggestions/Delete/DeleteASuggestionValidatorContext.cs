﻿using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Delete
{
    public class DeleteASuggestionValidatorContext
    {
        public DeleteASuggestionDto Dto { get; set; }

        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidatorContext DbContextValidatorContext { get; set; }

        public DeleteASuggestionValidatorContext(DeleteASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidatorContext = new SuggestionDbContextValidatorContext(dbContext, dto.Id)
            {
                Ip = dto.Ip
            };
        }
    }
}
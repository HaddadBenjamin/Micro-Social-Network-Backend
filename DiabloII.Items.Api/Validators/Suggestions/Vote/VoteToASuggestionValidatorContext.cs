﻿using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Requests.Suggestions;

namespace DiabloII.Items.Api.Validators.Suggestions.Vote
{
    public class VoteToASuggestionValidatorContext
    {
        public VoteToASuggestionDto Dto { get; set; }
       
        public ApplicationDbContext DbContext { get; }

        public SuggestionDbContextValidatorContext DbContextValidatorContext { get; }

        public VoteToASuggestionValidatorContext(VoteToASuggestionDto dto, ApplicationDbContext dbContext)
        {
            Dto = dto;
            DbContext = dbContext;
            DbContextValidatorContext = new SuggestionDbContextValidatorContext(dbContext, dto.SuggestionId);
        }
    }
}
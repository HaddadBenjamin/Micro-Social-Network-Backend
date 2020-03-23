using System;
using System.Collections.Generic;
using DiabloII.Items.Api.DbContext;
using DiabloII.Items.Api.Queries.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;
using DiabloII.Items.Api.Services.Suggestions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace DiabloII.Items.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [EnableCors("AllowOrigin")]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionsService SuggestionsService;
        private readonly ApplicationDbContext ApplicationDbContext;

        public SuggestionsController(ISuggestionsService suggestionsService, ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
            SuggestionsService = suggestionsService;
        } 

        [Route("create")]
        [HttpPost]
        public void Create(CreateSuggestionDto createSuggestion) => SuggestionsService.Create(createSuggestion, ApplicationDbContext);

        [Route("vote")]
        [HttpPost]
        public SuggestionDto Vote(SuggestionVoteDto suggestionVote) => SuggestionsService.Vote(suggestionVote, ApplicationDbContext);

        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<SuggestionDto> GetAll() => SuggestionsService.GetAll(ApplicationDbContext);
    }
}

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
        private readonly ISuggestionsService _suggestionsService;

        public SuggestionsController(ISuggestionsService suggestionsService)
        {
            _suggestionsService = suggestionsService;
        } 

        [Route("create")]
        [HttpPost]
        public void Create(CreateASuggestionDto createASuggestion) => _suggestionsService.Create(createASuggestion);

        [Route("vote")]
        [HttpPost]
        public SuggestionDto Vote(VoteToASuggestionDto voteToASuggestion) => _suggestionsService.Vote(voteToASuggestion);

        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<SuggestionDto> GetAll() => _suggestionsService.GetAll();
    }
}

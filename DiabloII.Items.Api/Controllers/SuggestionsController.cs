using System.Collections.Generic;
using DiabloII.Items.Api.Requests.Suggestions;
using DiabloII.Items.Api.Responses.Suggestions;
using DiabloII.Items.Api.Services.Suggestions;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Items.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionsService _suggestionsService;

        public SuggestionsController(ISuggestionsService suggestionsService) => _suggestionsService = suggestionsService;

        [Route("create")]
        [HttpPost]
        public SuggestionDto Create([FromBody] CreateASuggestionDto createASuggestion) => _suggestionsService.Create(createASuggestion);

        [Route("vote")]
        [HttpPost]
        public SuggestionDto Vote([FromBody] VoteToASuggestionDto voteToASuggestion) => _suggestionsService.Vote(voteToASuggestion);

        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<SuggestionDto> GetAll() => _suggestionsService.GetAll();
    }
}

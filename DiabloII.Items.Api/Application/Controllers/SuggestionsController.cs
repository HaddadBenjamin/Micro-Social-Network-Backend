using System;
using System.Collections.Generic;
using System.Linq;
using DiabloII.Items.Api.Application.Mappers.Suggestions;
using DiabloII.Items.Api.Application.Requests.Suggestions;
using DiabloII.Items.Api.Application.Responses.Suggestions;
using DiabloII.Items.Api.Application.Services.Suggestions;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Items.Api.Application.Controllers
{
    [Route("api/v1/[controller]")]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionsService _suggestionsService;

        public SuggestionsController(ISuggestionsService suggestionsService) => _suggestionsService = suggestionsService;

        [Route("create")]
        [HttpPost]
        public SuggestionDto Create([FromBody] CreateASuggestionDto createASuggestion) =>
            SuggestionDtoMapper.ToSuggestionDto(_suggestionsService.Create(createASuggestion));

        [Route("vote")]
        [HttpPost]
        public SuggestionDto Vote([FromBody] VoteToASuggestionDto voteToASuggestion) =>
            SuggestionDtoMapper.ToSuggestionDto(_suggestionsService.Vote(voteToASuggestion));

        [Route("comment")]
        [HttpPost]
        public SuggestionDto Comment([FromBody] CommentASuggestionDto commentASuggestion) =>
            SuggestionDtoMapper.ToSuggestionDto(_suggestionsService.Comment(commentASuggestion));

        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<SuggestionDto> GetAll() => _suggestionsService
            .GetAll()
            .Select(SuggestionDtoMapper.ToSuggestionDto)
            .ToList();

        [Route("delete")]
        [HttpDelete]
        public Guid Delete([FromBody] DeleteASuggestionDto deleteASuggestion) => _suggestionsService.Delete(deleteASuggestion);

        [Route("deletecomment")]
        [HttpDelete]
        public SuggestionDto DeleteComment([FromBody] DeleteASuggestionCommentDto deleteASuggestionComment) =>
            SuggestionDtoMapper.ToSuggestionDto(_suggestionsService.DeleteAComment(deleteASuggestionComment));
    }
}

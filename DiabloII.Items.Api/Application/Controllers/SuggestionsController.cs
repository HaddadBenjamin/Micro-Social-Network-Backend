using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public SuggestionsController(ISuggestionsService suggestionsService, IMapper mapper)
        {
            _suggestionsService = suggestionsService;
            _mapper = mapper;
        }

        [Route("create")]
        [HttpPost]
        public SuggestionDto Create([FromBody] CreateASuggestionDto createASuggestion) =>
            _mapper.Map<SuggestionDto>(_suggestionsService.Create(createASuggestion));

        [Route("vote")]
        [HttpPost]
        public SuggestionDto Vote([FromBody] VoteToASuggestionDto voteToASuggestion) =>
            _mapper.Map<SuggestionDto>(_suggestionsService.Vote(voteToASuggestion));

        [Route("comment")]
        [HttpPost]
        public SuggestionDto Comment([FromBody] CommentASuggestionDto commentASuggestion) =>
            _mapper.Map<SuggestionDto>(_suggestionsService.Comment(commentASuggestion));

        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<SuggestionDto> GetAll() => _suggestionsService
            .GetAll()
            .Select(_mapper.Map<SuggestionDto>)
            .ToList();

        [Route("delete")]
        [HttpDelete]
        public Guid Delete([FromBody] DeleteASuggestionDto deleteASuggestion) => _suggestionsService.Delete(deleteASuggestion);

        [Route("deletecomment")]
        [HttpDelete]
        public SuggestionDto DeleteComment([FromBody] DeleteASuggestionCommentDto deleteASuggestionComment) =>
            _mapper.Map<SuggestionDto>(_suggestionsService.DeleteAComment(deleteASuggestionComment));
    }
}

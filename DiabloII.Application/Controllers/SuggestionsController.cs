using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Readers;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/[controller]")]
    public class SuggestionsController : Controller
    {
        private readonly ISuggestionReader _reader;
        private readonly ISuggestionCommandHandler _handler;
        private readonly IMapper _mapper;

        public SuggestionsController(ISuggestionReader reader, ISuggestionCommandHandler handler, IMapper mapper)
        {
            _reader = reader;
            _handler = handler;
            _mapper = mapper;
        }

        [Route("getall")]
        [HttpGet]
        public IReadOnlyCollection<SuggestionDto> GetAll() => _reader
            .GetAll()
            .Select(_mapper.Map<SuggestionDto>)
            .ToList();

        [Route("create")]
        [HttpPost]
        public SuggestionDto Create([FromBody] CreateASuggestionDto createASuggestion) =>
            _mapper.Map<SuggestionDto>(_handler.Create(_mapper.Map<CreateASuggestionCommand>(createASuggestion)));

        [Route("vote")]
        [HttpPost]
        public SuggestionDto Vote([FromBody] VoteToASuggestionDto voteToASuggestion) =>
            _mapper.Map<SuggestionDto>(_handler.Vote(_mapper.Map<VoteToASuggestionCommand>(voteToASuggestion)));

        [Route("comment")]
        [HttpPost]
        public SuggestionDto Comment([FromBody] CommentASuggestionDto commentASuggestion) =>
            _mapper.Map<SuggestionDto>(_handler.Comment(_mapper.Map<CommentASuggestionCommand>(commentASuggestion)));

        [Route("delete")]
        [HttpDelete]
        public Guid Delete([FromBody] DeleteASuggestionDto deleteASuggestion) =>
            _handler.Delete(_mapper.Map<DeleteASuggestionCommand>(deleteASuggestion));

        [Route("deletecomment")]
        [HttpDelete]
        public SuggestionDto DeleteComment([FromBody] DeleteASuggestionCommentDto deleteASuggestionComment) =>
            _mapper.Map<SuggestionDto>(_handler.DeleteAComment(_mapper.Map<DeleteASuggestionCommentCommand>(deleteASuggestionComment)));
    }
}

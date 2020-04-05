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
        public SuggestionDto Create([FromBody] CreateASuggestionDto createASuggestion)
        {
            var command = _mapper.Map<CreateASuggestionCommand>(createASuggestion);
            var model = _handler.Create(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return responseDto;
        }

        [Route("vote")]
        [HttpPost]
        public SuggestionDto Vote([FromBody] VoteToASuggestionDto voteToASuggestion)
        {
            var command = _mapper.Map<VoteToASuggestionCommand>(voteToASuggestion);
            var model = _handler.Vote(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return responseDto;
        }

        [Route("comment")]
        [HttpPost]
        public SuggestionDto Comment([FromBody] CommentASuggestionDto commentASuggestion)
        {
            var command = _mapper.Map<CommentASuggestionCommand>(commentASuggestion);
            var model = _handler.Comment(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return responseDto;
        }

        [Route("delete")]
        [HttpDelete]
        public Guid Delete([FromBody] DeleteASuggestionDto deleteASuggestion)
        {
            var command = _mapper.Map<DeleteASuggestionCommand>(deleteASuggestion);
            var response = _handler.Delete(command);

            return response;
        }

        [Route("deletecomment")]
        [HttpDelete]
        public SuggestionDto DeleteComment([FromBody] DeleteASuggestionCommentDto deleteASuggestionComment)
        {
            var command = _mapper.Map<DeleteASuggestionCommentCommand>(deleteASuggestionComment);
            var model = _handler.DeleteAComment(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return responseDto;
        }
    }
}

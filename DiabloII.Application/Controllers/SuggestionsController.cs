using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Readers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
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

        [Route("suggestions")]
        [HttpGet]
        public ActionResult<IReadOnlyCollection<SuggestionDto>> GetAll()
        {
            var responseDto = _reader
                .GetAll()
                .Select(_mapper.Map<SuggestionDto>)
                .ToList();

            return Ok(responseDto);
        }

        [Route("suggestions")]
        [HttpPost]
        public ActionResult<SuggestionDto> Create([FromBody] CreateASuggestionDto createASuggestion)
        {
            var command = _mapper.Map<CreateASuggestionCommand>(createASuggestion);
            var model = _handler.Create(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return this.CreatedByUsingTheRequestRoute(responseDto);
        }

        [Route("suggestions/{suggestionId:guid}/votes")]
        [HttpPost]
        public ActionResult<SuggestionDto> Vote([FromBody] VoteToASuggestionDto voteToASuggestion, Guid suggestionId)
        {
            voteToASuggestion.SuggestionId = suggestionId;

            var command = _mapper.Map<VoteToASuggestionCommand>(voteToASuggestion);
            var model = _handler.Vote(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return this.CreatedByUsingTheRequestRoute(responseDto);
        }

        [Route("suggestions/{suggestionId:guid}/comments")]
        [HttpPost]
        public ActionResult<SuggestionDto> Comment([FromBody] CommentASuggestionDto commentASuggestion, Guid suggestionId)
        {
            commentASuggestion.SuggestionId = suggestionId;

            var command = _mapper.Map<CommentASuggestionCommand>(commentASuggestion);
            var model = _handler.Comment(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return this.CreatedByUsingTheRequestRoute(responseDto);
        }

        [Route("suggestions/{suggestionId:guid}")]
        [HttpDelete]
        public ActionResult<Guid> Delete([FromBody] DeleteASuggestionDto deleteASuggestion, Guid suggestionId)
        {
            deleteASuggestion.Id = suggestionId;

            var command = _mapper.Map<DeleteASuggestionCommand>(deleteASuggestion);
            var response = _handler.Delete(command);

            return Ok(response);
        }

        [Route("suggestions/{suggestionId:guid}/comments/{commentId:guid}")]
        [HttpDelete]
        public ActionResult<SuggestionDto> DeleteComment([FromBody] DeleteASuggestionCommentDto deleteASuggestionComment, Guid suggestionId, Guid commentId)
        {
            deleteASuggestionComment.SuggestionId = suggestionId;
            deleteASuggestionComment.Id = commentId;

            var command = _mapper.Map<DeleteASuggestionCommentCommand>(deleteASuggestionComment);
            var model = _handler.DeleteAComment(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return Ok(responseDto);
        }
    }
}

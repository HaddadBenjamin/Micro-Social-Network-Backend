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
using Microsoft.AspNetCore.Http;

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

        /// <summary>
        /// Get all the suggestions
        /// </summary>
        [Route("suggestions")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<SuggestionDto>), StatusCodes.Status200OK)]
        public ActionResult<IReadOnlyCollection<SuggestionDto>> GetAll()
        {
            var responseDto = _reader
                .GetAll()
                .Select(_mapper.Map<SuggestionDto>)
                .ToList();

            return Ok(responseDto);
        }

        /// <summary>
        /// Create a suggestion
        /// </summary>
        [Route("suggestions")]
        [HttpPost]
        [ProducesResponseType(typeof(SuggestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SuggestionDto> Create([FromBody] CreateASuggestionDto createASuggestion)
        {
            var command = _mapper.Map<CreateASuggestionCommand>(createASuggestion);
            var model = _handler.Create(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return this.CreatedByUsingTheRequestRoute(responseDto);
        }

        /// <summary>
        /// Vote to a suggestion
        /// </summary>
        [Route("suggestions/{suggestionId:guid}/votes")]
        [HttpPost]
        [ProducesResponseType(typeof(SuggestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SuggestionDto> Vote([FromBody] VoteToASuggestionDto voteToASuggestion, Guid suggestionId)
        {
            voteToASuggestion.SuggestionId = suggestionId;

            var command = _mapper.Map<VoteToASuggestionCommand>(voteToASuggestion);
            var model = _handler.Vote(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return this.CreatedByUsingTheRequestRoute(responseDto);
        }

        /// <summary>
        /// Comment a suggestion
        /// </summary>
        [Route("suggestions/{suggestionId:guid}/comments")]
        [HttpPost]
        [ProducesResponseType(typeof(SuggestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SuggestionDto> Comment([FromBody] CommentASuggestionDto commentASuggestion, Guid suggestionId)
        {
            commentASuggestion.SuggestionId = suggestionId;

            var command = _mapper.Map<CommentASuggestionCommand>(commentASuggestion);
            var model = _handler.Comment(command);
            var responseDto = _mapper.Map<SuggestionDto>(model);

            return this.CreatedByUsingTheRequestRoute(responseDto);
        }

        /// <summary>
        /// Delete a suggestion
        /// </summary>
        [Route("suggestions/{suggestionId:guid}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Guid> Delete([FromBody] DeleteASuggestionDto deleteASuggestion, Guid suggestionId)
        {
            deleteASuggestion.Id = suggestionId;

            var command = _mapper.Map<DeleteASuggestionCommand>(deleteASuggestion);
            var response = _handler.Delete(command);

            return Ok(response);
        }

        /// <summary>
        /// Delete a comment from a suggestion
        /// </summary>
        [Route("suggestions/{suggestionId:guid}/comments/{commentId:guid}")]
        [HttpDelete]
        [ProducesResponseType(typeof(SuggestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

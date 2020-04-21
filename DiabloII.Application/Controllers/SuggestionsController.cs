using System;
using System.Collections.Generic;
using AutoMapper;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Readers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
    public class SuggestionsController : BaseController<Suggestion, SuggestionDto>
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
        public ActionResult<IReadOnlyCollection<SuggestionDto>> GetAll() =>
            GetAll(_reader, _mapper);

        /// <summary>
        /// Create a suggestion
        /// </summary>
        [Route("suggestions")]
        [HttpPost]
        [ProducesResponseType(typeof(SuggestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SuggestionDto> Create([FromBody] CreateASuggestionDto dto) =>
            Create<CreateASuggestionDto, CreateASuggestionCommand>(dto, _handler, _mapper);

        /// <summary>
        /// Vote to a suggestion
        /// </summary>
        [Route("suggestions/{suggestionId:guid}/votes")]
        [HttpPost]
        [ProducesResponseType(typeof(SuggestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SuggestionDto> Vote([FromBody] VoteToASuggestionDto dto, Guid suggestionId)
        {
            dto.SuggestionId = suggestionId;

            return Create<VoteToASuggestionDto, VoteToASuggestionCommand>(dto, _handler, _mapper);
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
        public ActionResult<SuggestionDto> Comment([FromBody] CommentASuggestionDto dto, Guid suggestionId)
        {
            dto.SuggestionId = suggestionId;

            return Create<CommentASuggestionDto, CommentASuggestionCommand>(dto, _handler, _mapper);
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
        public ActionResult<Guid> Delete([FromBody] DeleteASuggestionDto dto, Guid suggestionId)
        {
            dto.Id = suggestionId;

            return Delete<DeleteASuggestionDto, DeleteASuggestionCommand>(dto, _handler, _mapper);
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
        public ActionResult<SuggestionDto> DeleteComment([FromBody] DeleteASuggestionCommentDto dto, Guid suggestionId, Guid commentId)
        {
            dto.SuggestionId = suggestionId;
            dto.Id = commentId;

            return DeleteWithMap<DeleteASuggestionCommentDto, DeleteASuggestionCommentCommand, Suggestion, SuggestionDto>(dto, _handler, _mapper);
        }
    }
}

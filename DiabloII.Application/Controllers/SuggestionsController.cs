using System;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Requests.Suggestions;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Application.Services.Hals.Suggestions;
using DiabloII.Domain.Commands.Suggestions;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Readers;
using Halcyon.HAL;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
    public class SuggestionsController : BaseController<Suggestion, SuggestionDto>
    {
        private readonly ISuggestionReader _reader;

        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        private readonly ISuggestionHalService _halService;

        public SuggestionsController(ISuggestionReader reader, IMediator mediator, IMapper mapper, ISuggestionHalService halService)
        {
            _reader = reader;
            _mediator = mediator;
            _mapper = mapper;
            _halService = halService;
        }

        /// <summary>
        /// Get all the suggestions
        /// </summary>
        [Route("suggestions")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponses<SuggestionDto>), StatusCodes.Status200OK)]
        public ActionResult<HALResponse> GetAll() =>
            GetAll(_reader, _mapper, _halService);

        /// <summary>
        /// Create a suggestion
        /// </summary>
        [Route("suggestions")]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateASuggestionDto dto) =>
            await Create<CreateASuggestionDto, CreateASuggestionCommand>(dto, _mediator, _mapper);

        /// <summary>
        /// Vote to a suggestion
        /// </summary>
        [Route("suggestions/{suggestionId:guid}/votes")]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> Vote([FromBody] VoteToASuggestionDto dto, Guid suggestionId)
        {
            dto.SuggestionId = suggestionId;

            return await Create<VoteToASuggestionDto, VoteToASuggestionCommand>(dto, _mediator, _mapper);
        }

        /// <summary>
        /// Comment a suggestion
        /// </summary>
        [Route("suggestions/{suggestionId:guid}/comments")]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> Comment([FromBody] CommentASuggestionDto dto, Guid suggestionId)
        {
            dto.SuggestionId = suggestionId;

            return await Create<CommentASuggestionDto, CommentASuggestionCommand>(dto, _mediator, _mapper);
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
        public async Task<ActionResult<Guid>> Delete([FromBody] DeleteASuggestionDto dto, Guid suggestionId)
        {
            dto.Id = suggestionId;

            return await Delete<DeleteASuggestionDto, DeleteASuggestionCommand, Guid>(dto, _mediator, _mapper);
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
        public async Task<ActionResult<HALResponse>> DeleteComment([FromBody] DeleteASuggestionCommentDto dto, Guid suggestionId, Guid commentId)
        {
            dto.SuggestionId = suggestionId;
            dto.Id = commentId;

            return await DeleteWithMap<DeleteASuggestionCommentDto, DeleteASuggestionCommentCommand, SuggestionDto>(dto, _mediator, _mapper, _halService);
        }
    }
}

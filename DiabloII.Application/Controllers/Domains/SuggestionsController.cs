using System;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Controllers.Bases;
using DiabloII.Application.Hals.Domains.Suggestions.Decorators;
using DiabloII.Application.Requests.Write.Suggestions;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Suggestions;
using DiabloII.Domain.Commands.Domains.Suggestions;
using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Readers.Domains;
using Halcyon.HAL;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers.Domains
{
    [Route("api/v1/")]
    public class SuggestionsController : BaseController<Suggestion, SuggestionDto>
    {
        private readonly ISuggestionReader _reader;

        private readonly ISuggestionHalDecorator _halDecorator;

        public SuggestionsController(ISuggestionReader reader, IMediator mediator, IMapper mapper, ISuggestionHalDecorator halDecorator) :
            base(mediator, mapper)
        {
            _reader = reader;
            _halDecorator = halDecorator;
        }

        /// <summary>
        /// Get all the suggestions
        /// </summary>
        [Route("suggestions")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponses<SuggestionDto>), StatusCodes.Status200OK)]
        public ActionResult<HALResponse> GetAll() =>
            GetAll(_reader, _halDecorator);

        /// <summary>
        /// Get a user
        /// </summary>
        [Route("suggestions/{suggestionId:guid}")]
        [HttpGet]
        [ProducesResponseType(typeof(SuggestionDto), StatusCodes.Status200OK)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<HALResponse> Get(Guid suggestionId) =>
            Get(suggestionId, _reader, _halDecorator);

        /// <summary>
        /// Create a suggestion
        /// </summary>
        [Route("suggestions")]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateASuggestionDto dto) =>
            await Create<CreateASuggestionDto, CreateASuggestionCommand>(dto);

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

            return await Create<VoteToASuggestionDto, VoteToASuggestionCommand>(dto);
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

            return await Create<CommentASuggestionDto, CommentASuggestionCommand>(dto);
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

            return await Delete<DeleteASuggestionDto, DeleteASuggestionCommand>(dto);
        }

        /// <summary>
        /// Delete a comment from a suggestion
        /// </summary>
        [Route("suggestions/{suggestionId:guid}/comments/{commentId:guid}")]
        [HttpDelete]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Guid>> DeleteComment([FromBody] DeleteASuggestionCommentDto dto, Guid suggestionId, Guid commentId)
        {
            dto.SuggestionId = suggestionId;
            dto.Id = commentId;

            return await Delete<DeleteASuggestionCommentDto, DeleteASuggestionCommentCommand>(dto);
        }
    }
}

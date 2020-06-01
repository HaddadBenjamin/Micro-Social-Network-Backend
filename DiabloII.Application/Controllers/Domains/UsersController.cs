using System;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Controllers.Bases;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Resolvers.User;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Users;
using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Readers.Domains;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers.Domains
{
    [Route("api/v1/")]
    public class UsersController : BaseController<User, UserDto>
    {
        private readonly IUserReader _reader;

        private readonly IUserResolver _userResolver;

        public UsersController(IMediator mediator, IUserReader reader, IMapper mapper, IUserResolver userResolver) :
            base(mediator, mapper)
        {
            _reader = reader;
            _userResolver = userResolver;
        }

        /// <summary>
        /// Get all the users
        /// </summary>
        [Route("users")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponses<UserDto>), StatusCodes.Status200OK)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<ApiResponses<UserDto>> GetAll() =>
            GetAll(_reader);

        /// <summary>
        /// Get a user
        /// </summary>
        [Route("users/{userId}")]
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ApiExplorerSettings(IgnoreApi = true)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> Get(string userId) =>
            Get(userId, _reader);

        /// <summary>
        /// Identify the current user.
        /// </summary>
        [Route("users/identifyme")]
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> IdentifyMe()
        {
            var user = await _userResolver.ResolveAsync();
            var userResponseDto = _mapper.Map<UserDto>(user);

            return Ok(userResponseDto);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        [Route("users/{userId}")]
        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<Guid>> Update([FromBody] UpdateAUserDto dto, string userId)
        {
            dto.UserId = userId;

            var command = _mapper.Map<UpdateAUserCommand>(dto);
            var updatedResourceId = await _mediator.Send(command);

            return Ok(updatedResourceId);
        }
    }
}

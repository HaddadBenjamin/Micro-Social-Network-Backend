using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Controllers.Bases;
using DiabloII.Application.Requests.Write.Users;
using DiabloII.Application.Resolvers.Implementations.User;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Domains.Users;
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
        private readonly IMediator _mediator;

        private readonly IUserReader _reader;

        private readonly IMapper _mapper;

        private readonly IUserResolver _userResolver;

        public UsersController(IMediator mediator, IUserReader reader, IMapper mapper, IUserResolver userResolver)
        {
            _mediator = mediator;
            _reader = reader;
            _mapper = mapper;
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
            GetAll(_reader, _mapper);

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
        /// Create a user
        /// </summary>
        [Route("users")]
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateAUserDto dto) =>
            await Create<CreateAUserDto, CreateAUserCommand>(dto, _mediator, _mapper);

        /// <summary>
        /// Update a user
        /// </summary>
        [Route("users/{userId}")]
        [HttpPut]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<ActionResult<UserDto>> Update([FromBody] UpdateAUserDto dto, string userId)
        {
            dto.UserId = userId;

            return await Update<UpdateAUserDto, UpdateAUserCommand>(dto, _mediator, _mapper);
        }
    }
}

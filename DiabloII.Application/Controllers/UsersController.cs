using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Users;
using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Readers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
    public class UsersController : BaseController<User, UserDto>
    {
        private readonly IMediator _mediator;
        
        private readonly IUserReader _reader;

        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IUserReader reader, IMapper mapper)
        {
            _mediator = mediator;
            _reader = reader;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the users
        /// </summary>
        [Route("users")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<UserDto>), StatusCodes.Status200OK)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<IReadOnlyCollection<UserDto>> GetAll() =>
            GetAll(_reader, _mapper);

        /// <summary>
        /// Create a user
        /// </summary>
        [Route("users")]
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateAUserDto dto)
        {
            var command = _mapper.Map<CreateAUserCommand>(dto);
            var model = await _mediator.Send( command);
            var response = _mapper.Map<User>(model);

            return this.CreatedByUsingTheRequestRoute(response);
        }

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

            var command = _mapper.Map<UpdateAUserCommand>(dto);
            var model = await _mediator.Send(command);
            var response = _mapper.Map<User>(model);

            return Ok(response);
        }
    }
}
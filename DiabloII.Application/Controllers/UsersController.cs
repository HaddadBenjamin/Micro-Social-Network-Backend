using System.Collections.Generic;
using AutoMapper;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Users;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Readers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
    public class UsersController : BaseController<User, UserDto>
    {
        private readonly IUserCommandHandler _handler;

        private readonly IUserReader _reader;

        private readonly IMapper _mapper;

        public UsersController(IUserCommandHandler handler, IUserReader reader, IMapper mapper)
        {
            _handler = handler;
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
        public ActionResult<UserDto> Create([FromBody] CreateAUserDto dto) =>
            Create(dto, _handler, _mapper);

        /// <summary>
        /// Update a user
        /// </summary>
        [Route("users/{userId}")]
        [HttpPut]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<UserDto> Update([FromBody] UpdateAUserDto dto, string userId)
        {
            dto.UserId = userId;

            return Update(dto, _handler, _mapper);
        }
    }
}
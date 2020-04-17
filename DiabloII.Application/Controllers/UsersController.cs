using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Requests.Users;
using DiabloII.Application.Responses.Suggestions;
using DiabloII.Application.Responses.Users;
using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
    public class UsersController : Controller
    {
        private readonly IUserCommandHandler _handler;
        private readonly IMapper _mapper;

        public UsersController(IUserCommandHandler handler, IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        [Route("users")]
        [HttpPost]
        [ProducesResponseType(typeof(SuggestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<UserDto> Create([FromBody] CreateAUserDto dto)
        {
            var command = _mapper.Map<CreateAUserCommand>(dto);
            var model = _handler.Create(command);
            var responseDto = _mapper.Map<UserDto>(model);

            return this.CreatedByUsingTheRequestRoute(responseDto);
        }
    }
}
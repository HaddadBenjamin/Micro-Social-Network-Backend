using AutoMapper;
using DiabloII.Application.Extensions;
using DiabloII.Application.Requests.Notifications;
using DiabloII.Application.Responses.Notifications;
using DiabloII.Domain.Commands.Notifications;
using DiabloII.Domain.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
    public class NotificationsController : Controller
    {
        private readonly INotificationCommandHandler _handler;

        private readonly IMapper _mapper;

        public NotificationsController(INotificationCommandHandler handler, IMapper mapper)
        {
            _handler = handler;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a notification
        /// </summary>
        [Route("notifications")]
        [HttpPost]
        [ProducesResponseType(typeof(NotificationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<NotificationDto> Create([FromBody] CreateANotificationDto dto)
        {
            var command = _mapper.Map<CreateANotificationCommand>(dto);
            var model = _handler.Create(command);
            var response = _mapper.Map<NotificationDto>(model);

            return this.CreatedByUsingTheRequestRoute(response);
        }
    }
}
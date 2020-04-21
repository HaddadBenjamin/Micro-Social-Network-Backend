using System.Collections.Generic;
using AutoMapper;
using DiabloII.Application.Requests.Notifications;
using DiabloII.Application.Responses.Notifications;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Readers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class NotificationsController : BaseController<Notification, NotificationDto>
    {
        private readonly INotificationCommandHandler _handler;
       
        private readonly INotificationReader _reader;
        
        private readonly IMapper _mapper;

        public NotificationsController(INotificationCommandHandler handler, INotificationReader reader, IMapper mapper)
        {
            _handler = handler;
            _reader = reader;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all the notifications
        /// </summary>
        [Route("notifications")]
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<NotificationDto>), StatusCodes.Status200OK)]
        public ActionResult<IReadOnlyCollection<NotificationDto>> GetAll() =>
            GetAll(_reader, _mapper);

        /// <summary>
        /// Create a notification
        /// </summary>
        [Route("notifications")]
        [HttpPost]
        [ProducesResponseType(typeof(NotificationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<NotificationDto> Create([FromBody] CreateANotificationDto dto) =>
            Create(dto, _handler, _mapper);
    }
}
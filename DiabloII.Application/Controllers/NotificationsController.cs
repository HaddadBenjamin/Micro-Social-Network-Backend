using System;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Application.Requests.Notifications;
using DiabloII.Application.Responses.Read.Bases;
using DiabloII.Application.Responses.Read.Notifications;
using DiabloII.Domain.Commands.Notifications;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Readers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DiabloII.Application.Controllers
{
    [Route("api/v1/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class NotificationsController : BaseController<Notification, NotificationDto>
    {
        private readonly INotificationReader _reader;

        private readonly IMapper _mapper;

        private readonly IMediator _mediator;

        public NotificationsController(INotificationReader reader, IMapper mapper, IMediator mediator)
        {
            _reader = reader;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Get all the notifications
        /// </summary>
        [Route("notifications")]
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponses<NotificationDto>), StatusCodes.Status200OK)]
        public ActionResult<ApiResponses<NotificationDto>> GetAll() =>
            GetAll(_reader, _mapper);

        /// <summary>
        /// Create a notification
        /// </summary>
        [Route("notifications")]
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateANotificationDto dto) =>
            await Create<CreateANotificationDto, CreateANotificationCommand>(dto, _mediator, _mapper);
    }
}
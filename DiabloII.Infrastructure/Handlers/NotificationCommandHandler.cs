using AutoMapper;
using DiabloII.Domain.Commands.Notifications;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Services.Notifications;
using DiabloII.Domain.Validations.Notifications.Create;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Handlers
{
    public class NotificationCommandHandler : INotificationCommandHandler
    {
        private readonly IMapper _mapper;

        private readonly INotificationService _service;

        private readonly ApplicationDbContext _dbContext;

        private readonly CreateANotificationValidator _createValidator;

        public NotificationCommandHandler(
            INotificationService service,
            IMapper mapper,
            ApplicationDbContext dbContext,
            CreateANotificationValidator createValidator)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _createValidator = createValidator;
            _service = service;
        }

        public Notification Create(CreateANotificationCommand command)
        {
            var validationContext = new CreateANotificationValidationContext(command);

            _createValidator.Validate(validationContext);

            var notification = _mapper.Map<Notification>(command);

            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();

            _service.Notify(notification, command.ConcernedUserIds);

            return notification;
        }
    }
}
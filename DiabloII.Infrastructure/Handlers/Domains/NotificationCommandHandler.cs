using AutoMapper;
using DiabloII.Domain.Commands.Domains.Notifications;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Services.Notifications;
using DiabloII.Domain.Validations.Notifications.Create;
using DiabloII.Infrastructure.DbContext;
using DiabloII.Infrastructure.Handlers.Bases;

namespace DiabloII.Infrastructure.Handlers.Domains
{
    public class NotificationCommandHandler : CommandHandler<CreateANotificationCommand>
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

        public override void Handle(CreateANotificationCommand command)
        {
            var validationContext = new CreateANotificationValidationContext(command);

            _createValidator.Validate(validationContext);

            var notification = _mapper.Map<Notification>(command);

            _dbContext.Notifications.Add(notification);
            _dbContext.SaveChanges();

            _service.Notify(notification, command.ConcernedUserIds);
        }
    }
}
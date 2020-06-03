using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DiabloII.Domain.Commands.Domains.Notifications;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Services.Notifications;
using DiabloII.Domain.Validations.Notifications.Create;
using DiabloII.Infrastructure.DbContext;
using MediatR;

namespace DiabloII.Infrastructure.Handlers
{
    public class NotificationCommandHandler : IRequestHandler<CreateANotificationCommand>
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

        public async Task<Unit> Handle(CreateANotificationCommand command, CancellationToken cancellationToken = default)
        {
            var validationContext = new CreateANotificationValidationContext(command);

            _createValidator.Validate(validationContext);

            var notification = _mapper.Map<Notification>(command);

            _dbContext.Notifications.Add(notification);
            await _dbContext.SaveChangesAsync();

            _service.Notify(notification, command.ConcernedUserIds);

            return Unit.Value;
        }
    }
}
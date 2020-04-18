using AutoMapper;
using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Handlers;
using DiabloII.Domain.Models.Users;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Validations.Users.Create;
using DiabloII.Domain.Validations.Users.Update;
using DiabloII.Infrastructure.DbContext;

namespace DiabloII.Infrastructure.Handlers
{
    public class UserCommandHandler : IUserCommandHandler
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly CreateAUserValidator _createAUserValidator;
        private readonly UpdateAUserValidator _updateAUserValidator;
        private readonly IUserRepository _repository;

        public UserCommandHandler(
            ApplicationDbContext dbContext,
            IUserRepository repository,
            IMapper mapper,
            CreateAUserValidator createAUserValidator,
            UpdateAUserValidator updateAUserValidator)
        {
            _dbContext = dbContext;
            _repository = repository;
            _mapper = mapper;
            _createAUserValidator = createAUserValidator;
            _updateAUserValidator = updateAUserValidator;
        }

        public User Create(CreateAUserCommand command)
        {
            var validationContext = new CreateAUserValidationContext(command, _repository);

            _createAUserValidator.Validate(validationContext);

            var user = _mapper.Map<User>(command);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public User Update(UpdateAUserCommand command)
        {
            var validationContext = new UpdateAUserValidationContext(command, _repository);

            _updateAUserValidator.Validate(validationContext);

            var user = _repository.GetUser(command.UserId);

            user.Email = command.Email;
            user.NotificationSetting.AcceptedNotifications = command.AcceptedNotifications;
            user.NotificationSetting.AcceptedNotifiers = command.AcceptedNotifiers;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return user;
        }
    }
}
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
        private readonly CreateAUserValidator _createValidator;
        private readonly UpdateAUserValidator _updateValidator;
        private readonly IUserRepository _repository;

        public UserCommandHandler(
            ApplicationDbContext dbContext,
            IUserRepository repository,
            IMapper mapper,
            CreateAUserValidator createValidator,
            UpdateAUserValidator updateValidator)
        {
            _dbContext = dbContext;
            _repository = repository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public User Create(CreateAUserCommand command)
        {
            var validationContext = new CreateAUserValidationContext(command, _repository);

            _createValidator.Validate(validationContext);

            if (_repository.DoesUserExists(command.UserId))
                return _repository.Get(command.UserId);

            var user = _mapper.Map<User>(command);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            return user;
        }

        public User Update(UpdateAUserCommand command)
        {
            var validationContext = new UpdateAUserValidationContext(command, _repository);

            _updateValidator.Validate(validationContext);

            var user = _repository.Get(command.UserId);

            user.Update(command);

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return user;
        }
    }
}
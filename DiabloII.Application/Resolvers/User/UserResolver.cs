using System.Threading.Tasks;
using DiabloII.Application.Resolvers.UserId;
using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Repositories;
using MediatR;

namespace DiabloII.Application.Resolvers.User
{
    public class UserResolver : IUserResolver
    {
        private readonly IUserIdResolver _userIdResolver;

        private readonly IUserRepository _userRepository;
  
        private readonly IMediator _mediator;

        public UserResolver(IUserIdResolver userIdResolver, IUserRepository userRepository, IMediator mediator)
        {
            _userIdResolver = userIdResolver;
            _userRepository = userRepository;
            _mediator = mediator;
        }

        public async Task<Domain.Models.Users.User> ResolveAsync()
        {
            var userId = _userIdResolver.Resolve();
            var existingUser = _userRepository.GetUserOrDefaultByItsId(userId);

            if (existingUser != null)
                return existingUser;

            var createUserCommand = new CreateAUserCommand
            {
                UserId = userId
            };
            var createdUser = await _mediator.Send(createUserCommand);
            
            return createdUser;
        }
    }
}
using AutoMapper;
using DiabloII.Application.Requests.Write.Users;
using DiabloII.Application.Resolvers.Implementations.UserId;
using DiabloII.Domain.Commands.Domains.Users;
using DiabloII.Domain.Extensions;
using DiabloII.Domain.Helpers;

namespace DiabloII.Application.Mappers.Users
{
    public class UserDtoToCommandLayer : Profile
    {
        public UserDtoToCommandLayer()
        {
            CreateMap<CreateAUserDto, CreateAUserCommand>()
                .ForMember(command => command.Id, src => src.MapFrom<UserIdValueConverter>());

            CreateMap<UpdateAUserDto, UpdateAUserCommand>()
                .Ignore(command => command.AcceptedNotifications)
                .Ignore(command => command.AcceptedNotifiers)
                .AfterMap((dto, command) =>
            {
                command.AcceptedNotifications = EnumerationFlagsHelpers.ToInteger(dto.AcceptedNotifications);
                command.AcceptedNotifiers = EnumerationFlagsHelpers.ToInteger(dto.AcceptedNotifiers);
            });
        }
    }

    internal class UserIdValueConverter : IValueResolver<CreateAUserDto, object, string>
    {
        private readonly IUserIdResolver _userIdResolver;

        public UserIdValueConverter(IUserIdResolver userIdResolver) => _userIdResolver = userIdResolver;
        public string Resolve(CreateAUserDto source, object destination, string destMember, ResolutionContext context) =>
            _userIdResolver.Resolve();
    }
}
using AutoMapper;
using DiabloII.Application.Requests.Users;
using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Extensions;
using DiabloII.Domain.Helpers;

namespace DiabloII.Application.Mappers.Users
{
    public class UserDtoToCommandLayer : Profile
    {
        public UserDtoToCommandLayer()
        {
            CreateMap<CreateAUserDto, CreateAUserCommand>();

            CreateMap<UpdateAUserDto, UpdateAUserCommand>()
                .Ignore(command => command.AcceptedNotifications)
                .Ignore(command => command.AcceptedNotifiers)
                .AfterMap((dto, command) =>
            {
                command.AcceptedNotifications = EnumerationFlagsHelper.ToInteger(dto.AcceptedNotifications);
                command.AcceptedNotifiers = EnumerationFlagsHelper.ToInteger(dto.AcceptedNotifiers);
            });
        }
    }
}
using AutoMapper;
using DiabloII.Application.Requests.Users;
using DiabloII.Domain.Commands.Users;

namespace DiabloII.Application.Mappers.Users
{
    public class UserCommandToDtoLayer : Profile
    {
        public UserCommandToDtoLayer()
        {
            CreateMap<CreateAUserCommand, CreateAUserDto>();
        }
    }
}
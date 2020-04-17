using AutoMapper;
using DiabloII.Application.Responses.Users;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Application.Mappers.Users
{
    public class UserDataToDtoLayer : Profile
    {
        public UserDataToDtoLayer()
        {
            CreateMap<User, UserDto>();
        }
    }
}
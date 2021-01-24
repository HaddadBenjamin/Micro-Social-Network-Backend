using AutoMapper;
using DiabloII.Domain.Queries.Domains.Users;

namespace DiabloII.Application.Mappers.Users
{
    public class UserDtoToQueryLayer : Profile
    {
        public UserDtoToQueryLayer() =>
            CreateMap<string, GetUserQuery>()
                .AfterMap((dto, query) => query.Id = dto);
    }
}
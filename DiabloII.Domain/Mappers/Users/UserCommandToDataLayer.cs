using AutoMapper;
using DiabloII.Domain.Commands.Domains.Users;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Mappers.Users
{
    public class UserCommandToDataLayer : Profile
    {
        public UserCommandToDataLayer()
        {
            CreateMap<CreateAUserCommand, User>().AfterMap((command, dataModel) =>
            {
                dataModel.NotificationSetting = new UserNotificationSetting
                {
                    User = dataModel,
                    UserId = dataModel.Id
                };
            });
        }
    }
}
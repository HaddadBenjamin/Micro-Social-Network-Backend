using AutoMapper;
using DiabloII.Domain.Commands.Users;
using DiabloII.Domain.Models.Users;

namespace DiabloII.Domain.Mappers.Suggestions
{
    public class UserCommandToDataLayer : Profile
    {
        public UserCommandToDataLayer()
        {
            CreateMap<CreateAUserCommand, User>().AfterMap((command, dataModel) =>
            {
                dataModel.Id = command.UserId;
                dataModel.NotificationSetting = new UserNotificationSetting
                {
                    User = dataModel,
                    UserId = dataModel.Id
                };
            });
        }
    }
}
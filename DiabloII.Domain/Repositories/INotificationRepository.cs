using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Repositories.Bases;

namespace DiabloII.Domain.Repositories
{
    public interface INotificationRepository : IRepositoryGetAll<Notification>
    {
    }
}
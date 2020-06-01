using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Repositories.Bases;

namespace DiabloII.Domain.Repositories.Domains
{
    public interface INotificationRepository : IRepositoryGetAll<Notification>
    {
    }
}
using System.Collections.Generic;
using DiabloII.Domain.Models.Notifications;
using DiabloII.Domain.Readers;
using DiabloII.Domain.Readers.Domains;
using DiabloII.Domain.Repositories;
using DiabloII.Domain.Repositories.Domains;

namespace DiabloII.Infrastructure.Readers
{
    public class NotificationReader : INotificationReader
    {
        private readonly INotificationRepository _repository;

        public NotificationReader(INotificationRepository repository) => _repository = repository;

        public IReadOnlyCollection<Notification> GetAll() => _repository.GetAll();
    }
}
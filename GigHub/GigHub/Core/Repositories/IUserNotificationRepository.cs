using GigHub.Core.Models;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        int GetNewNotificationNumFor(string userId);
        IEnumerable<Notification> GetNotificationsFor(string userId);
        IEnumerable<UserNotification> GetUnreadNotificationsFor(string userId);
        void Dispose();
    }
}
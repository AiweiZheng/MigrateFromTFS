using System.Collections.Generic;

namespace GigHub.Core.Dtos
{
    public class NotificationsDto
    {
        public int NewNotificationCount { get; set; }
        public IEnumerable<NotificationDto> Notifications { get; set; }
    }
}
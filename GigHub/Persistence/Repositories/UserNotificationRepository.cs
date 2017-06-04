using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
    {
        private readonly IApplicationDbContext _context;

        public UserNotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public int GetNewNotificationNumFor(string userId)
        {
            return _context.UserNotifications
                .Count(un => un.UserId == userId && !un.IsRead);
        }

        public IEnumerable<Notification> GetNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId).Select(un => un.Notification)
                .Include(n => n.Gig.Artist)
                .OrderBy(n => n.Gig.ArtistId)
                .ToList();
        }

        public IEnumerable<UserNotification> GetUnreadNotificationsFor(string userId)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == userId && !un.IsRead)
                .ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
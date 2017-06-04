using GigHub.Core.Repositories;

namespace GigHub.Core
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IUserNotificationRepository UserNotifications { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        ILoginRepository Logins { get; }
        //   IUserDescriptionRepository UserDescriptions { get; }
        void Complete();
        void Dispose();
    }
}
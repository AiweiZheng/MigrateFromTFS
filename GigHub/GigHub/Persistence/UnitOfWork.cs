using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }
        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public ILoginRepository Logins { get; private set; }
        // public IUserDescriptionRepository UserDescriptions { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
            Users = new UserRepository(_context);
            Roles = new RoleRepository(_context);
            Logins = new LoginRepository(_context);
            //  UserDescriptions = new UserDescriptionRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Gigs.Dispose();
            Attendances.Dispose();
            Followings.Dispose();
            Genres.Dispose();
            UserNotifications.Dispose();
            Users.Dispose();
            Roles.Dispose();
            //  UserDescriptions.Dispose();
        }
    }
}
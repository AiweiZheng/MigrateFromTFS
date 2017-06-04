using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IApplicationDbContext _context;

        public LoginRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void PutOrPostLogin(Login login)
        {
            var logins = _context.Logins.Where(l => l.Username == login.Username);

            if (logins.Any())
            {
                var tempLogin = logins.First();
                tempLogin.SessionId = login.SessionId;
                tempLogin.Date = login.Date;
                // _context.Entry(tempLogin).State = EntityState.Modified;
            }
            else
            {
                _context.Logins.Add(login);
            }
        }

        public bool IsLoggedIn(string user, string session)
        {
            var isLoggedIn = _context.Logins.Any(l => l.Username == user
                                                && l.SessionId == session);

            return isLoggedIn;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
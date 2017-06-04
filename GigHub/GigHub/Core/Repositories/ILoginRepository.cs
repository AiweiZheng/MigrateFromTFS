using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface ILoginRepository
    {
        void PutOrPostLogin(Login login);
        bool IsLoggedIn(string user, string session);
        void Dispose();
    }
}
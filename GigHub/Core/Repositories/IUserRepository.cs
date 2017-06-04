using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<ApplicationUser> GetUsers();
        ApplicationUser GetUser(string id);
        int GetTotalNumberOfByRoleId(string roleId);
        IEnumerable<ApplicationUser> GetUsersByRoleId(string roleId, string query = null);
        IEnumerable<ApplicationUser> GetUsersByRoleId(string roleId, int startIndex, int count);
        string GetUserDescriptionBy(string id);
        void Dispose();
    }
}
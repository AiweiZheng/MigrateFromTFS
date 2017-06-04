using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GigHub.Core.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<IdentityRole> GetRoles();
        string GetRoleIdBy(string roleName);
        void Dispose();
    }
}
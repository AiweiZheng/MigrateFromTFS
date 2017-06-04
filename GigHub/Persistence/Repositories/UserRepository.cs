using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using Utilities;

namespace GigHub.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetUsers()
        {
            return _context.Users.Include(u => u.Roles).ToList();
        }

        public ApplicationUser GetUser(string id)
        {
            return _context.Users.Include(u => u.Roles).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<ApplicationUser> GetUsersByRoleId(string roleId, int startIndex, int count)
        {
            IEnumerable<ApplicationUser> query = _context.Users
                .Where(
                    u => u.Activated
                         && u.Roles.Any(r => r.RoleId == roleId)
                )
                .OrderBy(u => u.Name);

            return
                query.Skip(startIndex)
                .Take(count)
                .ToList();
        }

        public IEnumerable<ApplicationUser> GetUsersByRoleId(string roleId, string query = null)
        {
            Expression<Func<ApplicationUser, bool>> filterExpression = u =>
                u.Activated
                && u.Roles.Any(r => r.RoleId == roleId);

            if (query == null)
                return _context.Users
                    .Where(filterExpression)
                    .OrderBy(u => u.Name).ToList();
            {
                Expression<Func<ApplicationUser, bool>> nameFilter = u =>
                    u.Name.Contains(query);
                filterExpression = ExpressionCombiner.And(filterExpression, nameFilter);
            }
            return _context.Users
                .Where(filterExpression)
                .OrderBy(u => u.Name).ToList();
        }

        public int GetTotalNumberOfByRoleId(string roleId)
        {
            return _context.Users
                .Count(
                    u => u.Activated
                         && u.Roles.Any(r => r.RoleId == roleId)
                );
        }

        public string GetUserDescriptionBy(string id)
        {
            return _context.Users.Single(u => u.Id == id).Description;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
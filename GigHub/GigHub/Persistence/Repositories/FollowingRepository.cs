using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly IApplicationDbContext _context;
        public FollowingRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Following> GetFolloweesFor(string userId)
        {
            return _context.Followings
                .Where(f => f.FollowerId == userId)
                .Include(f => f.Followee)
                .OrderBy(f => f.Followee.Name)
                .ToList();
        }

        public Following GetFollowing(string artistId, string userId)
        {
            return _context.Followings
                .SingleOrDefault(f => f.FolloweeId == artistId && f.FollowerId == userId);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }
        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IFollowingRepository
    {
        IEnumerable<Following> GetFolloweesFor(string userId);
        Following GetFollowing(string artistId, string userId);
        void Add(Following following);
        void Remove(Following following);
        void Dispose();
    }
}
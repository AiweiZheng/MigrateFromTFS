using System.Collections.Generic;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public interface IVenueRepository
    {
        IEnumerable<Venue> GetVenues(string query = null);
        Venue GetVenueById(byte id);
        void Dispose();
    }
}
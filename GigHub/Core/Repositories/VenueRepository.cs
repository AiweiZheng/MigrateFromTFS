using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private readonly IApplicationDbContext _context;

        public VenueRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Venue> GetVenues(string query = null)
        {
            return query == null ? _context.Venues.ToList() :
                _context.Venues.Where(v => v.Name.Contains(query)).ToList();
        }

        public Venue GetVenueById(byte id)
        {
            return _context.Venues.Single(v => v.Id == id);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
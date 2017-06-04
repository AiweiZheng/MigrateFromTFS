using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;

namespace GigHub.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IApplicationDbContext _context;
        public GenreRepository(IApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Genre> GetGenres(string query = null)
        {
            if (query == null)
                return _context.Genres.ToList();

            return _context.Genres.Where(g => g.Name.Contains(query));
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
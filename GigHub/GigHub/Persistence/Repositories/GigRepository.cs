using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Core.ViewModels;

namespace GigHub.Persistence.Repositories
{
    public class GigRepository : IGigRepository
    {
        private readonly IApplicationDbContext _context;

        public GigRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId)
        {
            return _context.Gigs
                .Where(g => g.ArtistId == artistId
                            && g.DateTime > DateTime.Now)
                .Include(g => g.Genre)
                .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId, int startIndex, int count)
        {
            IQueryable<Gig> query = _context.Gigs.Include(g => g.Artist)
                .Where(g => g.ArtistId == artistId
                            && !g.IsCancelled
                            && g.DateTime > DateTime.Now)
                .Include(g => g.Genre);

            return query.OrderByDescending(g => g.DateTime)
                        .Skip(startIndex)
                        .Take(count)
                        .ToList();
        }

        public int GetTotolNumOfUpcomingGigsForArtist(string artistId)
        {
            return _context.Gigs
                .Count(g => g.ArtistId == artistId
                            && !g.IsCancelled
                            && g.DateTime > DateTime.Now);

        }
        public Gig GetGigWithAttendees(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Attendances.Select(a => a.Attendee))
                .SingleOrDefault(g => g.Id == gigId);
        }

        private IEnumerable<Gig> GetUpcomingGigs(int startIndex, int count)
        {
            IQueryable<Gig> query = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now
                       && g.Artist.Activated
                       && !g.IsCancelled)
                .OrderBy(g => g.DateTime);

            return query.Skip(startIndex).Take(count).ToList();
        }

        private IEnumerable<Gig> GetUpcomingGigsByFilter(int startIndex, int count, string filter)
        {
            IQueryable<Gig> query = _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(g => g.DateTime > DateTime.Now
                            && g.Artist.Activated
                            && !g.IsCancelled
                            && (
                                g.Artist.Name.Contains(filter) ||
                                g.Genre.Name.Contains(filter) ||
                                g.Venue.Contains(filter)))
                .OrderBy(g => g.DateTime);

            return query.Skip(startIndex).Take(count).ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigs(int startIndex, int count, string filter = null)
        {
            if (filter == null)
                return GetUpcomingGigs(startIndex, count);

            return GetUpcomingGigsByFilter(startIndex, count, filter);
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .SingleOrDefault(g => g.Id == gigId);
        }
        public IEnumerable<Gig> GetGigsUserAttending(string userId, int startIndex, int count)
        {
            IQueryable<Gig> query = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .OrderBy(g => g.DateTime);

            return query.Skip(startIndex).Take(count).ToList();
        }

        public void Add(Gig gig)
        {
            var artist = _context.Users
                .Include(u => u.Followers)
                .Single(u => u.Id == gig.ArtistId);

            artist.Followers = _context.Followings
                .Include(f => f.Follower)
                .Where(f => f.Followee.Id == gig.ArtistId).ToList();

            gig.Artist = artist;

            _context.Gigs.Add(gig);

            gig.Create();
        }

        public List<ArtistWithGigsViewMode> GetCountOfUpcomingGigsPerformedBy(IEnumerable<string> artistsId, int gigCount)
        {
            IEnumerable<Gig> query = _context.Gigs
                .Include(g => g.Genre)
                .Where(
                    g => !g.IsCancelled
                         && g.DateTime > DateTime.Now
                         && artistsId.Any(id => id == g.ArtistId)
                );

            return query.GroupBy(g => g.ArtistId)
                 .Select(v => new ArtistWithGigsViewMode
                 {
                     Gigs = v.OrderBy(g => g.DateTime).Take(gigCount),
                     ArtistId = v.Key
                 }).ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
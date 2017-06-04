using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Core.ViewModels;
using Utilities;

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
                .Include(g => g.Venue)
                .ToList();
        }

        public IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId, int startIndex, int count)
        {
            IQueryable<Gig> query = _context.Gigs.Include(g => g.Artist)
                .Where(g => g.ArtistId == artistId
                            && !g.IsCancelled
                            && g.DateTime > DateTime.Now)
                .Include(g => g.Genre)
                .Include(g => g.Venue);

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

        public IEnumerable<Gig> GetUpcomingGigs(int? startIndex, int? count, GigFilterParams filter)
        {
            var gigFilterExpression = _getGigFilterExpression(filter);

            IQueryable<Gig> query = _context.Gigs
               .Include(g => g.Artist)
               .Include(g => g.Genre)
               .Include(g => g.Venue)
               .Where(gigFilterExpression)
               .OrderBy(g => g.DateTime);

            if (!startIndex.HasValue || !count.HasValue)
                return query.ToList();

            return query.Skip(startIndex.Value).Take(count.Value).ToList();
        }

        private Expression<Func<Gig, bool>> _getGigFilterExpression(GigFilterParams filter)
        {
            Expression<Func<Gig, bool>> filterExpression = g =>
               g.DateTime > DateTime.Now
               && g.Artist.Activated
               && !g.IsCancelled;

            return string.IsNullOrWhiteSpace(filter.SearchTerm) ? filterExpression :
                ExpressionCombiner.And(filterExpression, getSeachFilter(filter));
        }

        private Expression<Func<Gig, bool>> _getMyAttendingGigFilterExpression(GigFilterParams filter)
        {
            Expression<Func<Gig, bool>> filterExpression = g =>
               g.DateTime > DateTime.Now;

            return string.IsNullOrWhiteSpace(filter.SearchTerm) ? filterExpression :
                ExpressionCombiner.And(filterExpression, getSeachFilter(filter));
        }

        private Expression<Func<Gig, bool>> getSeachFilter(GigFilterParams filter)
        {
            var searchTerm = filter.SearchTerm;
            switch (filter.SearchBy)
            {
                case AppConst.SearchAll:
                    Expression<Func<Gig, bool>> allExpression = g =>
                     g.Artist.Name.Contains(searchTerm) ||
                          g.Genre.Name.Contains(searchTerm) ||
                          g.Venue.Name.Contains(searchTerm);

                    return allExpression;

                case AppConst.SearchByArtist:
                    Expression<Func<Gig, bool>> artistExpression = g =>
                        g.Artist.Name.Contains(searchTerm);

                    return artistExpression;

                case AppConst.SearchByGener:
                    Expression<Func<Gig, bool>> genreExpression = g =>
                        g.Genre.Name.Contains(searchTerm);

                    return genreExpression;

                case AppConst.SearchByVenue:

                    Expression<Func<Gig, bool>> venueExpression = g =>
                       g.Venue.Name.Contains(searchTerm);

                    return venueExpression;

                default:
                    return null;
            }
        }


        public IEnumerable<Gig> GetGigsUserAttending(string userId, int startIndex, int count, GigFilterParams filter)
        {
            var gigFilterExpression = _getMyAttendingGigFilterExpression(filter);

            IQueryable<Gig> query = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Include(g => g.Venue)
                .Where(gigFilterExpression)
                .OrderBy(g => g.DateTime);

            return query.Skip(startIndex).Take(count).ToList();
        }

        public Gig GetGig(int gigId)
        {
            return _context.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Include(g => g.Venue)
                .SingleOrDefault(g => g.Id == gigId);
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
                .Include(g => g.Venue)
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
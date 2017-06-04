using System.Collections.Generic;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGigWithAttendees(int gigId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId);
        IEnumerable<Gig> GetUpcomingGigsByArtist(string artistId, int startIndex, int count);
        int GetTotolNumOfUpcomingGigsForArtist(string artistId);
        IEnumerable<Gig> GetUpcomingGigs(int startIndex, int count, string filter = null);
        Gig GetGig(int gigId);
        IEnumerable<Gig> GetGigsUserAttending(string userId, int startIndex, int count);
        void Add(Gig gig);
        List<ArtistWithGigsViewMode> GetCountOfUpcomingGigsPerformedBy(IEnumerable<string> artists, int count);
        void Dispose();
    }
}
using System.Collections.Generic;
using System.Linq;
using GigHub.Core.Models;

namespace GigHub.Core.ViewModels
{
    public class ArtistWithGigsViewMode
    {
        public string ArtistId { get; set; }
        public IEnumerable<Gig> Gigs { get; set; }
        public ILookup<int, Attendance> MyAttendances { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GigHub.Core.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCancelled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public Byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Create()
        {
            var notification = Notification.GigCreated(this);

            foreach (var follower in Artist.Followers)
            {
                follower.Follower.Notify(notification);
            }
        }
        public void Cancel()
        {
            IsCancelled = true;

            var notification = Notification.GigCancelled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

        public void Reopen()
        {
            IsCancelled = false;

            var notification = Notification.GigReopen(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

        public void Modify(string venue, DateTime dateTime, byte genreId)
        {
            var notificaition = Notification.GigUpdated(this, DateTime, Venue);

            Venue = venue;
            DateTime = dateTime;
            GenreId = genreId;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notificaition);
        }
    }
}
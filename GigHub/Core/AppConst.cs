namespace GigHub.Core
{
    public static class AppConst
    {
        // view 
        public const string UnfollowText = "Follow";
        public const string FollowingText = "Following";
        public const string NotGoingYetText = "Going ?";
        public const string GoingText = "Going";
        public const string AppBrand = "GigHub";

        public const int PageSizeLg = 20;
        public const int PageSizeMd = 15;
        public const int PageSizeSm = 10;
        public const int PageSizeXs = 5;
        public const int CountOfGigPerLoad = 3;

        //web mvc controller
        public const string GigCannotBeNull = "Gig cannot be null";

        public const string TitleForMyAttendGigs = "Gigs I'm Attending";
        public const string TitleForHomeGigs = "Upcoming Gigs";

        //bootstrap class wrapper
        public const string BsHide = "hide";
        public const string BsShow = "show";

        //for gig search category
        public const string SearchAll = "Anything";
        public const string SearchByArtist = "Artist";
        public const string SearchByGener = "Genre";
        public const string SearchByVenue = "Venue";
    }
}
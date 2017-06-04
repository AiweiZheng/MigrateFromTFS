using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.Filters;
using GigHub.Core.Models;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArtistsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: Artists
        public ActionResult Index()
        {
            return View();
        }

        [Route("Artists/More/{startIndex}")]
        public ActionResult GetMoreArtists(int startIndex)
        {
            var artistRoleId = _unitOfWork.Roles.GetRoleIdBy(RoleName.Artist);
            var artists = _unitOfWork.Users.GetUsersByRoleId(artistRoleId, startIndex, AppConst.PageSizeXs);
            return PartialArtistsView(artists);
        }

        [Authorize]
        [AuthorizeActivatedAccount]
        [AuthorizeSingleLogin]
        public ActionResult Following()
        {
            return View();
        }

        [Authorize]
        [AuthorizeActivatedAccount]
        [AuthorizeSingleLogin]
        [Route("Followings/More/{startIndex}")]
        public ActionResult GetMoreFollowing(int startIndex)
        {
            var followings = _unitOfWork.Followings.GetFolloweesFor(User.Identity.GetUserId());

            var artists = followings.Skip(startIndex)
                .Take(AppConst.PageSizeXs)
                .Select(f => f.Followee);

            return PartialArtistsView(artists);
        }

        private ActionResult PartialArtistsView(IEnumerable<ApplicationUser> artists)
        {

            if (!artists.Any())
                return Content(HttpStatusCode.NoContent.ToString());

            var gigsPerFormByArtists = _unitOfWork.Gigs
                .GetCountOfUpcomingGigsPerformedBy(artists.Select(a => a.Id), AppConst.CountOfGigPerLoad);

            var userId = User.Identity.GetUserId();
            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);
            var followings = _unitOfWork.Followings.GetFolloweesFor(userId)
                .ToLookup(f => f.Followee.Id);

            var artistsViewModel = new ArtistsViewModel
            {
                Artists = artists,
                Gigs = gigsPerFormByArtists,
                Attendances = attendances,
                Followings = followings
            };

            return PartialView("_MoreArtists", artistsViewModel);
        }

    }
}
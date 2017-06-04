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
    [Authorize]
    public class GigsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [AuthorizeActivatedAccount]
        [AuthorizeSingleLogin]
        public ViewResult Mine()
        {
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(User.Identity.GetUserId());

            return View(gigs);
        }

        [AuthorizeActivatedAccount]
        [AuthorizeSingleLogin]
        public ViewResult Attending()
        {
            var viewModel = new GigsViewModel
            {
                Heading = AppConst.TitleForMyAttendGigs
            };

            return View("Gigs", viewModel);
        }

        [Route("Gigs/Attending/More/{startIndex}")]
        public ActionResult GetMoreGigs(int startIndex)
        {
            var userId = User.Identity.GetUserId();
            var upcomingGigs = _unitOfWork.Gigs.GetGigsUserAttending(userId, startIndex, AppConst.PageSizeSm);

            if (!upcomingGigs.Any())
                return Content(HttpStatusCode.NoContent.ToString());

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                    .ToLookup(a => a.GigId)
            };

            return PartialView("_MoreGigs", viewModel);
        }

        [AuthorizeActivatedAccount]
        [AuthorizeSingleLogin]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading = "Add a Gig"
            };

            return View("GigForm", viewModel);
        }

        [AuthorizeActivatedAccount]
        [AuthorizeSingleLogin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.DateTime,
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _unitOfWork.Gigs.Add(gig);
            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [AuthorizeActivatedAccount]
        [AuthorizeSingleLogin]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != userId)
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                Heading = "Edit a Gig",
                Id = gig.Id,
                Genres = _unitOfWork.Genres.GetGenres(),
                DateTime = gig.DateTime,
                Genre = gig.GenreId,
                Venue = gig.Venue
            };

            return View("GigForm", viewModel);
        }

        [AuthorizeActivatedAccount]
        [AuthorizeSingleLogin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendees(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.Venue, viewModel.DateTime, viewModel.Genre);

            _unitOfWork.Complete();

            return RedirectToAction("Mine", "Gigs");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            var userId = User.Identity.GetUserId();

            var viewModel = new GigDetailsViewModel
            {
                Gig = gig,
            };

            if (User.Identity.IsAuthenticated)
            {
                viewModel.IsAttending =
                    _unitOfWork.Attendances.GetAttendance(gig.Id, userId) != null;

                viewModel.IsFollowing =
                    _unitOfWork.Followings.GetFollowing(gig.ArtistId, userId) != null;
            }

            return View("Details", viewModel);
        }

        [AllowAnonymous]
        [Route("Artists/{artistId}/Gigs/More/{startIndex}")]
        public ActionResult GetMoreGigs(string artistId, int startIndex)
        {
            const int numPerLoad = AppConst.CountOfGigPerLoad;
            var gigs = _unitOfWork.Gigs.GetUpcomingGigsByArtist(artistId, startIndex, numPerLoad);

            if (!gigs.Any())
                return Content(HttpStatusCode.NoContent.ToString());

            var attendances = _unitOfWork.Attendances.GetFutureAttendances(
                User.Identity.GetUserId()).ToLookup(k => k.GigId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = gigs,
                Attendances = attendances
            };

            return PartialView("_MoreGigs", viewModel);
        }
    }
}
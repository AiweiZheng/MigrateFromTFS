using System.Linq;
using System.Net;
using System.Web.Mvc;
using GigHub.Core;
using GigHub.Core.ViewModels;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Landing()
        {
            return View();
        }
        public ActionResult Index(string query = null)
        {
            var viewModel = new GigsViewModel
            {
                Heading = AppConst.TitleForHomeGigs,
                SearchTerm = query
            };

            return View("Gigs", viewModel);
        }

        [Route("Gigs/More/{startIndex}")]
        public ActionResult GetMoreGigs(int startIndex, string query = null)
        {
            var upcomingGigs = _unitOfWork.Gigs.GetUpcomingGigs(startIndex, AppConst.PageSizeSm, query);

            if (!upcomingGigs.Any())
                return Content(HttpStatusCode.NoContent.ToString());

            var userId = User.Identity.GetUserId();

            var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId)
                .ToLookup(a => a.GigId);

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Attendances = attendances
            };

            return PartialView("_MoreGigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
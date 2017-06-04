using System.Web.Mvc;
using GigHub.Core.Filters;
using GigHub.Core.Models;

namespace GigHub.Controllers
{
    [AuthorizeActivatedAccount]
    [AuthorizeSingleLogin]
    [Authorize(Roles = RoleName.AccountManager)]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("Accounts");
        }

        public ActionResult New()
        {
            return View("Register");
        }

        public ActionResult Edit()
        {
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register()
        {
            return RedirectToAction("Index", "Admin");
        }
    }
}
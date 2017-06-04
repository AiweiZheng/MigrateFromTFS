using System.Web;
using System.Web.Mvc;
using GigHub.Controllers;
using GigHub.Persistence;

namespace GigHub.Core.Filters
{
    public sealed class AuthorizeSingleLogin : AuthorizeAttribute
    {
        private bool _isAuthorized;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.User.Identity.Name;
            var access = httpContext.Session.SessionID;

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(access))
            {
                _isAuthorized = true;
            }

            var unitOfWork = new UnitOfWork(new ApplicationDbContext());
            _isAuthorized = unitOfWork.Logins.IsLoggedIn(user, access);

            return _isAuthorized;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            base.OnAuthorization(filterContext);

            if (!_isAuthorized)
            {
                filterContext.Controller.TempData.Add("RedirectReason", ErrorMsg.AccountHasLoggedIn);
            }
        }

    }
}
using System.Web;
using System.Web.Mvc;
using GigHub.Controllers;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;


namespace GigHub.Core.Filters
{
    public sealed class AuthorizeActivatedAccount : AuthorizeAttribute
    {
        private bool _isAuthorized = false;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userId = httpContext.User.Identity.GetUserId();

            if (!_isLogged(userId))
                _isAuthorized = true;

            else if (!_isActivated(userId))
            {
                _isAuthorized = false;
                httpContext
                    .GetOwinContext()
                    .Authentication
                    .SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            }
            else
                _isAuthorized = base.AuthorizeCore(httpContext);

            return _isAuthorized;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (!_isAuthorized)
            {
                filterContext.Controller.TempData.Add("RedirectReason", ErrorMsg.AccountIsInactivated);
            }
        }

        private bool _isLogged(string userId)
        {
            return userId != null;
        }

        private bool _isActivated(string userId)
        {
            var unitOfWork = new UnitOfWork(new ApplicationDbContext());
            var user = unitOfWork.Users.GetUser(userId);
            return user.Activated;
        }
    }
}
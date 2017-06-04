using System.Web;
using System.Web.Http.Controllers;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace GigHub.Controllers.Api.Filters
{
    public sealed class ApiAuthorizeActivatedAccount : AuthorizeAttribute
    {
        private bool _isAuthorized;

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();

            if (!_isLogged(userId))
                _isAuthorized = true;

            else if (!_isActivated(userId))
            {
                _isAuthorized = false;
                HttpContext.Current
                    .GetOwinContext()
                    .Authentication
                    .SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            }
            else
                _isAuthorized = base.IsAuthorized(actionContext);

            return _isAuthorized;
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
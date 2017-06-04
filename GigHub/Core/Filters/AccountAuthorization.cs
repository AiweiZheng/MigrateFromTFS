
using System;
using System.Web;
using System.Web.Mvc;
using GigHub.Controllers;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;

namespace GigHub.Core.Filters
{

    public class AccountAuthorization : AuthorizeAttribute
    {
        private bool _isAuthorized;
        private bool _isActivate;
        private bool _isSingleLogin;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var url = httpContext.Request.Url;
            _isAuthorized = base.AuthorizeCore(httpContext);

            return _isAuthorized;
            if (!_isAuthorized)
                return false;


            var userId = httpContext.User.Identity.GetUserId();

            if (!_checkAccountState(userId))
            {
                _isActivate = false;
                httpContext
                    .GetOwinContext()
                    .Authentication
                    .SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                return false;
            }

            if (!_checkSingleLogin(httpContext.User.Identity.Name, httpContext.Session.SessionID))
            {
                _isSingleLogin = false;

                httpContext
                    .GetOwinContext()
                    .Authentication
                    .SignOut(DefaultAuthenticationTypes.ApplicationCookie);

                return false;
            }


            return _isAuthorized;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            if (!_isActivate)
            {
                filterContext.Controller.TempData.Add("RedirectReason", ErrorMsg.AccountIsInactivated);
                return;
            }

            if (!_isSingleLogin)
            {
                filterContext.Controller.TempData.Add("RedirectReason", ErrorMsg.AccountHasLoggedIn);

            }
        }


        private bool _checkAccountState(string userId)
        {
            var unitOfWork = new UnitOfWork(new ApplicationDbContext());
            var user = unitOfWork.Users.GetUser(userId);
            return user.Activated;
        }

        private bool _checkSingleLogin(string userName, string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                _isAuthorized = true;
            }

            var unitOfWork = new UnitOfWork(new ApplicationDbContext());
            _isAuthorized = unitOfWork.Logins.IsLoggedIn(userName, sessionId);

            return _isAuthorized;
        }
    }
}
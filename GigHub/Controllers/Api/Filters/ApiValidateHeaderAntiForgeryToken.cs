using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace GigHub.Controllers.Api.Filters
{
    public class ApiValidateHeaderAntiForgeryToken : AuthorizeAttribute
    {
        private const string Token = "__RequestVerificationToken";

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var headerToken = actionContext
                .Request
                .Headers
                .GetValues(Token)
                .FirstOrDefault();

            var cookieToken = actionContext
                .Request
                .Headers
                .GetCookies()
                .Select(c => c[AntiForgeryConfig.CookieName])
                .FirstOrDefault();

            //check for missing cookie or header
            if (cookieToken == null || headerToken == null)
            {
                return false;
            }

            //ensure that the cookies matches the header
            try
            {
                AntiForgery.Validate(cookieToken.Value, headerToken);
            }
            catch
            {
                return false;
            }
            return base.IsAuthorized(actionContext);
        }
    }
}
using System;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Core
{
    public static class HtmlLinkHelper
    {
        public static MvcHtmlString ActiveLink(
            this HtmlHelper helper,
            string name,
            string actionName,
            string controllerName
        )
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);

            var anchor = new TagBuilder("a");
            if ((currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) &&
                 currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase)))
            {
                anchor.AddCssClass("active");
            }

            anchor.Attributes.Add("href", url.Action(actionName, controllerName));
            anchor.SetInnerText(name);

            return new MvcHtmlString(anchor.ToString());
        }

        public static MvcHtmlString ActiveLink(
            this HtmlHelper helper,
            string name,
            string actionName,
            string controllerName,
            object routerValues,
            object htmlAttributes
        )
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];

            var anchor = new TagBuilder("a");


            if ("Register".Equals(actionName, StringComparison.CurrentCultureIgnoreCase))
            {
                anchor.Attributes.Add("id", "registerLink");
            }

            else if ("Login".Equals(actionName, StringComparison.CurrentCultureIgnoreCase))
            {
                anchor.Attributes.Add("id", "loginLink");
            }

            else if (AppConst.AppBrand.Equals(name, StringComparison.CurrentCultureIgnoreCase))
            {
                anchor.AddCssClass("navbar-brand");
            }

            if ((currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) &&
                 currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase)))
            {
                anchor.AddCssClass("active");
            }

            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            anchor.Attributes.Add("href", url.Action(actionName, controllerName));
            anchor.SetInnerText(name);

            return new MvcHtmlString(anchor.ToString());
        }

    }
}
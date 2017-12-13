// <copyright file="FormExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;
using System.Web.Routing;
    using System.Reflection;
    using System.Text;
    using Appva.Mvc.Security;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class FormExtensions
    {
        //IPost Post<T>(string action);
        //IPost Post<T>(Expression<Action<T>> action);
        //IPost Post();
        //IGet Get<T>(Expression<Action<T>> action);
        //IGet Get();
        //IPut Put<T>(Expression<Action<T>> action);
        //IPut Put();

        public static Post Post<T>(this HtmlHelper htmlHelper, Expression<Action<T>> action) where T : Controller
        {
            var x = Infrastructure.Internal.Route.New(htmlHelper, action);
            return new Post(htmlHelper, x);
        }

        public static Post Post<T>(this HtmlHelper htmlHelper, string action) where T : Controller
        {
            /*var Html = helper;
            if (! Html.IsOperationAllowed<T>(x => x.Dispose()))
            {
                return null;
            }
            Html.Post<SequenceController>("create").AcceptCharset().Content(null);*/
            //UrlHelper.Action()
            //new UrlHelper(helper.ViewContext.RequestContext).Action();
            var x = Infrastructure.Internal.Route.New(htmlHelper, action, new { });
            return new Post(htmlHelper, x);
        }
    }

    public static class PermissionsKKKKK
    {
        /// <summary>
        /// area.controller.action
        /// *.controller.action
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static MvcHtmlString Register(Assembly assembly)
        {
            var mi = assembly.GetTypes()
                .Where(x => typeof(Controller).IsAssignableFrom(x) && x.IsAbstract == false) //filter controllers
                //.SelectMany(x => x.GetMethods())
                //.Where(method => method.IsPublic && method.IsDefined(typeof(NonActionAttribute)))
                .ToList();
            mi.First().CustomAttributes.ToList();
            var sb = new StringBuilder();
            foreach (var m in mi)
            {
                var memberInfo = m.GetCustomAttributes(true);
                var sjj = "";
                foreach (var attr in memberInfo)
                {
                    if (attr is RouteAreaAttribute)
                    {
                        var route = attr as RouteAreaAttribute;

                        sjj += "<li>" + route.AreaName + "</li>";
                    }
                    if (attr is PermissionsAttribute)
                    {
                        var p = attr as PermissionsAttribute;
                        sjj += "<li>PermissionsAttribute</li>";
                    }
                }
                sb.Append("<h3>" + m.Name.ToLowerInvariant() + "</h3>");
                if (sjj.Length > 0)
                {
                    sb.Append("<b>Attributes:</b>");
                    sb.Append("<ul>" + sjj + "</ul>");
                }
                var methods = m.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)
                    .Where(x => x.IsPublic && ! x.IsDefined(typeof(NonActionAttribute) ));
                sb.Append("<b>Methods:</b>");
                sb.Append("<ul>");
                foreach (var method in methods)
                {
                    var stuff = "";
                    var info = method.GetCustomAttributes(true);
                    sb.Append("<li>" + method.Name);

                    foreach (var attr in info)
                    {
                        if (attr is PermissionsAttribute)
                        {
                            var p = attr as PermissionsAttribute;
                            stuff += "<li>" + "PermissionAttribute" +  "</li>";
                        }
                        if (attr is AllowAnonymousAttribute)
                        {
                            var p = attr as AllowAnonymousAttribute;
                            stuff += "<li>" + "AllowAnonymousAttribute" + "</li>";
                        }
                    }
                    if (stuff.Length > 0)
                    {
                        sb.Append("<ul>" + stuff + "</ul>");
                    }
                    if (stuff.Length == 0)
                    {
                        var k = m.Name + " " + method.Name;
                        var oo = k + "";
                    }
                    sb.Append("</li>");
                }
                sb.Append("</ul>");
                /// each method AllowAnonymous PermissionsAttribute
            }
            return new MvcHtmlString(sb.ToString());
        }
    }

    public class MvcUrl
    {
        // permissino cache on controller action.
        // all controllers and actions, 
        private readonly IDictionary<string, string> cache;

        public MvcUrl(string action, string controller, string area, object parameters)
        {
            // if acton is null then grab current
            // if controller is null then grab current
            // if area is null then grab current
            // if parameters is null and 
            // if no controller action area then exception
        }
    }
    public static class ResolveRouteData
    {
        public static void Resolve(ViewContext context, RouteValueDictionary routes)
        {
            //new UrlHelper(context.RequestContext).RequestContext.RouteData;
            object action     = null;
            object controller = null;
            object area       = null;
            context.RouteData.Values.TryGetValue    ("action",     out action);
            context.RouteData.Values.TryGetValue    ("controller", out controller);
            context.RouteData.DataTokens.TryGetValue("area",       out area);

            context.RouteData.Values.Where(x => x.Key == "").Select(x => x.Value).SingleOrDefault();
            //string formAction = htmlHelper.ViewContext.HttpContext.Request.RawUrl;
            //context.RouteData.Values.Select(x).Where(route.x.Key)
        }
    }
}
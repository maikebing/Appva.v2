// <copyright file="FormExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System.Web.Mvc;
    ///using System.Web.Routing;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class FormExtensions
    {
        public static IFormMethodGet Get<T>(this HtmlHelper htmlHelper, string action = null) where T : Controller
        {
            return new FormMethodGetElement(htmlHelper, Route.New(htmlHelper, action, new { }));
        }

        public static IFormMethodPost Post<T>(this HtmlHelper htmlHelper, string action = null) where T : Controller
        {
            return new FormMethodPostElement(htmlHelper, Route.New(htmlHelper, action, new { }));
        }

        public static IFormMethodPut Put<T>(this HtmlHelper htmlHelper, string action = null) where T : Controller
        {
            return new FormMethodPutElement(htmlHelper, Route.New(htmlHelper, action, new { }));
        }

        public static IFormMethodDelete Delete<T>(this HtmlHelper htmlHelper, string action = null) where T : Controller
        {
            return new FormMethodDeleteElement(htmlHelper, Route.New(htmlHelper, action, new { }));
        }
    }
}
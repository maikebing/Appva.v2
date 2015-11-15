// <copyright file="TimingAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System.Diagnostics;
    using System.Web;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Timing diagnostic attribute for debugging.
    /// </summary>
    public sealed class TimingAttribute : IActionFilter, IResultFilter
    {
        #region Variables.

        /// <summary>
        /// The action timing key.
        /// </summary>
        private const string Action = "https://schemas.appva.se/diagnostics/timing/action";

        /// <summary>
        /// The page rendering timing key.
        /// </summary>
        private const string Result = "https://schemas.appva.se/diagnostics/timing/result";

        #endregion

        #region IActionFilter Members.

        /// <inheritdoc />
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            this.Stop(filterContext.HttpContext, Action);
        }

        /// <inheritdoc />
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            this.Start(filterContext.HttpContext, Action);
        }

        #endregion

        #region IResultFilter Members.

        /// <inheritdoc />
        void IResultFilter.OnResultExecuted(ResultExecutedContext filterContext)
        {
            this.Stop(filterContext.HttpContext, Result);
            if (filterContext.IsChildAction || filterContext.HttpContext.Request.IsAjaxRequest())
            {
                return;
            }
            var response = filterContext.HttpContext.Response;
            if (! response.ContentType.Equals("text/html"))
            {
                return;
            }
            response.Write(string.Format(
                @"<!-- Action '{0}/{1}' took {2} ms to execute and {3} ms to render -->",
                filterContext.RouteData.Values["controller"],
                filterContext.RouteData.Values[Action],
                this.StopWatch(filterContext.HttpContext, Action).ElapsedMilliseconds,
                this.StopWatch(filterContext.HttpContext, Result).ElapsedMilliseconds));
        }

        /// <inheritdoc />
        void IResultFilter.OnResultExecuting(ResultExecutingContext filterContext)
        {
            this.Start(filterContext.HttpContext, Result);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Returns a <see cref="Stopwatch"/> or creates one if it does not exist.
        /// </summary>
        /// <param name="context">The http context</param>
        /// <param name="key">The unique identifier</param>
        /// <returns>A reference to a <see cref="StopWatch"/></returns>
        private Stopwatch StopWatch(HttpContextBase context, string key)
        {
            var item = context.Items[key];
            if (item == null)
            {
                context.Items[key] = new Stopwatch();
            }
            return context.Items[key] as Stopwatch;
        }

        /// <summary>
        /// Starts the current <see cref="Stopwatch"/>.
        /// </summary>
        /// <param name="context">The http context</param>
        /// <param name="key">The unique identifier</param>
        private void Start(HttpContextBase context, string key)
        {
            this.StopWatch(context, key).Start();
        }

        /// <summary>
        /// Stops the current <see cref="Stopwatch"/>.
        /// </summary>
        /// <param name="context">The http context</param>
        /// <param name="key">The unique identifier</param>
        private void Stop(HttpContextBase context, string key)
        {
            this.StopWatch(context, key).Stop();
        }

        #endregion
    }
}
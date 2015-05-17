// <copyright file="ValidateAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System;
    using System.Net;
    using System.Web.Mvc;
    using JetBrains.Annotations;

    #endregion

    /// <summary>
    /// An <see cref="ActionFilterAttribute"/> for automatically validating ModelState 
    /// before a controller action is executed.
    /// Performs a Redirect to the previous GET action if ModelState is invalid. 
    /// Assumes the <see cref="HydrateAttribute"/> is used on the GET action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ValidateAttribute : AbstractModelStateAttribute
    {
        #region Variables.

        /// <summary>
        /// Model state data token key.
        /// </summary>
        private const string ModelStateContextToken = "ModelStateContext";

        #endregion

        #region ActionFilterAttribute Overrides.

        /// <inheritdoc />
        public override void OnActionExecuting([NotNull] ActionExecutingContext filterContext)
        {
            this.HandleInvalidModelState(filterContext);
            base.OnActionExecuting(filterContext);
        }

        /// <inheritdoc />
        public override void OnActionExecuted([NotNull] ActionExecutedContext filterContext)
        {
            this.HandleInvalidModelState(filterContext);
            base.OnActionExecuted(filterContext);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Redirects to previous view if model state is invalid.
        /// </summary>
        /// <remarks>Throws a bad request in case of Ajax</remarks>
        /// <param name="filterContext">The <see cref="ActionExecutedContext"/></param>
        private void HandleInvalidModelState([NotNull] ActionExecutingContext filterContext)
        {
            var isValid = filterContext.Controller.ViewData.ModelState.IsValid;
            filterContext.HttpContext.Items[ModelStateContextToken] = isValid;
            if (isValid)
            {
                return;
            }
            if (! filterContext.HttpContext.Request.HttpMethod.Equals(this.HttpPost))
            {
                return;
            }
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult((int) HttpStatusCode.BadRequest);
            }
            else
            {
                this.Add(filterContext);
                filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
            }
        }

        /// <summary>
        /// Redirects to previous view if model state is invalid.
        /// </summary>
        /// <remarks>Throws a bad request in case of Ajax</remarks>
        /// <param name="filterContext">The <see cref="ActionExecutedContext"/></param>
        private void HandleInvalidModelState([NotNull] ActionExecutedContext filterContext)
        {
            var isValid = filterContext.Controller.ViewData.ModelState.IsValid;
            filterContext.HttpContext.Items[ModelStateContextToken] = isValid;
            if (isValid)
            {
                return;
            }
            if (!filterContext.HttpContext.Request.HttpMethod.Equals(this.HttpPost))
            {
                return;
            }
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            else
            {
                this.Add(filterContext);
                filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
            }
        }

        #endregion
    }
}
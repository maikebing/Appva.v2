// <copyright file="ValidateAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Filters
{
    #region Imports.

    using System;
    using System.Net;
    using System.Web.Mvc;

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
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var isValid = filterContext.Controller.ViewData.ModelState.IsValid;
            filterContext.HttpContext.Items[ModelStateContextToken] = isValid;
            if (filterContext.HttpContext.Request.HttpMethod.Equals(this.HttpPost))
            {
                if (! isValid)
                {
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
            }
            base.OnActionExecuting(filterContext);
        }

        #endregion
    }
}
// <copyright file="HydrateAttribute.cs" company="Appva AB">
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
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// Restores the <see cref="ModelState"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HydrateAttribute : AbstractModelStateAttribute
    {
        #region ActionFilterAttribute Overrides.

        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.HttpContext.Request.HttpMethod.Equals(this.HttpGet))
            {
                if (filterContext.Result is ViewResult)
                {
                    this.Merge(filterContext);
                }
                else
                {
                    this.Remove(filterContext);
                }
            }
            base.OnActionExecuted(filterContext);
        }

        #endregion
    }
}
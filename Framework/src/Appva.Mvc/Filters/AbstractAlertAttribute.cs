// <copyright file="AbstractAlertAttribute.cs" company="Appva AB">
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
    /// Adds an alert message to the temp data.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class AbstractAlertAttribute : ActionFilterAttribute
    {
        #region Variables.

        /// <summary>
        /// The alert message to convey.
        /// </summary>
        private readonly string key;

        /// <summary>
        /// The alert message to convey.
        /// </summary>
        private readonly string message;

        #endregion
        
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractAlertAttribute"/> class.
        /// </summary>
        /// <param name="message">The message to convey</param>
        protected AbstractAlertAttribute(string message)
        {
            this.key = typeof(AbstractAlertAttribute).FullName;
            this.message = message;
        }

        #endregion

        #region ActionFilterAttribute Overrides.

        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (! filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Controller.TempData[this.key] = this.message;
                filterContext.Controller.TempData[this.key + "class"] = this.GetContext();
            }
            base.OnActionExecuted(filterContext);
        }

        #endregion

        #region Protected Abstract Members.

        /// <summary>
        /// Returns the alert context.
        /// </summary>
        /// <returns>The context</returns>
        protected abstract string GetContext();

        #endregion
    }
}
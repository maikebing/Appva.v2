// <copyright file="AbstractModelStateAttribute.cs" company="Appva AB">
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
    /// Abstract base class for transfering <see cref="ModelState"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class AbstractModelStateAttribute : ActionFilterAttribute
    {
        #region Variables.

        /// <summary>
        /// The temp data key.
        /// </summary>
        private readonly string key;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractModelStateAttribute"/> class.
        /// </summary>
        protected AbstractModelStateAttribute()
        {
            this.key = typeof(AbstractModelStateAttribute).FullName;
            this.Order = 50;
        }

        #endregion

        #region Protected Properties.

        /// <summary>
        /// HTTP get request.
        /// </summary>
        protected string HttpGet
        {
            get
            {
                return "GET";
            }
        }

        /// <summary>
        /// HTTP post request.
        /// </summary>
        protected string HttpPost
        {
            get
            {
                return "POST";
            }
        }

        #endregion

        #region Protected Members.

        /// <summary>
        /// Adds the current <see cref="ModelState"/> to <see cref="TempDataDictionary"/>.
        /// </summary>
        /// <param name="context">The context</param>
        protected void Add(ControllerContext context)
        {
            context.Controller.TempData[this.key] = context.Controller.ViewData.ModelState;
        }

        /// <summary>
        /// Merges the current <see cref="ModelState"/> from <see cref="TempDataDictionary"/>.
        /// </summary>
        /// <param name="context">The context</param>
        protected void Merge(ControllerContext context)
        {
            var prevModelState = context.Controller.TempData[this.key] as ModelStateDictionary;
            context.Controller.ViewData.ModelState.Merge(prevModelState);
        }

        /// <summary>
        /// Removes the <see cref="ModelState"/> from <see cref="TempDataDictionary"/>.
        /// </summary>
        /// <param name="context">The context</param>
        protected void Remove(ControllerContext context)
        {
            context.Controller.TempData[this.key] = null;
        }

        #endregion
    }
}
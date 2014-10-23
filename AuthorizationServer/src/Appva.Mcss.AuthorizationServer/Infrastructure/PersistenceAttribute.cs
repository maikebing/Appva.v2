// <copyright file="PersistenceAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a></author>
namespace Appva.Mcss.AuthorizationServer.Code
{
    #region Imports.

    using System.Data;
    using System.Web.Mvc;
    using Persistence;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// NHibernate transaction filter for MVC controllers.
    /// </summary>
    public class PersistenceAttribute : ActionFilterAttribute
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceAttribute"/> class.
        /// </summary>
        public PersistenceAttribute()
        {
            this.Order = 0;
        }

        #endregion

        #region ActionFilterAttribute overrides.

        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            DependencyResolver.Current.GetService<IPersistenceContext>()
                .Open().BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var state = ((filterContext.Exception.IsNull() && ! filterContext.ExceptionHandled)
                && ! filterContext.IsChildAction && filterContext.Controller.ViewData.ModelState.IsValid);
            DependencyResolver.Current.GetService<IPersistenceContext>().Commit(state);
        }

        #endregion
    }
}
// <copyright file="PersistenceAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Infrastructure.WebApi
{
    #region Imports.

    using System.Data;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using Persistence;
    using Core.Extensions;
    using Autofac.Integration.WebApi;

    #endregion

    /// <summary>
    /// NHibernate transaction filter for Web API controllers.
    /// The Web API internals create filter instances and then cache them, never to be created again. 
    /// This removes any "hooks" that might otherwise have existed to do anything on a per-request basis 
    /// in a filter. Due to the shortcomings in Web API the usual <see cref="ActionFilterAttribute"/> is
    /// replaced by <see cref="IAutofacActionFilter"/> and configured at start up.
    /// </summary>
    /// <remarks>This is unlike the MVC filter not ordered!</remarks>
    public class PersistenceAttribute : IAutofacActionFilter
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceAttribute"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public PersistenceAttribute(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region ActionFilterAttribute overrides.

        /// <inheritdoc />
        public void OnActionExecuting(HttpActionContext actionContext)
        {
            this.persistenceContext.Open().BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <inheritdoc />
        public void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var state = (actionExecutedContext.Exception.IsNull() && 
                actionExecutedContext.ActionContext.ModelState.IsValid);
            this.persistenceContext.Commit(state);
        }

        #endregion
    }
}
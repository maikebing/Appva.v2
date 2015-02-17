// <copyright file="PersistenceAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Application.Persistence
{
    #region Imports.

    using System.Data;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using Appva.Persistence;
    using Autofac.Integration.WebApi;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class PersistenceAttribute : IAutofacActionFilter
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistenceAttribute"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public PersistenceAttribute(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }
        
        #endregion

        #region IAutofacActionFilter Members

        /// <inheritdoc />
        public void OnActionExecuting(HttpActionContext actionContext)
        {
            this.persistenceContext.Open().BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <inheritdoc />
        public void OnActionExecuted(HttpActionExecutedContext context)
        {
            var modelState = context.ActionContext.ModelState;
            var isNotHttpGet = ! context.Request.Method.Equals(HttpMethod.Get);
            var commit = isNotHttpGet && modelState.IsValid && context.Exception == null;
            this.persistenceContext.Commit(commit);
        }

        #endregion
    }
}
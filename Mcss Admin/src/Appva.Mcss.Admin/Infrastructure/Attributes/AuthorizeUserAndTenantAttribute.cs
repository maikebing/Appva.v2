// <copyright file="AuthorizeUserAndTenantAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure.Attributes
{
    #region Imports.

    using System.Net;
    using System.Web.Mvc;
    using Appva.Core.Logging;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuthorizeUserAndTenantAttribute : AuthorizeAttribute
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<AuthorizeUserAndTenantAttribute>();

        /// <summary>
        /// Autoinjected authorization.
        /// </summary>
        public IAuthorizeTenantIdentity AuthorizeTenantIdentity
        {
            get;
            set;
        }

        #endregion

        #region Routes.

        /// <inheritdoc />
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext != null && filterContext.HttpContext != null && filterContext.HttpContext.Request != null)
            {
                var result = this.AuthorizeTenantIdentity.Validate(filterContext.HttpContext.Request);
                if (! result.IsAuthorized)
                {
                    if (result.IsNotFound)
                    {
                        Log.Error("The tenant for this request was not found");
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.PreconditionFailed, "Precondition Failed");
                        return;
                    }
                    if (result.IsUnauthorized)
                    {
                        Log.Error("The tenant was unauthorized for the request {0}", filterContext.HttpContext.Request.Url);
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Conflict, "Conflict");
                        return;
                    }
                }
            }
            base.OnAuthorization(filterContext);
        }

        #endregion
    }
}
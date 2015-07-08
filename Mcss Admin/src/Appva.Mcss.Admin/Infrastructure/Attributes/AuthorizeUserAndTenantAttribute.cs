// <copyright file="AuthorizeUserAndTenantAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure.Attributes
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
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

        #endregion

        #region Routes.

        /// <inheritdoc />
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext != null && filterContext.HttpContext != null && filterContext.HttpContext.Request != null)
            {
                var authorize = DependencyResolver.Current.GetService(typeof(IAuthorizeTenantIdentity)) as IAuthorizeTenantIdentity;
                var result = authorize.Validate(filterContext.HttpContext.Request);
                if (! result.IsAuthorized)
                {
                    if (result.IsNotFound)
                    {
                        Log.Error("The tenant for this request was not found");
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Bad Request");
                        return;
                    }
                    if (result.IsUnauthorized)
                    {
                        Log.Error("The tenant was unauthorized for the request {0}", filterContext.HttpContext.Request.Url);
                        filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Forbidden");
                        return;
                    }
                }
            }
            base.OnAuthorization(filterContext);
        }

        #endregion
    }
}
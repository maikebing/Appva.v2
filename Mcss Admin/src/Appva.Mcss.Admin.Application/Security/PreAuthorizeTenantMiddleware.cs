// <copyright file="PreAuthorizeTenantMiddleware.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Security
{
    #region Imports.

    using System.Threading.Tasks;
    using Appva.Core.Logging;
    using Appva.Mcss.Admin.Application.Services;
    using Microsoft.Owin;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PreAuthorizeTenantMiddleware : OwinMiddleware
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<PreAuthorizeTenantMiddleware>();

        /// <summary>
        /// The <see cref="ITenantService"/>.
        /// </summary>
        private readonly ITenantService tenantService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PreAuthorizeTenantMiddleware"/> class.
        /// </summary>
        /// <param name="tenantService">The <see cref="ITenantService"/></param>
        /// <param name="next">The <see cref="OwinMiddleware"/></param>
        public PreAuthorizeTenantMiddleware(ITenantService tenantService, OwinMiddleware next)
            : base(next)
        {
            this.tenantService = tenantService;
        }

        #endregion

        #region OwinMiddleware Overrides.

        /// <inheritdoc />
        public async override Task Invoke(IOwinContext context)
        {
            if (context != null && context.Request != null)
            {
                var result = this.tenantService.Validate(context);
                if (result.IsNotFound)
                {
                    Log.Error("The tenant identification for this request was not found; returning 'Precondition Failed' 412");
                    context.Response.StatusCode = 412;
                    return;
                }
                if (result.IsInvalid)
                {
                    Log.Error("The tenant identity is unauthorized for the request {0}; returning 'Conflict' 409", context.Request.Uri);
                    context.Response.StatusCode = 409;
                    return;
                }
            }
            await Next.Invoke(context);
        }

        #endregion
    }
}
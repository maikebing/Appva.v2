// <copyright file="CreateTenantHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Mvc.Imaging;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateTenantHandler : PersistentRequestHandler<CreateTenant, Id<DetailsTenant>>
    {
        #region Variables.

        /// <summary>
        /// Image processing.
        /// </summary>
        private readonly IImageProcessor imageProcessor;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTenantHandler"/> class.
        /// </summary>
        /// <param name="imageProcessor">The <see cref="IImageProcessor"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateTenantHandler(IImageProcessor imageProcessor, IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
            this.imageProcessor = imageProcessor;
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Id<DetailsTenant> Handle(CreateTenant message)
        {
            string fileName = null;
            string mimeType = null;
            if (message.Logotype.IsNotNull())
            {
                this.imageProcessor.Save(message.Logotype, out fileName);
                mimeType = message.Logotype.ContentType;
            }
            var tenant = new Tenant(message.Identifier, message.HostName, message.Name, message.Description, fileName, mimeType, message.ConnectionString).Activate();
            this.Persistence.Save(tenant);
            return new Id<DetailsTenant>
            {
                Id   = tenant.Id,
                Slug = tenant.Slug.Name
            };
        }

        #endregion
    }
}
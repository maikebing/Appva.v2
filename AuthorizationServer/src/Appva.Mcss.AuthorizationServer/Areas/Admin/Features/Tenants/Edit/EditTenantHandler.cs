// <copyright file="EditTenantHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using Appva.Mvc.Imaging;
    using Appva.Persistence;
    using Core.Extensions;
    using Domain.Entities;

    #endregion

    #region Request.
 
    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditTenantRequestHandler
        : PersistentRequestHandler<Id<EditTenantRequest>, EditTenantRequest>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTenantRequestHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public EditTenantRequestHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override EditTenantRequest Handle(Id<EditTenantRequest> message)
        {
            var tenant = this.Persistence.Get<Tenant>(message.Id);
            return new EditTenantRequest
            {
                Id = tenant.Id,
                Identifier = tenant.Identifier,
                HostName = tenant.HostName,
                Name = tenant.Name,
                Description = tenant.Description,
                Logotype = new Logotype(tenant.Image),
                ConnectionString = (tenant.DatabaseConnection == null) ? null : tenant.DatabaseConnection.ConnectionString
            };
        }

        #endregion
    }

    #endregion

    #region Response.

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class EditTenantResponseHandler : PersistentRequestHandler<EditTenantResponse, Id<DetailsTenant>>
    {
        #region Variables.

        /// <summary>
        /// Image processing.
        /// </summary>
        private readonly IImageProcessor imageProcessor;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditTenantResponseHandler"/> class.
        /// </summary>
        /// <param name="imageProcessor">The <see cref="IImageProcessor"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public EditTenantResponseHandler(IImageProcessor imageProcessor, IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
            this.imageProcessor = imageProcessor;
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override Id<DetailsTenant> Handle(EditTenantResponse message)
        {
            var tenant = this.Persistence.Get<Tenant>(message.Id);
            string fileName = null;
            string mimeType = null;
            if (message.Logotype.IsNotNull())
            {
                this.imageProcessor.Save(message.Logotype, out fileName);
                mimeType = message.Logotype.ContentType;
            }
            tenant.Update(
                message.Identifier,
                message.HostName,
                message.Name,
                message.Description,
                fileName,
                mimeType,
                message.ConnectionString,
                null);
            this.Persistence.Update(tenant);
            return new Id<DetailsTenant>
            {
                Id = tenant.Id,
                Slug = tenant.Slug.Name
            };
        }

        #endregion
    }

    #endregion
}
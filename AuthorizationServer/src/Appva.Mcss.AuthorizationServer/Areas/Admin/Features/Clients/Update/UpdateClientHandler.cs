// <copyright file="UpdateClientHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Core.Extensions;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class UpdateClientHandler : PersistentRequestHandler<UpdateClientId, UpdateClient>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateClientHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public UpdateClientHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override UpdateClient Handle(UpdateClientId message)
        {
            UpdateClient client = null;
            var result = this.Persistence.QueryOver<Client>().Where(x => x.Id == message.Id)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Client>(x => x.Name).WithAlias(() => client.Name))
                    .Add(Projections.Property<Client>(x => x.Description).WithAlias(() => client.Description))
                    .Add(Projections.Property<Client>(x => x.Image.FileName).WithAlias(() => client.ImageFileName))
                    .Add(Projections.Property<Client>(x => x.Image.MimeType).WithAlias(() => client.MimeType))
                    .Add(Projections.Property<Client>(x => x.RedirectionEndpoint).WithAlias(() => client.RedirectionEndpoint))
                    //.Add(Projections.Property<Client>(x => x.Identifier).WithAlias(() => client.Identifier))
                    //.Add(Projections.Property<Client>(x => x.Secret).WithAlias(() => client.Secret))
                    //.Add(Projections.Property<Client>(x => x.Grant).WithAlias(() => client.Grant))
                )
                .TransformUsing(Transformers.AliasToBean<UpdateClient>())
                .SingleOrDefault<UpdateClient>();
            return result;
        }

        #endregion
    }
}
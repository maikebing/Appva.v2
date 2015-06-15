// <copyright file="CreateUserHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;
    using Appva.Cryptography;
    using Appva.Mcss.AuthorizationServer.Domain.Authentication;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mvc;
    using Appva.Mvc.Imaging;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    #region Get Request Handler.

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateUserHttpGetHandler : PersistentRequestHandler<NoParameter<CreateUser>, CreateUser>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserHttpGetHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateUserHttpGetHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override CreateUser Handle(NoParameter<CreateUser> message)
        {
            Tickable tickable = null;
            var roles = this.Persistence.QueryOver<Role>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Role>(x => x.Id).WithAlias(() => tickable.Id))
                    .Add(Projections.Property<Role>(x => x.Name).WithAlias(() => tickable.Label)))
                .TransformUsing(Transformers.AliasToBean<Tickable>())
                .List<Tickable>();
            var tenants = this.Persistence.QueryOver<Tenant>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<Tenant>(x => x.Id).WithAlias(() => tickable.Id))
                    .Add(Projections.Property<Tenant>(x => x.Name).WithAlias(() => tickable.Label)))
                .TransformUsing(Transformers.AliasToBean<Tickable>())
                .List<Tickable>();
            return new CreateUser
            {
                Roles = roles,
                Tenants = tenants,
                Password = Password.Random()
            };
        }

        #endregion
    }
    
    #endregion

    #region Post Request Handler.

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateUserHandler : PersistentRequestHandler<CreateUser, DetailsUserId>
    {
        #region Variables.

        /// <summary>
        /// Image processing.
        /// </summary>
        private readonly IImageProcessor imageProcessor;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserHandler"/> class.
        /// </summary>
        /// <param name="imageProcessor">The <see cref="IImageProcessor"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateUserHandler(IImageProcessor imageProcessor, IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
            this.imageProcessor = imageProcessor;
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override DetailsUserId Handle(CreateUser message)
        {
            string fileName = null;
            if (message.ProfileImage.IsNotNull())
            {
                this.imageProcessor.Save(message.ProfileImage, out fileName);
            }
            IList<Role> roles = null;
            if (message.Roles.IsNotEmpty())
            {
                var roleIds = message.Roles.Where(x => x.IsSelected.Equals(true)).Select(x => x.Id).ToArray();
                if (roleIds.Length > 0)
                {
                    roles = this.Persistence.QueryOver<Role>().WhereRestrictionOn(x => x.Id).IsIn(roleIds).List();
                }
            }
            IList<Tenant> tenants = null;
            if (message.Tenants.IsNotEmpty())
            {
                var tenantIds = message.Tenants.Where(x => x.IsSelected.Equals(true)).Select(x => x.Id).ToArray();
                if (tenantIds.Length > 0)
                {
                    tenants = this.Persistence.QueryOver<Tenant>().WhereRestrictionOn(x => x.Id).IsIn(tenantIds).List();
                }
            }
            var uam = this.Persistence.QueryOver<UserAuthenticationMethod>()
                .Where(x => x.Key == "oauth").SingleOrDefault();
            var user = new User(
                message.PersonalIdentityNumber, 
                message.FirstName, 
                message.LastName,
                message.EmailAddress,
                message.MobilePhoneNumber,
                fileName,
                roles,
                tenants
            ).Activate();
            this.Persistence.Save(user);
            this.Persistence.Save(new UserAuthentication(user, new PersonalIdentityNumberPasswordAuthentication { LastLoginAt = DateTime.Now, Password = new Credentials(message.Password).Password }, uam));
            return new DetailsUserId
            {
                Id = user.Id,
                Slug = user.Slug.Name
            };
        }

        #endregion
    }

    #endregion
}
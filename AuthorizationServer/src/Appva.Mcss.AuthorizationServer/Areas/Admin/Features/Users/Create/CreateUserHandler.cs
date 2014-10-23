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
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
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
                    .Add(Projections.Property<Role>(x => x.Name).WithAlias(() => tickable.Name))
                    .Add(Projections.Property<Role>(x => x.Id).WithAlias(() => tickable.Id)))
                .TransformUsing(Transformers.AliasToBean<Tickable>())
                .List<Tickable>();
            return new CreateUser()
            {
                Roles = roles
            };
        }

        #endregion
    }
    
    #endregion

    #region Post Request Handler.

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateUserHandler : PersistentRequestHandler<CreateUser, CreateUserAuthenticationUserId>
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
        public override CreateUserAuthenticationUserId Handle(CreateUser message)
        {
            string fileName = null;
            if (message.ProfileImage.IsNotNull())
            {
                this.imageProcessor.Save(message.ProfileImage, out fileName);
            }
            IList<Role> roles = null;
            if (message.Roles.IsNotEmpty())
            {
                var roleIds = message.Roles.Where(x => x.Selected.Equals(true)).Select(x => x.Id).ToArray();
                if (roleIds.Length > 0)
                {
                    roles = this.Persistence.QueryOver<Role>().WhereRestrictionOn(x => x.Id).IsIn(roleIds).List();
                }
            }
            var user = new User(
                message.PersonalIdentityNumber, 
                message.FirstName, 
                message.LastName,
                message.EmailAddress,
                message.MobilePhoneNumber,
                fileName,
                roles
            );
            return new CreateUserAuthenticationUserId()
            {
                Id = (Guid) this.Persistence.Save(user)
            };
        }

        #endregion

    }
    #endregion
}
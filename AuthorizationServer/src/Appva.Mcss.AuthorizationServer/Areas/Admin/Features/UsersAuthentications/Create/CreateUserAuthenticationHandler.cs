// <copyright file="CreateUserHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cryptography;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion
    
    #region Get Request Handler.

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateUserAuthenticationHttpGetHandler : PersistentRequestHandler<CreateUserAuthenticationUserId, CreateUserAuthentication>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserAuthenticationHttpGetHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateUserAuthenticationHttpGetHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override CreateUserAuthentication Handle(CreateUserAuthenticationUserId message)
        {
            SelectListItem item = null;
            var subquery = QueryOver.Of<UserAuthentication>().Where(x => x.User.Id == message.Id).Select(x => x.UserAuthenticationMethod.Id);
            var authenticationMethods = this.Persistence.QueryOver<UserAuthenticationMethod>()
                .WithSubquery.WhereProperty(x => x.Id).NotIn(subquery)
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<UserAuthenticationMethod>(x => x.Name).WithAlias(() => item.Text))
                    .Add(Projections.Property<UserAuthenticationMethod>(x => x.Key).WithAlias(() => item.Value)))
                .TransformUsing(Transformers.AliasToBean<SelectListItem>())
                .List<SelectListItem>();
            return new CreateUserAuthentication()
            {
                Id = message.Id,
                Password = Password.Random(),
                AuthenticationMethods = authenticationMethods
            };
        }
        #endregion
    }

    #endregion

    #region Post Request Handler.

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateUserAuthenticationHandler : PersistentRequestHandler<CreateUserAuthentication, CreateUserAuthenticationUserId>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserAuthenticationHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public CreateUserAuthenticationHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override CreateUserAuthenticationUserId Handle(CreateUserAuthentication message)
        {
            var user = this.Persistence.Get<User>(message.Id);
            var method = this.Persistence.QueryOver<UserAuthenticationMethod>()
                .Where(x => x.Key == message.AuthenticationMethodKey).SingleOrDefault();
            /*this.Persistence.Save(new UserAuthentication()
            {
                User = user,
                UserAuthenticationMethod = method,
                Credentials = new Credentials(message.Password)
            });*/
            return new CreateUserAuthenticationUserId()
            {
                Id = user.Id
            };
        }

        #endregion
    }

    #endregion
}
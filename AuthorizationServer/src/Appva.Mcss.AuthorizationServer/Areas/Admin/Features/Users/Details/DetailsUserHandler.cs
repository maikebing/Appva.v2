// <copyright file="DetailsUserHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Mcss.AuthorizationServer.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class DetailsUserHandler : PersistentRequestHandler<DetailsUserId, DetailsUser>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsUserHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public DetailsUserHandler(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion

        #region Overrides.

        /// <inheritdocs />
        public override DetailsUser Handle(DetailsUserId message)
        {
            var user = this.Persistence.Get<User>(message.Id);
            return new DetailsUser()
            {
                Id = user.Id,
                IsActive = user.IsActive,
                Resource = new MetaData(user),
                PersonalIdentityNumber = user.PersonalIdentityNumber,
                FullName = user.FullName.AsFormattedName,
                FirstName = user.FullName.FirstName,
                LastName = user.FullName.LastName,
                EmailAddress = user.Contact.EmailAddress,
                MobilePhoneNumber = user.Contact.MobilePhoneNumber,
                ProfilePicture = new ProfilePicture(user.Image)
            };
        }

        #endregion
    }
}
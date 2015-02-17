// <copyright file="User.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Represents a user account in the system.
    /// </summary>
    public class User : AggregateRoot<User>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="personalIdentityNumber"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="fileName"></param>
        /// <param name="roles"></param>
        /// <param name="tenants"></param>
        /// <param name="userAuthentications"></param>
        public User(string personalIdentityNumber, string firstName, string lastName, string emailAddress, string mobilePhoneNumber, string fileName, ICollection<Role> roles = null, ICollection<Tenant> tenants = null, ICollection<UserAuthentication> userAuthentications = null)
            : this(personalIdentityNumber, new FullName(firstName, lastName), new Contact(emailAddress, mobilePhoneNumber), new Image(fileName, null), roles, tenants, userAuthentications)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="personalIdentityNumber"></param>
        /// <param name="fullName"></param>
        /// <param name="contact"></param>
        /// <param name="image"></param>
        /// <param name="roles"></param>
        /// <param name="tenants"></param>
        /// <param name="userAuthentications"></param>
        public User(string personalIdentityNumber, FullName fullName, Contact contact, Image image, ICollection<Role> roles = null, ICollection<Tenant> tenants = null, ICollection<UserAuthentication> userAuthentications = null)
        {
            this.IsActive = false;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
            this.Slug = new Slug(fullName.AsFormattedName);
            this.PersonalIdentityNumber = personalIdentityNumber;
            this.FullName = fullName;
            this.Contact = contact;
            this.Image = image;
            this.Roles = roles;
            this.Tenants = tenants;
            this.UserAuthentications = userAuthentications;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected User()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Whether the user is active or not.
        /// </summary>
        public virtual bool IsActive
        {
            get;
            protected set;
        }

        /// <summary>
        /// The user slug.
        /// </summary>
        public virtual Slug Slug
        {
            get;
            protected set;
        }

        /// <summary>
        /// A unique identitier for a user.
        /// E.g. for swedish citizens: personnummer.
        /// </summary>
        public virtual string PersonalIdentityNumber
        {
            get;
            protected set;
        }

        /// <summary>
        /// The users' first and last name.
        /// E.g. "John Doe".
        /// </summary>
        public virtual FullName FullName
        {
            get;
            protected set;
        }

        /// <summary>
        /// The users' e-mail address and mobile phone number.
        /// </summary>
        public virtual Contact Contact
        {
            get;
            protected set;
        }

        /// <summary>
        /// The users' profile image.
        /// </summary>
        public virtual Image Image
        {
            get;
            protected set;
        }

        /// <summary>
        /// The users' assigned roles.
        /// </summary>
        public virtual ICollection<Role> Roles
        {
            get;
            protected set;
        }

        /// <summary>
        /// The users' tenants.
        /// </summary>
        public virtual ICollection<Tenant> Tenants
        {
            get;
            protected set;
        }

        /// <summary>
        /// The users' methods of authentication.
        /// </summary>
        public virtual ICollection<UserAuthentication> UserAuthentications
        {
            get;
            protected set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Activates the user.
        /// </summary>
        public virtual User Activate()
        {
            this.IsActive = true;
            return this;
        }

        /// <summary>
        /// Inactivates the user.
        /// </summary>
        public virtual User Inactivate()
        {
            this.IsActive = false;
            return this;
        }

        #endregion
    }
}
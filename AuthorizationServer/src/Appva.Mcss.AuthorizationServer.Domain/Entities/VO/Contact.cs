// <copyright file="Contact.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Represents a piece of contact information.
    /// </summary>
    public class Contact : ValueObject<Contact>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class.
        /// </summary>
        /// <param name="emailAddress">The e-mail address</param>
        /// <param name="mobilePhoneNumber">The mobile telephone number</param>
        public Contact(string emailAddress, string mobilePhoneNumber)
        {
            this.EmailAddress = emailAddress;
            this.MobilePhoneNumber = mobilePhoneNumber;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class.
        /// </summary>
        protected Contact()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the E-mail address.
        /// </summary>
        public virtual string EmailAddress
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the mobile telephone number.
        /// </summary>
        public virtual string MobilePhoneNumber
        {
            get;
            private set;
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override bool Equals(Contact other)
        {
            return other != null &&
                object.Equals(this.EmailAddress, other.EmailAddress) &&
                object.Equals(this.MobilePhoneNumber, other.MobilePhoneNumber);
        }

        #endregion
    }
}
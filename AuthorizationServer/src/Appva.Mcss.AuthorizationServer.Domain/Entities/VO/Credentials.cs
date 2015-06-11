// <copyright file="Credentials.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Represents secure credentials.
    /// </summary>
    public class Credentials : ValueObject<Credentials>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        /// <param name="plainText">Password as plainText</param>
        public Credentials(string plainText)
        {
            this.Password = Cryptography.Password.Pbkdf2(plainText);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate</remarks>
        protected Credentials()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the password.
        /// </summary>
        public virtual string Password
        {
            get;
            private set;
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override bool Equals(Credentials other)
        {
            return other != null &&
                object.Equals(this.Password, other.Password);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Password.GetHashCode();
        }

        #endregion
    }
}
// <copyright file="FullName.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports

    using System;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// Represents a full name.
    /// </summary>
    public class FullName : ValueObject<FullName>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FullName"/> class.
        /// </summary>
        /// <param name="firstName">The first name</param>
        /// <param name="lastName">The last name</param>
        public FullName(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FullName"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected FullName()
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the first name.
        /// </summary>
        public string FirstName
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the last name.
        /// </summary>
        public string LastName
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns "full" name - {first name} {last name}
        /// </summary>
        public string AsFormattedName
        {
            get
            {
                return string.Join(" ", (this.FirstName + " " + this.LastName).Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override bool Equals(FullName other)
        {
            return (! string.IsNullOrWhiteSpace(this.FirstName)) &&
                   (! string.IsNullOrWhiteSpace(this.LastName)) &&
                   object.Equals(this.FirstName, other.FirstName) &&
                   object.Equals(this.LastName, other.LastName);
        }

        #endregion
    }
}
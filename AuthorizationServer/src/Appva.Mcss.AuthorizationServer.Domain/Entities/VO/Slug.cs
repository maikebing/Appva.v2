// <copyright file="Slug.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using Common.Domain;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// Represents a slug - a url friendly identifier.
    /// </summary>
    public class Slug : ValueObject<Slug>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Slug"/> class.
        /// </summary>
        /// <param name="str">The string to be converted to a slug</param>
        public Slug(string str)
        {
            this.Name = str.ToUrlFriendly();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Slug"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate</remarks>
        protected Slug()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the slug name.
        /// </summary>
        public virtual string Name
        {
            get;
            private set;
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override bool Equals(Slug other)
        {
            return other.IsNotNull() && this.Name.Equals(other.Name);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        #endregion
    }
}
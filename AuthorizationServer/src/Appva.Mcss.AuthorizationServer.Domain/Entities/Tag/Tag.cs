// <copyright file="Tag.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// A representation of a reusable tag.
    /// </summary>
    public class Tag : Entity<Tag>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        public Tag(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Tag"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Tag()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        #endregion
    }
}
// <copyright file="TaxonId.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using Appva.Mcss.Admin.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Serializable]
    public sealed class TaxonId : IReference<Guid>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonId"/> class.
        /// </summary>
        /// <param name="id">The ID.</param>
        public TaxonId(Guid id)
        {
            this.Value = id;
        }

        #endregion

        #region IReference<Guid> Members.

        /// <inheritdoc />
        public Guid Value
        {
            get;
            private set;
        }

        #endregion

        #region Constructors.

        /// <summary>
        /// Creates a new instance of the <see cref="TaxonId"/> class.
        /// </summary>
        /// <param name="id">The ID.</param>
        /// <returns>A new <see cref="TaxonId"/> instance.</returns>
        public static TaxonId New(Guid id)
        {
            return new TaxonId(id);
        }

        #endregion
    }
}
// <copyright file="PractitionerLocation.cs" company="Appva AB">
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
    public sealed class PractitionerLocation : ILocation
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerLocation"/> class.
        /// </summary>
        /// <param name="taxonId">
        /// The taxon ID reference.
        /// </param>
        /// <param name="isPreferred">
        /// Whether or not the location is the preferred location; optional, 
        /// defaults to <c>false</c>.
        /// </param>
        /// <param name="isLocked">
        /// Whether or not the location is locked for the practitioner; optional, 
        /// defaults to <c>false</c>.
        /// </param>
        public PractitionerLocation(IReference<Guid> taxonId, bool isPreferred = false, bool isLocked = false)
        {
            this.ConceptId     = taxonId;
            this.IsPreferred = isPreferred;
            this.IsLocked    = isLocked;
        }

        #endregion

        #region ILocation Members.

        /// <inheritdoc />
        public bool IsPreferred
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool IsLocked
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public IReference<Guid> ConceptId
        {
            get;
            private set;
        }

        #endregion

        #region Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="PractitionerLocation"/> class.
        /// </summary>
        /// <param name="taxonId">
        /// The taxon ID reference.
        /// </param>
        /// <param name="isPreferred">
        /// Whether or not the location is the preferred location; optional, 
        /// defaults to <c>false</c>.
        /// </param>
        /// <param name="isLocked">
        /// Whether or not the location is locked for the practitioner; optional, 
        /// defaults to <c>false</c>.
        /// </param>
        /// <returns>A new <see cref="PractitionerLocation"/> instance.</returns>
        public static PractitionerLocation New(IReference<Guid> taxonId, bool isPreferred = false, bool isLocked = false)
        {
            return new PractitionerLocation(taxonId, isPreferred, isLocked);
        }

        #endregion
    }
}
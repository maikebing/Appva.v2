// <copyright file="AuditLoggingConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class AuditLoggingConfiguration : ValueObject<AuditLoggingConfiguration>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditLoggingConfiguration"/> class.
        /// </summary>
        /// <param name="unitIds">The units</param>
        [JsonConstructor]
        private AuditLoggingConfiguration(IList<Guid> units)
        {
            this.Units = units;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The units.
        /// </summary>
        [JsonProperty]
        public IList<Guid> Units
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="AuditLoggingConfiguration"/> class.
        /// </summary>
        /// <param name="unitIds">The units</param>
        /// <returns>A new <see cref="AuditLoggingConfiguration"/> instance</returns>
        public static AuditLoggingConfiguration New(IList<Guid> units)
        {
            return new AuditLoggingConfiguration(units);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Units;
        }

        #endregion
    }
}
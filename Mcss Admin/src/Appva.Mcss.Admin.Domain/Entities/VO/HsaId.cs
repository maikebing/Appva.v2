// <copyright file="HsaId.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Core;

    #endregion

    /// <summary>
    /// Represents a HSA ID (SE{organizational}-{serial number for object}).
    /// </summary>
    public sealed class HsaId : Primitive, IValidator
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="HsaId"/> class.
        /// </summary>
        /// <param name="value">The initial value</param>
        public HsaId(string value)
            : base(value)
        {
        }

        #endregion

        #region IValidator Members.

        /// <inheritdoc />
        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(this.Value) ? false : this.Value.StartsWith("SE");
        }

        #endregion
    }
}
// <copyright file="HsaId.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;
    using Appva.Core;

    #endregion

    /// <summary>
    /// Represents a HSA ID (SE{organizational}-{serial number for object}).
    /// </summary>
    public sealed class HsaId : ValueObject<HsaId>, IValidator
    {
        #region Variables.

        /// <summary>
        /// The underlying value.
        /// </summary>
        private readonly string value;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HsaId"/> class.
        /// </summary>
        /// <param name="value">The initial value</param>
        public HsaId(string value)
        {
            this.value = value;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The value
        /// </summary>
        public string Value
        {
            get
            {
                return value;
            }
        }

        #endregion

        #region ValueObject overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Value;
        }

        /// <inheritdoc />
        public override bool Equals(HsaId other)
        {
            if (other == null)
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(this.Value) || string.IsNullOrWhiteSpace(other.Value))
            {
                return false;
            }
            return this.Value.Equals(other.Value);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
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
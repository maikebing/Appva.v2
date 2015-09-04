// <copyright file="TenantIdentifier.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Tenant.Identity
{
    /// <summary>
    /// A representation of a tenant ID.
    /// </summary>
    public interface ITenantIdentifier
    {
        /// <summary>
        /// The tenant ID value.
        /// </summary>
        string Value
        {
            get;
        }
    }

    /// <summary>
    /// A <see cref="ITenantIdentifier"/> implementation.
    /// </summary>
    public sealed class TenantIdentifier : ITenantIdentifier
    {
        #region Variables.

        /// <summary>
        /// The actual tenant ID.
        /// </summary>
        private readonly string value;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TenantIdentifier"/> class.
        /// </summary>
        /// <param name="value">The unique identification</param>
        public TenantIdentifier(string value)
        {
            this.value = value;
        }

        #endregion

        #region ITenantIdentifier Members.

        /// <inheritdoc /> 
        public string Value
        {
            get
            {
                return this.value;
            }
        }

        #endregion

        #region Object Overrides.

        /// <inheritdoc />
        public override string ToString()
        {
            return this.value ?? base.ToString();
        }

        #endregion
    }
}
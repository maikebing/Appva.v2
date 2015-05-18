// <copyright file="StrictTransportSecurityAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Security
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class StrictTransportSecurityAttribute : SecurityHeader
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="StrictTransportSecurityAttribute"/> 
        /// class.
        /// </summary>
        /// <param name="value">
        /// Optional value, defaults to max-age=31536000; includeSubDomains
        /// </param>
        public StrictTransportSecurityAttribute(string value = null)
            : base("Strict-Transport-Security", value)
        {
        }

        #endregion

        #region SecurityHeader Overrides.

        /// <inheritdoc />
        protected override string DefaultValue
        {
            get
            {
                return "max-age=31536000; includeSubDomains";
            }
        }

        #endregion
    }
}
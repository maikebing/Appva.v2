// <copyright file="FrameOptionsAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Security
{
    /// <summary>
    /// Improves the protection of web applications against clickjacking.
    /// <externalLink>
    ///     <linkText>HTTP Header Field X-Frame-Options</linkText>
    ///     <linkUri>http://tools.ietf.org/html/rfc7034</linkUri>
    /// </externalLink>
    /// This will be obsolete with 
    /// <externalLink>
    ///     <linkText>Content Security Policy Level 2</linkText>
    ///     <linkUri>http://www.w3.org/TR/CSP11/#frame-ancestors-and-frame-options</linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class FrameOptionsAttribute : SecurityHeader
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameOptionsAttribute"/> class.
        /// </summary>
        /// <param name="value">Optional value, defaults to SAMEORIGIN</param>
        /// <remarks>
        /// Must be one of DENY, SAMEORIGIN or ALLOW-FROM http://www.example.com
        /// </remarks>
        public FrameOptionsAttribute(string value = null)
            : base("X-Frame-Options", value)
        {
        }

        #endregion

        #region SecurityHeader Overrides.

        /// <inheritdoc />
        protected override string DefaultValue
        {
            get
            {
                return "SAMEORIGIN";
            }
        }

        #endregion
    }
}
// <copyright file="XssProtectionAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Security
{
    /// <summary>
    /// Cross-site scripting (XSS) filter header.
    /// See 
    /// <externalLink>
    ///     <linkText>HTTP header fields</linkText>
    ///     <linkUri>http://en.wikipedia.org/wiki/List_of_HTTP_header_fields#Common_non-standard_response_fields</linkUri>
    /// </externalLink>
    /// and 
    /// <externalLink>
    ///     <linkText>X-XSS-Protection</linkText>
    ///     <linkUri>http://blogs.msdn.com/b/ie/archive/2008/07/02/ie8-security-part-iv-the-xss-filter.aspx</linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class XssProtectionAttribute : SecurityHeader
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="XssProtectionAttribute"/> class.
        /// </summary>
        /// <param name="value">Optional value, defaults to 1; mode=block</param>
        public XssProtectionAttribute(string value = null)
            : base("X-XSS-Protection", value)
        {
        }

        #endregion

        #region SecurityHeader Overrides.

        /// <inheritdoc />
        protected override string DefaultValue
        {
            get
            {
                return "1; mode=block";
            }
        }

        #endregion
    }
}
// <copyright file="ContentSecurityPolicyAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Security
{
    /// <summary>
    /// Content Security Policy is a mechanism web applications can use to mitigate a 
    /// broad class of content injection vulnerabilities, such as cross-site scripting 
    /// (XSS). Content Security Policy is a declarative policy that lets the authors (or 
    /// server administrators) of a web application inform the client about the sources 
    /// from which the application expects to load resources. 
    /// To mitigate XSS attacks, for example, a web application can declare that it only 
    /// expects to load script from specific, trusted sources. This declaration allows 
    /// the client to detect and block malicious scripts injected into the application 
    /// by an attacker.
    /// <externalLink>
    ///     <linkText>Content Security Policy Level 2</linkText>
    ///     <linkUri>http://www.w3.org/TR/CSP11</linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class ContentSecurityPolicyAttribute : SecurityHeader
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSecurityPolicyAttribute"/> 
        /// class.
        /// </summary>
        /// <param name="value">Optional value, defaults to default-src 'self'</param>
        public ContentSecurityPolicyAttribute(string value = null)
            : base("Content-Security-Policy", value)
        {
        }

        #endregion

        #region SecurityHeader Overrides.

        /// <inheritdoc />
        protected override string DefaultValue
        {
            get
            {
                return "default-src 'self'";
            }
        }

        #endregion
    }
}
// <copyright file="ContentTypeOptionsAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Security
{
    /// <summary>
    /// Protects against MIME type confusion attacks.
    /// Content sniffing is a method browsers use to attempt to determine the 'real' content type 
    /// of a response by looking at the content itself, instead of the response header's 
    /// content-type value. By returning X-Content-Type-Options: nosniff, certain elements will 
    /// only load external resources if their content-type matches what is expected.
    /// </summary>
    public sealed class ContentTypeOptionsAttribute : SecurityHeader
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTypeOptionsAttribute"/> class.
        /// </summary>
        public ContentTypeOptionsAttribute()
            : base("X-Content-Type-Options", "nosniff")
        {
        }

        #endregion

        #region SecurityHeader Overrides.

        /// <inheritdoc />
        protected override string DefaultValue
        {
            get
            {
                return null;
            }
        }

        #endregion
    }
}
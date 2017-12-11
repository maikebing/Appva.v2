// <copyright file="Base64Binary.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using Appva.Core.Extensions;
    using Newtonsoft.Json;
    using Validation;

    #endregion

    /// <summary>
    /// Represents a base64 binary blob.
    /// </summary>
    public class Base64Binary : Primitive
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="Base64Binary"/> class.
        /// </summary>
        /// <param name="value">The base64 encoded string value.</param>
        private Base64Binary(string value)
            : base(value)
        {
            Requires.NotNullOrWhiteSpace(value, "value");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Base64Binary"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected Base64Binary()
        {
        }

        #endregion

        #region Public Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="Base64Binary"/> class.
        /// </summary>
        /// <param name="value">The base64 encoded string value.</param>
        /// <returns>A new <see cref="Base64Binary"/> instance.</returns>
        public static Base64Binary New<T>(T model) where T : class
        {
            Requires.NotNull(model, "model");
            var serialized = JsonConvert.SerializeObject(model);
            return new Base64Binary(serialized.ToBase64());
        }

        #endregion
    }
}
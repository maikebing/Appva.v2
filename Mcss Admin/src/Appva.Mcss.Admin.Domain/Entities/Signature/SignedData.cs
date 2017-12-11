// <copyright file="SignedData.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.VO;
    using Validation;

    #endregion

    /// <summary>
    /// Representation of data which is to be signed.
    /// </summary>
    public class SignedData : Entity
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedData"/> class.
        /// </summary>
        /// <param name="data">The base64 data which is signed.</param>
        public SignedData(Base64Binary data)
        {
            Requires.NotNull(data,                         "data");
            Requires.NotNullOrWhiteSpace(data.Value, "data.value");
            this.Data = data.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SignedData"/> class.
        /// </summary>
        /// <remarks>
        /// An NHibernate visible no-argument constructor.
        /// </remarks>
        protected SignedData()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The data.
        /// </summary>
        public virtual string Data
        {
            get;
            internal protected set;
        }

        /// <summary>
        /// The signature for the data.
        /// </summary>
        /// <remarks>NHibernate populated.</remarks>
        public virtual Signature Signature
        {
            get;
            internal protected set;
        }

        #endregion

        #region Static Builders.

        /// <summary>
        /// Creates a new instance of the <see cref="SignedData"/> class.
        /// </summary>
        /// <param name="data">The base64 data which is signed.</param>
        /// <returns>A new <see cref="SignedData"/> instance.</returns>
        public static SignedData New(Base64Binary data)
        {
            return new SignedData(data);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="SignedData"/> class.
        /// </summary>
        /// <param name="data">The base64 data which is signed.</param>
        /// <returns>A new <see cref="SignedData"/> instance.</returns>
        public static SignedData New<T>(T data) where T : class
        {
            return new SignedData(Base64Binary.New<T>(data));
        }

        #endregion
    }
}
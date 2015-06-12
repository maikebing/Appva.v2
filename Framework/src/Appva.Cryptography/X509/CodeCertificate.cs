// <copyright file="CodeCertificate.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using System;
    using System.Security.Cryptography.X509Certificates;
    using Core.Extensions;
    using Org.BouncyCastle.Asn1.X509;
    using Validation;

    #endregion

    /// <summary>
    /// Self signed code certificate generation. 
    /// </summary>
    public sealed class CodeCertificate : SelfSignedCertificate
    {
        #region Variables.

        /// <summary>
        /// The client certificate CA signer.
        /// </summary>
        private readonly X509Certificate2 signer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeCertificate"/> class.
        /// </summary>
        /// <param name="signer">The client certificate CA signer</param>
        public CodeCertificate(X509Certificate2 signer)
        {
            Requires.NotNull(signer, "signer");
            this.signer = signer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeCertificate"/> class.
        /// </summary>
        /// <param name="subjectName">The root cert distinguished subject name, e.g. CN=CARootTest</param>
        /// <param name="location">The location of the X.509 certificate store</param>
        /// <param name="store">The name of the X.509 certificate store to open</param>
        public CodeCertificate(
            string subjectName,
            StoreLocation location = StoreLocation.CurrentUser,
            StoreName store = StoreName.Root)
        {
            Requires.NotNullOrEmpty(subjectName, "subjectName");
            var value = subjectName.Contains("=") ? subjectName : "CN=" + subjectName;
            this.signer = CertificateUtils.LoadCertificate(
                X509FindType.FindBySubjectDistinguishedName,
                value,
                store,
                location);
            if (this.signer == null)
            {
                throw new InvalidOperationException(
                    "No trusted root found for {0} at store {1} and location {2}"
                    .FormatWith(value, store, location));
            }
        }

        #endregion

        #region SelfSignedCertificate Overrides.

        /// <inheritdoc />
        protected override X509Certificate2 CreateCertificate()
        {
            return CertificateUtils.CreateCertificate(
                Usage.Code,
                new X509Name(this.SubjectName),
                new Validity(this.NotBeforeDate, this.NotAfterDate, 2),
                this.signer,
                this.Cipher,
                this.SignatureAlgorithm);
        }

        #endregion
    }
}
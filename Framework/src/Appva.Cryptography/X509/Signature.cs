// <copyright file="Signature.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    /// <summary>
    /// The following are supported:
    /// <externalLink>
    ///     <linkText>Signature Algorithm Schemes</linkText>
    ///     <linkUri>
    ///         https://www.bouncycastle.org/specifications.html
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class Signature
    {
        #region Variables.

        /// <summary>
        /// The algorithm name.
        /// </summary>
        private readonly string algorithm;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Signature"/> class.
        /// </summary>
        /// <param name="algorithm">The algorithm to use</param>
        private Signature(string algorithm)
        {
            this.algorithm = algorithm;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// SHA 256 with ECDSA.
        /// </summary>
        public static Signature Sha256WithEcdsa
        {
            get
            {
                return new Signature("SHA256withECDSA");
            }
        }

        /// <summary>
        /// SHA 384 with ECDSA.
        /// </summary>
        public static Signature Sha384WithEcdsa
        {
            get
            {
                return new Signature("SHA384withECDSA");
            }
        }

        /// <summary>
        /// SHA 512 with ECDSA.
        /// </summary>
        public static Signature Sha512WithEcdsa
        {
            get
            {
                return new Signature("SHA512withECDSA");
            }
        }

        /// <summary>
        /// SHA 1 with RSA.
        /// </summary>
        public static Signature Sha1WithRsa
        {
            get
            {
                return new Signature("SHA1withRSA");
            }
        }

        /// <summary>
        /// SHA 224 with RSA.
        /// </summary>
        public static Signature Sha224WithRsa
        {
            get
            {
                return new Signature("SHA224withRSA");
            }
        }

        /// <summary>
        /// SHA 256 with RSA.
        /// </summary>
        public static Signature Sha256WithRsa
        {
            get
            {
                return new Signature("SHA256withRSA");
            }
        }

        /// <summary>
        /// SHA 384 with RSA.
        /// </summary>
        public static Signature Sha384WithRsa
        {
            get
            {
                return new Signature("SHA384withRSA");
            }
        }

        /// <summary>
        /// SHA 512 with RSA.
        /// </summary>
        public static Signature Sha512WithRsa
        {
            get
            {
                return new Signature("SHA512withRSA");
            }
        }

        /// <summary>
        /// Gets the algorithm name.
        /// </summary>
        public string Algorithm
        {
            get
            {
                return this.algorithm;
            }
        }

        #endregion
    }
}

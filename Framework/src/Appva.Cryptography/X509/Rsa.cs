// <copyright file="Rsa.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Generators;
    using Org.BouncyCastle.Crypto.Prng;
    using Org.BouncyCastle.Security;

    #endregion

    /// <summary>
    /// RSA algorithm key generation.
    /// <externalLink>
    ///     <linkText>RSA</linkText>
    ///     <linkUri>
    ///         http://en.wikipedia.org/wiki/RSA_(cryptosystem)
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    internal sealed class Rsa : ICipher
    {
        #region Variables.

        /// <summary>
        /// The <see cref="KeySize"/>.
        /// </summary>
        private readonly KeySize size;

        /// <summary>
        /// A <see cref="SecureRandom"/>
        /// </summary>
        private readonly SecureRandom random;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Rsa"/> class.
        /// </summary>
        /// <param name="size">The key size to use</param>
        internal Rsa(KeySize size)
        {
            this.size = size;
            this.random = new SecureRandom(new CryptoApiRandomGenerator());
        }

        #endregion

        #region ICipher Members.

        /// <inheritdoc />
        SecureRandom ICipher.Random
        {
            get
            {
                return this.random;
            }
        }

        /// <inheritdoc />
        IAsymmetricCipherKeyPairGenerator ICipher.CreateNew()
        {
            var generator = new RsaKeyPairGenerator();
            generator.Init(new KeyGenerationParameters(this.random, this.size.Value));
            return generator;
        }

        #endregion
    }
}

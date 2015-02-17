// <copyright file="Ecdh.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    #region Imports.

    using System;
    using Org.BouncyCastle.Asn1.Nist;
    using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Generators;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Crypto.Prng;
    using Org.BouncyCastle.Security;

    #endregion
    
    /// <summary>
    /// An anonymous key agreement protocol that allows two parties, each having an 
    /// elliptic curve public–private key pair, to establish a shared secret over an 
    /// insecure channel.
    /// <externalLink>
    ///     <linkText>Elliptic curve Diffie–Hellman</linkText>
    ///     <linkUri>
    ///         http://en.wikipedia.org/wiki/Elliptic_curve_Diffie%E2%80%93Hellman
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    internal sealed class Ecdh : ICipher
    {
        #region Variables.

        /// <summary>
        /// The <see cref="Curve"/>.
        /// </summary>
        private readonly Curve curve;

        /// <summary>
        /// A <see cref="SecureRandom"/>
        /// </summary>
        private readonly SecureRandom random;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Ecdh"/> class.
        /// </summary>
        /// <param name="curve">The curve to use</param>
        internal Ecdh(Curve curve)
        {
            this.curve = curve;
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
            var param = NistNamedCurves.GetByName(this.curve.Name);
            var generator = GeneratorUtilities.GetKeyPairGenerator("ECDH");
            generator.Init(new ECKeyGenerationParameters(
                new ECDomainParameters(param.Curve, param.G, param.N, param.H, param.GetSeed()),
                this.random));
            return generator;
        }

        #endregion
    }
}

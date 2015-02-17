// <copyright file="Curve.cs" company="Appva AB">
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
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Crypto.Prng;
    using Org.BouncyCastle.Security;

    #endregion

    /// <summary>
    /// A EC curve.
    /// <externalLink>
    ///     <linkText>Supported Curves</linkText>
    ///     <linkUri>http://www.bouncycastle.org/wiki/pages/viewpage.action?pageId=362269</linkUri>
    /// </externalLink>
    /// </summary>
    public sealed class Curve
    {
        #region Variables.

        /// <summary>
        /// The EC curve as string.
        /// </summary>
        private string curve;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Curve"/> class.
        /// </summary>
        /// <param name="curve">The curve to use</param>
        private Curve(string curve)
        {
            this.curve = curve;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// NIST 224 bit size.
        /// </summary>
        /// <returns>A <see cref="Curve"/></returns>
        public static Curve P224
        {
            get
            {
                return new Curve("P-224");
            }
        }

        /// <summary>
        /// NIST 256 bit size.
        /// </summary>
        /// <returns>A <see cref="Curve"/></returns>
        public static Curve P256
        {
            get
            {
                return new Curve("P-256");
            }
        }

        /// <summary>
        /// NIST 384 bit size.
        /// </summary>
        /// <returns>A <see cref="Curve"/></returns>
        public static Curve P384
        {
            get
            {
                return new Curve("P-384");
            }
        }

        /// <summary>
        /// NIST 521 bit size.
        /// </summary>
        /// <returns>A <see cref="Curve"/></returns>
        public static Curve P521
        {
            get
            {
                return new Curve("P-521");
            }
        }

        /// <summary>
        /// Returns the curve name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.curve;
            }
        }

        #endregion
    }
}

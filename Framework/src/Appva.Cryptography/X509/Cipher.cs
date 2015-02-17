// <copyright file="Cipher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    /// <summary>
    /// Asymmetric key pair cipher. 
    /// </summary>
    public static class Cipher
    {
        /// <summary>
        /// EC key pair generator.
        /// </summary>
        /// <param name="curve">The <see cref="Curve"/> to use</param>
        /// <returns><see cref="ICipher"/></returns>
        public static ICipher Ecdh(Curve curve)
        {
            return new Ecdh(curve);
        }

        /// <summary>
        /// An RSA key pair generator.
        /// </summary>
        /// <param name="size">The <see cref="KeySize"/> to use</param>
        /// <returns><see cref="ICipher"/></returns>
        public static ICipher Rsa(KeySize size)
        {
            return new Rsa(size);
        }
    }
}

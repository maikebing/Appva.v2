// <copyright file="IDataProtector.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.DataProtection
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Represents an object that can perform cryptographic operations.
    /// </summary>
    public interface IDataProtector : IDisposable
    {
        /// <summary>
        /// Cryptographically protects some input data.
        /// </summary>
        /// <param name="unprotectedData">
        /// The data to be protected
        /// </param>
        /// <returns>
        /// An array containing cryptographically protected data
        /// </returns>
        /// <remarks>
        /// To retrieve the original data, call Unprotect on the protected data
        /// </remarks>
        byte[] Protect(byte[] unprotectedData);

        /// <summary>
        /// Retrieves the original data that was protected by a call to Protect.
        /// </summary>
        /// <param name="protectedData">
        /// The protected data to be decrypted
        /// </param>
        /// <returns>
        /// The original data
        /// </returns>
        /// <exception cref="System.Security.Cryptography.CryptographicException">
        /// If the <em>protectedData</em> parameter has been tampered with
        /// </exception>
        byte[] Unprotect(byte[] protectedData);
    }
}

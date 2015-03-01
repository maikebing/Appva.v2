// <copyright file="ProtectedDataProtector.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.DataProtection
{
    #region Imports.

    using System.Security.Cryptography;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// This class provides access to the Data Protection API (DPAPI) available in 
    /// Microsoft Windows 2000 and later operating systems. This is a service that is 
    /// provided by the operating system and does not require additional libraries. 
    /// <para>
    /// It provides protection using the user or machine credentials to encrypt or 
    /// decrypt data. The class consists of two wrappers for the unmanaged DPAPI, Protect 
    /// and Unprotect. These two methods can be used to encrypt and decrypt data such as 
    /// passwords, keys, and connection strings.
    /// </para>
    /// </summary>
    public sealed class ProtectedDataProtector : IDataProtector
    {
        #region Variables.

        /// <summary>
        /// One of the enumeration values that specifies the scope of the data protection 
        /// (either the current user or the local machine). The default is CurrentUser.
        /// </summary>
        private readonly DataProtectionScope scope;
        
        /// <summary>
        /// An additional byte array used to increase the complexity of the encryption, or 
        /// null for no additional complexity.
        /// </summary>
        private readonly byte[] entropy;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectedDataProtector"/> class.
        /// </summary>
        /// <param name="scope">The scope of the data</param>
        /// <param name="purpose">Purpose for the data</param>
        public ProtectedDataProtector(DataProtectionScope scope, string purpose)
            : this(scope, purpose.ToUtf8Bytes())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectedDataProtector"/> class.
        /// </summary>
        /// <param name="scope">The scope of the data</param>
        /// <param name="entropy">Entropy for the data</param>
        private ProtectedDataProtector(DataProtectionScope scope, byte[] entropy)
        {
            this.scope = scope;
            this.entropy = entropy;
        }

        #endregion

        #region IDataProtector Members.

        /// <inheritdoc />
        public byte[] Protect(byte[] unprotectedData)
        {
            return ProtectedData.Protect(unprotectedData, this.entropy, this.scope);
        }

        /// <inheritdoc />
        public byte[] Unprotect(byte[] protectedData)
        {
            return ProtectedData.Unprotect(protectedData, this.entropy, this.scope);
        }

        #endregion

        #region IDisposable Members.

        /// <inheritdoc />
        public void Dispose()
        {
            //// No op.
        }

        #endregion
    }
}

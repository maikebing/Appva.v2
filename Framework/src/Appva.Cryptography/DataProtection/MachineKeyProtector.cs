// <copyright file="MachineKeyProtector.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Cryptography.DataProtection
{
    #region Imports.

    using System.Web.Security;

    #endregion

    /// <summary>
    /// Provides a way to encrypt or hash data (or both) by using the same algorithms 
    /// and key values that are used for ASP.NET forms authentication and view state.
    /// </summary>
    public sealed class MachineKeyProtector : IDataProtector
    {
        #region Variables.

        /// <summary>
        /// A list of purposes for the data. If this value is specified, 
        /// the same list must be passed to the Unprotect method in order to 
        /// decipher the returned ciphertext.
        /// </summary>
        private readonly string[] purposes;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineKeyProtector"/> class.
        /// </summary>
        /// <param name="purpose">Purpose for the data</param>
        public MachineKeyProtector(string purpose)
        {
            this.purposes = new[] { purpose };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MachineKeyProtector"/> class.
        /// </summary>
        /// <param name="purposes">A list of purposes for the data</param>
        public MachineKeyProtector(string[] purposes)
        {
            this.purposes = purposes;
        }

        #endregion

        #region IDataProtector Members.

        /// <inheritdoc />
        public byte[] Protect(byte[] unprotectedData)
        {
            return MachineKey.Protect(unprotectedData, this.purposes);
        }

        /// <inheritdoc />
        public byte[] Unprotect(byte[] protectedData)
        {
            return MachineKey.Unprotect(protectedData, this.purposes);
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
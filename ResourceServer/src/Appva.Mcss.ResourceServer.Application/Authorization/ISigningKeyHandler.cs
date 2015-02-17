// <copyright file="ISigningKeyHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Application.Authorization
{
    #region Imports.

    using System.Security.Cryptography;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISigningKeyHandler
    {
        /// <summary>
        /// Returns the <see cref="RSACryptoServiceProvider"/>.
        /// </summary>
        RSACryptoServiceProvider Provider
        {
            get;
        }
    }
}
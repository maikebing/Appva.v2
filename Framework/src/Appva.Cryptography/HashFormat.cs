// <copyright file="HashFormat.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography
{
    /// <summary>
    /// HashFormats for comparing hashes.
    /// </summary>
    public enum HashFormat
    {
        /// <summary>
        /// Applies no extra hash processing.
        /// </summary>
        None,

        /// <summary>
        /// Converts the hash to a base64 string.
        /// </summary>
        Base64
    }
}
// <copyright file="Usage.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Cryptography.X509
{
    /// <summary>
    /// Certificate usage.
    /// </summary>
    public enum Usage
    {
        /// <summary>
        /// Certificate for a root Certification Authority (CA).
        /// </summary>
        CertificateAuthority,

        /// <summary>
        /// SSL/TLS Server certificate.
        /// </summary>
        Server,

        /// <summary>
        /// SSL/TLS Client certificate.
        /// </summary>
        Client,

        /// <summary>
        /// Code certificate used to sign applications.
        /// </summary>
        Code
    }
}

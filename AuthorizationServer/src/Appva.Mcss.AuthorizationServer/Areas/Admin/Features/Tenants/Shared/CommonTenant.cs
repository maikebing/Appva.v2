// <copyright file="CommonTenant.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using Appva.Cqrs;

    #endregion

    public class CommonTenant<T> : CommonTenant, IRequest<T> where T : class
    {
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    /// <typeparam name="T">The response class</typeparam>
    public class CommonTenant : Idg
    {
        /// <summary>
        /// The tenant identifier.
        /// </summary>
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant host name.
        /// </summary>
        public string HostName
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The tenant database connection.
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }
    }
}
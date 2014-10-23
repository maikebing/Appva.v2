// <copyright file="CreateTenant.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System.Web;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class CreateTenant : Identity, IRequest<DetailsTenantId>
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

        /// <summary>
        /// The tenant logotype if any.
        /// </summary>
        public HttpPostedFileBase Logotype
        {
            get;
            set;
        }
    }
}
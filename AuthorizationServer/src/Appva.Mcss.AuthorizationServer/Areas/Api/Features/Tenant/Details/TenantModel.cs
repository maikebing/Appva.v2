// <copyright file="TenantModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Api.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Models;

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class TenantModel : IdentityAndSlug
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
        /// The tenant secret.
        /// </summary>
        public string Secret
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
        /// The tenant logotype.
        /// </summary>
        public Logotype Logotype
        {
            get;
            set;
        }
    }
}
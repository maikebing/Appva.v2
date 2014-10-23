// <copyright file="DetailsTenant.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class DetailsTenant : IdentityAndSlug
    {
        /// <summary>
        /// Whether the tenant is active or not.
        /// </summary>
        public bool IsActive
        {
            get;
            set;
        }

        /// <summary>
        /// Resource meta.
        /// </summary>
        public MetaData Resource
        {
            get;
            set;
        }

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
        /// The tenant logotype.
        /// </summary>
        public Logotype Logotype
        {
            get;
            set;
        }
    }
}
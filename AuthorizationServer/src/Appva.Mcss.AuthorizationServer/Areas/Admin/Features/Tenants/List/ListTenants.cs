// <copyright file="ListTenants.cs" company="Appva AB">
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
    public class ListTenants : IdentityAndSlug
    {
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
        /// The tenant avatar.
        /// </summary>
        public Logotype Logotype
        {
            get;
            set;
        }
    }
}
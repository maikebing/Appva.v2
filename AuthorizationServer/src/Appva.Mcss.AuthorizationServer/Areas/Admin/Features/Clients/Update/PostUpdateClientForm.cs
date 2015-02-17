// <copyright file="PostUpdateClientForm.cs" company="Appva AB">
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
    using System.Web;
    using Appva.Mcss.AuthorizationServer.Common;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PostUpdateClientForm : CommonClient<Id<DetailsClient>>
    {
        /// <summary>
        /// The client password.
        /// </summary>
        public string Password
        {
            get;
            set;
        }

        /// <summary>
        /// The client secret.
        /// </summary>
        public string Secret
        {
            get;
            set;
        }

        /// <summary>
        /// The client logotype.
        /// </summary>
        public HttpPostedFileBase Logotype
        {
            get;
            set;
        }
    }
}
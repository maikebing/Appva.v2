// <copyright file="UpdateClient.cs" company="Appva AB">
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
    public sealed class UpdateClient : CreateClient
    {
        /// <summary>
        /// The client logotype.
        /// </summary>
        public string ImageFileName
        {
            get;
            set;
        }

        /// <summary>
        /// The client logotype mime type.
        /// </summary>
        public string MimeType
        {
            get;
            set;
        }
    }
}
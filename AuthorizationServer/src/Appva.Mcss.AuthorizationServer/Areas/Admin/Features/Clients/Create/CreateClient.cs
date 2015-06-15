// <copyright file="CreateClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System.Collections.Generic;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mvc;

    #endregion

    /// <summary>
    /// Create client command model.
    /// </summary>
    public class CreateClient : CommonClient<Id<DetailsClient>>
    {
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
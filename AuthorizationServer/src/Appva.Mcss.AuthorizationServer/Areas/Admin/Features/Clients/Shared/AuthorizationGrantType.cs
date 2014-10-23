// <copyright file="AuthorizationGrantType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public enum AuthorizationGrantType
    {
        [Display(Name = "Authorization Code")]
        AuthorizationCode,
        [Display(Name = "Implicit")]
        Implicit,
        [Display(Name = "Resource Owner Password Credentials")]
        ResourceOwnerPasswordCredentials,
        [Display(Name = "Client Credentials")]
        ClientCredentials
    }
}
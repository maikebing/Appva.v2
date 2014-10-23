// <copyright file="IdentityAndSlug.cs" company="Appva AB">
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
    /// Base class with an identity and a slug.
    /// </summary>
    public abstract class IdentityAndSlug : Identity
    {
        /// <summary>
        /// The slug.
        /// </summary>
        public string Slug
        {
            get;
            set;
        }
    }
}
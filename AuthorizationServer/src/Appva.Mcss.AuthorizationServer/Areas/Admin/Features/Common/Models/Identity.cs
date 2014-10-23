// <copyright file="Identity.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Base class with an identity.
    /// </summary>
    public abstract class Identity
    {
        /// <summary>
        /// The identity.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }
    }
}
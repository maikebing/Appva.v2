// <copyright file="IProxyRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories.Contracts
{
    #region Imports.

    using Appva.Common.Domain;
    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IProxyRepository<T> where T : Entity<T>
    {
        /// <summary>
        /// Creates a proxy from the guid
        /// </summary>
        /// <param name="id">The Guid</param>
        /// <returns></returns>
        T Load(Guid id);
    }
}
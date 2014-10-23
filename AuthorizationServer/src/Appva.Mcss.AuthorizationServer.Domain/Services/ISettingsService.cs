// <copyright file="ISettingsService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        TValue Get<TValue>(string key, object defaultValue = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setting"></param>
        /// <returns>True if the <see cref="Setting"/> is successfully cached</returns>
        bool Add(Setting setting);
    }
}
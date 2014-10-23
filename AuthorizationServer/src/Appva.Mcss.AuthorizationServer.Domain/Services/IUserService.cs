// <copyright file="IUserService.cs" company="Appva AB">
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
    public interface IUserService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="personalIdentityNumber"></param>
        /// <param name="password"></param>
        /// <param name="authenticationMethod"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        bool AuthenticateWithPersonalIdentityNumber(string personalIdentityNumber, string password, string authenticationMethod, out User user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personalIdentityNumber"></param>
        /// <returns></returns>
        User UserByPersonalIdentityNumber(string personalIdentityNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authenticationMethod"></param>
        /// <returns></returns>
        UserAuthentication UserAuthentication(User user, string authenticationMethod);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool IsPasswordExpired(User user);
    }
}
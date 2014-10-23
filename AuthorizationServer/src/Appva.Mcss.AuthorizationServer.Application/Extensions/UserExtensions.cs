// <copyright file="UserExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Application
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public static class UserExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //public static bool IsNew(this User user)
        //{
        //    return user.LastLogin == null;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsNotActive(this User user)
        {
            return ! user.IsActive;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsVerified(this User user)
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsNotVerified(this User user)
        {
            return ! user.IsVerified();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsBlocked(this User user)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsNotBlocked(this User user)
        {
            return ! user.IsBlocked();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> GetIdentificationClaims(this User user)
        {
            return new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString("D")),
                    new Claim(ClaimTypes.Name, user.FullName.AsFormattedName)
                    //new Claim(ClaimTypes.Tenant, user.Tenant)
                };
        }
    }
}
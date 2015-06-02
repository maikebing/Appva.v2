// <copyright file="AccountUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Appva.Core.Resources;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class AccountUtils
    {
        /// <summary>
        /// If the Account is in a specific role.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="role"></param>
        public static bool IsInRole(Account account, Role role)
        {
            return IsInRole(account, role.MachineName);
        }

        /// <summary>
        /// If the Account is in a specific role.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="role"></param>
        public static bool IsInRole(Account account, string role)
        {
            return account.Roles.Any(x => x.MachineName.Equals(role));
        }

        /// <summary>
        /// If true then the Account has access to the device area.
        /// </summary>
        /// <param name="account"></param>
        public static bool IsDeviceAccount(Account account)
        {
            return IsInRole(account, RoleTypes.Device);
        }

        /// <summary>
        /// If true then the Account has access to the web area.
        /// </summary>
        /// <param name="account"></param>
        public static bool IsBackendAccount(Account account)
        {
            return IsInRole(account, RoleTypes.Backend);
        }

        /// <summary>
        /// If true then the Account has access to the admin area.
        /// </summary>
        /// <param name="account"></param>
        public static bool IsSuperAdministrator(Account account)
        {
            return IsInRole(account, RoleTypes.SuperAdmin);
        }

        /// <summary>
        /// Filters a list of accounts by bottom up.
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="path"></param>
        public static List<Account> FilterByPathBottomUp(List<Account> accounts, string path)
        {
            if (path.IsEmpty())
            {
                return accounts;
            }
            var retval = new List<Account>();
            var iterations = path.Split('.').Count();
            if (iterations == 0)
            {
                iterations = 1;
            }
            for (var i = 0; i < iterations; i++)
            {
                foreach (var account in accounts)
                {
                    if (account.Taxon.Path.StartsWith(path))
                    {
                        if (!retval.Contains(account))
                        {
                            retval.Add(account);
                        }
                    }
                }
                var index = path.LastIndexOf(".");
                if (index > -1)
                {
                    path = path.Remove(index);
                }
            }
            if (retval.Count > 0)
            {
                return retval;
            }
            return accounts;
        }

        /// <summary>
        /// Returns a filtered collections of <see cref="Account"/> by nodes steps including root.
        /// </summary>
        /// <param name="accounts"></param>
        /// <param name="path"></param>
        /// <param name="nodesFromRoot"></param>
        public static List<Account> FilterByPath(List<Account> accounts, string path, int steps)
        {
            if (path.IsNotNull())
            {
                var paths = path.Split('.');
                var count = paths.Count();
                if (count > 0)
                {
                    if (steps >= count)
                    {
                        steps = count;
                    }
                    var temp = new List<string>();
                    for (var i = 0; i < steps; i++)
                    {
                        temp.Add(paths[i]);
                    }
                    path = string.Join(".", temp.ToArray());
                    var retval = new List<Account>();
                    foreach (var account in accounts)
                    {
                        if (account.Taxon.Path.StartsWith(path))
                        {
                            if (!retval.Contains(account))
                            {
                                retval.Add(account);
                            }
                        }
                    }
                    if (retval.Count > 0)
                    {
                        return retval;
                    }
                }
            }
            return accounts;
        }



        /// <summary>
        /// Creates a unique username from firstname and lastname
        /// </summary>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="usernames"></param>
        /// <returns></returns>
        public static string CreateUserName(string firstname, string lastname, IList<string> usernames)
        {
            var number = "";
            Regex regex = new Regex("[^a-zåäöA-ZÅÄÖ]");
            Regex toO = new Regex("[öÖ]");
            Regex toA = new Regex("[åäÅÄ]");
            firstname = regex.Replace(firstname, "");
            lastname = regex.Replace(lastname, "");
            if (firstname.Length > 3)
            {
                firstname = firstname.Substring(0, 3);
                firstname = toA.Replace(firstname, "a");
                firstname = toO.Replace(firstname, "o");
            }
            if (lastname.Length > 3)
            {
                lastname = lastname.Substring(0, 3);
                lastname = toA.Replace(lastname, "a");
                lastname = toO.Replace(lastname, "o");
            }
            firstname = firstname.ToLower();
            lastname = lastname.ToLower();
            if (usernames.Contains(string.Format("{0}{1}", firstname, lastname)))
            {
                var counter = 1;
                while (usernames.Contains(string.Format("{0}{1}{2}", firstname, lastname, counter)))
                {
                    counter++;
                }
                number = counter.ToString();
            }
            return string.Format("{0}{1}{2}", firstname, lastname, number);
        }

        public static string GenerateClientPassword(Random rand = null, int length = 4)
        {
            Random randGen = rand != null ? rand : new Random();
            var retval = string.Empty;
            for (int i = 0; i < length; i++)
            {
                retval = string.Format("{0}{1}", randGen.Next(9), retval);
            }
            return retval;
        }

    }
}
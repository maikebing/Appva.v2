// <copyright file="Defaults.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Resources;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class Defaults
    {
        /// <summary>
        /// A static account ID.
        /// </summary>
        public static readonly Guid AccountId = new Guid("8364a38a-1280-4045-8907-7cb3e0e4cdda");

        /// <summary>
        /// Creates and returns basic roles.
        /// </summary>
        /// <returns>A collection of <see cref="Setting"/></returns>
        public static IList<Role> CreateRoles()
        {
            return new List<Role>
            {
                Role.New(RoleTypes.AdministrativePersonnel, "Administrative personnel", null, 1, false, false),
                Role.New(RoleTypes.Appva,                   "God Account",              null, 0, false, false),
                Role.New(RoleTypes.Assistant,               "Assistant Nurse",          null, 2),
                Role.New(RoleTypes.Backend,                 "Internal Admin Access",    null, 3, false, false),
                Role.New(RoleTypes.Developer,               "Developer",                null, 4, false, false),
                Role.New(RoleTypes.Device,                  "Internal Device Access",   null, 5, false, false),
                Role.New(RoleTypes.Dietician,               "Dietician",                null, 6),
                Role.New(RoleTypes.Nurse,                   "Registered Nurse",         null, 7),
                Role.New(RoleTypes.OccupationalTherapist,   "Occupational Therapist",   null, 8),
                Role.New(RoleTypes.Physiotherapist,         "Physiotherapist",          null, 9)
            };
        }

        /// <summary>
        /// Creates the most common taxonomies.
        /// </summary>
        /// <returns>A collection of <see cref="Taxonomy"/></returns>
        public static IList<Taxonomy> CreateTaxonomies()
        {
            return new List<Taxonomy>
            {
                Taxonomy.New("ORG", "Organization", "The organizational structure", 0),
                Taxonomy.New("SST", "Statuses",     "The statuses of tasks",        1)
            };
        }

        /// <summary>
        /// Creates multiple taxa.
        /// </summary>
        /// <param name="taxonomies">The collection of taxonomies for which taxas will be produced</param>
        /// <param name="generate">The amount of organizational taxa to create, defaults to 10</param>
        /// <returns>A collection of <see cref="Taxon"/></returns>
        public static IList<Taxon> CreateTaxa(IList<Taxonomy> taxonomies, int generate = 10)
        {
            var taxa = new List<Taxon>();
            foreach (var taxonomy in taxonomies)
            {
                if (taxonomy.MachineName == "ORG")
                {
                    var root = Taxon.New(taxonomy, "root", "A root object", null);
                    taxa.Add(root);
                    for (var i = 0; i < generate; i++)
                    {
                        taxa.Add(Taxon.New(taxonomy, string.Format("Generated sub object {0}", i + 1), "A sub object", null, root, i + 1));
                    }
                }
                if (taxonomy.MachineName == "SST")
                {
                    taxa.Add(Taxon.New(taxonomy, "Not administered", null, null, null, 0));
                    taxa.Add(Taxon.New(taxonomy, "Unable to administer", null, null, null, 0));
                    taxa.Add(Taxon.New(taxonomy, "Partially administered", null, null, null, 0));
                    taxa.Add(Taxon.New(taxonomy, "Disposed", null, null, null, 0));
                }
            }
            return taxa;
        }

        /// <summary>
        /// Creates a collection of permissions.
        /// </summary>
        /// <returns>A collection of <see cref="Permission"/></returns>
        public static IList<Permission> CreatePermissions()
        {
            return new List<Permission>
            {
                new Permission(Permissions.Admin.Login.Key,  null, Permissions.Admin.Login.Value),
                new Permission(Permissions.Device.Login.Key, null, Permissions.Device.Login.Value)
            };
        }

        /// <summary>
        /// Creates and returns basic settings.
        /// </summary>
        /// <returns>A collection of <see cref="Setting"/></returns>
        public static IList<Setting> CreateSettings()
        {
            return new List<Setting>
            {
                Setting.New(
                    ApplicationSettings.MailMessagingConfiguration.Key, 
                    ApplicationSettings.MailMessagingConfiguration.Namespace,
                    ApplicationSettings.MailMessagingConfiguration.Name,
                    ApplicationSettings.MailMessagingConfiguration.Description, 
                    JsonConvert.SerializeObject(SecurityMailerConfiguration.New(false, false, false, false, false, null)), 
                    typeof(string))
            };
        }

        /// <summary>
        /// Creates a principal account.
        /// </summary>
        /// <param name="roles">A collection of roles</param>
        /// <returns>An <see cref="Account"/></returns>
        public static Account CreatePrincipalAccount(IList<Role> roles)
        {
            var account       = Account.CreateForTest(AccountId);
            account.Roles     = roles;
            account.FirstName = "John";
            account.LastName  = "Doe";
            account.FullName  = "John Doe";
            account.PersonalIdentityNumber = new PersonalIdentityNumber("19020101-0101");
            account.SymmetricKey = "x";
            return account;
        }
    }
}
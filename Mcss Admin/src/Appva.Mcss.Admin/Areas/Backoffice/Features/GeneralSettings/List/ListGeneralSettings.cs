// <copyright file = "ListGeneralSettingsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//      <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Cqrs;
    using Domain.VO;
    using Ldap.Configuration;

    #endregion

    public class ListGeneralSettings
    {
        public Guid Id
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string MachineName
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Value
        {
            get;
            set;
        }

        public string Category
        {
            get;
            set;
        }

        public Type Type
        {
            get;
            set;
        }

        public string CategoryColorCode
        {
            get;
            set;
        }

        public int? IntValue
        {
            get;
            set;
        }

        public string StringValue
        {
            get;
            set;
        }

        public bool? BoolValue
        {
            get;
            set;
        }

        public bool IsJson
        {
            get;
            set;
        }

        public IList<double> ListValues
        {
            get;
            set;
        }

        public PdfLookAndFeel PdfLookAndFeel
        {
            get;
            set;
        }

        public string[] machineNames { get; set; }
          

        public AuditLoggingConfiguration AuditLoggingConfig { get; set; }

        public SecurityTokenConfiguration SecurityTokenConfig { get; set; }

        public SecurityMailerConfiguration SecurityMailerConfig { get; set; }

        public LdapConfiguration LdapConfig { get; set; }

        public static string ToCategoryString(string s, int max)
        {
            var array = s.Split('.');
            string category = "";

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].ToLower() != "mcss")
                {
                    category += array[i] + (i > 0 && i < array.Length - 1 ? " / " : "");
                }
            }

            if (category.Length > max)
            {
                category = category.Substring(category.Length - max).Trim();
                int index = category.IndexOf(' ');
                category = category.Contains("/") && category[0] != '/' ? category.Remove(0, index) : category;
            }

            return category;
        }
    }
}
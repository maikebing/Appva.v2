// <copyright file = "ListGeneralSettings.cs" company="Appva AB">
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
        /// <summary>
        /// The setting Id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The setting Index
        /// </summary>
        public int Index
        {
            get;
            set;
        }

        /// <summary>
        /// The setting name
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The setting machine name
        /// </summary>
        public string MachineName
        {
            get;
            set;
        }

        /// <summary>
        /// The setting description
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The setting value
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// The setting category
        /// </summary>
        public string Category
        {
            get;
            set;
        }

        /// <summary>
        /// The subcategory of a category
        /// </summary>
        public string SubCategory
        {
            get;
            set;
        }

        /// <summary>
        /// The setting type
        /// </summary>
        public Type Type
        {
            get;
            set;
        }

        /// <summary>
        /// The categorycolor
        /// </summary>
        public string CategoryColor
        {
            get;
            set;
        }

        /// <summary>
        /// The category start
        /// </summary>
        public string CategoryStartHtml
        {
            get;
            set;
        }

        /// <summary>
        /// The category end
        /// </summary>
        public string CategoryEndHtml
        {
            get;
            set;
        }

        /// <summary>
        /// If the setting is Json or not
        /// </summary>
        public bool IsJson
        {
            get;
            set;
        }
        
        /// <summary>
        /// JSON object
        /// </summary>
        public PdfLookAndFeel PdfLookAndFeel
        {
            get;
            set;
        }

        /// <summary>
        /// JSON object
        /// </summary>
        public SecurityTokenConfiguration SecurityTokenConfig
        {
            get;
            set;
        }

        /// <summary>
        /// JSON object
        /// </summary>
        public SecurityMailerConfiguration SecurityMailerConfig
        {
            get;
            set;
        }
    }
}
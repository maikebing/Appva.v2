// <copyright file="ListSetting.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Area51.Cache
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListSetting
    {
        /// <summary>
        /// The setting entry id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The setting name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The setting description.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The setting key.
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// The setting value.
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// The setting namespace.
        /// </summary>
        public string Namespace
        {
            get;
            set;
        }
    }
}
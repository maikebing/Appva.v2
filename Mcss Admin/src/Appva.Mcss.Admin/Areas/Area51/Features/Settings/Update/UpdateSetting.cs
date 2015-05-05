// <copyright file="CreateSetting.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Settings.Create
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class UpdateSetting : IRequest<bool>
    {
        /// <summary>
        /// The Name.
        /// </summary>
        [Required]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The description.
        /// </summary>
        [Required]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The internal system name.
        /// </summary>
        [Required]
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// The value or a JSON object.
        /// </summary>
        [Required]
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// The section namespace.
        /// </summary>
        [Required]
        public string Namespace
        {
            get;
            set;
        }

        /// <summary>
        /// The type.
        /// </summary>
        [Required]
        public string Type
        {
            get;
            set;
        }
    }
}
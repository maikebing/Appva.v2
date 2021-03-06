﻿// <copyright file="EditSignatureModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class EditSignatureModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The signing id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The signing name
        /// </summary>
        [DisplayName("Beskrivning")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The image path
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Check if it's a root node
        /// </summary>
        public bool IsRoot
        {
            get;
            set;
        }

        /// <summary>
        /// Submit button value
        /// </summary>
        public string Submit
        {
            get;
            set;
        }

        /// <summary>
        /// The image id and path
        /// </summary>
        public Dictionary<string, string> Images
        {
            get;
            set;
        }

        #endregion
    }
}
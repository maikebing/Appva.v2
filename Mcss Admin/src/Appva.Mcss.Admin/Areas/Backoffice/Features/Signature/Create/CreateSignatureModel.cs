// <copyright file="CreateSignatureModel.cs" company="Appva AB">
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
    public class CreateSignatureModel : IRequest<bool>
    {
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
        /// The signing id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// Check it it's a root node
        /// </summary>
        public bool IsRoot
        {
            get;
            set;
        }

        /// <summary>
        /// Id and path of images
        /// </summary>
        public Dictionary<string, string> Images
        {
            get;
            set;
        }
    }
}
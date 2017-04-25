// <copyright file="EditDeviceModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class EditDeviceModel
    {
        #region Properties.

        /// <summary>
        /// The device guid.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The device name.
        /// </summary>
        public string Description
        {
            get;
            set;
        }

        #endregion
    }
}
// <copyright file="DeviceDetailsModel.cs" company="Appva AB">
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

    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    public class DeviceDetailsModel
    {
        /// <summary>
        /// The device.
        /// </summary>
        public Device Device
        {
            get; set;
        }
    }
}
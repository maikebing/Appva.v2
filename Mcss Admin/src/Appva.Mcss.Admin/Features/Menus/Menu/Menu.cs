// <copyright file="Menu.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Menus
{
    #region Imports.

    using System;
    using System.Collections.Generic;
using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class Menu : IRequest<IMenuList<IMenuItem>>
    {
        /// <summary>
        /// The menu key.
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// The current action route.
        /// </summary>
        public string Action
        {
            get;
            set;
        }

        /// <summary>
        /// The current controller route.
        /// </summary>
        public string Controller
        {
            get;
            set;
        }

        /// <summary>
        /// The current area route.
        /// </summary>
        public string Area
        {
            get;
            set;
        }
    }
}
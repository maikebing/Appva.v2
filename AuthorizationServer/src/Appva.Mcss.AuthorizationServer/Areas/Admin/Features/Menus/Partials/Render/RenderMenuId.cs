// <copyright file="RenderMenuId.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.AuthorizationServer.Domain.Services;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class RenderMenuId : IRequest<IEnumerable<IMenuNode>>
    {
        /// <summary>
        /// The menu key. 
        /// E.g. top_header_menu, side_bar_menu.
        /// </summary>
        public string MenuKey
        {
            get;
            set;
        }
        /// <summary>
        /// The current url.
        /// </summary>
        public Uri Uri
        {
            get;
            set;
        }
    }
}
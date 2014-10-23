// <copyright file="MenuNode.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Services
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface IMenuNode
    {
        /// <summary>
        /// The menu label.
        /// </summary>
        string Label
        {
            get;
        }

        /// <summary>
        /// The menu link URL.
        /// E.g. /controller/action
        /// </summary>
        string Url
        {
            get;
        }

        /// <summary>
        /// Whether the menu link is selected.
        /// </summary>
        bool IsSelected
        {
            get;
        }

        /// <summary>
        /// The menu link CSS class, set on the LI element.
        /// E.g. "special-menu-link-item".
        /// </summary>
        string CssClass
        {
            get;
        }

        /// <summary>
        /// Menu link children.
        /// </summary>
        IEnumerable<IMenuNode> Children
        {
            get;
        }
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MenuNode : IMenuNode
    {
        #region IMenuNode Members

        /// <inheritdoc />
        public string Label
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Url
        {
            get;
            set;
        }

        /// <inheritdoc />
        public bool IsSelected
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string CssClass
        {
            get;
            set;
        }

        /// <inheritdoc />
        public IEnumerable<IMenuNode> Children
        {
            get;
            set;
        }

        #endregion
    }
}
// <copyright file="Tickable.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// ReSharper disable CheckNamespace
namespace Appva.Mvc
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// A tickable (checkbox) element.
    /// </summary>
    public class Tickable
    {
        /// <summary>
        /// The id of the tickable checkbox.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The label of the tickable checkbox.
        /// </summary>
        public string Label
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not the tickable checkbox is
        /// selected or not.
        /// </summary>
        public bool IsSelected
        {
            get;
            set;
        }

        /// <summary>
        /// The tickable checkbox helper text.
        /// </summary>
        public string HelpText
        {
            get;
            set;
        }

        /// <summary>
        /// The tickable checkbox group if any.
        /// </summary>
        public string Group
        {
            get;
            set;
        }
    }
}
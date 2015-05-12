// <copyright file="CheckBoxViewModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Web.ViewModels
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CheckBoxViewModel
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxViewModel"/> class.
        /// </summary>
        public CheckBoxViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckBoxViewModel"/> class.
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="isChecked">Optional; whether or not the checkbox is checked; defaults to false</param>
        public CheckBoxViewModel(int id, bool isChecked = false)
        {
            this.Id = id;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The checkbox ID.
        /// </summary>
        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not it is checked.
        /// </summary>
        public bool Checked
        {
            get;
            set;
        }

        #endregion
    }

}
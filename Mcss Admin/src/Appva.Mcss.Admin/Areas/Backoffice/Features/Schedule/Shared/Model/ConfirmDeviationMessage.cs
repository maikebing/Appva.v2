// <copyright file="ConfirmDeviationMessage.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Features.Schedule.Shared.Model
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ConfirmDeviationMessage
    {
        #region Constants.

        /// <summary>
        /// The default message
        /// </summary>
        private const string DefaultMessage = "";

        #endregion

        #region Fields.

        /// <summary>
        /// The complete message
        /// </summary>
        private string CompleteMessage;

        #endregion

        #region Properties

        /// <summary>
        /// The notice-header
        /// </summary>
        public string Header 
        {
            get; 
            set; 
        }

        /// <summary>
        /// The notice message
        /// </summary>
        public string Message 
        { 
            get;
            set; 
        }

        /// <summary>
        /// If list of nurses to contact should be included
        /// </summary>
        public bool IncludeListOfNurses
        {
            get;
            set;
        }

        #endregion
    }
}
// <copyright file="DelegationSettingsModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
using Appva.Cqrs;
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.



    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DelegationSettingsModel : IRequest<object>
    {
        #region Properties.

        /// <summary>
        /// If activation shall be needed after delegation update
        /// </summary>
        public bool RequireActivationOnChange
        {
            get;
            set;
        }

        /// <summary>
        /// If reason should be specified on delegation removal
        /// </summary>
        public bool SpecifyReasonIsActive
        {
            get;
            set;
        }

        /// <summary>
        /// The reasons
        /// </summary>
        public string Reasons
        {
            get;
            set;
        }

        #endregion
    }
}
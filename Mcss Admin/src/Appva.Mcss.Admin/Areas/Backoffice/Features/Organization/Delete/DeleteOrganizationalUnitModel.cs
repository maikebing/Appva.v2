// <copyright file="DeleteOrganizationalUnitModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports

    using Appva.Cqrs;
    using System;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class DeleteOrganizationalUnitModel : IRequest<bool>
    {
        #region Properties

        /// <summary>
        /// The organizationalunit Id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }       

        #endregion
    }
}
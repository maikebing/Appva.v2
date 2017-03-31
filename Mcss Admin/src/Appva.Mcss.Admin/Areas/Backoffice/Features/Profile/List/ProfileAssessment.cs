// <copyright file="ProfileAssessment.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ProfileAssessment : IRequest<ListProfileModel>
    {
        /// <summary>
        /// The assessment id.
        /// </summary>
        public Guid Id
        {
            get; set;
        }

        /// <summary>
        /// The assessment name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// The assessment description.
        /// </summary>
        public string Description
        {
            get; set;
        }

        /// <summary>
        /// The assessment type.
        /// </summary>
        public string Type
        {
            get; set;
        }

        /// <summary>
        /// Check if the assessment is active.
        /// </summary>
        public bool? IsActive
        {
            get; set;
        }

        /// <summary>
        /// Number of assessment users.
        /// </summary>
        public int? UsedByPatientsCount
        {
            get; set;
        }
    }
}
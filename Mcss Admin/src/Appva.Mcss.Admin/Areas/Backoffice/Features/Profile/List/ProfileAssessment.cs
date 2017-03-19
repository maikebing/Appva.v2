// <copyright file="ProfileController.cs" company="Appva AB">
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

    using Appva.Cqrs;
    using System;

    #endregion

    public class ProfileAssessment : IRequest<ListProfileModel>
    {
        public Guid Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string Type
        {
            get; set;
        }

        public bool? Active
        {
            get; set;
        }

        public int? UsedBy
        {
            get; set;
        }
    }
}
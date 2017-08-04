// <copyright file="InactivateArticleModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class InactivateArticleModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The article id.
        /// </summary>
        public Guid ArticleId
        {
            get;
            set;
        }

        /// <summary>
        /// The patient id.
        /// </summary>
        public Guid PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// The sequence.
        /// </summary>
        public Guid SequenceId
        {
            get;
            set;
        }

        /// <summary>
        /// The sequence name.
        /// </summary>
        public string SequenceName
        {
            get;
            set;
        }

        /// <summary>
        /// The schedule list name.
        /// </summary>
        public string ScheduleListName
        {
            get;
            set;
        }

        #endregion
    }
}
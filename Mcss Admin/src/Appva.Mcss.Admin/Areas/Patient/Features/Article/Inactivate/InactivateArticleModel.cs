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
    using System.Collections.Generic;

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
        /// The sequences.
        /// </summary>
        public List<Sequence> Sequences
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the article.
        /// </summary>
        /// <value>
        /// The article.
        /// </value>
        public Article Article 
        { 
            get; 
            set; 
        }

        #endregion

        
    }
}
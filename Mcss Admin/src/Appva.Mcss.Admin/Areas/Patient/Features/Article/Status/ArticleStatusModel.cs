// <copyright file="ArticleStatusModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;
    using Microsoft.AspNet.Identity;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ArticleStatusModel : IRequest<ListArticle>
    {
        #region Fields.

        /// <summary>
        /// The user <see cref="Guid"/>.
        /// </summary>
        private readonly Guid user = new Guid(HttpContext.Current.User.Identity.GetUserId());

        #endregion

        #region Properties.

        /// <summary>
        /// The patient id.
        /// </summary>
        public Guid PatientId
        {
            get;
            set;
        }

        /// <summary>
        /// The current user id.
        /// </summary>
        public Guid UserId
        {
            get
            {
                return this.user;
            }
        }

        /// <summary>
        /// A collection of articles.
        /// </summary>
        public IList<ArticleModel> OrderedArticles
        {
            get;
            set;
        }

        #endregion
    }
}
﻿// <copyright file="OverviewUpdateArticleHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Auditing;
    using Appva.Mcss.Admin.Application.Services;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class OverviewUpdateArticleHandler : RequestHandler<OverviewUpdateArticle, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IArticleService"/>.
        /// </summary>
        private readonly IArticleService service;

        /// <summary>
        /// The <see cref="IAuditService"/>.
        /// </summary>
        private readonly IAuditService auditing;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="OverviewUpdateArticleHandler"/> class.
        /// </summary>
        /// <param name="service">The <see cref="IArticleService"/>.</param>
        /// <param name="auditing">The <see cref="IAuditService"/>.</param>
        public OverviewUpdateArticleHandler(IArticleService service, IAuditService auditing)
        {
            this.service = service;
            this.auditing = auditing;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override bool Handle(OverviewUpdateArticle message)
        {
            foreach (var article in message.Articles)
            {
                this.service.UpdateStatusFor(article.Id, article.Status);
            }
            return true;
        }

        #endregion
    }
}
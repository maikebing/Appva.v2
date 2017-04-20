// <copyright file="EditReasonHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Fields
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Admin.Models;
    using Application.Services;
    #endregion

    internal sealed class EditReasonHandler : RequestHandler<Identity<EditReasonModel>, EditReasonModel>
    {
        #region  Fields
        /// <summary>
        /// The Taxonomy service
        /// </summary>
        private readonly ITaxonomyService taxonomyService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of <see cref="EditReasonModel"/> class.
        /// </summary>
        /// <param name="taxonomyService"></param>
        public EditReasonHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }
        #endregion

        #region RequestHandler Overrides
        /// <summary>
        /// Handles the EditReasonModel
        /// </summary>
        /// <param name="message">The <see cref="EditReasonModel"/> message</param>
        /// <returns>A new <see cref="EditReasonModel"/></returns>
        public override EditReasonModel Handle(Identity<EditReasonModel> message)
        {
            var taxon = this.taxonomyService.Get(message.Id);

            return new EditReasonModel
            {
                Id = taxon.Id,
                Name = taxon.Name
            };
        }
        #endregion
    }
}
// <copyright file="EditSignatureHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    internal sealed class EditSignatureHandler : RequestHandler<Identity<EditSignatureModel>, EditSignatureModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="EditSignatureHandler"/> class.
        /// </summary>
        public EditSignatureHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override EditSignatureModel Handle(Identity<EditSignatureModel> message)
        {
            var signature = this.taxonomyService.Find(message.Id);
            var list = this.taxonomyService.Roots(TaxonomicSchema.SignStatus);
            var editSignature = new EditSignatureModel();
            editSignature.Images = new Dictionary<string, string>();
            int btnIndex = 1;

            foreach (var item in list.GroupBy(x => x.Path).Select(x => x.FirstOrDefault()).OrderBy(x => x.Path))
            {
                editSignature.Images.Add("radioBtn" + btnIndex, item.Path);

                btnIndex++;
            }

            editSignature.Id = message.Id;
            editSignature.Name = signature.Name;
            editSignature.Path = signature.Path;
            editSignature.IsRoot = signature.IsRoot;
            return editSignature;
        }

        #endregion
    }
}
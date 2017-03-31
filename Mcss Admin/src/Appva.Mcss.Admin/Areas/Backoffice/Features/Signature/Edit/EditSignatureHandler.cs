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
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;
    using System.Reflection;
    using System;
    #endregion

    /// <summary>
    /// Handles the edit request
    /// </summary>
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
        /// <param name="taxonomyService"></param>
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
            var signatures = typeof(Signatures).GetFields(BindingFlags.Public | BindingFlags.Static);
            var editSignature = new EditSignatureModel();
            editSignature.Images = new Dictionary<string, string>();

                int btnIndex = 1;

            foreach (var item in signatures)
            {
                editSignature.Images.Add("radioBtn" + btnIndex, Convert.ToString(item.GetValue(null)));
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
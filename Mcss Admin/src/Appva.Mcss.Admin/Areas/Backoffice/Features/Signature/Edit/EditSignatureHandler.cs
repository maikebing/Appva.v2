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

    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Models;

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
        /// </summary>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/> implementation</param>
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

            for (int i = 0; i < signatures.Length; i++)
            {
                editSignature.Images.Add("radioBtn" + i, Convert.ToString(signatures[i].GetValue(null)));
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
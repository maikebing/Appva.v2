// <copyright file="ListUploadHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Log.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListUploadHandler : RequestHandler<Parameterless<ListUploadModel>, ListUploadModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListUploadHandler"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        public ListUploadHandler(IFileService fileService)
        {
            this.fileService = fileService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override ListUploadModel Handle(Parameterless<ListUploadModel> message)
        {
            return new ListUploadModel
            {
                Files = this.fileService.GetUploadedFiles() ?? new List<DataFile>()
            };
        }

        #endregion
    }
}
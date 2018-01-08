﻿// <copyright file="PractitionerPreviewPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerPreviewPublisher : RequestHandler<PractitionerPreviewModel, Guid>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerPreviewPublisher"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        public PractitionerPreviewPublisher(IFileService fileService)
        {
            this.fileService = fileService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override Guid Handle(PractitionerPreviewModel message)
        {
            var file = this.fileService.Get(message.Id);

            if (file == null)
            {
                return Guid.Empty;
            }

            var properties = JsonConvert.DeserializeObject<FileUploadProperties>(file.Properties);

            if(properties.PractitionerImportProperties == null)
            {
                properties.PractitionerImportProperties = new PractitionerImportProperties
                {
                    IsImportable = message.IsImportable
                };

                file.Properties = JsonConvert.SerializeObject(properties);
                this.fileService.Save(file);
            }

            return file.Id;
        }

        #endregion
    }
}
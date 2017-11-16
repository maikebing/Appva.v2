// <copyright file="PractitionerPreviewPublisher.cs" company="Appva AB">
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
    using Appva.Persistence;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerPreviewPublisher : RequestHandler<PractitionerPreviewModel, Guid>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerPreviewPublisher"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        public PractitionerPreviewPublisher(IPersistenceContext persistence, IFileService fileService)
        {
            this.persistence = persistence;
            this.fileService = fileService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override Guid Handle(PractitionerPreviewModel message)
        {
            var file = this.fileService.Get(message.FileId);

            if (file == null)
            {
                return Guid.Empty;
            }

            var properties = JsonConvert.DeserializeObject<FileUploadProperties>(file.Properties);
            
            if(properties.PractitionerImportProperties != null)
            {
                properties.PractitionerImportProperties.IsImportable = message.IsImportable;
            }
            else
            {
                properties.PractitionerImportProperties = new PractitionerImportProperties
                {
                    IsImportable = message.IsImportable
                };
            }

            file.Properties = JsonConvert.SerializeObject(properties);
            this.persistence.Save(file);

            return file.Id;
        }

        #endregion
    }
}
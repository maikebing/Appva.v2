// <copyright file="PractitionerOrganizationPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerOrganizationPublisher : RequestHandler<PractitionerOrganizationModel, Guid>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerOrganizationPublisher"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        public PractitionerOrganizationPublisher(IFileService fileService)
        {
            this.fileService = fileService;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override Guid Handle(PractitionerOrganizationModel message)
        {
            var file = this.fileService.Get(message.Id);

            if (file == null || message == null)
            {
                return Guid.Empty;
            }

            var nodes = new List<KeyValuePair<string, Guid>>();
            for (int i = 0; i < message.SelectedNodes.Count; i++)
            {
                var result = Guid.Empty;
                if (Guid.TryParse(message.SelectedNodes[i], out result) && result != Guid.Empty)
                {
                    nodes.Add(new KeyValuePair<string, Guid>(message.ImportedNodes[i], result));
                }
            }

            var properties = JsonConvert.DeserializeObject<FileUploadProperties>(file.Properties);
            properties.PractitionerImportProperties.Nodes = nodes;
            file.Properties = JsonConvert.SerializeObject(properties);
            this.fileService.Save(file);

            return file.Id;
        }

        #endregion
    }
}
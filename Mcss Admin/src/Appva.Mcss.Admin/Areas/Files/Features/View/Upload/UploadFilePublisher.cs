// <copyright file="UploadFilePublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System.IO;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UploadFilePublisher : RequestHandler<UploadFileModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UploadFilePublisher"/> class.
        /// </summary>
        public UploadFilePublisher(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(UploadFileModel message)
        {
            if(message.UploadedFile == null)
            {
                return false;
            }

            var properties = new FileUploadProperties
            {
                IsSetAsImportableExcelFile = message.IsSetAsImportableExcelFile
            };

            var fileName = Path.GetFileName(message.UploadedFile.FileName);
            var contentType = message.UploadedFile.ContentType;

            using (var fileStream = message.UploadedFile.InputStream)
            {
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    var bytes = binaryReader.ReadBytes((int)fileStream.Length);
                    this.persistence.Save(new DataFile
                    {
                        Name = fileName,
                        ContentType = contentType,
                        Data = bytes,
                        Title = message.Title,
                        Description = message.Description,
                        Properties = JsonConvert.SerializeObject(properties)
                    });
                }
            }

            return true;
        }

        #endregion
    }
}
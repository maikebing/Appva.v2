// <copyright file="ListUploadHandler.cs" company="Appva AB">
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
    using System.IO;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Models;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListUploadHandler : RequestHandler<ListUploadModel, ListUploadModel>
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
        public override ListUploadModel Handle(ListUploadModel message)
        {
            var files = new List<ListUpload>();

            foreach (var file in this.fileService.GetUploadedFiles(message.IsFilteredByImages))
            {
                var typeImage = SetImagePath(file.ContentType.ToLower(), Path.GetExtension(file.Name).ToLower());
                var fileSize = GetFileSizeFormat(file.Data.Length);
                var contentType = file.ContentType.Split('/');

                files.Add(new ListUpload
                {
                    Id = file.Id,
                    Name = file.Name,
                    Title = file.Title,
                    Description = file.Description,
                    Type = contentType.Length > 1 ? contentType[1] : contentType[0],
                    TypeImage = typeImage,
                    Size = fileSize,
                    DateAdded = file.CreatedAt.ToShortDateString(),
                    Properties = file.Properties == null ? null : JsonConvert.DeserializeObject<FileUploadProperties>(file.Properties)
                });
            }

            return new ListUploadModel
            {
                Files = files,
                IsFilteredByImages = message.IsFilteredByImages
            };
        }

        #endregion

        #region Private methods.

        /// <summary>
        /// Gets a formatted file size.
        /// </summary>
        /// <param name="contentLength">The content length.</param>
        /// <returns>The formatted file size.</returns>
        private string GetFileSizeFormat(int contentLength)
        {
            var fileSize = string.Empty;
            var kB = 1024;

            if (contentLength > kB)
            {
                double length = contentLength;
                fileSize = length > Math.Pow(kB, 2) ? Math.Round(length / Math.Pow(kB, 2), 1) + " MB" : Math.Round(length / kB, 1) + " kB";
            }
            else
            {
                fileSize = contentLength + " bytes";
            }

            return fileSize;
        }

        /// <summary>
        /// Sets the image icon for the file type.
        /// </summary>
        /// <param name="contentType">The file content type.</param>
        /// <param name="extension">The file extension.</param>
        /// <returns>The image path.</returns>
        private string SetImagePath(string contentType, string extension)
        {
            var typeImage = "icon_fl_doc.png";

            if (contentType.ToLower().Contains("image"))
            {
                typeImage = "icon_fl_image.png";
            }
            else if (extension == ".pdf")
            {
                typeImage = "icon_fl_pdf.png";
            }
            else if (extension == ".xlsx" || extension == ".xls")
            {
                typeImage = "icon_fl_excel.png";
            }
            else if (extension == ".docx" || extension == ".doc")
            {
                typeImage = "icon_fl_word.png";
            }
            else if (extension == ".ppt")
            {
                typeImage = "icon_fl_pp.png";
            }

            return typeImage;
        }

        #endregion
    }
}
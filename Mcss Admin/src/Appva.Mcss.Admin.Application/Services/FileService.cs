// <copyright file="FileService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;

    #endregion

    /// <summary>
    /// The <see cref="DataFile"/> service.
    /// </summary>
    public interface IFileService : IService
    {
        /// <summary>
        /// Get a file by its <see cref="Guid"/>.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A <see cref="DataFile"/>.</returns>
        DataFile Get(Guid id);

        /// <summary>
        /// Gets a collection of uploaded files.
        /// </summary>
        /// <param name="isFilteredByImages">If true, only images will be fetched.</param>
        /// <returns>A <see cref="IList{DataFile}"/> collection.</returns>
        IList<DataFile> GetUploadedFiles(bool? isFilteredByImages = null);

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="id">The <see cref="DataFile"/>.</param>
        void Delete(DataFile file);

        /// <summary>
        /// Saves the file to a temporary folder.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="fileData">The file data.</param>
        /// <returns>The file path.</returns>
        string SaveToDisk(string fileName, byte[] fileData);

        /// <summary>
        /// Gets a formatted file size.
        /// </summary>
        /// <param name="contentLength">The content length.</param>
        /// <returns>The formatted file size.</returns>
        string GetFileSizeFormat(int contentLength);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class FileService : IFileService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IFileRepository"/>.
        /// </summary>
        private readonly IFileRepository repository;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="FileService"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="IFileRepository"/>.</param>
        public FileService(IFileRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region IFileService members.

        /// <inheritdoc />
        public IList<DataFile> GetUploadedFiles(bool? isFilteredByImages = null)
        {
            return this.repository.GetUploadedFiles(isFilteredByImages);
        }

        /// <inheritdoc />
        public DataFile Get(Guid id)
        {
            return this.repository.Get(id);
        }

        public void Delete(DataFile file)
        {
            this.repository.Delete(file);
        }

        /// <inheritdoc />
        public string SaveToDisk(string fileName, byte[] fileData)
        {
            var folder = HttpContext.Current.Server.MapPath("~/Content/Temp");
            var path = string.Format("{0}\\{1}", folder, fileName);

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllBytes(path, fileData);
            return path;
        }

        /// <inheritdoc />
        public string GetFileSizeFormat(int contentLength)
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

        #endregion
    }
}
// <copyright file="ImageProcessor.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Imaging
{
    #region Imports.

    using System;
    using System.Drawing;
    using System.IO;
    using System.Security.Cryptography;
    using System.Web;
    using Core.Extensions;
    using Cryptography;
    using Validation;

    #endregion

    /// <summary>
    /// Image processor and reader.
    /// </summary>
    public interface IImageProcessor
    {
        /// <summary>
        /// Reads the image from disk.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="mimeType">The file mime type</param>
        /// <returns>A <see cref="ImageContentResult"/></returns>
        ImageContentResult Read(string fileName, string mimeType);

        /// <summary>
        /// Saves a file to disk.
        /// </summary>
        /// <param name="file">The posted file</param>
        /// <param name="fileName">The stored file name</param>
        /// <returns>True if the file was saved successfully</returns>
        /// <exception cref="ArgumentNullException">If the file parameter is null</exception>
        bool Save(HttpPostedFileBase file, out string fileName);
    }

    /// <summary>
    /// Implementation of <see cref="IImageProcessor"/>.
    /// </summary>
    public sealed class ImageProcessor : IImageProcessor
    {
        #region Variables.

        /// <summary>
        /// The image storage path.
        /// </summary>
        private readonly string path;

        /// <summary>
        /// The image width.
        /// </summary>
        private readonly int width;

        /// <summary>
        /// The image height.
        /// </summary>
        private readonly int height;

        /// <summary>
        /// The image quality.
        /// </summary>
        private readonly int quality;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProcessor"/> class.
        /// </summary>
        /// <param name="configuration">The image configuration</param>
        public ImageProcessor(ImageConfiguration configuration)
        {
            Requires.NotNull(configuration, "configuration");
            this.path = configuration.Path;
            this.quality = configuration.Quality;
            this.width = configuration.Width;
            this.height = configuration.Height;
        }

        #endregion

        #region Public Methods.

        /// <inheritdoc />
        public ImageContentResult Read(string fileName, string mimeType)
        {
            var filePath = Path.Combine(this.path, fileName);
            if (fileName.IsEmpty() || mimeType.IsEmpty() || ! File.Exists(filePath))
            {
                return null;
            }
            using (var fileStream = File.OpenRead(filePath))
            using (var reader = new BinaryReader(fileStream))
            {
                var bytes = reader.ReadBytes(Convert.ToInt32(fileStream.Length));
                return new ImageContentResult(bytes, mimeType.FromHex());
            }
        }

        /// <inheritdoc />
        public bool Save(HttpPostedFileBase file, out string fileName)
        {
            Requires.NotNull(file, "file");
            using (var stream = file.InputStream)
            using (var image = Image.FromStream(stream))
            using (var resizer = new ImageResizer(image, this.width, this.height, this.quality))
            {
                resizer.Resize();
                fileName = this.CreateFileName(resizer.Stream);
                using (var fileStream = File.Open(Path.Combine(this.path, fileName), FileMode.OpenOrCreate))
                {
                    resizer.Stream.Reset().CopyTo(fileStream);
                }
                return true;
            }
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates a new file name with format {random}.{guid}.{checksum}.
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <returns>Returns the new unique file name</returns>
        private string CreateFileName(Stream stream)
        {
            var checksum = Checksum.Using<SHA256Managed>(stream.ToArray()).Build();
            var fileName = string.Join("-", Path.GetRandomFileName().Replace(".", string.Empty), Guid.NewGuid().ToString().Replace("-", string.Empty), checksum);
            return fileName.ToLower().StripInvalidFileNameCharacters();
        }

        #endregion
    }
}
// <copyright file="ImageResizer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Imaging
{
    #region Imports.

    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using Core.Extensions;

    #endregion

    /// <summary>
    /// Image resizer.
    /// </summary>
    public interface IImageResizer : IDisposable
    {
        /// <summary>
        /// Returns the resize width.
        /// </summary>
        int Width
        {
            get;
        }

        /// <summary>
        /// Returns the resize height.
        /// </summary>
        int Height
        {
            get;
        }

        /// <summary>
        /// Returns the resize quality.
        /// </summary>
        int Quality
        {
            get;
        }

        /// <summary>
        /// Returns the memory stream.
        /// </summary>
        Stream Stream
        {
            get;
        }

        /// <summary>
        /// Resizes the image.
        /// </summary>
        void Resize();
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ImageResizer : IImageResizer
    {
        #region Variables.

        /// <summary>
        /// The image original.
        /// </summary>
        private readonly Image original;

        /// <summary>
        /// The resize width.
        /// </summary>
        private readonly int width;

        /// <summary>
        /// The resize height.
        /// </summary>
        private readonly int height;

        /// <summary>
        /// The resize quality.
        /// </summary>
        private readonly int quality;

        /// <summary>
        /// The memory stream.
        /// </summary>
        private readonly Stream stream;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResizer"/> class.
        /// </summary>
        /// <param name="original">The original image</param>
        /// <param name="width">The resize width</param>
        /// <param name="height">The resize height</param>
        /// <param name="quality">The resize quality</param>
        public ImageResizer(Image original, int width, int height, int quality = 90)
        {
            this.original = original;
            this.width = width;
            this.height = height;
            this.quality = quality;
            this.stream = new MemoryStream();
        }

        #endregion

        #region Public Properties.

        /// <inheritdoc />
        public int Width
        {
            get
            {
                return this.width;
            }
        }

        /// <inheritdoc />
        public int Height
        {
            get
            {
                return this.height;
            }
        }

        /// <inheritdoc />
        public int Quality
        {
            get
            {
                return this.quality;
            }
        }

        /// <inheritdoc />
        public Stream Stream
        {
            get
            {
                return this.stream;
            }
        }

        /// <summary>
        /// Returns the encoder parameters.
        /// </summary>
        /// <returns></returns>
        internal EncoderParameters EncoderParameters
        {
            get
            {
                return new EncoderParameters
                {
                    Param = new[]
                    { 
                        new EncoderParameter(Encoder.Quality, this.quality)
                    }
                };
            }
        }

        /// <summary>
        /// Resolves the mime type from the image.
        /// </summary>
        /// <returns>The mime type</returns>
        internal string MimeType
        {
            get
            {
                var formatId = this.original.RawFormat.Guid;
                foreach (var codec in ImageCodecInfo.GetImageDecoders().Where(codec => codec.FormatID.Equals(formatId)))
                {
                    return codec.MimeType;
                }
                return "image/unknown";
            }
        }

        /// <summary>
        /// Returns the encoder by mime type.
        /// </summary>
        /// <returns>The appropriate encoder for the mime type</returns>
        internal ImageCodecInfo ImageEncoder
        {
            get
            {
                return ImageCodecInfo.GetImageEncoders()
                    .Select(x => x).FirstOrDefault(x => x.MimeType.Equals(this.MimeType, StringComparison.OrdinalIgnoreCase));
            }
        }

        #endregion

        #region IImageResizer Members

        /// <inheritdoc />
        [SuppressMessage("ReSharper", "PossibleLossOfFraction", Justification = "Reviewed.")]
        [SuppressMessage("ReSharper", "LocalVariableHidesMember", Justification = "Reviewed.")]
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:OpeningParenthesisMustBeSpacedCorrectly", Justification = "Reviewed.")]
        public void Resize()
        {
            double aspectRatio = this.original.Width / this.original.Height;
            double boxRatio = this.Width / this.Height;
            double scaleFactor;
            if (boxRatio >= aspectRatio)
            {
                scaleFactor = (double) this.Height / this.original.Height;
            }
            else
            {
                scaleFactor = (double) this.Width / this.original.Width;
            }
            var width  = (int) (this.original.Width * scaleFactor);
            var height = (int) (this.original.Height * scaleFactor);
            using (var image = new Bitmap(width, height))
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(this.original, new Rectangle(0, 0, width, height));
                image.Save(this.Stream, this.ImageEncoder, this.EncoderParameters);
                this.Stream.Reset();
            }
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.Stream.IsNotNull())
            {
                this.Stream.Dispose();
            }
        }

        #endregion
    }
}
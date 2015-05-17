// <copyright file="Detector.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Imaging.Detection
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using Core.Extensions;
    using Resources;
    using Validation;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface IDetector
    {
        /// <summary>
        /// Detects the content type of the given input document. The detector will 
        /// read bytes from the start of the stream to help in type detection.
        /// </summary>
        /// <param name="input">Input document input stream</param>
        /// <returns>The detected media type</returns>
        /// <exception cref="ArgumentNullException">If the input stream is null</exception>
        MediaType Detect(Stream input);
    }

    /// <summary>
    /// Auto detects file format by reading the first bytes from the 
    /// file header.
    /// </summary>
    public sealed class AutoDetector : IDetector
    {
        #region Constants.

        /// <summary>
        /// The header size in bytes.
        /// </summary>
        private const int HeaderSize = 560;

        #endregion

        #region Variables.

        /// <summary>
        /// Known media types which are detectable.
        /// </summary>
        private readonly IReadOnlyList<MediaType> knownMediaTypes = new List<MediaType>
        {
            new MediaType(new byte?[] { 0xFF, 0xD8, 0xFF }, 0, "jpg", MediaTypes.Jpeg),
            new MediaType(new byte?[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }, 0, "png", MediaTypes.Png),
            new MediaType(new byte?[] { 0x47, 0x49, 0x46, 0x38, null, 0x61 }, 0, "gif", MediaTypes.Gif)
        };

        #endregion

        #region IDetector Members.

        /// <inheritdoc />
        public MediaType Detect(Stream input)
        {
            Requires.NotNull(input, "input");
            var fileHeader = new byte[HeaderSize];
            input.Read(fileHeader, 0, HeaderSize);
            input.Reset();
            foreach (var type in this.knownMediaTypes)
            {
                var matches = 0;
                for (var i = 0; i < type.Header.Length; i++)
                {
                    if (type.Header[i] != null && type.Header[i] != fileHeader[i + type.Offset])
                    {
                        matches = 0;
                        break;
                    }
                    matches++;
                }
                if (matches.Equals(type.Header.Length))
                {
                    return type;
                }
            }
            return null;
        }

        #endregion
    }
}
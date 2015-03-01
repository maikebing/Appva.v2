// <copyright file="MediaType.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mvc.Imaging.Detection
{
    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MediaType
    {
        #region Variables.

        /// <summary>
        /// File header byte array signature.
        /// </summary>
        private readonly byte?[] header;

        /// <summary>
        /// File header byte offset.
        /// </summary>
        private readonly int offset;

        /// <summary>
        /// File extension.
        /// </summary>
        private readonly string extension;

        /// <summary>
        /// File mime type.
        /// </summary>
        private readonly string mimeType;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaType"/> class.
        /// </summary>
        /// <param name="header">The file header byte array</param>
        /// <param name="offset">The file header offset</param>
        /// <param name="extension">The file extension</param>
        /// <param name="mimeType">The mime type</param>
        public MediaType(byte?[] header, int offset, string extension, string mimeType)
        {
            this.header = header;
            this.offset = offset;
            this.extension = extension;
            this.mimeType = mimeType;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the file header byte signature.
        /// </summary>
        internal byte?[] Header
        { 
            get
            {
                return this.header;
            }
        }

        /// <summary>
        /// Returns the file header offset.
        /// </summary>
        internal int Offset
        { 
            get
            {
                return this.offset;
            }
        }

        /// <summary>
        /// Returns the file extension.
        /// </summary>
        internal string Extension
        {
            get
            {
                return this.extension;
            }
        }

        /// <summary>
        /// Returns the file mime type.
        /// </summary>
        internal string MimeType
        {
            get
            {
                return this.mimeType;
            }
        }

        #endregion
    }
}
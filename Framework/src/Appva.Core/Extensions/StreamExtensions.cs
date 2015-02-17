// <copyright file="StreamExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Extensions
{
    #region Imports.

    using System.IO;

    #endregion

    /// <summary>
    /// <see cref="Stream"/> extension helper methods.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Resets the stream so it can be read again - position = 0.
        /// </summary>
        /// <param name="stream">The stream to be reset</param>
        /// <returns>The stream</returns>
        public static Stream Reset(this Stream stream)
        {
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Converts the stream to a byte array.
        /// </summary>
        /// <param name="stream">The stream to be read</param>
        /// <returns>A new copy of the stream as byte array</returns>
        public static byte[] ToArray(this Stream stream)
        {
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
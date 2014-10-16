// <copyright file="ByteArrayExtensions.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Core.Extensions
{
    #region Imports.

    using System;
    using System.Text;

    #endregion

    /// <summary>
    /// Extension helpers for byte arrays.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// Converts a byte array to an unsigned 32 bit integer.
        /// </summary>
        /// <param name="bytes">The byte array to do work on</param>
        /// <param name="startIndex">An optional start index</param>
        /// <returns>An unsigned int</returns>
        public static uint ToUInt32(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToUInt32(bytes, startIndex);
        }

        /// <summary>
        /// Converts a byte array to an unsigned 64 bit integer.
        /// </summary>
        /// <param name="bytes">The byte array to do work on</param>
        /// <param name="startIndex">An optional start index</param>
        /// <returns>An unsigned long</returns>
        public static ulong ToUInt64(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToUInt64(bytes, startIndex);
        }

        /// <summary>
        /// Converts a byte array to a string.
        /// </summary>
        /// <param name="bytes">The byte array to do work on</param>
        /// <param name="startIndex">An optional start index</param>
        /// <returns>A string representation</returns>
        public static string ToString(this byte[] bytes, int startIndex = 0)
        {
            return BitConverter.ToString(bytes, startIndex);
        }

        /// <summary>
        /// Converts a byte array to a base64 string.
        /// </summary>
        /// <param name="bytes">The bytes to be converted</param>
        /// <returns>A base64 string representation</returns>
        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts a byte array to a UTF-8 string.
        /// </summary>
        /// <param name="bytes">The bytes to be converted</param>
        /// <returns>A UTF-8 string</returns>
        public static string ToUtf8(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Returns a URL safe base 64 string.
        /// See <a href="http://tools.ietf.org/html/rfc4648#page-7">RFC 4648</a>
        /// </summary>
        /// <param name="bytes">The bytes to be converted</param>
        /// <returns>A URL safe base 64 string representation</returns>
        public static string ToUrlSafeBase64(this byte[] bytes)
        {
            if (bytes.Length < 1)
            {
                return string.Empty;
            }
            var base64Str = bytes.ToBase64();
            int endPos;
            for (endPos = base64Str.Length; endPos > 0; endPos--)
            {
                if (base64Str[endPos - 1] != '=')
                {
                    break;
                }
            }
            var base64Chars = new char[endPos + 1];
            base64Chars[endPos] = (char)('0' + base64Str.Length - endPos);
            for (var i = 0; i < endPos; i++)
            {
                var character = base64Str[i];
                switch (character)
                {
                    case '+':
                        base64Chars[i] = '-';
                        break;
                    case '/':
                        base64Chars[i] = '_';
                        break;
                    case '=':
                        base64Chars[i] = character;
                        break;
                    default:
                        base64Chars[i] = character;
                        break;
                }
            }
            return new string(base64Chars);
        }
    }
}
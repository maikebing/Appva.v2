// <copyright file="ProtoBufUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Tools
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using ProtoBuf;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class ProtoBufUtils
    {
        /// <summary>
        /// Serializes an instance.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="instance">The instance to be serialized</param>
        /// <returns>A byte array</returns>
        public static byte[] Serialize<T>(T instance)
        {
            using (var stream = new MemoryStream())
            {
                Serializer.Serialize<T>(stream, instance);
                var retval = new byte[stream.Position];
                Array.Copy(stream.GetBuffer(), retval, retval.Length);
                return retval;
            }
        }

        /// <summary>
        /// Deserializes a byte array.
        /// </summary>
        /// <typeparam name="T">The type to deserialize to</typeparam>
        /// <param name="bytes">The bytes to deserialize</param>
        /// <returns>A {T}</returns>
        public static T Deserialize<T>(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return Serializer.Deserialize<T>(stream);
            }
        }
    }
}
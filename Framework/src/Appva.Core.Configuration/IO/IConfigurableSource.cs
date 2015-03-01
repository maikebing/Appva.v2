// <copyright file="IConfigurableSource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Configuration.IO
{
    #region Imports.

    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// A representation of a configurable source, e.g. json file source
    /// or XML file source.
    /// </summary>
    public interface IConfigurableSource
    {
        /// <summary>
        /// Reads from a source.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>The {T}</returns>
        T Read<T>();

        /// <summary>
        /// Reads from a source async.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>The {T}</returns>
        Task<T> ReadAsync<T>();

        /// <summary>
        /// Writes to source.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="obj">The object to be written</param>
        void Write<T>(T obj);

        /// <summary>
        /// Writes to source async.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="obj">The object to be written</param>
        /// <returns>The <see cref="Task"/></returns>
        Task WriteAsync<T>(T obj);
    }
}

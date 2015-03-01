// <copyright file="ConfigurableSource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Configuration.IO
{
    #region Imports.

    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Validation;

    #endregion

    /// <summary>
    /// Represents a configurable source.
    /// </summary>
    internal abstract class ConfigurableSource : IConfigurableSource, IDisposable
    {
        #region Variables.

        /// <summary>
        /// The file path.
        /// </summary>
        private readonly string path;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurableSource"/> class.
        /// </summary>
        /// <param name="path">The file path</param>
        protected ConfigurableSource(string path)
        {
            Requires.NotNullOrEmpty(path, "path");
            this.path = (path.IndexOf(":", StringComparison.Ordinal) > 0) ? path : PathResolver.ResolveAppRelativePath(path);
            this.path = this.path.Replace(@"\bin\Debug",   string.Empty)
                                 .Replace(@"\bin\Release", string.Empty);
        }

        #endregion

        #region IConfigurableSource Members.

        /// <inheritdoc />
        public virtual T Read<T>()
        {
            using (var stream = new FileStream(this.path, FileMode.Open, FileAccess.Read))
            {
                return this.Read<T>(stream);
            } 
        }

         /// <inheritdoc />
        public async virtual Task<T> ReadAsync<T>()
        {
            using (var stream = new FileStream(this.path, FileMode.Open, FileAccess.Read, FileShare.None, 4096, true))
            {
                return await this.ReadAsync<T>(stream);
            }
        }

        /// <inheritdoc />
        public virtual void Write<T>(T obj)
        {
            using (var stream = new FileStream(this.path, FileMode.Create, FileAccess.Write))
            {
                this.Write(obj, stream);
            } 
        }

        /// <inheritdoc />
        public async virtual Task WriteAsync<T>(T obj)
        {
            using (var stream = new FileStream(this.path, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            {
                await this.WriteAsync(obj, stream);
            }
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            //// no ops.
        }

        #endregion

        #region Protected Abstract Methods.

        /// <summary>
        /// Deserialize a stream to a type.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="stream">The input stream</param>
        /// <returns>The {T}</returns>
        protected abstract T Read<T>(Stream stream);

        /// <summary>
        /// Deserialize a stream to a type async.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="stream">The input stream</param>
        /// <returns>The {T}</returns>
        protected abstract Task<T> ReadAsync<T>(Stream stream);

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="obj">The object to be serialized</param>
        /// <param name="stream">The stream</param>
        protected abstract void Write<T>(T obj, Stream stream);

        /// <summary>
        /// Serializes an object async.
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="obj">The object to be serialized</param>
        /// <param name="stream">The stream</param>
        /// <returns>The <see cref="Task"/></returns>
        protected abstract Task WriteAsync<T>(T obj, Stream stream);

        #endregion
    }
}

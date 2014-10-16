// <copyright file="JsonConfigurableSource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Core.Configuration.IO
{
    #region Imports.

    using System.IO;
    using System.Threading.Tasks;
    using Extensions;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// A JSON configurable source.
    /// </summary>
    internal sealed class JsonConfigurableSource : ConfigurableSource
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonConfigurableSource"/> class.
        /// </summary>
        /// <param name="path">The path to the resource to be processed</param>
        public JsonConfigurableSource(string path) : base(path)
        {
        }

        #endregion

        #region ConfigurableSource Overrides.

        /// <inheritdoc />
        protected override T Read<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return (T) new JsonSerializer().Deserialize(new JsonTextReader(reader), typeof(T));
            }
        }

        /// <inheritdoc />
        protected async override Task<T> ReadAsync<T>(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var value = await reader.ReadToEndAsync();
                return (T) JsonConvert.DeserializeObject(value, typeof(T));
            }
        }

        /// <inheritdoc />
        protected override void Write<T>(T obj, Stream stream)
        {
            using (var writer = new StreamWriter(stream))
            {
                new JsonSerializer().Serialize(new JsonTextWriter(writer) { Formatting = Formatting.Indented }, obj);
            }
        }

        /// <inheritdoc />
        protected async override Task WriteAsync<T>(T obj, Stream stream)
        {
            var bytes = JsonConvert.SerializeObject(obj, Formatting.Indented).ToUtf8Bytes();
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }

        #endregion
    }
}

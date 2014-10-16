// <copyright file="ProtectedJsonConfigurableSource.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Core.Configuration.IO
{
    #region Imports.

    using System.IO;
    using System.Threading.Tasks;
    using Cryptography.DataProtection;
    using Extensions;
    using Newtonsoft.Json;
    using Validation;

    #endregion

    /// <summary>
    /// A protected JSON configurable source.
    /// </summary>
    internal sealed class ProtectedJsonConfigurableSource : ConfigurableSource
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IDataProtector"/>.
        /// </summary>
        private readonly IDataProtector protector;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ProtectedJsonConfigurableSource"/> class.
        /// </summary>
        /// <param name="protector">The protector</param>
        /// <param name="path">The file path</param>
        public ProtectedJsonConfigurableSource(IDataProtector protector, string path) : base(path)
        {
            Requires.NotNull(protector, "protector");
            this.protector = protector;
        }

        #endregion

        #region ConfigurableSource Overrides.

        /// <inheritdoc />
        protected override T Read<T>(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                var data = this.protector.Unprotect(memoryStream.ToArray()).ToUtf8();
                return JsonConvert.DeserializeObject<T>(data);
            }
        }

        /// <inheritdoc />
        protected async override Task<T> ReadAsync<T>(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                var data = this.protector.Unprotect(memoryStream.ToArray()).ToUtf8();
                return JsonConvert.DeserializeObject<T>(data);
            }
        }

        /// <inheritdoc />
        protected override void Write<T>(T obj, Stream stream)
        {
            using (var writer = new BinaryWriter(stream))
            {
                var data = JsonConvert.SerializeObject(obj, Formatting.Indented);
                var protectedData = this.protector.Protect(data.ToUtf8Bytes());
                writer.Write(protectedData);
            }
        }

        /// <inheritdoc />
        protected async override Task WriteAsync<T>(T obj, Stream stream)
        {
            var data = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var protectedData = this.protector.Protect(data.ToUtf8Bytes());
            await stream.WriteAsync(protectedData, 0, protectedData.Length);
        }

        #endregion
    }
}

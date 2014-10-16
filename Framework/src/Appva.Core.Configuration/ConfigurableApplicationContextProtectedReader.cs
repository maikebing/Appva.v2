// <copyright file="ConfigurableApplicationContextProtectedReader.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Core.Configuration
{
    #region Imports.

    using System.Threading.Tasks;
    using Cryptography.DataProtection;
    using IO;
    using Validation;

    #endregion

    /// <summary>
    /// Internal protected implemantation of a <see cref="IConfigurableResource"/> reader.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    internal sealed class ConfigurableApplicationContextProtectedReader<T> :
        IConfigurableApplicationContextReaderExecute<T>
        where T : class, IConfigurableResource
    {
        #region Variables;

        /// <summary>
        /// The data protector.
        /// </summary>
        private readonly IDataProtector protector;

        /// <summary>
        /// The configurable reader.
        /// </summary>
        private readonly ConfigurableApplicationContextReader<T> reader;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurableApplicationContextProtectedReader{T}"/> class.
        /// </summary>
        /// <param name="reader">The configurable reader</param>
        public ConfigurableApplicationContextProtectedReader(ConfigurableApplicationContextReader<T> reader) 
            : this(new MachineKeyProtector(typeof(T).FullName), reader)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurableApplicationContextProtectedReader{T}"/> class.
        /// </summary>
        /// <param name="protector">A protector implementation</param>
        /// <param name="reader">The configurable reader</param>
        public ConfigurableApplicationContextProtectedReader(IDataProtector protector, ConfigurableApplicationContextReader<T> reader)
        {
            Requires.NotNull(protector, "protector");
            Requires.NotNull(reader, "reader");
            this.protector = protector;
            this.reader = reader;
        }

        #endregion

        #region IConfigurableApplicationContextReaderExecute<T> Members.

        /// <inheritdoc />
        T IConfigurableApplicationContextReaderExecute<T>.ToObject()
        {
            using (this.protector)
            using (var source = new ProtectedJsonConfigurableSource(this.protector, this.reader.Location))
            {
                return source.Read<T>();
            }
        }

        /// <inheritdoc />
        async Task<T> IConfigurableApplicationContextReaderExecute<T>.ToObjectAsync()
        {
            using (this.protector)
            using (var source = new ProtectedJsonConfigurableSource(this.protector, this.reader.Location))
            {
                return await source.ReadAsync<T>();
            }
        }

        #endregion
    }
}

// <copyright file="ConfigurableApplicationContextProtectedWriter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Core.Configuration
{
    #region Imports

    using System.Threading.Tasks;
    using Cryptography.DataProtection;
    using IO;
    using Validation;

    #endregion

    /// <summary>
    /// Internal protected implemantation of a <see cref="IConfigurableResource"/> 
    /// writer.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    internal sealed class ConfigurableApplicationContextProtectedWriter<T> :
        IConfigurableApplicationContextWriterExecute
        where T : class, IConfigurableResource
    {
        #region Variables;

        /// <summary>
        /// The data protector.
        /// </summary>
        private readonly IDataProtector protector;

        /// <summary>
        /// The configurable writer.
        /// </summary>
        private readonly ConfigurableApplicationContextWriter<T> writer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ConfigurableApplicationContextProtectedWriter{T}"/> class.
        /// </summary>
        /// <param name="writer">The configurable writer</param>
        public ConfigurableApplicationContextProtectedWriter(ConfigurableApplicationContextWriter<T> writer)
            : this(new MachineKeyProtector(typeof(T).FullName), writer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ConfigurableApplicationContextProtectedWriter{T}"/> class.
        /// </summary>
        /// <param name="protector">A protector implementation</param>
        /// <param name="writer">The configurable writer</param>
        public ConfigurableApplicationContextProtectedWriter(IDataProtector protector, ConfigurableApplicationContextWriter<T> writer)
        {
            Requires.NotNull(protector, "protector");
            Requires.NotNull(writer, "writer");
            this.protector = protector;
            this.writer = writer;
        }

        #endregion

        #region IConfigurableApplicationContextWriterExecute<T> Members.

        /// <inheritdoc />
        void IConfigurableApplicationContextWriterExecute.Execute()
        {
            using (this.protector)
            using (var source = new ProtectedJsonConfigurableSource(this.protector, this.writer.Location))
            {
                source.Write(this.writer.Configuration);
            }
        }

        /// <inheritdoc />
        async Task IConfigurableApplicationContextWriterExecute.ExecuteAsync()
        {
            using (this.protector)
            using (var source = new ProtectedJsonConfigurableSource(this.protector, this.writer.Location))
            {
                await source.WriteAsync(this.writer.Configuration);
            }
        }

        #endregion
    }
}

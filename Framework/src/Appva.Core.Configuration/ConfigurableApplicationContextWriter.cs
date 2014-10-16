// <copyright file="ConfigurableApplicationContextWriter.cs" company="Appva AB">
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

    //// Interface constraints in order to make the fluent interface follow
    //// explicitly 1. To, 2a. Protect or 2b. Execute. 

    #region Constraints.

    /// <summary>
    /// Constraint which allows the To() method.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    public interface IConfigurableApplicationContextWriterTo<T>
    {
        /// <summary>
        /// Sets the location which the configurable resource should be written to.
        /// </summary>
        /// <param name="location">The path of the configurable resource</param>
        /// <returns><see cref="IConfigurableApplicationContextWriterProtectOrExecute{T}"/></returns>
        IConfigurableApplicationContextWriterProtectOrExecute<T> To(string location);
    }

    /// <summary>
    /// Constraint which allows either the Protect() or Execute() method.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    public interface IConfigurableApplicationContextWriterProtectOrExecute<T> :
        IConfigurableApplicationContextWriterExecute<T>
    {
        /// <summary>
        /// Encrypts a configurable resource.
        /// </summary>
        /// <returns><see cref="IConfigurableApplicationContextWriterExecute{T}"/></returns>
        IConfigurableApplicationContextWriterExecute<T> Protect();

        /// <summary>
        /// Encrypts a configurable resource.
        /// </summary>
        /// <param name="protect">If protect should be used</param>
        /// <returns><see cref="IConfigurableApplicationContextWriterExecute{T}"/></returns>
        IConfigurableApplicationContextWriterExecute<T> Protect(bool protect);

        /// <summary>
        /// Encrypts a configurable resource.
        /// </summary>
        /// <param name="protector">A protector implementation</param>
        /// <returns><see cref="IConfigurableApplicationContextWriterExecute{T}"/></returns>
        IConfigurableApplicationContextWriterExecute<T> Protect(IDataProtector protector);

        /// <summary>
        /// Encrypts a configurable resource.
        /// </summary>
        /// <param name="protector">A protector implementation</param>
        /// <param name="protect">If protect should be used</param>
        /// <returns><see cref="IConfigurableApplicationContextWriterExecute{T}"/></returns>
        IConfigurableApplicationContextWriterExecute<T> Protect(IDataProtector protector, bool protect);
    }

    /// <summary>
    /// Constraint which allows the Execute() method.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    public interface IConfigurableApplicationContextWriterExecute<T>
    {
        /// <summary>
        /// Writes the <see cref="IConfigurableResource"/> to disk.
        /// </summary>
        void Execute();

        /// <summary>
        /// Writes the <see cref="IConfigurableResource"/> to disk async.
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task ExecuteAsync();
    }

    #endregion

    /// <summary>
    /// Internal implemantation of a <see cref="IConfigurableResource"/> writer.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    internal sealed class ConfigurableApplicationContextWriter<T> :
        IConfigurableApplicationContextWriterTo<T>,
        IConfigurableApplicationContextWriterProtectOrExecute<T>
        where T : class, IConfigurableResource
    {
        #region Variables.

        /// <summary>
        /// The configurable resource to be serialized.
        /// </summary>
        private readonly T configuration;

        /// <summary>
        /// The location of the configurable resource.
        /// </summary>
        private string location;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurableApplicationContextWriter{T}"/> class.
        /// </summary>
        /// <param name="configuration">The configurable resource</param>
        public ConfigurableApplicationContextWriter(T configuration)
        {
            Requires.NotNull(configuration, "configuration");
            this.configuration = configuration;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the location of the configurable resource.
        /// </summary>
        public string Location
        {
            get
            {
                return this.location;
            }
        }

        /// <summary>
        /// Returns the configurable resource to be serialized.
        /// </summary>
        public T Configuration
        {
            get
            {
                return this.configuration;
            }
        }

        #endregion

        #region IConfigurableApplicationContextWriterTo<T> Members.

        /// <inheritdoc />
        IConfigurableApplicationContextWriterProtectOrExecute<T> IConfigurableApplicationContextWriterTo<T>.To(string location)
        {
            Requires.NotNullOrEmpty(location, "location");
            this.location = location;
            return this;
        }

        #endregion

        #region IConfigurableApplicationContextWriterProtectOrExecute<T> Members.

        /// <inheritdoc />
        IConfigurableApplicationContextWriterExecute<T> IConfigurableApplicationContextWriterProtectOrExecute<T>.Protect()
        {
            return new ConfigurableApplicationContextProtectedWriter<T>(this);
        }

        /// <inheritdoc />
        IConfigurableApplicationContextWriterExecute<T> IConfigurableApplicationContextWriterProtectOrExecute<T>.Protect(bool protect)
        {
            if (protect)
            {
                return new ConfigurableApplicationContextProtectedWriter<T>(this);
            }
            return this;
        }

        /// <inheritdoc />
        IConfigurableApplicationContextWriterExecute<T> IConfigurableApplicationContextWriterProtectOrExecute<T>.Protect(IDataProtector protector)
        {
            return new ConfigurableApplicationContextProtectedWriter<T>(protector, this);
        }

        /// <inheritdoc />
        IConfigurableApplicationContextWriterExecute<T> IConfigurableApplicationContextWriterProtectOrExecute<T>.Protect(IDataProtector protector, bool protect)
        {
            if (protect)
            {
                return new ConfigurableApplicationContextProtectedWriter<T>(protector, this);
            }
            return this;
        }

        #endregion

        #region IConfigurableApplicationContextWriterExecute<T> Members.

        /// <inheritdoc />
        void IConfigurableApplicationContextWriterExecute<T>.Execute()
        {
            using (var source = new JsonConfigurableSource(this.location))
            {
                source.Write<T>(this.configuration);
            }
        }

        /// <inheritdoc />
        async Task IConfigurableApplicationContextWriterExecute<T>.ExecuteAsync()
        {
            using (var source = new JsonConfigurableSource(this.location))
            {
                await source.WriteAsync<T>(this.configuration);
            }
        }

        #endregion
    }
}

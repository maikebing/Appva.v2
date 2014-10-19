// <copyright file="ConfigurableApplicationContextReader.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Core.Configuration
{
    #region Imports.

    using System;
    using System.Threading.Tasks;
    using Cryptography.DataProtection;
    using IO;
    using Validation;

    #endregion

    //// Interface constraints in order to make the fluent interface follow
    //// explicitly 1. From, 2a. AsMachineNameSpecific, 2b. Unprotect or 2c. ToObject. 

    #region Constraints.

    /// <summary>
    /// Constraint which allows the From() method.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    public interface IConfigurableApplicationContextReaderFrom<T>
    {
        /// <summary>
        /// Sets the location which the configurable resource should be read from.
        /// </summary>
        /// <param name="location">The path of the configurable resource</param>
        /// <returns>IConfigurableApplicationContextReaderUnprotectOrExecute{T}</returns>
        IConfigurableApplicationContextReaderAsMachineNameSpecificOrUnprotectOrExecute<T> From(string location);
    }

    /// <summary>
    /// Constraint which allows the AsMachineNameSpecific() method.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    public interface IConfigurableApplicationContextReaderAsMachineNameSpecificOrUnprotectOrExecute<T>
        : IConfigurableApplicationContextReaderUnprotectOrExecute<T>
    {
        /// <summary>
        /// Applies the machine name to the configurable resource (Only in DEBUG mode), e.g. 
        /// application.config becomes application.WIN-1234567.config.
        /// </summary>
        /// <returns>IConfigurableApplicationContextReaderUnprotectOrExecute{T}</returns>
        IConfigurableApplicationContextReaderUnprotectOrExecute<T> AsMachineNameSpecific();
    }

    /// <summary>
    /// Constraint which allows either Unprotect() or ToObject() method.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    public interface IConfigurableApplicationContextReaderUnprotectOrExecute<T>
        : IConfigurableApplicationContextReaderExecute<T>
    {
        /// <summary>
        /// Decrypts a configurable resource.
        /// </summary>
        /// <returns>IConfigurableApplicationContextReaderExecute{T}</returns>
        IConfigurableApplicationContextReaderExecute<T> Unprotect();

        /// <summary>
        /// Decrypts a configurable resource.
        /// </summary>
        /// <param name="unprotect">If unprotect should be used</param>
        /// <returns>IConfigurableApplicationContextReaderExecute{T}</returns>
        IConfigurableApplicationContextReaderExecute<T> Unprotect(bool unprotect);

        /// <summary>
        /// Decrypts a configurable resource.
        /// </summary>
        /// <param name="protector">A protector implementation</param>
        /// <returns>IConfigurableApplicationContextReaderExecute{T}</returns>
        IConfigurableApplicationContextReaderExecute<T> Unprotect(IDataProtector protector);

        /// <summary>
        /// Decrypts a configurable resource.
        /// </summary>
        /// <param name="protector">A protector implementation</param>
        /// <param name="unprotect">If unprotect should be used</param>
        /// <returns>IConfigurableApplicationContextReaderExecute{T}</returns>
        IConfigurableApplicationContextReaderExecute<T> Unprotect(IDataProtector protector, bool unprotect);
    }

    /// <summary>
    /// Constraint which allows the ToObject() method.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    public interface IConfigurableApplicationContextReaderExecute<T>
    {
        /// <summary>
        /// Returns the read <see cref="IConfigurableResource"/>.
        /// </summary>
        /// <returns>The {T}</returns>
        T ToObject();

        /// <summary>
        /// Returns the asynchronous read <see cref="IConfigurableResource"/>.
        /// </summary>
        /// <returns>The {T}</returns>
        Task<T> ToObjectAsync();
    }

    #endregion

    /// <summary>
    /// Internal implemantation of a <see cref="IConfigurableResource"/> reader.
    /// </summary>
    /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
    internal sealed class ConfigurableApplicationContextReader<T> :
        IConfigurableApplicationContextReaderFrom<T>,
        IConfigurableApplicationContextReaderAsMachineNameSpecificOrUnprotectOrExecute<T>,
        IConfigurableApplicationContextReaderUnprotectOrExecute<T>
        where T : class, IConfigurableResource
    {
        #region Variables.

        /// <summary>
        /// The location of the configurable resource.
        /// </summary>
        private string location;

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

        #endregion

        #region IConfigurableApplicationContextReaderFrom<T> Members.

        /// <inheritdoc />
        IConfigurableApplicationContextReaderAsMachineNameSpecificOrUnprotectOrExecute<T> IConfigurableApplicationContextReaderFrom<T>.From(string location)
        {
            Requires.NotNullOrEmpty(location, "location");
            this.location = location;
            return this;
        }

        #endregion

        #region IConfigurableApplicationContextReaderAsMachineNameSpecificOrUnprotectOrExecute<T> Members

        /// <inheritdoc />
        public IConfigurableApplicationContextReaderUnprotectOrExecute<T> AsMachineNameSpecific()
        {
            #if DEBUG
                this.location = this.location.Insert(this.location.LastIndexOf('.'), "." + Environment.MachineName);
            #endif
            return this;
        }

        #endregion

        #region IConfigurableApplicationContextReaderUnprotectOrExecute<T> Members.

        /// <inheritdoc />
        IConfigurableApplicationContextReaderExecute<T> IConfigurableApplicationContextReaderUnprotectOrExecute<T>.Unprotect()
        {
            return new ConfigurableApplicationContextProtectedReader<T>(this);
        }

        /// <inheritdoc />
        IConfigurableApplicationContextReaderExecute<T> IConfigurableApplicationContextReaderUnprotectOrExecute<T>.Unprotect(IDataProtector protector)
        {
            return new ConfigurableApplicationContextProtectedReader<T>(protector, this);
        }

        /// <inheritdoc />
        IConfigurableApplicationContextReaderExecute<T> IConfigurableApplicationContextReaderUnprotectOrExecute<T>.Unprotect(bool unprotect)
        {
            if (unprotect) 
            {
                return new ConfigurableApplicationContextProtectedReader<T>(this);
            }
            return this;
        }

        /// <inheritdoc />
        IConfigurableApplicationContextReaderExecute<T> IConfigurableApplicationContextReaderUnprotectOrExecute<T>.Unprotect(IDataProtector protector, bool unprotect)
        {
            if (unprotect)
            {
                return new ConfigurableApplicationContextProtectedReader<T>(protector, this);
            }
            return this;
        }

        #endregion

        #region IConfigurableApplicationContextReaderExecute<T> Members.

        /// <inheritdoc />
        T IConfigurableApplicationContextReaderExecute<T>.ToObject()
        {
            using (var source = new JsonConfigurableSource(this.location))
            {
                return source.Read<T>();
            }
        }

        /// <inheritdoc />
        async Task<T> IConfigurableApplicationContextReaderExecute<T>.ToObjectAsync()
        {
            using (var source = new JsonConfigurableSource(this.location))
            {
                return await source.ReadAsync<T>();
            }
        }

        #endregion
    }
}

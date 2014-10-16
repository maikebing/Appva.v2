// <copyright file="ConfigurableApplicationContext.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Core.Configuration
{
    #region Imports.

    using System;
    using System.Collections.Concurrent;

    #endregion

    /// <summary>
    /// A static implementation to read/write configurable resources, e.g. persistence configuration, authentication
    /// configuration or application configuration.
    /// </summary>
    /// <example>ConfigurableApplicationContext.Get{ConfigurableResource}()</example>
    /// <example>ConfigurableApplicationContext.Read{ConfigurableResource}().From("config.json").ToObject()</example>
    /// <example>ConfigurableApplicationContext.Read{ConfigurableResource}().From("config.json").Unprotect().ToObject()</example>
    /// <example>ConfigurableApplicationContext.Write{ConfigurableResource}(obj).To("config.json").Execute()</example>
    /// <example>ConfigurableApplicationContext.Write{ConfigurableResource}(obj).To("config.json").Protect().Execute()</example>
    public static class ConfigurableApplicationContext
    {
        #region Variabels.

        /// <summary>
        /// Internal configuration storage with 1-1 relationship. Accessable with a Get{T}().
        /// </summary>
        private static readonly ConcurrentDictionary<Type, Lazy<IConfigurableResource>> Configurations =
            new ConcurrentDictionary<Type, Lazy<IConfigurableResource>>();

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Returns a configurable implementation of <see cref="IConfigurableResource"/> from the
        /// cached configurables.
        /// </summary>
        /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
        /// <returns>Null or <see cref="IConfigurableResource"/></returns>
        public static T Get<T>() where T : IConfigurableResource
        {
            Lazy<IConfigurableResource> resource;
            if (! Configurations.TryGetValue(typeof(T), out resource))
            {
                return default(T);
            }
            return (T) resource.Value;
        }

        /// <summary>
        /// Adds a configurable implementation of <see cref="IConfigurableResource"/> to the 
        /// cached configurables. If an instance of this configurable type has previously been added
        /// it will get overritten.
        /// </summary>
        /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
        /// <param name="resource">The configurable to add</param>
        public static void Add<T>(IConfigurableResource resource)
        {
            Configurations.AddOrUpdate(typeof(T), new Lazy<IConfigurableResource>(() => resource), (key, value) => value);
        }

        /// <summary>
        /// Reads a configurable resource from a specific path.
        /// </summary>
        /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
        /// <returns>A <see cref="IConfigurableApplicationContextReaderFrom{T}"/></returns>
        public static IConfigurableApplicationContextReaderFrom<T> Read<T>()
            where T : class, IConfigurableResource
        {
            return new ConfigurableApplicationContextReader<T>();
        }

        /// <summary>
        /// Writes a configurable resource to a specific path.
        /// </summary>
        /// <typeparam name="T">An implementation of <see cref="IConfigurableResource"/></typeparam>
        /// <param name="entity">The object to be serialized</param>
        /// <returns>A <see cref="IConfigurableApplicationContextWriterTo{T}"/></returns>
        public static IConfigurableApplicationContextWriterTo<T> Write<T>(T entity)
            where T : class, IConfigurableResource
        {
            return new ConfigurableApplicationContextWriter<T>(entity);
        }

        #endregion
    }
}
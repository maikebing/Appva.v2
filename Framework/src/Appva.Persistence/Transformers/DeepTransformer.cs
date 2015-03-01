// <copyright file="DeepTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
namespace Appva.Persistence.Transformers
{
    #region Imports.

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// A deep result transformer. 
    /// Aliases with the '.' represents complex IPersistentEntity chain.
    /// <externalLink>
    ///     <linkText>Stackoverflow answer</linkText>
    ///     <linkUri>
    ///         http://stackoverflow.com/questions/26893475/how-to-partially-project-a-child-object-with-many-fields-in-nhibernate
    ///     </linkUri>
    /// </externalLink>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public class DeepTransformer<TEntity> : IResultTransformer where TEntity : class
    {
        #region IResultTransformer Members.

        /// <inheritdoc />
        public IList TransformList(IList collection)
        {
            return Transformers.AliasToBean<TEntity>().TransformList(collection);
        }

        /// <inheritdoc />
        public object TransformTuple(object[] tuple, string[] aliases)
        {
            var list = new List<string>(aliases);
            var propertyAliases = new List<string>(list);
            var complexAliases = new List<string>();
            for (var i = 0; i < list.Count; i++)
            {
                var aliase = list[i];
                if (aliase.Contains('.'))
                {
                    complexAliases.Add(aliase);
                    propertyAliases[i] = null;
                }
            }
            var result = Transformers.AliasToBean<TEntity>().TransformTuple(tuple, propertyAliases.ToArray());
            this.TransformPersistentChain(tuple, complexAliases, result, list);
            return result;
        }

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Transforms complex chains.
        /// </summary>
        /// <param name="tuple">The tuple array</param>
        /// <param name="complexAliases">List of complex aliases</param>
        /// <param name="result">The simple transformed result</param>
        /// <param name="list">The aliases</param>
        protected virtual void TransformPersistentChain(object[] tuple, List<string> complexAliases, object result, List<string> list)
        {
            var entity = (TEntity) result;
            foreach (var aliase in complexAliases)
            {
                var index = list.IndexOf(aliase);
                var value = tuple[index];
                if (value == null)
                {
                    continue;
                }
                var parts = aliase.Split('.');
                var name = parts[0];
                var propertyInfo = entity.GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                object currentObject = entity;
                var current = 1;
                while (current < parts.Length)
                {
                    name = parts[current];
                    object instance = propertyInfo.GetValue(currentObject);
                    if (instance == null)
                    {
                        instance = Activator.CreateInstance(propertyInfo.PropertyType, true);
                        propertyInfo.SetValue(currentObject, instance);
                    }
                    propertyInfo = propertyInfo.PropertyType.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                    currentObject = instance;
                    current++;
                }
                var dictionary = currentObject as IDictionary;
                if (dictionary != null)
                {
                    dictionary[name] = value;
                }
                else
                {
                    propertyInfo.SetValue(currentObject, value);
                }
            }
        }

        #endregion
    }
}
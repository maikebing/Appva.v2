// <copyright file="DeepTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
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
    /// http://stackoverflow.com/questions/26893475/how-to-partially-project-a-child-object-with-many-fields-in-nhibernate
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DeepTransformer<TEntity> : IResultTransformer where TEntity : class
    {
        // rows iterator
        public object TransformTuple(object[] tuple, string[] aliases)
        {
            var list = new List<string>(aliases);

            var propertyAliases = new List<string>(list);
            var complexAliases = new List<string>();

            for (var i = 0; i < list.Count; i++)
            {
                var aliase = list[i];
                // Aliase with the '.' represents complex IPersistentEntity chain
                if (aliase.Contains('.'))
                {
                    complexAliases.Add(aliase);
                    propertyAliases[i] = null;
                }
            }

            // be smart use what is already available
            // the standard properties string, valueTypes
            var result = Transformers
                 .AliasToBean<TEntity>()
                 .TransformTuple(tuple, propertyAliases.ToArray());

            this.TransformPersistentChain(tuple, complexAliases, result, list);

            return result;
        }

        /// <summary>Iterates the Path Client.Address.City.Code </summary>
        protected virtual void TransformPersistentChain(object[] tuple
              , List<string> complexAliases, object result, List<string> list)
        {
            var entity = result as TEntity;

            foreach (var aliase in complexAliases)
            {
                // the value in a tuple by index of current Aliase
                var index = list.IndexOf(aliase);
                var value = tuple[index];
                if (value == null)
                {
                    continue;
                }

                // split the Path into separated parts
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

                // even dynamic objects could be injected this way
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

        // convert to DISTINCT list with populated Fields
        public System.Collections.IList TransformList(System.Collections.IList collection)
        {
            var results = Transformers.AliasToBean<TEntity>().TransformList(collection);
            return results;
        }
    }
}
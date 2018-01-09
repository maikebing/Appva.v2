// <copyright file="Meta.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Appva.Html.Infrastructure;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class Annotation
    {
        #region Variables.

        /// <summary>
        /// The internal attribtue cache.
        /// </summary>
        /// <remarks>
        /// ConcurrentDictionary in Net 4.6 does not implement IReadOnlyDictionary.
        /// </remarks>
        private readonly static ConcurrentDictionary<Type, Dictionary<string, List<Attribute>>> Cache = new ConcurrentDictionary<Type, Dictionary<string, List<Attribute>>>();

        #endregion

        public static V Find<V, T>(Expression<Action<T>> expression) where V : Attribute
        {
            return Find<V>(typeof(T), (expression.Body as MethodCallExpression).Method.Name);
        }

        public static V Find<V>(Type type, string methodName) where V : Attribute
        {
            return FindAll(type, methodName).FirstOrDefault(x => x is V) as V;
        }

        public static IReadOnlyList<Attribute> FindAll<T>(Expression<Action<T>> expression)
        {
            return FindAll(typeof(T), (expression.Body as MethodCallExpression).Method.Name);
        }

        public static IReadOnlyList<Attribute> FindAll(Type type, string methodName)
        {
            Argument.Guard.NotNull ("type", type);
            Argument.Guard.NotEmpty("methodName", methodName);
            List<Attribute> retval = null;
            GetOrCacheType(type).TryGetValue(methodName, out retval);
            return (IReadOnlyList<Attribute>) retval ?? new List<Attribute>();
        }

        private static IDictionary<string, List<Attribute>> GetOrCacheType(Type key)
        {
            var retval = new Dictionary<string, List<Attribute>>();
            if (Cache.ContainsKey(key))
            {
                return Cache[key];
            }
            MemberInfo[] members;
            if (key.IsEnum)
            {
                members = key
                    .GetFields(BindingFlags.GetField | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static)
                    .Cast<MemberInfo>().ToArray();
            }
            else
            {
                members =
                    key.GetFields(BindingFlags.Public).Cast<MemberInfo>()
                        .Concat(key.GetMembers   (BindingFlags.Public))
                        .Concat(key.GetFields    (BindingFlags.Public))
                        .Concat(key.GetProperties(BindingFlags.Public))
                        .Concat(key.GetMethods   (BindingFlags.Public))
                        .Concat(key.GetInterfaces()/*.Where(x => x.IsAssignableFrom(typeof(IHtmlAttribute)))*/.SelectMany(x => x.GetMethods()))
                    .ToArray();
            }
            if (members.Length == 0)
            {
                return Cache.GetOrAdd(key, retval);
            }
            foreach (var member in members)
            {
                var attributes = member.GetCustomAttributes<Attribute>(true);
                if (attributes == null || attributes.Count() == 0)
                {
                    if (retval.ContainsKey(member.Name))
                    {
                        continue;
                    }
                    retval.Add(member.Name, new List<Attribute>());
                    continue;
                }
                foreach(var attribute in attributes)
                {
                    if (! retval.ContainsKey(member.Name))
                    {
                        retval.Add(member.Name, new List<Attribute>());
                    }
                    retval[member.Name].Add(attribute);
                }
            }
            Cache.TryAdd(key, retval);
            return retval;
        }
    }
}
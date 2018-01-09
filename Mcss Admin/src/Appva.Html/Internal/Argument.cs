// <copyright file="Argument.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    internal static class Error
    {
        public static ArgumentNullException ArgumentNull()
        {
            return new ArgumentNullException();
        }

        public static ArgumentException ArgumentEmpty()
        {
            return new ArgumentException();
        }

        public static ArgumentOutOfRangeException Range()
        {
            return new ArgumentOutOfRangeException();
        }
    }
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class Argument
    {

        public static class Guard
        {
            public static void Contains<TKey, TValue>(string parameterName, TKey key, IDictionary<TKey, TValue> dictionary)
            {
                if (dictionary.ContainsKey(key))
                {
                    return;
                }
                throw Error.ArgumentEmpty();
            }

            public static void NotNull<T>(string parameterName, T obj)
            {
                if (obj != null)
                {
                    return;
                }
                throw Error.ArgumentNull();
            }

            public static void NotEmpty(string parameterName, string str)
            {
                if (string.IsNullOrWhiteSpace(str) == false)
                {
                    return;
                }
                throw Error.ArgumentEmpty();
            }

            public static void NotEmpty<T>(string parameterName, IEnumerable<T> collection)
            {
                NotNull(parameterName, collection);
                if (collection != null && collection.Count() > 0)
                {
                    return;
                }
                throw Error.ArgumentEmpty();
            }

            public static void OneOf<T>(string parameterName, T value, IEnumerable<T> collection)
            {
                if (collection.Contains(value))
                {
                    return;
                }
                throw Error.Range();
            }
        }

        

        public static void Null<T>(T obj, string parameterName)
        {
            if (obj != null)
            {
                return;
            }
            throw new ArgumentNullException(parameterName, "");
        }

        public static void Empty(string str, string parameterName)
        {
            if (! string.IsNullOrWhiteSpace(str))
            {
                return;
            }
            throw new ArgumentException(parameterName + " " + "is null, empty (\"\") or consists only of whitespace characters.", parameterName);
        }
    }
}
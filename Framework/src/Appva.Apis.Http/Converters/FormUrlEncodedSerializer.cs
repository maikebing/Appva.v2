// <copyright file="FormUrlSerializer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Apis.Http.Converters
{
    #region Imports.

    using Appva.Apis.Http.Attributes;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class FormUrlEncodedSerializer
    {
        #region Static members

        public static FormUrlEncodedContent Serialize<T>(object content) where T : class
        {
            var d = new Dictionary<string, string>();
            var type = typeof(T);

            foreach (var p in type.GetProperties())
            {
                var key = GetProperyName(p);
                if (!d.ContainsKey(key))
                {
                    d.Add(key, p.GetValue(content).ToString());
                }
            }

            return new FormUrlEncodedContent(d);
        }

        #endregion

        #region Private helpers

        private static string GetProperyName(PropertyInfo p)
        {
            var a = p.GetCustomAttribute<FormUrlEncodedPropertyAttribute>();
            if (a != null && a.PropertyName.IsNotEmpty())
            {
                return a.PropertyName;
            }
            return p.Name;
        }

        #endregion
    }
}
// <copyright file="TegBuilderExtensions.cs" company="Appva AB">
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

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class TegBuilderExtensions
    {
        public static void AddStyle(this TagBuilder builder, string style)
        {
            Argument.Guard.NotEmpty("style", style);
            var attributes = builder.Attributes;
            if (! attributes.ContainsKey("style"))
            {
                attributes["style"] = style.Replace(";", "") + ";";
                return;
            }
            var styles = attributes["style"].Split(';').ToList();
            styles.Add(style);
            attributes["style"] = string.Join(";", styles);
        }

        /// <summary>
        /// Removes the element with the specified key from the 
        /// <see cref="System.Collections.Generic.IDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        public static void RemoveAttribute(this TagBuilder builder, string key)
        {
            Argument.Guard.NotEmpty("key", key);
            if (! builder.Attributes.ContainsKey(key))
            {
                return;
            }
            builder.Attributes.Remove(key);
        }
    }
}
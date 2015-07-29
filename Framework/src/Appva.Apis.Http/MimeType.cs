// <copyright file="MimeTypes.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Apis.Http
{
    #region Imports.

    using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class MimeTypeHelper
    {
        public static string Description(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
    }

    public enum MimeType
    {
        [Description("application/json")]
        Json,

        [Description("application/xml")]
        Xml,

        [Description("application/x-www-form-urlencoded")]
        FormUrlEncoded,

        [Description("text/plain")]
        PlainText
    }
}
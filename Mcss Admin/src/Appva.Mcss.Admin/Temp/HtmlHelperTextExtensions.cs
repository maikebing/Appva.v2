// <copyright file="Text.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin
{
    #region Imports.

    using System.Resources;
    using System.Web.Mvc;
    using Appva.Core.Extensions;


    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public static class HtmlHelperTextExtensions
    {

        #region Static members

        public static string TextHelper(this HtmlHelper helper, string key)
        {
            var lang = new ResourceManager("Appva.Mcss.Admin.Resources.Language",System.Reflection.Assembly.GetExecutingAssembly());
            var text = lang.GetString(key);
            if (text.IsEmpty())
            {
                return key.Replace('_', ' ');
            }

            return text;
        }

        

        #endregion
    }
}
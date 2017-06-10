// <copyright file="I18nUtils.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Application.Utils.i18n
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class I18nUtils
    {
        #region Constants.

        /// <summary>
        /// The email templates path
        /// </summary>
        private const string emailTemplatesPath = "~/Features/Shared/EmailTemplates";

        #endregion
        
        #region Static members.

        /// <summary>
        /// Checks if language-specific templeate exists, else using default.
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string GetEmailTemplatePath(string template)
        {
            var langCode = CultureInfo.CurrentCulture.Name;
            var langSpecificPath = string.Format("{0}/{1}", langCode, template);
            var relativePath = string.Format("{0}/{1}.cshtml", emailTemplatesPath, langSpecificPath);
            var absolutePath = HttpContext.Current.Server.MapPath(relativePath);

            if(File.Exists(absolutePath))
            {
                return langSpecificPath;
            }

            return template;
        }

        #endregion
    }
}
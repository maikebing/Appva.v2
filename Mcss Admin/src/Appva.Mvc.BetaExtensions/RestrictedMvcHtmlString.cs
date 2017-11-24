// <copyright file="RestrictedMvcHtmlString.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.BetaMvc
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class RestrictedMvcHtmlString : IDisposable
    {

        #region Fields. 
        
        /// <summary>
        /// The HTML helper
        /// </summary>
        private readonly HtmlHelper htmlHelper;

        /// <summary>
        /// The string builder
        /// </summary>
        private StringBuilder stringBuilder;

        /// <summary>
        /// The string builder backup
        /// </summary>
        private StringBuilder stringBuilderBackup;

        /// <summary>
        /// The link
        /// </summary>
        private MvcHtmlString endTag;

        /// <summary>
        /// The allowed to write
        /// </summary>
        private bool allowedToWrite;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictedMvcHtmlString"/> class.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="startTag">The start tag.</param>
        /// <param name="endTag">The end tag.</param>
        /// <param name="allowedToWrite">if set to <c>true</c> [allowed to write].</param>
        public RestrictedMvcHtmlString(HtmlHelper htmlHelper, MvcHtmlString startTag, MvcHtmlString endTag, bool allowedToWrite)
        {
            this.htmlHelper     = htmlHelper;
            this.allowedToWrite = allowedToWrite;

            if(!allowedToWrite)
            {
                BackupCurrentContent();
            }
            else
            {
                this.endTag = endTag;
                htmlHelper.ViewContext.Writer.Write(startTag);
            }
        }

        #endregion

        #region Private members.

        /// <summary>
        /// Backups the content of the current.
        /// </summary>
        private void BackupCurrentContent()
        {
            // make backup of current buffered content
            this.stringBuilder = ((StringWriter)htmlHelper.ViewContext.Writer).GetStringBuilder();
            this.stringBuilderBackup = new StringBuilder().Append(this.stringBuilder);
        }

        /// <summary>
        /// Denies the content.
        /// </summary>
        private void DenyContent()
        {
            // restore buffered content backup (destroying any buffered content since Restricted object initialization)
            this.stringBuilder.Length = 0;
            this.stringBuilder.Append(stringBuilderBackup);
        }

        #endregion

        #region IDisposable members.

        /// <inheritdoc />
        public void Dispose()
        {
            if (!this.allowedToWrite)
            {
                DenyContent();
            }
            this.htmlHelper.ViewContext.Writer.Write(this.endTag);

            
        }

        #endregion
    }
}
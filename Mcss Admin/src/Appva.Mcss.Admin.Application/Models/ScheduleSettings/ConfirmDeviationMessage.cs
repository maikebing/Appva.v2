// <copyright file="ConfirmDeviationMessage.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ConfirmDeviationMessage
    {
        #region Constants.

        /// <summary>
        /// The default header
        /// </summary>
        private const string DefaultHeader = "Kontakta sjuksköterska";

        /// <summary>
        /// The default message
        /// </summary>
        private const string DefaultMessage = "Du måste kontakta sjuksköterska vid avvikelse.";
        
        /// <summary>
        /// The message as html string
        /// </summary>
        private const string HtmlFormat = "<h2>{0}</h2><form method='post' action='#'><div class='text'><p>{1}";

        #endregion

        #region Fields.

        /// <summary>
        /// The header
        /// </summary>
        private string header;

        /// <summary>
        /// The message
        /// </summary>
        private string message;


        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmDeviationMessage"/> class.
        /// </summary>
        public ConfirmDeviationMessage()
        {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmDeviationMessage"/> class.
        /// </summary>
        /// <param name="htmlString">The Html string</param>
        public ConfirmDeviationMessage(string htmlString) : this(htmlString, false)
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfirmDeviationMessage"/> class.
        /// </summary>
        /// <param name="htmlString">The Html string</param>
        /// <param name="includeListOfContacts">If a list of contacts should be included</param>
        public ConfirmDeviationMessage(string htmlString, bool includeListOfContacts)
        {
            
            if (htmlString.IsEmpty())
            {
                return;
            }

            var strings = htmlString.Split(new string[] { "</h2><form method='post' action='#'><div class='text'><p>" }, StringSplitOptions.RemoveEmptyEntries);
            if (strings.Count() == 2)
            {
                this.header = strings[0].Replace("<h2>", string.Empty);
                this.message = strings[1];
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// The notice-header
        /// </summary>
        public string Header 
        {
            get
            {
                if (this.header == null)
                {
                    this.header = DefaultHeader;
                }
                return this.header;
            } 
            set
            {
                this.header = value;
            }
        }

        /// <summary>
        /// The notice message
        /// </summary>
        public string Message 
        {
            get
            {
                if (this.message == null)
                {
                    this.message = DefaultMessage;
                }
                return this.message;
            }
            set
            {
                this.message = value;
            } 
        }

        /// <summary>
        /// If list of nurses to contact should be included
        /// </summary>
        public bool IncludeListOfNurses
        {
            get;
            set;
        }

        #endregion

        #region Methods.

        /// <summary>
        /// Returns the message as a HTML-string
        /// E.g <h2>Kontakta sjuksköterska</h2><form method='post' action='#'><div class='text'><p>Du måste kontakta sjuksköterska vid avvikelse.
        /// </summary>
        /// <returns></returns>
        public string ToHtmlString() 
        {
            return string.Format(HtmlFormat, header, message);
        }

        #endregion
    }
}
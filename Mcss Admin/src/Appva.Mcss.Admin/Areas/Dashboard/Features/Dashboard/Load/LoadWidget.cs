// <copyright file="LoadWidget.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Core.Extensions;
    using Appva.Cryptography;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class LoadWidget
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadWidget"/> class.
        /// </summary>
        /// <param name="header">The header</param>
        /// <param name="action">the action route</param>
        /// <param name="controller">the controller route</param>
        private LoadWidget(string header, string action, string controller)
        {
            this.Id = Hash.Random().ToUrlSafeBase64();
            this.Header = header;
            this.Action = action;
            this.Controller = controller;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="LoadWidget"/> class.
        /// </summary>
        /// <param name="header">The header</param>
        /// <param name="action">the action route</param>
        /// <param name="controller">the controller route</param>
        public static LoadWidget CreateNew(string header, string action, string controller)
        {
            return new LoadWidget(header, action, controller);
        }

        #endregion

        /// <summary>
        /// The div element ID.
        /// </summary>
        public string Id
        {
            get;
            private set;
        }

        /// <summary>
        /// The header/title.
        /// </summary>
        public string Header
        {
            get;
            private set;
        }

        /// <summary>
        /// The route action.
        /// </summary>
        public string Action
        {
            get;
            private set;
        }

        /// <summary>
        /// The route controller.
        /// </summary>
        public string Controller
        {
            get;
            private set;
        }
    }
}
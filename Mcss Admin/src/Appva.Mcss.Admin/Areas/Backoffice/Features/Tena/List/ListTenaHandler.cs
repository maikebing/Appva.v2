﻿// <copyright file="ListTenaHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    #region Imports.

    using System;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Persistence;
    using Infrastructure.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListTenaHandler : RequestHandler<Parameterless<ListTenaModel>, ListTenaModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListTenaHandler"/> class.
        /// </summary>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/>.</param>
        public ListTenaHandler(ISettingsService settingsService, IPersistenceContext persistenceContext)
        {
            this.settingsService = settingsService;
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region RequestHandlers overrides.

        /// <inheritdoc />
        public override ListTenaModel Handle(Parameterless<ListTenaModel> message)
        {
            var tenaSettings = this.settingsService.Find(ApplicationSettings.TenaSettings);

            return new ListTenaModel
            {
                ClientIdMask = Mask(tenaSettings.ClientId),
                ClientSecretMask = Mask(tenaSettings.ClientSecret),
                BaseAddress = System.Configuration.ConfigurationManager.AppSettings["TenaAPI.ServerUrl"],
                IsInstalled = tenaSettings.IsInstalled
            };
        }

        #endregion

        #region Private methods.

        /// <inheritdoc />
        private string Mask(string text)
        {
            var mask = string.Empty;

            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            var foundDash = false;

            for (int i = text.Length - 1; i >= 0; i--)
            {
                if(text[i] == '-')
                {
                    mask += '-';
                    foundDash = true;
                }

                if(foundDash && text[i] != '-')
                {
                    mask += "X";
                }
                else if(foundDash == false && text[i] != '-')
                {
                    mask += text[i];
                }
            }

            var array = mask.ToCharArray();
            Array.Reverse(array);

            return new string(array);
        }

        #endregion
    }
}
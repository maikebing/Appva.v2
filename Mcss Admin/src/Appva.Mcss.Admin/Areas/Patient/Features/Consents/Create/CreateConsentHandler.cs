// <copyright file="CreateConsentHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Hip;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class CreateConsentHandler : RequestHandler<Identity<CreateConsent>, CreateConsent>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IPatientTransformer"/>.
        /// </summary>
        private readonly IPatientTransformer transformer;

        /// <summary>
        /// The <see cref="IHipClient"/>
        /// </summary>
        private readonly IHipClient hipClient;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateConsentHandler"/> class.
        /// </summary>
        public CreateConsentHandler(IPersistenceContext persistence, IPatientTransformer transformer, IHipClient hipClient)
        {
            this.persistence = persistence;
            this.transformer = transformer;
            this.hipClient = hipClient;
        }

        #endregion

        #region RequestHandler overrides

        public override CreateConsent Handle(Identity<CreateConsent> message)
        {
            var patient = this.transformer.ToPatient(this.persistence.Get<Patient>(message.Id));
            var consents = this.hipClient.Consents.GetAsync("191212121212").Result;

            return new CreateConsent
            {
                Patient = patient,
                Consents = consents,
                ValidPdlExists = consents.Pdl.Valid && consents.Pdl.Allowed
            };
        }

        #endregion
    }
}
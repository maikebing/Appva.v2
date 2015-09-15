// <copyright file="CreateConsentPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Models
{
    #region Imports.

    using Appva.Hip;
    using Appva.Mvc;
    using Appva.Core.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateConsentPublisher : ContextRequestHandler<CreateConsent, bool>
    {
        #region Fields

        /// <summary>
        /// The <see cref="IHipClient"/>
        /// </summary>
        private readonly IHipClient hipClient;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateConsentPublisher"/> class.
        /// </summary>
        public CreateConsentPublisher(HttpContextBase context, IHipClient hipClient)
            :base(context)
        {
            this.hipClient = hipClient;
        }

        #endregion

        #region Overrides

        public override bool Handle(CreateConsent message)
        {
            var result = false;
            if (!message.ValidPdlExists && message.PdlValidTo != null)
            {
                if (message.DruglistOngoingAccess || message.DruglistSingelAccess)
                {
                    result = this.hipClient.Consents.SetConsentsAsync("191212121212", message.DruglistOngoingAccess, message.PdlOnlyMe, message.PdlValidTo).Result;
                }
                else
                {
                    result = this.hipClient.Consents.SetPdlConsentAsync("191212121212", message.PdlOnlyMe, message.PdlValidTo).Result;
                }
            }
            else if (message.DruglistOngoingAccess || message.DruglistSingelAccess)
            {
                result = this.hipClient.Consents.SetDruglistConsentAsync("191212121212", message.DruglistOngoingAccess).Result;
            }

            if (result && message.Referer.IsNotEmpty())
            {
                this.Redirect(message.Referer);
            }
            return result;
        }

        #endregion
    }
}
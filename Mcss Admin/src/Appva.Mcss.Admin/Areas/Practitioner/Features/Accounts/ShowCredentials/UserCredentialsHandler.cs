// <copyright file="UserCredentialsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Practitioner.Features.Accounts.ShowCredentials
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Practitioner.Models;
    using Appva.Mcss.Admin.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class UserCredentialsHandler : RequestHandler<Identity<UserCredentialsModel>, UserCredentialsModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCredentialsHandler"/> class.
        /// </summary>
        public UserCredentialsHandler()
        {
        }

        #endregion

        #region RequestHandler Members.

        /// <inheritdoc />
        public override UserCredentialsModel Handle(Identity<UserCredentialsModel> message)
        {
            return new UserCredentialsModel();
        }

        #endregion
    }
}
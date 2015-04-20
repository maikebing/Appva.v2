// <copyright file="CreateAccountViewHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Accounts.Create.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateAccountViewHandler : IRequestHandler<CreateAccountModel, CreateAccountModel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateAccountViewHandler"/> class.
        /// </summary>
        public CreateAccountViewHandler()
        {
        }

        #endregion

        #region IRequestHandler<CreateAccountModel,CreateAccountModel> Members

        public CreateAccountModel Handle(CreateAccountModel message)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IRequestHandler Members

        public object Handle(object message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
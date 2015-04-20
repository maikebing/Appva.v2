// <copyright file="AbstractMediatorController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Infrastructure.Controllers
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
    internal abstract class AbstractMediatorController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractMediatorController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        protected AbstractMediatorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        #endregion

        #region Protected Methods.

        protected T ExecuteCommand<T>(object any)
        {
            return default(T);
        }

        #endregion
    }
}
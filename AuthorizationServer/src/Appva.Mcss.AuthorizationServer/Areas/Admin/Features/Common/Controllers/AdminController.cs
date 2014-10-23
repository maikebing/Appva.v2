// <copyright file="AdminController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Areas.Admin.Controllers
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// Base admin area controller.
    /// </summary>
    [Authorize, RouteArea("admin")]
    public class AdminController : MediatorController
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="mediator">The <see cref="IMediator"/></param>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public AdminController(IMediator mediator, IPersistenceContext persistenceContext)
            : base(mediator, persistenceContext)
        {
        }

        #endregion
    }
}
// <copyright file="MenusController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Menus
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Infrastructure.Controllers;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize, RoutePrefix("menus")]
    public sealed class MenusController : AbstractMediatorController
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MenusController"/> class.
        /// </summary>
        /// <param name="tenants">The <see cref="IMediator"/> implementation</param>
        public MenusController(IMediator mediator)
            : base(mediator)
        {
        }

        #endregion

        #region Routes.

        #region Menu.

        /// <summary>
        /// Return the menu list partial view.
        /// </summary>
        /// <returns>A <see cref="IMenuList{IMenuItem}"/></returns>
        [Route("render")]
        [ChildActionOnly]
        public PartialViewResult Menu(Menu request)
        {
            return this.PartialView(request.PartialView, this.ExecuteCommand(request));
        }

        #endregion

        #endregion
    }
}
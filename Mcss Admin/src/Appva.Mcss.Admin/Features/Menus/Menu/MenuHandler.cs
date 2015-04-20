// <copyright file="MenuHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Menus
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services.Menus;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MenuHandler : RequestHandler<Menu, IMenuList<IMenuItem>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/> dispatcher.
        /// </summary>
        private readonly IMenuService menus;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuHandler"/> class.
        /// </summary>
        public MenuHandler(IMenuService menus)
        {
            this.menus = menus;
        }

        #endregion

        #region RequestHandler<Menu, IMenuList<IMenuItem>> Members.

        /// <inheritdoc />
        public override IMenuList<IMenuItem> Handle(Menu message)
        {
            return menus.Render(message.Key, message.Action, message.Controller, message.Area);
        }

        #endregion
    }
}
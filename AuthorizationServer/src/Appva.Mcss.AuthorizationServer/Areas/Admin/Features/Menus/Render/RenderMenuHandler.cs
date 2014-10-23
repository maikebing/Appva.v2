// <copyright file="RenderMenuHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Mcss.AuthorizationServer.Domain.Services;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class RenderMenuHandler : PersistentRequestHandler<RenderMenuId, IEnumerable<IMenuNode>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMenuService"/>.
        /// </summary>
        private readonly IMenuService menuService;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderMenuHandler"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public RenderMenuHandler(IMenuService menuService, IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
            this.menuService = menuService;
        }

        #endregion

        #region Overrides.

        /// <inheritdoc />
        public override IEnumerable<IMenuNode> Handle(RenderMenuId message)
        {
            return this.menuService.Render(menuKey: message.MenuKey, uri: message.Uri);
        }

        #endregion
    }
}
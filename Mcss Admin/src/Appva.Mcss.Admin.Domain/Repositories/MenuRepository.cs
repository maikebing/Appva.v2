// <copyright file="MenuRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface IMenuRepository : IRepository
    {
        /// <summary>
        /// Returns a collection of <see cref="MenuLink"/> by the <see cref="Menu.Key"/>.
        /// </summary>
        /// <param name="menuKey">The unique <c>Menu</c> identifier</param>
        /// <returns>A collection of <see cref="MenuLink"/></returns>
        IList<MenuLink> ListByMenu(string menuKey);
    }

    /// <summary>
    /// 
    /// </summary>
    public sealed class MenuRepository : IMenuRepository
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> instance.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuRepository"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public MenuRepository(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IMenuRepository Members.

        /// <inheritdoc />
        public IList<MenuLink> ListByMenu(string menuKey)
        {
            return this.persistenceContext.QueryOver<MenuLink>()
                    .OrderBy(x => x.Sort).Asc
                    .JoinQueryOver<Menu>(x => x.Menu)
                    .Where(x => x.Key == menuKey)
                    .List();
        }

        #endregion
    }
}
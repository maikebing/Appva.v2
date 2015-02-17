// <copyright file="MenuService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Caching;
    using Appva.Core.Extensions;
    using Appva.Mcss.AuthorizationServer.Domain.Entities;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public interface IMenuService
    {
        /// <summary>
        /// Renders the menu by key and sets the selected menu
        /// node by the uri path.
        /// </summary>
        /// <param name="menuKey">The unique menu key</param>
        /// <param name="uri">The current page uri</param>
        /// <returns>An <see cref="IEnumerable{IMenuNode}"/></returns>
        IEnumerable<IMenuNode> Render(string menuKey, Uri uri);
    }

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MenuService : IMenuService
    {
        #region Variables.

        /// <summary>
        /// The menu cache.
        /// </summary>
        private static readonly MemoryCache Cache = MemoryCache.Default;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuService"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public MenuService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region IMenuService Members

        /// <inheritdoc/>
        public IEnumerable<IMenuNode> Render(string menuKey, Uri uri)
        {
            return this.FindByMenuKey(menuKey, uri);
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuKey"></param>
        private IEnumerable<IMenuNode> FindByMenuKey(string menuKey, Uri uri)
        {
            if (Cache.Contains(menuKey))
            {
                return this.BuildTree(menuKey, uri);
            }
            else
            {
                CachedMenuLink menuLink = null;
                var cachableNodes = this.persistenceContext.QueryOver<MenuLink>()
                   .JoinQueryOver(x => x.Menu).Where(x => x.Key == menuKey)
                   .Select(Projections.ProjectionList()
                       .Add(Projections.Property<MenuLink>(x => x.Id).WithAlias(() => menuLink.Id))
                       .Add(Projections.Property<MenuLink>(x => x.Label).WithAlias(() => menuLink.Label))
                       .Add(Projections.Property<MenuLink>(x => x.Url).WithAlias(() => menuLink.Url))
                       .Add(Projections.Property<MenuLink>(x => x.Sort).WithAlias(() => menuLink.Sort))
                       .Add(Projections.Property<MenuLink>(x => x.CssClass).WithAlias(() => menuLink.CssClass))
                       .Add(Projections.Property<MenuLink>(x => x.Parent.Id).WithAlias(() => menuLink.ParentId))
                       .Add(Projections.Property<MenuLink>(x => x.Permission.Id).WithAlias(() => menuLink.PermissionId))
                   ).TransformUsing(Transformers.AliasToBean<CachedMenuLink>())
                   .List<CachedMenuLink>();
                if (cachableNodes.IsNotNull())
                {
                    Cache.Add(new CacheItem(menuKey, cachableNodes), new CacheItemPolicy { Priority = CacheItemPriority.Default });
                    return this.BuildTree(menuKey, uri);
                }
            }
            return new List<IMenuNode>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="menuKey"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private IEnumerable<IMenuNode> BuildTree(string menuKey, Uri uri)
        {
            //// Get the local path as lower case.
            var url = uri.LocalPath.ToLower();
            //// Flatten the cache by creating a new dictionary{guid, CachableMenuLink}.
            var flattenedCacheCopy = ((IList<CachedMenuLink>) Cache.Get(menuKey))
                .ToDictionary(x => x.Id, x => new CachedMenuLink(x.Id, x.Label, x.Url, x.Sort, x.CssClass, x.ParentId, x.PermissionId));
            //// TODO Remove any links the user has no rights to view.
            //// this.ShowOnlyPermittedLinks(flattenedCacheCopy);
            //// Mark any descendant links as selected if CachableMenuLink.url.Equals(url).
            this.MarkAsSelected(flattenedCacheCopy, url);
            //// Build the tree.
            return this.BuildMenuLinkTree(flattenedCacheCopy.Values.ToList());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="url"></param>
        private void MarkAsSelected(IDictionary<Guid, CachedMenuLink> cache, string url)
        {
            var selectedMenuLink = cache.Where(x => this.IsSelected(x.Value.Url, url)).Select(x => x.Value).FirstOrDefault();
            if (selectedMenuLink.IsNotNull())
            {
                this.MarkAnyDescendantsAsSelected(selectedMenuLink.Id, cache);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="cache"></param>
        private void MarkAnyDescendantsAsSelected(Guid parentId, IDictionary<Guid, CachedMenuLink> cache)
        {
            while (true)
            {
                if (parentId.IsEmpty())
                {
                    return;
                }
                if (! cache.ContainsKey(parentId))
                {
                    return;
                }
                cache[parentId].IsSelected = true;
                parentId = cache[parentId].ParentId;
            }
        }

        /// <summary>
        /// Builds and sorts the list of menu link items.
        /// </summary>
        /// <param name="links">The full set of menu links</param>
        /// <returns>An <see cref="IEnumerable{MenuNode}"/></returns>
        private IEnumerable<IMenuNode> BuildMenuLinkTree(IEnumerable<CachedMenuLink> links)
        {
            var cachedMenuLinks = links as IList<CachedMenuLink> ?? links.ToList();
            var roots = cachedMenuLinks.Where(x => x.ParentId.IsEmpty()).OrderBy(x => x.Sort);
            return roots.Select(root => new MenuNode()
            {
                Label = root.Label, Url = root.Url, IsSelected = root.IsSelected, CssClass = root.CssClass, Children = this.BuildMenuLinkTree(root, cachedMenuLinks)
            }).Cast<IMenuNode>().ToList();
        }

        /// <summary>
        /// Builds and sorts child items of menu link parents.
        /// </summary>
        /// <param name="link">The parent menu link</param>
        /// <param name="links">The full set of menu links</param>
        /// <returns>An <see cref="IEnumerable{MenuNode}"/></returns>
        private IEnumerable<MenuNode> BuildMenuLinkTree(CachedMenuLink link, IEnumerable<CachedMenuLink> links)
        {
            var cachedMenuLinks = links as IList<CachedMenuLink> ?? links.ToList();
            var children = cachedMenuLinks.Where(x => x.ParentId.IsNotEmpty() && x.ParentId.Equals(link.Id)).OrderBy(x => x.Sort);
            return children.Select(child => new MenuNode()
            {
                Label = child.Label,
                Url = child.Url,
                IsSelected = child.IsSelected,
                CssClass = child.CssClass,
                Children = this.BuildMenuLinkTree(child, cachedMenuLinks)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryUrl"></param>
        /// <returns></returns>
        private bool IsSelected(string url, string queryUrl)
        {
            if (url.IsEmpty() || queryUrl.IsEmpty())
            {
                return false;
            }
            return url.Equals(queryUrl);
            /*var iterations = queryUrl.Split('/').Length;
            if (iterations == 0)
            {
                return false;
            }
            for (var i = 0; i < iterations; i++)
            {
                if (queryUrl.Equals(url.ToLower()))
                {
                    return true;
                }
                queryUrl = queryUrl.StripLast('/');
            }
            return false;*/
        }

        #endregion

        #region Private Classes.

        /// <summary>
        /// A cachable menu link item.
        /// </summary>
        private sealed class CachedMenuLink
        {
            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="CachedMenuLink"/> class.
            /// </summary>
            public CachedMenuLink()
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="CachedMenuLink"/> class.
            /// </summary>
            /// <param name="id"></param>
            /// <param name="label"></param>
            /// <param name="url"></param>
            /// <param name="sort"></param>
            /// <param name="cssClass"></param>
            /// <param name="parentId"></param>
            /// <param name="permissionId"></param>
            public CachedMenuLink(Guid id, string label, string url, int sort, string cssClass, Guid parentId, Guid permissionId)
            {
                this.Id = id;
                this.Label = label;
                this.Url = url;
                this.Sort = sort;
                this.CssClass = cssClass;
                this.ParentId = parentId;
                this.PermissionId = permissionId;
            }

            #endregion

            #region Public Properties.

            /// <summary>
            /// The menu link id.
            /// </summary>
            public Guid Id
            {
                get; private set;
            }
            /// <summary>
            /// The menu link label.
            /// E.g. "The road to no where"
            /// </summary>
            public string Label
            {
                get;
                private set;
            }

            /// <summary>
            /// The menu link URL.
            /// E.g. /controller/action
            /// </summary>
            public string Url
            {
                get;
                private set;
            }

            /// <summary>
            /// Whether the menu link is selected.
            /// </summary>
            public bool IsSelected
            {
                get;
                set;
            }

            /// <summary>
            /// The menu link order.
            /// E.g. 1, 2, 3 ... 100.
            /// </summary>
            public int Sort
            {
                get;
                private set;
            }

            /// <summary>
            /// The menu link CSS class, set on the LI element.
            /// E.g. "special-menu-link-item".
            /// </summary>
            public string CssClass
            {
                get;
                private set;
            }

            /// <summary>
            /// The menu link parent link if any.
            /// </summary>
            public Guid ParentId
            {
                get;
                private set;
            }

            /// <summary>
            /// The menu link permission if any.
            /// </summary>
            public Guid PermissionId
            {
                get;
                private set;
            }

            #endregion
        }

        #endregion
    }
}
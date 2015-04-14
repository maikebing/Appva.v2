// <copyright file="MenuList.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// Represents a collection of menu items.
    /// </summary>
    public interface IMenuList<T> : IList<T> where T : IMenuItem
    {
    }

    /// <summary>
    /// A <see cref="IMenuList"/> implementation.
    /// </summary>
    internal sealed class MenuList : List<IMenuItem>, IMenuList<IMenuItem>
    {
    }
}
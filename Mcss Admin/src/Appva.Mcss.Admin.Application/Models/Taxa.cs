// <copyright file="Taxa.cs" company="Appva AB">
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
    /// Represents a collection of taxons.
    /// </summary>
    public interface ITaxa : IList<ITaxon>
    {
    }

    /// <summary>
    /// A <see cref="ITaxa"/> implementation.
    /// </summary>
    public sealed class Taxa : List<ITaxon>, IList<ITaxon>
    {
    }
}
// <copyright file="Taxon.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// Representation of a taxon.
    /// </summary>
    public interface ITaxon
    {
        Guid Id
        {
            get;
        }

        /// <summary>
        /// The name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// The description.
        /// </summary>
        string Description
        {
            get;
        }

        /// <summary>
        /// The materialized path separated by a '.' (dot).
        /// </summary>
        string Path
        {
            get;
        }

        /// <summary>
        /// The type.
        /// </summary>
        string Type
        {
            get;
        }

        /// <summary>
        /// Whether or not it is a root node or not.
        /// </summary>
        bool IsRoot
        {
            get;
        }

        /// <summary>
        /// The sort order.
        /// </summary>
        int Sort
        {
            get;
        }

        /// <summary>
        /// The parent taxon id.
        /// </summary>
        Guid? ParentId
        {
            get;
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TaxonItem : ITaxon
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Taxon"/> class.
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="path">The materialized path; dot separated</param>
        /// <param name="type">The type</param>
        /// <param name="sort">Optional sorting order</param>
        /// <param name="parentId">Optional parent id. If null then it's a root node</param>
        public TaxonItem(Guid id, string name, string description, string path, string type, int sort = 0, Guid? parentId = null)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Path = path;
            this.Type = type;
            this.Sort = sort;
            this.IsRoot = parentId.HasValue;
            this.ParentId = parentId;
        }

        #endregion

        #region ITaxon Members.

        /// <inheritdoc />
        public Guid Id
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Name
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Description
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Path
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Type
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool IsRoot
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public int Sort
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public Guid? ParentId
        {
            get;
            private set;
        }

        #endregion
    }
}
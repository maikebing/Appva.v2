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
        /// <summary>
        /// The ID.
        /// </summary>
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
        /// Active state.
        /// </summary>
        bool IsActive
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

        /// <summary>
        /// The parent taxon id.
        /// </summary>
        ITaxon Parent
        {
            get;
        }

        /// <summary>
        /// The complete address
        /// </summary>
        string Address
        {
            get;
        }

        /// <summary>
        /// Updates the taxon
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="description"></param>
        void Update(string name, string description);
        void Update(string name, string description, bool isActive);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TaxonItem : ITaxon
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonItem"/> class.
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="path">The materialized path; dot separated</param>
        /// <param name="type">The type</param>
        /// <param name="sort">Optional sorting order</param>
        /// <param name="parentId">Optional parent id. If null then it's a root node</param>
        public TaxonItem(Guid id, string name, string description, string path, string type, int sort = 0, ITaxon parent = null)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Path = path;
            this.Type = type;
            this.Sort = sort;
            this.IsRoot = parent == null;
            this.ParentId = parent != null ? parent.Id : (Guid?) null;
            this.Parent = parent;
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

        /// <inheritdoc />
        public ITaxon Parent
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool IsActive
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Address
        {
            get
            {
                return this.IsRoot ? 
                    this.Name : 
                    string.Format("{0} {1}", this.Parent.Address, this.Name);
            }
        }

        /// <inheritdoc />
        public void Update(string name, string description)
        {
            this.Name        = name;
            this.Description = description;
        }

        public void Update(string name, string description, bool isActive)
        {
            this.Name = name;
            this.Description = description;
            this.IsActive = isActive;
        }

        #endregion
    }
}
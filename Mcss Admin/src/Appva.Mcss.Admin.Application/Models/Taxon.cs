// <copyright file="Taxon.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Models
{
    using Appva.Mcss.Admin.Domain.Entities;
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
            set;
        }

        /// <summary>
        /// The description.
        /// </summary>
        string Description
        {
            get;
            set;
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
        /// Check if the taxon is active or not.
        /// </summary>
        bool IsActive
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

        /// <summary>
        /// The parent taxon id.
        /// </summary>
        ITaxon Parent
        {
            get;
        }

        /// <summary>
        /// The complete address.
        /// </summary>
        string Address
        {
            get;
        }
 
        /// <summary>
        /// Updates the taxon
        /// </summary>
        /// <param name="Taxon"></param>
        void Update(ITaxon taxon);

        /// <summary>
        /// Updates the taxon with path and root.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="description"></param>
        void Update(string name, string path, int? sort = null, bool? isRoot = null);

        /// <summary>
        /// Inactivates the taxon
        /// </summary>
        /// <returns>The inactivated taxon</returns>
        ITaxon InActivate();

        /// <summary>
        /// Activates a taxon
        /// </summary>
        /// <returns>The activated taxon</returns>
        ITaxon Activate();
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
        /// <param name="isActive">If the riskassesment is activated</param>
        public TaxonItem(Guid id, string name, string description, string path, string type, int sort = 0, ITaxon parent = null, bool isActive = true)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            this.Path = path;
            this.Type = type;
            this.Sort = sort;
            this.IsActive = isActive;
            this.IsRoot = parent == null;
            this.ParentId = parent != null ? parent.Id : (Guid?) null;
            this.Parent = parent;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaxonItem"/> class.
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="name">The name</param>
        /// <param name="description">The description</param>
        /// <param name="path">The materialized path; dot separated</param>
        /// <param name="type">The type</param>
        /// <param name="sort">Optional sorting order</param>
        /// <param name="isRoot">If it's a root node or not</param>
        /// <param name="isRoot">Check if the taxon is active</param>
        public TaxonItem(Guid id, string name, string description, string path, string type, bool isRoot, bool isActive = true, int sort = 0)
            : this(id,name,description,path,type,sort: sort, parent: null, isActive: isActive)
        {
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
            set;
        }

        /// <inheritdoc />
        public string Description
        {
            get;
            set;
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
        public bool IsActive
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
        public static ITaxon FromTaxon(Taxon taxon)
        {
            return new TaxonItem(
                taxon.Id,
                taxon.Name,
                taxon.Description,
                taxon.Path,
                taxon.Type,
                taxon.Weight
                );
        }

        public void Update(string name, string description, int? sort = null,  bool? isRoot = null)
        {
            this.Name = name;
            this.Description = description;
            if (sort.HasValue)
            {
                this.Sort = sort.GetValueOrDefault();
            }
            if (isRoot.HasValue)
            {
                this.IsRoot = isRoot.GetValueOrDefault();
            }
        }

        /// <inheritdoc />
        public void Update(ITaxon taxon)
        {
            this.Name = taxon.Name;
            this.Description = taxon.Description;
            this.IsActive = taxon.IsActive;
        }

        /// <inheritdoc />
        public ITaxon InActivate()
        {
            this.IsActive = false;
            return this;
        }

        /// <inheritdoc />
        public ITaxon Activate()
        {
            this.IsActive = true;
            return this;
        }

        #endregion
    }
}
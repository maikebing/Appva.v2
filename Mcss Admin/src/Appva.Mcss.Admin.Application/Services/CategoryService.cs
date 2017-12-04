// <copyright file="CategoryService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Application.Services
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public interface ICategoryService : IService
    {
        /// <summary>
        /// Finds the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Category Find(Guid id);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        #region Fields.

        /// <summary>
        /// The <see cref="ICategoryRepository"/>
        /// </summary>
        private readonly ICategoryRepository categoryRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryService"/> class.
        /// </summary>
        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        #endregion

        #region ICategoryService members.

        /// <inheritdoc />
        public Category Find(Guid id)
        {
            return this.categoryRepository.Get(id);
        }

        #endregion
    }
}
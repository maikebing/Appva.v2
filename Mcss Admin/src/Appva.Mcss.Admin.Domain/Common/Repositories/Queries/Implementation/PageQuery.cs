// <copyright file="PageQuery.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class PageQuery : ValueObject<PageQuery>, IPageQuery
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="PageQuery"/> class.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The amount of items per page.</param>
        /// <param name="cursor">The date time cursor.</param>
        protected PageQuery(int pageNumber, int pageSize, DateTime? cursor)
        {
            this.PageNumber = pageNumber > 0 ? pageNumber : 1;
            this.PageSize   = pageSize   > 0 ? pageSize   : 10;
            this.Skip       = (this.PageNumber - 1) * this.PageSize;
            this.Cursor     = cursor;
        }

        #endregion

        #region IPageQuery Members.

        /// <inheritdoc />
        public int PageNumber
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public int PageSize
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public int Skip
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public bool IsFirstPage
        {
            get
            {
                return this.PageNumber == 0 || this.PageNumber == 1;
            }
        }

        /// <inheritdoc />
        public DateTime? Cursor
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Initializes a new instance of the <see cref="PageQuery"/> class.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The amount of items per page; defaults to 10.</param>
        /// <param name="cursor">The date and time specific cursor.</param>
        /// <returns>A new <see cref="PageQuery"/> instance.</returns>
        public static PageQuery New(int pageNumber, int pageSize = 10, DateTime? cursor = null)
        {
            return new PageQuery(pageNumber, pageSize, cursor);
        }

        #endregion

        #region ICursoredPageQuery Members.

        /// <inheritdoc /> 
        public void Update(DateTime? cursor)
        {
            this.Cursor = cursor;
        }

        #endregion

        #region Overrides.

        /// <inheritdoc />
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.PageNumber;
            yield return this.PageSize;
            yield return this.Cursor;
        }

        #endregion
    }
}
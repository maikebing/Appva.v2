// <copyright file="MetaData.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MetaData
    {
        #region Variables.

        /// <summary>
        /// The date and time when the resource was
        /// created.
        /// </summary>
        private readonly DateTime createdAt;

        /// <summary>
        /// The date and time when the resource last
        /// was updated.
        /// </summary>
        private readonly DateTime updatedAt;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaData"/> class.
        /// </summary>
        /// <param name="entity"></param>
        public MetaData(IEntity entity)
            : this(entity.CreatedAt, entity.UpdatedAt)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaData"/> class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createdAt"></param>
        /// <param name="updatedAt"></param>
        public MetaData(DateTime createdAt, DateTime updatedAt)
        {
            this.createdAt = createdAt;
            this.updatedAt = updatedAt;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// Returns the date and time the resource was created.
        /// </summary>
        public DateTime CreatedAt
        {
            get
            {
                return this.createdAt;
            }
        }

        /// <summary>
        /// Returns the date and time the resource last was updated.
        /// </summary>
        public DateTime UpdatedAt
        {
            get
            {
                return this.updatedAt;
            }
        }

        #endregion
    }
}
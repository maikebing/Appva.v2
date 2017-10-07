// <copyright file="DataFile.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DataFile : AggregateRoot<DataFile>
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="DataFile"/> class.
        /// </summary>
        public DataFile()
        {

        }

        #endregion

        #region Properties.

        /// <summary>
        /// The file name.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The title.
        /// </summary>
        public virtual string Title
        {
            get;
            set;
        }

        /// <summary>
        /// The file description.
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The content type.
        /// </summary>
        public virtual string ContentType
        {
            get;
            set;
        }

        /// <summary>
        /// The file properties.
        /// </summary>
        public virtual string Properties
        {
            get;
            set;
        }

        /// <summary>
        /// The data.
        /// </summary>
        public virtual byte[] Data
        {
            get;
            set;
        }

        #endregion
    }
}
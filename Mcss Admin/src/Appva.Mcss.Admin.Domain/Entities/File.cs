// <copyright file="File.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using Appva.Common.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class File : AggregateRoot<File>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="File"/> class.
        /// </summary>
        public File()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The name
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The description
        /// </summary>
        public virtual string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The resource
        /// </summary>
        public virtual byte[] Resource
        {
            get;
            set;
        }

        public virtual FileType FileType
        {
            get;
            set;
        } 

        #endregion
    }

    public enum FileType
    {
        XLS
    }
}
// <copyright file="Article.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class Article
    {
        #region Properties.

        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Form
        {
            get;
            set;
        }

        public virtual string Strength
        {
            get;
            set;
        }

        public virtual string NplId
        {
            get;
            set;
        }

        public virtual string NplPackId
        {
            get;
            set;
        }

        public virtual string PackageUnit
        {
            get;
            set;
        }

        public virtual string PackageType
        {
            get;
            set;
        }

        public virtual double PackageSize 
        { 
            get; 
            set; 
        }

        public virtual string ArticleNumber
        {
            get;
            set;
        }

        public virtual string Atc
        {
            get;
            set;
        }

        public virtual string AtcCode
        {
            get;
            set;
        }

        public virtual string Stakeholder
        {
            get;
            set;
        }

        public virtual bool Provided
        {
            get;
            set;
        }

        #endregion
    }
}
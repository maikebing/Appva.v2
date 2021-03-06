﻿// <copyright file="KnowledgeTest.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
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
    public class KnowledgeTest : AggregateRoot
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="KnowledgeTest"/> class.
        /// </summary>
        public KnowledgeTest()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The knowledge test completed date.
        /// </summary>
        public virtual DateTime? CompletedDate
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="Account"/> which has passed a knowledge test.
        /// </summary>
        public virtual Account Account
        {
            get;
            set;
        }

        /// <summary>
        /// If the knowledge test is connected to a delegation taxon.
        /// </summary>
        public virtual Taxon DelegationTaxon
        {
            get;
            set;
        }

        /// <summary>
        /// The super knowledge test taxon, see taxonomy.
        /// </summary>
        /// <remarks>Not used anymore. Replaced by plain text, see below.</remarks>
        public virtual Taxon KnowledgeTestTaxon
        {
            get;
            set;
        }

        /// <summary>
        /// The knowledge test in written text.
        /// </summary>
        public virtual string Name
        {
            get;
            set;
        }

        #endregion
    }
}
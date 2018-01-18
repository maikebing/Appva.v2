//// <copyright file="SubSequence.cs" company="Appva AB">
////     Copyright (c) Appva AB. All rights reserved.
//// </copyright>
//// <author>
////     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
//// </author>
//namespace Appva.Mcss.Admin.Domain.Entities
//{
//    #region Imports.

//    using System;
//    using System.Collections.Generic;
//    using System.Linq;
//    using Validation;

//    #endregion

//    /// <summary>
//    /// TODO: Add a descriptive summary to increase readability.
//    /// </summary>
//    public class SubSequence : AggregateRoot
//    {
//        #region Constructors.

//        /// <summary>
//        /// Initializes a new instance of the <see cref="SubSequence"/> class.
//        /// </summary>
//        /// <param name="sequence">The sequence.</param>
//        /// <param name="observation">The observation.</param>
//        public SubSequence(Sequence sequence, DosageObservation dosageObservation)
//        {
//            Requires.NotNull(sequence,    "sequence"   );
//            Requires.NotNull(dosageObservation, "SequenceObservation");
//        }

//        /// <summary>
//        /// Initializes a new instance of the <see cref="SubSequence"/> class.
//        /// </summary>
//        /// <remarks>
//        /// An NHibernate visible no-argument constructor.
//        /// </remarks>
//        protected SubSequence()
//        {
//        }

//        #endregion

//        #region Properties.

//        /// <summary>
//        /// The parent sequence.
//        /// </summary>
//        public virtual Sequence Sequence
//        {
//            get;
//            internal protected set;
//        }

//        /// <summary>
//        /// The observation.
//        /// </summary>
//        public virtual DosageObservation SequenceObservation
//        {
//            get;
//            internal protected set;
//        }

//        #endregion
//    }
//}
// <copyright file="ListCorruptSequencesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Handlers
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Persistence;
    using NHibernate.Transform;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListCorruptSequencesHandler : RequestHandler<Parameterless<ListSequenceModel>, ListSequenceModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext context; 

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListCorruptSequencesHandler"/> class.
        /// </summary>
        public ListCorruptSequencesHandler(IPersistenceContext context)
        {
            this.context = context;
        }

        #endregion

        #region RequestHandler Overrides.

        /// <inheritdoc />
        public override ListSequenceModel Handle(Parameterless<ListSequenceModel> message)
        {
            var sequences = this.context.QueryOver<Sequence>()
                .Where(x => x.IsActive)
                .And(x => x.Repeat.StartAt > x.Repeat.EndAt) 
                .Fetch(x => x.Patient).Eager
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();

            return new ListSequenceModel
            {
                Sequences = sequences
            };
        }

        #endregion
    }
}
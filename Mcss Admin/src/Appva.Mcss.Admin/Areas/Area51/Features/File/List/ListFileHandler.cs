// <copyright file="ListFileHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.File.List
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Cqrs;
    using Domain.Entities;
    using Infrastructure.Models;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class ListFileHandler : RequestHandler<Parameterless<IList<FileModel>>, IList<FileModel>>
    {
        private readonly IPersistenceContext context;

        public ListFileHandler(IPersistenceContext context)
        {
            this.context = context;
        }

        public override IList<FileModel> Handle(Parameterless<IList<FileModel>> message)
        {
            FileModel fileAlias = null;
            var query = this.context.QueryOver<XLS>()
                .Select(Projections.ProjectionList()
                    .Add(Projections.Property<XLS>(x => x.Id).WithAlias(() => fileAlias.Id))
                    .Add(Projections.Property<XLS>(x => x.Name).WithAlias(() => fileAlias.Name))
                    .Add(Projections.Property<XLS>(x => x.Description).WithAlias(() => fileAlias.Description))
                    .Add(Projections.Property<XLS>(x => x.CreatedAt).WithAlias(() => fileAlias.CreatedAt))
                    .Add(Projections.Property<XLS>(x => x.UploadedBy).WithAlias(() => fileAlias.UploadedBy)))
                .TransformUsing(Transformers.AliasToBean<FileModel>());

            return query.List<FileModel>();
        }
    }
}
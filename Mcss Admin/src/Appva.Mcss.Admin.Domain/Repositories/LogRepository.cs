// <copyright file="LogRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Core.Resources;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// The log repository for reading in the log
    /// </summary>
    public interface ILogRepository : IRepository<Log>
    {
        /// <summary>
        /// Lists all log-posts 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Paged<LogModel> List(DateTime? cursor = null, int page = 1, int pageSize = 100, bool isAppvaAccount = false);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class LogRepository : Repository<Log>, ILogRepository
    {
        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="LogRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/>.</param>
        public LogRepository(IPersistenceContext context)
            : base(context)
        {
        }

        #endregion

        #region ILogRepository Members.

        /// <inheritdoc />
        public Paged<LogModel> List(DateTime? cursor = null, int page = 1, int pageSize = 100, bool isAppvaAccount = false)
        {
            var pageQuery   = PageQuery.New(page, pageSize, cursor);
            LogModel alias  = null;
            Patient patient = null;
            Account account = null;
            var query = this.Context
                .QueryOver<Log>()
                    .Left.JoinAlias(x => x.Patient, () => patient)
                    .Left.JoinAlias(x => x.Account, () => account);
            //// If we have a cursor, remove everything before the cursor to get the correct page
            if (cursor.HasValue)
            {
                query.Where(x => x.CreatedAt <= cursor.Value);
            }
            //// If not an appva-account, filter on appva-accounts
            if (! isAppvaAccount)
            {
                var sub = QueryOver.Of<Account>()
                    .Select(x => x.Id)
                    .Inner.JoinQueryOver<Role>(x => x.Roles)
                        .Where(x => x.MachineName == RoleTypes.Appva);
                query.WithSubquery.WhereProperty(() => account.Id).NotIn(sub);
            }
            query.SelectList(list => list
                    .Select(x => x.Id)             .WithAlias(() => alias.Id)
                    .Select(x => x.IpAddress)      .WithAlias(() => alias.IpAddress)
                    .Select(x => x.Level)          .WithAlias(() => alias.Level)
                    .Select(x => x.Message)        .WithAlias(() => alias.Message)
                    .Select(() => patient.FullName).WithAlias(() => alias.PatientName)
                    .Select(x => x.Route)          .WithAlias(() => alias.Route)
                    .Select(x => x.System)         .WithAlias(() => alias.System)
                    .Select(x => x.Type)           .WithAlias(() => alias.Type)
                    .Select(() => account.FullName).WithAlias(() => alias.AccountName)
                    .Select(x => x.CreatedAt)      .WithAlias(() => alias.CreatedAt))
                .OrderByAlias(() => alias.CreatedAt).Desc
                .TransformUsing(Transformers.AliasToBean<LogModel>());
            var count = query.RowCount();
            var items = query.Skip(pageQuery.Skip).Take(pageQuery.PageSize).List<LogModel>();
            return Paged<LogModel>.New(pageQuery, items, count);
        }

        #endregion
    }
}
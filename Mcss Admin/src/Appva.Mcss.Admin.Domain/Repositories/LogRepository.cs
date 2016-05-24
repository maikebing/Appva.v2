﻿// <copyright file="LogRepository.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports.

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Persistence;
    using Appva.Repository;
    using NHibernate.Transform;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// The log repository for reading in the log
    /// </summary>
    public interface ILogRepository : 
        IIdentityRepository<Log>,
        IRepository
    {
        /// <summary>
        /// Lists all log-posts 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        PageableSet<LogModel> List(DateTime? cursor = null, int page = 1, int pageSize = 100);
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class LogRepository : ILogRepository
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;
        
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="LogRepository"/> class.
        /// </summary>
        public LogRepository(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region ILogRepository Members.

        /// <inheritdoc />
        public PageableSet<LogModel> List(DateTime? cursor = null, int page = 1, int pageSize = 100)
        {
            //// If page is less then 1, then adjust it.
            page = page < 1 ? 1 : page; 

            LogModel alias = null;
            Patient patient = null;
            Account account = null;
            var query = this.persistence.QueryOver<Log>()
                //.Where(x => x.IsActive)
                .Full.JoinAlias(x => x.Patient, () => patient)
                .Full.JoinAlias(x => x.Account, () => account)
                .SelectList(list => list
                    .Select(x => x.Id).WithAlias(() => alias.Id)
                    .Select(x => x.IpAddress).WithAlias(() => alias.IpAddress)
                    .Select(x => x.Level).WithAlias(() => alias.Level)
                    .Select(x => x.Message).WithAlias(() => alias.Message)
                    .Select(() => patient.FullName).WithAlias(() => alias.PatientName)
                    .Select(x => x.Route).WithAlias(() => alias.Route)
                    .Select(x => x.System).WithAlias(() => alias.System)
                    .Select(x => x.Type).WithAlias(() => alias.Type)
                    .Select(() => account.FullName).WithAlias(() => alias.AccountName)
                    .Select(x => x.CreatedAt).WithAlias(() => alias.CreatedAt))
                .OrderByAlias(() => alias.CreatedAt).Desc
                .TransformUsing(Transformers.AliasToBean<LogModel>());

            //// If we have a cursor, use it for pagination, else use the page for "normal" pagination.
            var entities = cursor.HasValue ?
                query.Clone().Where(x => x.CreatedAt < cursor.GetValueOrDefault()).Take(pageSize).List<LogModel>() :
                query.Skip(((page-1) * pageSize)).Take(pageSize).List<LogModel>();

            return new PageableSet<LogModel>
                {
                    TotalCount = query.RowCount(),
                    Entities = entities,
                    CurrentPage = page,
                    NextPage = page++,
                    PageSize = pageSize
                };
        }

        #endregion

        #region IIdentiyRepository Members.

        /// <inheritdoc />
        public Log Find(Guid id)
        {
            return this.persistence.Get<Log>(id);
        }

        #endregion
    }
}
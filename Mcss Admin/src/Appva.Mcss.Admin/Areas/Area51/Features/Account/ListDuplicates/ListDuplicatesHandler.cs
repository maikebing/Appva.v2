// <copyright file="ListDuplicatesHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Account.ListDuplicates
{
    #region Imports.

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Areas.Area51.Models;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using Appva.Persistence;
    using NHibernate.Criterion;
    using NHibernate.SqlCommand;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListDuplicatesHandler : RequestHandler<Parameterless<List<DuplicatedAccount>>, List<DuplicatedAccount>>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ListDuplicatesHandler"/> class.
        /// </summary>
        public ListDuplicatesHandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler

        public override List<DuplicatedAccount> Handle(Parameterless<List<DuplicatedAccount>> message)
        {
            var hql = @"SELECT a1 AS Primary, 
                               a2 AS Secondary 
                        FROM Account a1, Account a2 
                        WHERE a1.PersonalIdentityNumber = a2.PersonalIdentityNumber AND a1.Id != a2.Id";
            var accounts = this.persistence.Session.CreateQuery(hql)
                                .SetResultTransformer(NHibernate.Transform.Transformers.AliasToBean<DuplicatedAccount>())
                                .List<DuplicatedAccount>();
            return accounts.ToList();
                
                /*from a1 in this.persistence.Session.QueryOver<Account>() 
                           join a2 in this.persistence.Session.QueryOver<Account>() 
                               on a1.PersonalIdentityNumber equals a2.PersonalIdentityNumber 
                               select(new KeyValuePair<Account,Account>{ Key = a1, Value = a2 });*/
                
        }

        #endregion
    }
}
// <copyright file="Nonce.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Common.Domain;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class Nonce : Entity<Client>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Nonce"/> class.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="code"></param>
        /// <param name="timestamp"></param>
        public Nonce(string context, string code, DateTime timestamp)
        {
            this.Context = context;
            this.Code = code;
            this.Timestamp = timestamp;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Nonce"/> class.
        /// </summary>
        /// <remarks>Required by NHibernate.</remarks>
        protected Nonce()
        {
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The nonce context.
        /// </summary>
        public virtual string Context
        {
            get;
            protected set;
        }

        /// <summary>
        /// The nonce code.
        /// </summary>
        public virtual string Code
        {
            get;
            protected set;
        }

        /// <summary>
        /// The nonce timestamp.
        /// </summary>
        public virtual DateTime Timestamp
        {
            get;
            protected set;
        }
        
        #endregion
    }
}
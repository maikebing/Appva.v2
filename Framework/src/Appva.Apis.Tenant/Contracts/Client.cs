// <copyright file="Client.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Apis.TenantServer.Contracts
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    #endregion

    /// <summary>
    /// The client model contract.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public sealed class Client
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client()
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// The client identifier.
        /// </summary>
        public string Identifier
        {
            get;
            set;
        }

        /// <summary>
        /// The client secret.
        /// </summary>
        public string Secret
        {
            get;
            set;
        }

        #endregion
    }
}
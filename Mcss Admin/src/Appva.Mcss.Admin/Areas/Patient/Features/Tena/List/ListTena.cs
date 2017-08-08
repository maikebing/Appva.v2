// <copyright file="ListTena.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>

namespace Appva.Mcss.Admin.Models
{
    #region imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Web.ViewModels;
    
    #endregion

    /// <summary>
    /// TODO: 
    /// </summary>

    public sealed class ListTena : IRequest<ListTenaModel>
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
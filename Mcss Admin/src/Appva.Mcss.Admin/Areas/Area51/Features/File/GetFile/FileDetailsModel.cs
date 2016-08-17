// <copyright file="GetFile.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.File.GetFile
{
    using Admin.Models;
    using Common.Domain;
    using Domain.Entities;
    #region Imports.

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class FileDetailsModel
    {
        public IList<Account> Accounts
        {
            get;
            set;
        }
    }
}
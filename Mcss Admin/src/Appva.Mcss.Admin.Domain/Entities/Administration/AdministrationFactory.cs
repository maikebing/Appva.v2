// <copyright file="AdministrationFactory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    using System;
    #region Imports.

    using System.Collections.Generic;
    using Appva.Core.Extensions;
    using Appva.Mcss.Admin.Application.Models;

    #endregion

    public static class AdministrationFactory
    {
        /// <summary>
        /// Builds a new Administration from a model.
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="model"></param>
        /// <returns>A <see cref="MedicationAdministration"/>.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static Administration CreateNew(Sequence sequence, AdministrationValueModel model)
        {
            return new MedicationAdministration(sequence, model);
        }
    }
}

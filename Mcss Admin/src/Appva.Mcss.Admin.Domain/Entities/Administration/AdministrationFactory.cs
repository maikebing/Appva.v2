// <copyright file="AdministrationFactory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Core.Extensions;

    #endregion
    public static class AdministrationFactory
    {
        public static Administration CreateNew(string name, Sequence sequence, UnitOfMeasurement unit, IList<double> customValues = null)
        {
            return new MedicationAdministration(name, sequence, unit, customValues);
        }
    }
}

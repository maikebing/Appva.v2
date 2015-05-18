// <copyright file="CreatePatient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
// <remarks>
//      Areas/Patient/Features/Patient/Shared/
// </remarks>
// ReSharper disable CheckNamespace
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreatePatient : CreateOrUpdatePatient, IRequest<bool>
    {
    }
}
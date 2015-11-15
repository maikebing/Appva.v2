// <copyright file="Register.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// The register password form post request model.
    /// </summary>
    public sealed class Register : AbstractPassword, IRequest<bool>
    {
    }
}
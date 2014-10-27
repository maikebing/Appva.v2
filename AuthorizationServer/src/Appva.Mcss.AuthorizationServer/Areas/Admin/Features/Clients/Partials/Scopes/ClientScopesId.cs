// <copyright file="ClientScopesId.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public class ClientScopesId : Identity, IRequest<IList<ClientScope>>
    {
    }
}
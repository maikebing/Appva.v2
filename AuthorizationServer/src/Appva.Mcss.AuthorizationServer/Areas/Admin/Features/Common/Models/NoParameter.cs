// <copyright file="NoParameter.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// Used for controller actions without request parameters when
    /// applying the <see cref="DispatchAttribute"/>.
    /// </summary>
    /// <example>
    /// [Dispatch(typeof(NoParameter{ActionWithoutParametersResponse}))]
    /// public ActionResult ActionWithoutParameters()
    /// {
    ///     return View();
    /// }
    /// </example>
    /// <typeparam name="TResponse">The response type</typeparam>
    public sealed class NoParameter<TResponse> : IRequest<TResponse>
    {
    }
}
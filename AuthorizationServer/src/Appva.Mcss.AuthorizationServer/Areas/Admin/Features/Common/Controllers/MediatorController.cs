// <copyright file="MediatorController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Areas.Admin.Controllers
{
    #region Imports.

    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Appva.Cqrs;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// A base controller for mediating <see cref="IMediator"/>.
    /// </summary>
    public class MediatorController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IMediator mediator;

        /// <summary>
        /// The <see cref="IMediator"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MediatorController"/> class.
        /// </summary>
        /// <param name="mediator">A <see cref="IMediator"/></param>
        /// <param name="persistenceContext">A <see cref="IPersistenceContext"/></param>
        public MediatorController(IMediator mediator, IPersistenceContext persistenceContext)
        {
            this.mediator = mediator;
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Sends a request message syncronously.
        /// </summary>
        /// <typeparam name="TResponse">The response type</typeparam>
        /// <param name="request">The async request message</param>
        /// <returns>{TResponse}</returns>
        protected TResponse Send<TResponse>(IRequest<TResponse> request)
        {
            return this.mediator.Send<TResponse>(request);
        }

        /// <summary>
        /// Sends a request message asyncronously.
        /// </summary>
        /// <typeparam name="TResponse">The response type</typeparam>
        /// <param name="request">The async request message</param>
        /// <returns>Task{TResponse}</returns>
        protected async Task<TResponse> SendAsync<TResponse>(IAsyncRequest<TResponse> request)
        {
            return await this.mediator.SendAsync<TResponse>(request);
        }

        #endregion
    }
}
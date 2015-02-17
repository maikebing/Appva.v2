// <copyright file="AuditAttribute.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.AuthorizationServer.Infrastructure
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Appva.Common.Domain;
    using Appva.Persistence;
    using NHibernate.Event;
    using Appva.Core.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AuditAttribute : ActionFilterAttribute
    {
        #region Public Properties.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        /// <remarks>Must be public for IOC unfortunately</remarks>
        public IPersistenceContext Persistence
        {
            get;
            set;
        }

        /// <summary>
        /// The <see cref="IEventListener"/>.
        /// </summary>
        /// <remarks>Must be public for IOC unfortunately</remarks>
        public IEventListener EventListener
        {
            get;
            set;
        }

        #endregion

        #region ActionFilterAttribute Overrides.

        /// <inheritdoc />
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var impl = this.Persistence.Session.GetSessionImplementation();
            impl.Listeners.PostUpdateEventListeners = new IPostUpdateEventListener[] {
               this.EventListener
            };
        }

        #endregion
    }

    /// <summary>
    /// Marker interface for events.
    /// </summary>
    public interface IEventListener : IPostUpdateEventListener, IPostInsertEventListener
    {
    }

    public class TestSaveOrUpdateEventListeneer : IEventListener
    {
        public void OnPostUpdate(PostUpdateEvent evt)
        {
            if (evt.Entity.IsNot<IAggregateRoot>())
            {
                return;
            }
            var aggregate = evt.Entity as IAggregateRoot;
            aggregate.Events.ForEach(x =>
            {

            });
        }

        public void OnPostInsert(PostInsertEvent @event)
        {
            throw new NotImplementedException();
        }
    }

}
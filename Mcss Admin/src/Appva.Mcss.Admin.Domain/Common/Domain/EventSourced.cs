// <copyright file="EventSourced.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    #region Imports.

    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// A collection of objects that are bound together by a root entity, otherwise 
    /// known as an aggregate root. The aggregate root guarantees the consistency of 
    /// changes being made within the aggregate by forbidding external objects from 
    /// holding references to its members.
    /// </summary>
    public abstract class EventSourced : AggregateRoot, IEventSourced
    {
        #region Variables.

        /// <summary>
        /// The domain change events.
        /// </summary>
        private readonly IList<IDomainEvent> changes = new List<IDomainEvent>();

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSourced"/> class.
        /// </summary>
        protected EventSourced()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSourced"/> class.
        /// </summary>
        /// <param name="events"></param>
        protected EventSourced(IEnumerable<IDomainEvent> events)
        {
            this.LoadFromHistory(events);
        }

        #endregion

        #region IEventSourced Members.

        /// <summary>
        /// Returns the uncommitted change events.
        /// </summary>
        /// <returns>
        /// A collection of <see cref="IDomainEvent"/>.
        /// </returns>
        protected internal virtual IEnumerable<IDomainEvent> UncommittedChanges()
        {
            return this.changes;
        }

        /// <summary>
        /// Marks the change events as committed.
        /// </summary>
        protected internal virtual void MarkChangesAsCommitted()
        {
            this.changes.Clear();
        }

        #endregion

        #region Protected Members.

        /// <summary>
        /// Applies the change.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        protected internal virtual void ApplyChange(IDomainEvent domainEvent)
        {
            this.ApplyChange(domainEvent, true);
        }

        /// <summary>
        /// Registers an event.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        protected internal virtual void RegisterEvent(IDomainEvent domainEvent)
        {
            if (this.changes.Contains(domainEvent))
            {
                return;
            }
            this.changes.Add(domainEvent);
        }

        #endregion

        #region Private Members.

        /// <summary>
        /// Push atomic aggregate changes to local history for further processing.
        /// </summary>
        /// <param name="domainEvent"></param>
        /// <param name="isNew"></param>
        private void ApplyChange(IDomainEvent domainEvent, bool isNew)
        {
            (this as dynamic).Apply(domainEvent);
            if (isNew)
            {
                this.changes.Add(domainEvent);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="history"></param>
        private void LoadFromHistory(IEnumerable<IDomainEvent> events)
        {
            foreach (var evt in events)
            {
                this.ApplyChange(evt, false);
            }
        }

        #endregion
    }
}
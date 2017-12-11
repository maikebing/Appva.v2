// <copyright file="IEventSourced.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain
{
    /// <summary>
    /// Marker interface. Event Sourcing ensures that all changes to application state 
    /// are stored as a sequence of events. Not just can we query these events, we can 
    /// also use the event log to reconstruct past states, and as a foundation to 
    /// automatically adjust the state to cope with retroactive changes.
    /// </summary>
    public interface IEventSourced
    {
    }
}
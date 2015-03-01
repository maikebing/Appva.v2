// <copyright file="IJobFactory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler.Infrastructure.Jobs
{
    /// <summary>
    /// Marker interface in order to remove redundant Quartz references.
    /// </summary>
    public interface IJobFactory : Quartz.Spi.IJobFactory
    {
    }
}
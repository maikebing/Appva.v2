// <copyright file="DisallowConcurrentExecutionJob.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler.Infrastructure.Jobs
{
    #region Imports.

    using Quartz;

    #endregion

    /// <summary>
    /// Abstract base <see cref="IJob"/> cladd.
    /// </summary>
    [DisallowConcurrentExecution]
    public abstract class DisallowConcurrentExecutionJob : IJob
    {
        #region IJob Members.

        /// <inheritdoc />
        public abstract void Execute(IJobExecutionContext context);

        #endregion
    }
}
// <copyright file="JobFactory.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler.Infrastructure.Jobs
{
    #region Imports.

    using System;
    using Autofac;
    using Quartz;
    using Quartz.Spi;
    using Validation;

    #endregion

    /// <summary>
    /// Custom Quartz <see cref="IJobFactory"/> implementation with 
    /// Autofac resolver.
    /// </summary>
    public sealed class JobFactory : IJobFactory
    {
        #region Variabels.

        /// <summary>
        /// The <see cref="ILifetimeScope"/>.
        /// </summary>
        private readonly ILifetimeScope scope;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="JobFactory"/> class.
        /// </summary>
        /// <param name="scope">The <see cref="ILifetimeScope"/></param>
        public JobFactory(ILifetimeScope scope)
        {
            Requires.NotNull(scope, "scope");
            this.scope = scope;
        }

        #endregion

        #region IJobFactory Members.

        /// <inheritdoc/>
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            Console.WriteLine("Creating new job!");
            return this.scope.Resolve(bundle.JobDetail.JobType) as IJob;
        }

        /// <inheritdoc/>
        public void ReturnJob(IJob job)
        {
            Console.WriteLine("Return job");
        }

        #endregion
    }
}
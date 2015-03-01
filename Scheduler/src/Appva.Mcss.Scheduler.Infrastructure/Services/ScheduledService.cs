// <copyright file="ScheduledService.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler.Infrastructure.Services
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Messaging;
    using Appva.Mcss.Scheduler.Infrastructure.Configuration;
    using Appva.Mcss.Scheduler.Infrastructure.Jobs;
    using Appva.Mcss.Scheduler.Infrastructure.UI;
    using Quartz;
    using Quartz.Impl;
    using Topshelf;

    #endregion

    // <summary>
    /// A scheduled service implementation of <see cref="HostedService"/>.
    /// </summary>
    public sealed class ScheduledService : HostedService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISchedulerFactory"/>.
        /// </summary>
        private readonly ISchedulerFactory schedulerFactory;

        /// <summary>
        /// The <see cref="IScheduler"/>.
        /// </summary>
        private readonly IScheduler scheduler;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledService"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IApplicationConfiguration"/></param>
        /// <param name="messageService">The <see cref="IMessageService"/></param>
        /// <param name="jobFactory">The <see cref="IJobFactory"/></param>
        public ScheduledService(IApplicationConfiguration configuration, IMessageService messageService, IJobFactory jobFactory)
            : base(configuration, messageService)
        {
            this.schedulerFactory = new StdSchedulerFactory();
            this.scheduler = this.schedulerFactory.GetScheduler();
            this.scheduler.JobFactory = jobFactory;
            /*var name = typeof(ValidateTaskDataIntegrityJob).AssemblyQualifiedName;
            var job = JobBuilder.Create<ValidateTaskDataIntegrityJob>().WithIdentity(name).Build();
            var trigger = TriggerBuilder.Create()
                            .WithIdentity(name)
                            .StartAt(DateTime.Now.AddMinutes(1))
                            .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                            .Build();
            this.scheduler.ScheduleJob(job, trigger);*/
            Display.Welcome();
        }

        #endregion

        #region HostedService Overrides.

        /// <inheritdoc />
        public override void Start()
        {
            this.scheduler.Start();
            base.Start();
        }

        /// <inheritdoc />
        public override void Stop()
        {
            this.scheduler.Shutdown(true);
            base.Stop();
        }

        /// <inheritdoc />
        public override void Pause()
        {
            this.scheduler.PauseAll();
            base.Pause();
        }

        /// <inheritdoc />
        public override void Continue()
        {
            this.scheduler.ResumeAll();
            base.Continue();
        }

        /// <inheritdoc />
        public override void Shutdown()
        {
            this.scheduler.Shutdown(true);
            base.Shutdown();
        }

        /// <inheritdoc />
        public override bool Start(HostControl hostControl)
        {
            this.Start();
            return true;
        }

        /// <inheritdoc />
        public override bool Stop(HostControl hostControl)
        {
            this.Stop();
            return true;
        }

        #endregion
    }
}
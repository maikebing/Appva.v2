// <copyright file="Program.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Core.Messaging;
    using Appva.Mcss.Scheduler.Infrastructure.Configuration;
    using Appva.Mcss.Scheduler.Infrastructure.Jobs;
    using Appva.Mcss.Scheduler.Infrastructure.Services;
    using Autofac;
    using Topshelf;
    using Topshelf.Autofac;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            var configuration = ApplicationXmlConfiguration.Build();
            //builder.Register(x => new Persistence()).As<IPersistence>().SingleInstance();
            builder.Register(x => configuration).As<IApplicationConfiguration>().SingleInstance();
            builder.RegisterType<EmailService>().As<IMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<JobFactory>().As<IJobFactory>().InstancePerLifetimeScope();
            builder.RegisterType<ScheduledService>().As<IHostedService>().InstancePerLifetimeScope();
            //builder.RegisterType<ValidateTaskDataIntegrityJob>().AsSelf().InstancePerDependency();
            var container = builder.Build();
            HostFactory.Run(x =>
            {
                x.UseAutofacContainer(container);
                x.Service<IHostedService>(h =>
                {
                    h.ConstructUsingAutofacContainer();
                    h.WhenStarted(s => s.Start());
                    h.WhenStopped(s => s.Stop());
                    h.WhenPaused(s => s.Pause());
                    h.WhenContinued(s => s.Continue());
                    h.WhenShutdown(s => s.Shutdown());
                });
                x.EnableServiceRecovery(rc => rc.RestartService(1));
                x.StartAutomaticallyDelayed();
                x.RunAsLocalSystem();
                x.SetServiceName(configuration.ServiceName);
                x.SetDisplayName(configuration.ServiceDisplayName);
                x.SetDescription(configuration.ServiceDescription);
            });
        }
    }
}
// <copyright file="HostedService.cs" company="Appva AB">
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
    using Common.Logging;
    using Topshelf;

    #endregion

    /// <summary>
    /// Abstract implementation of <see cref="IHostedService"/>.
    /// </summary>
    public abstract class HostedService : IHostedService
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        protected static readonly ILog Log = LogManager.GetLogger<HostedService>();

        /// <summary>
        /// The <see cref="IApplicationConfiguration"/>.
        /// </summary>
        private readonly IApplicationConfiguration configuration;

        /// <summary>
        /// The <see cref="INotificationService"/> .
        /// </summary>
        private readonly IMessageService messageService;

        /// <summary>
        /// Date time of initialization.
        /// </summary>
        private readonly DateTime? initializedAt;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HostedService"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IApplicationConfiguration"/></param>
        /// <param name="messageService">The <see cref="IMessageService"/></param>
        public HostedService(IApplicationConfiguration configuration, IMessageService messageService)
        {
            this.configuration = configuration;
            this.messageService = messageService;
            this.initializedAt = DateTime.Now;
            Log.Debug(x => x("Service initialized at {0}", this.initializedAt));
        }

        #endregion

        #region IHostedService Members

        /// <inheritdoc />
        public virtual void Start()
        {
            //await this.SendMail(this.configuration.SendNotificationOnStart, "started");
            Log.Debug(x => x("Service started at {0}", DateTime.Now));
        }

        /// <inheritdoc />
        public virtual void Stop()
        {
            //await this.SendMail(this.configuration.SendNotificationOnStop, "stopped");
            Log.Debug(x => x("Service stopped at {0}", DateTime.Now));
        }

        /// <inheritdoc />
        public virtual void Pause()
        {
            //await this.SendMail(this.configuration.SendNotificationOnPause, "paused");
            Log.Debug(x => x("Service paused at {0}", DateTime.Now));
        }

        /// <inheritdoc />
        public virtual void Continue()
        {
            //await this.SendMail(this.configuration.SendNotificationOnResume, "resumed");
            Log.Debug(x => x("Service resumed at {0}", DateTime.Now));
        }

        /// <inheritdoc />
        public virtual void Shutdown()
        {
            //await this.SendMail(this.configuration.SendNotificationOnShutdown, "shutdown");
            Log.Debug(x => x("Service shutdown at {0}", DateTime.Now));
        }

        /// <inheritdoc />
        public abstract bool Start(HostControl hostControl);

        /// <inheritdoc />
        public abstract bool Stop(HostControl hostControl);

        #endregion
    }
}
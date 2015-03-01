// <copyright file="NoOpJob.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler.Jobs
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Scheduler.Infrastructure.Jobs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class NoOpJob : DisallowConcurrentExecutionJob
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOpJob"/> class.
        /// </summary>
        public NoOpJob()
        {
        }

        #endregion

        public override void Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
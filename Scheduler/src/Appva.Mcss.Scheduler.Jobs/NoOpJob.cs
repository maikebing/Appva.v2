// <copyright file="NoOpJob.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Scheduler.Jobs
{
    #region Imports.

    using Appva.Core.Logging;
    using Appva.Mcss.Scheduler.Infrastructure.Jobs;
    using Quartz;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NoOpJob : DisallowConcurrentExecutionJob
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<NoOpJob>();

        /// <summary>
        ///  A simple mock object.
        /// </summary>
        private readonly INoOpIoCMock mock;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NoOpJob"/> class.
        /// </summary>
        /// <param name="mock">The <see cref="INoOpIoCMock"/></param>
        public NoOpJob(INoOpIoCMock mock)
        {
            this.mock = mock;
        }

        #endregion

        /// <inheritdoc />
        public override void Execute(IJobExecutionContext context)
        {
            this.mock.HelloWorld();
        }
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public interface INoOpIoCMock
    {
        /// <summary>
        /// Prints 'Hello World'.
        /// </summary>
        void HelloWorld();
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NoOpIoCMock : INoOpIoCMock
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ILog"/>.
        /// </summary>
        private static readonly ILog Log = LogProvider.For<NoOpIoCMock>();

        #endregion

        #region INoOpIoCMock Members.

        /// <inheritdoc />
        public void HelloWorld()
        {
            Log.Debug("Hello world!");
        }

        #endregion
    }
}
// <copyright file="TrackablePersistenceContext.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Persistence.Autofac
{
    #region Imports.

    using System;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class TrackablePersistenceContext : IDisposable
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private IPersistenceContext persistenceContext;

        /// <summary>
        /// Whether or not the class is disposed or not.
        /// </summary>
        private bool isDisposed;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackablePersistenceContext"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public TrackablePersistenceContext(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the persistence context.
        /// </summary>
        public IPersistenceContext Persistence
        {
            get
            {
                return this.persistenceContext;
            }
        }

        #endregion

        #region IDisposable Members

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.isDisposed)
            {
                return;
            }
            try
            {
                var request = HttpRequest.Current();
                var isCommitable = /*request.IsNotGet() &&*/ request.IsWithoutExceptions() /*&& request.IsValidModelState()*/;
                this.persistenceContext.Commit(isCommitable);
            } 
            finally
            {
                this.persistenceContext.Dispose();
                this.persistenceContext = null;
                this.isDisposed = true;
            }
        }

        #endregion
    }
}
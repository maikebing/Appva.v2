// <copyright file="Wizard.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.AuthorizationServer.Models
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using Appva.Core.Extensions;
    using Validation;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class Wizard<T> : IDisposable where T : class
    {
        #region Variabels.

        /// <summary>
        /// The http session key.
        /// </summary>
        private readonly string key;

        /// <summary>
        /// The model.
        /// </summary>
        private readonly T model;

        /// <summary>
        /// The http session.
        /// </summary>
        private readonly HttpSessionStateBase session;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="Wizard"/> class.
        /// </summary>
        /// <param name="model">The model to initiate the wizard</param>
        /// <param name="session">The http session object</param>
        public Wizard(T model, HttpSessionStateBase session)
        {
            Requires.NotNull(session, "session");
            this.key = "wizard." + typeof(T).AssemblyQualifiedName;
            this.session = session;
            this.model = model ?? this.session[this.key] as T;
            if (model.IsNotNull())
            {
                this.session[this.key] = this.model;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wizard"/> class.
        /// Loads the initiated model from the http session.
        /// </summary>
        /// <param name="session">The http session object</param>
        public Wizard(HttpSessionStateBase session)
            : this(null, session)
        {
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the model.
        /// </summary>
        public T Model
        {
            get
            {
                if (this.model.IsNull())
                {
                    throw new InvalidOperationException("Wizard is not initialized");
                }
                return this.model;
            }
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// 
        /// </summary>
        public void FreeMemory()
        {
            if (this.session[this.key].IsNotNull())
            {
                this.session[this.key] = null;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this.session[this.key].IsNotNull())
            {
                //this.session[this.key] = null;
            }
        }

        #endregion
    }
}
// <copyright file="TestHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Mcss.Monitoring.Controllers.Test
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.Monitoring.Domain.Entities;
    using Appva.Mcss.Monitoring.Infrastructure.Persistence;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO Add a descriptive summary to increase readability.
    /// </summary>
    internal class TestHandler : PersistenceHandler<TestRequest, TestResponse>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="TestHandler"/> class.
        /// </summary>
        public TestHandler(IPersistenceContext persistenceContext)
            : base(persistenceContext)
        {
        }

        #endregion

        #region PersistenceHandler<TestRequest, TestResponse> Overrides
        
        /// <inheritdoc />
        public override TestResponse Handle(TestRequest message)
        {
            var log = new Log("test", Environment.MachineName, "", "", 200);
            var id = (Guid) this.Persistence.Save(log);
            return new TestResponse { FooBar = "Baz " + id.ToString()  };
        }

        #endregion
    }
}
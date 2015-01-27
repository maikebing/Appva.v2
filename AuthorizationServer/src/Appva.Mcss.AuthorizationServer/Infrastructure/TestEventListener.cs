using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Event;

namespace Appva.Mcss.AuthorizationServer.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class TestEventListener : IPostInsertEventListener
    {
        #region IPostInsertEventListener Members

        /// <inheritdoc/>
        public void OnPostInsert(PostInsertEvent @event)
        {
            
        }

        #endregion
    }
}
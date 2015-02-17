using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Common.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEvent"/> class.
        /// </summary>
        /// <param name="occured">The date and time in UTC the event occured</param>
        protected DomainEvent(DateTime occured)
        {
            this.Occurred = occured;
        }

        #endregion 

        #region IDomainEvent Members.

        /// <summary>
        /// Returns the date and time the event occured.
        /// </summary>
        public DateTime Occurred
        {
            get;
            private set;
        }

        #endregion

        #region Abstract Methods.

        /// <summary>
        /// Process the <see cref="DomainEvent"/>.
        /// </summary>
        public abstract void Process();

        #endregion
    }
}

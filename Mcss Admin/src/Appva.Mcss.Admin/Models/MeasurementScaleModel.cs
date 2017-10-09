using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class MeasurementScaleModel
    {
        /// <summary>
        /// The inventory id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the list.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The unit.
        /// </summary>
        public string Unit
        {
            get;
            set;
        }

        /// <summary>
        /// List of amounts in current list.
        /// </summary>
        public IList<double> Amounts
        {
            get;
            set;
        }
    }
}
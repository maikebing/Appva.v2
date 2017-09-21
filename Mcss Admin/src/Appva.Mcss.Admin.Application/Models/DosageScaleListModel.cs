using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Mcss.Admin.Application.Models
{
    public class DosageScaleListModel
    {
        #region Properties

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

        #endregion
    }
}

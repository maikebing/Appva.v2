using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class ListSignature
    {
        /// <summary>
        /// The signing id.
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The signing name.
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The signing image path.
        /// </summary>
        public string Path
        {
            get;
            set;
        }

        /// <summary>
        /// Return true if the signing option is in use.
        /// </summary>
        public bool IsUsedByList
        {
            get;
            set;
        }
    }
}
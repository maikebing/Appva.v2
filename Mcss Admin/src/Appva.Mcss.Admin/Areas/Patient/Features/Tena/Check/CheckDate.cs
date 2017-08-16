using Appva.Mcss.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public sealed class CheckDate : Identity<string>
    {
        public DateTime Date
        {
            get;
            set;
        }
    }
}
﻿using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Web.ViewModels
{
    public class OrderOverviewViewModel
    {
        public IList<Sequence> Orders { get; set; }
    }
}
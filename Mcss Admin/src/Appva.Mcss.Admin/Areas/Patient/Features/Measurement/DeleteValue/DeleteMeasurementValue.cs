﻿using Appva.Mcss.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Models
{
    public class DeleteMeasurementValue : Identity<DeleteMeasurementValueModel>
    {
        public Guid ValueId { get; set; }
    }
}
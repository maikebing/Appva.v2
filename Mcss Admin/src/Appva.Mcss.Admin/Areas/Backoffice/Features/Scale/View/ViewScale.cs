using Appva.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class ViewScale : IRequest<ViewScaleModel>
    {
        public Guid Id { get; set; }
    }
}
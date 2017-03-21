using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    public class CreateSignatureModel : IRequest<bool>
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public Guid Id { get; set; }

        public bool IsRoot { get; set; }

        public IList<ITaxon> Images { get; set; }


    }
}
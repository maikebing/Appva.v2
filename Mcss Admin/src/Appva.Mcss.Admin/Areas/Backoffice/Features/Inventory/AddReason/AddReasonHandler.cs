using Appva.Cqrs;
using Appva.Mcss.Admin.Areas.Backoffice.Models;
using Appva.Mcss.Admin.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    internal sealed class AddReasonHandler : RequestHandler<Parameterless<AddReasonModel>, AddReasonModel>
    {
        public override AddReasonModel Handle(Parameterless<AddReasonModel> message)
        {
            return new AddReasonModel
            {

            };
        }
    }
}
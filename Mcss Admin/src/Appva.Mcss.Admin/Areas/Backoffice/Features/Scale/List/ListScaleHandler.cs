using Appva.Cqrs;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Infrastructure.Models;
using Appva.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    public class ListScaleHandler : RequestHandler<Parameterless<ListScaleModel>, ListScaleModel>
    {
        private readonly IPersistenceContext context;

        private ListScaleHandler(IPersistenceContext context)
        {
            this.context = context;
        }

        public override ListScaleModel Handle(Parameterless<ListScaleModel> message)
        {
            return new ListScaleModel
            {
                Scales = this.context.QueryOver<Scale>().List()
            };
        }
    }
}
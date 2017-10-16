using Appva.Cqrs;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.Models.Handlers
{
    public class ViewScaleHandler : RequestHandler<ViewScale, ViewScaleModel>
    {
        private readonly IPersistenceContext context;

        public ViewScaleHandler(IPersistenceContext context)
        {
            this.context = context;
        }
        public override ViewScaleModel Handle(ViewScale message)
        {
            var scale = this.context.QueryOver<Scale>()
                .Where(x => x.IsActive)
                    .And(x => x.Id == message.Id)
                        .SingleOrDefault();

            return new ViewScaleModel
            {
                Name = scale.Name,
                Description = scale.Description,
                Unit = scale.Unit,
                ScaleType = scale.ScaleType,
                Values = scale.Values
            };
        }
    }
}
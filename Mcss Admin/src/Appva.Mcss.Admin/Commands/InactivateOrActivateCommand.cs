using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Infrastructure;

namespace Appva.Mcss.Web.Controllers {

    public class InactivateOrActivateCommand<T> : Command<IEnumerable<object>> where T : Entity {

        public Guid Id { get; set; }
        public bool IsActive { get; set; }

        public override void Execute() {
            var entity = Session.Get<T>(Id);
            entity.Active = IsActive;
            entity.Modified = DateTime.Now;
            Session.SaveOrUpdate(entity);
        }

    }

}
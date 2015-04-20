using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using Appva.Mcss.Infrastructure;

namespace Appva.Mcss.Web.Controllers {
    
    public class IsUniqueIdentifierCommand<T> : Command<bool> where T : Person {
        
        public Guid? Id { get; set; }
        public string UniqueIdentifier { get; set; }

        public override void Execute() {
            if (Id.HasValue) {
                var person = Session.Get<T>(Id);
                if (person.IsNotNull() && person.UniqueIdentifier.Equals(UniqueIdentifier)) {
                    Result = true;
                } else {
                    Result = IsAlreadyInUse(UniqueIdentifier);
                }
            } else {
                Result = IsAlreadyInUse(UniqueIdentifier);
            }
        }

        private bool IsAlreadyInUse(string uid) {
            var persons = Session.QueryOver<T>()
                    .Where(x => x.UniqueIdentifier == uid)
                    .List().Count;
            return persons.Equals(0);
        }

    }

}
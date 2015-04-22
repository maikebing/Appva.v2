using System;
using System.Collections.Generic;
using Appva.Common.Domain;
using Appva.Cqrs;
using Appva.Mcss.Admin.Application.Services.Settings;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Persistence;

namespace Appva.Mcss.Web.Controllers {

    public class InactivateOrActivateCommand<T> : IRequest<IEnumerable<object>> where T : Entity<T> {

        public Guid Id { get; set; }
        public bool IsActive { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public sealed class InactivateOrActivatehandler<T> : RequestHandler<InactivateOrActivateCommand<T>, IEnumerable<object>>
        where T : Entity<T>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/> dispatcher.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="InactivateOrActivatehandler"/> class.
        /// </summary>
        public InactivateOrActivatehandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<InactivateOrActivateCommand<T>, IEnumerable<object>> Overrides.
        
        /// <inheritdoc /> 
        public override IEnumerable<object> Handle(InactivateOrActivateCommand<T> message)
        {
 	        var entity = this.persistence.Get<T>(message.Id);
            //entity.IsActive = message.IsActive;
            //entity.UpdatedAt = DateTime.Now;
            this.persistence.Update(entity);
            return null;
        }

        #endregion
    }
}
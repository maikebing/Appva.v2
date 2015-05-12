using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;
using Appva.Core.Extensions;
using Appva.Cqrs;
using Appva.Persistence;

namespace Appva.Mcss.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IsUniqueIdentifierCommand<T> : IRequest<bool> where T : Person<T>
    {
        public Guid? Id { get; set; }
        public string UniqueIdentifier { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class IsUniqueIdentifierhandler<T> : RequestHandler<IsUniqueIdentifierCommand<T>, bool>
        where T : Person<T>
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
        public IsUniqueIdentifierhandler(IPersistenceContext persistence)
        {
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler<InactivateOrActivateCommand<T>, IEnumerable<object>> Overrides.

        /// <inheritdoc /> 
        public override bool Handle(IsUniqueIdentifierCommand<T> message)
        {
            if (message.Id.HasValue)
            {
                /*var person = this.persistence.Get<T>(message.Id);
                if (person.IsNotNull() && person.PersonalIdentityNumber.Equals(message.UniqueIdentifier))
                {
                    return true;
                }*/
            }
            return this.IsAlreadyInUse(message.UniqueIdentifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        private bool IsAlreadyInUse(string uid) {
            var persons = this.persistence.QueryOver<T>()
                    //.Where(x => x.PersonalIdentityNumber == uid)
                    .List().Count;
            return persons.Equals(0);
        }
        #endregion
    }
}
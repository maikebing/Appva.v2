using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appva.Persistence;

namespace Appva.Mcss.Admin.Domain.Repositories
{
    public interface IDosageObservationRepository : IRepository<DosageObservation>
    {
        #region Fields

        void Save(DosageObservation dosageObservation);
        DosageObservation GetBySequence(Guid sequenceId);

        #endregion

    }

    public sealed class DosageObservationRepository : Repository<DosageObservation>, IDosageObservationRepository
    {
        #region Constructor

        public DosageObservationRepository(IPersistenceContext context) : base(context)
        {
        }

        #endregion

        #region IDosageObservationRepository members

        public DosageObservation GetBySequence(Guid sequenceId)
        {
            return this.Context.QueryOver<DosageObservation>()
                .Where(x => x.IsActive)
                    .And(x => x.Sequence.Id == sequenceId)
                .SingleOrDefault();
        }

        #endregion
    }
}

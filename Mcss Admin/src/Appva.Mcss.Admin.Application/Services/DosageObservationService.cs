using Appva.Mcss.Admin.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Admin.Application.Services
{
    public interface IDosageObservationService : IService
    {
        #region Fields

        void Save(DosageObservation dosageObservation);
        DosageObservation GetDosageObservationBySequence(Guid sequenceId);

        #endregion

    }

    public sealed class DosageObservationService : IDosageObservationService
    {
        #region Variables

        private readonly IDosageObservationRepository dosageRepository;

        #endregion

        #region Constructor

        public DosageObservationService(IDosageObservationRepository dosageRepository)
        {
            this.dosageRepository = dosageRepository;
        }

        #endregion

        #region IDosageObservationRepository members

        public DosageObservation GetDosageObservationBySequence(Guid sequenceId)
        {
            return this.dosageRepository.GetBySequence(sequenceId);
        }

        public void Save(DosageObservation dosageObservation)
        {
            this.dosageRepository.Save(dosageObservation);
        }

        #endregion
    }
}

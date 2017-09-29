using Appva.Mcss.Admin.Domain.Entities;
using Appva.Mcss.Admin.Domain.Repositories;
using Appva.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appva.Mcss.Admin.Application.Services
{
    public interface IMeasurementService : IService
    {
        IList<MeasurementObservation> GetMeasurementCategories(Guid patientId);
        Patient GetPatient(Guid id);
        void CreateMeasurementObservation(MeasurementObservation observation);
    }
    public sealed class MeasurementService : IMeasurementService
    {
        #region Variables

        private readonly IPersistenceContext context;
        private readonly IMeasurementRepository measurementRepository;
        private readonly IPatientRepository patientRepository;

        #endregion

        #region Constructor

        public MeasurementService(IPersistenceContext context, IMeasurementRepository measurementRepository, IPatientRepository patientRepository)
        {
            this.context = context;
            this.measurementRepository = measurementRepository;
            this.patientRepository = patientRepository;
        }

        #endregion

        #region IMeasurementRepository members

        public IList<MeasurementObservation> GetMeasurementCategories (Guid patientId)
        {
            return this.measurementRepository.GetMeasurementCategories(patientId);
        }

        public void NewMeasurementObservation(MeasurementObservation measurementList)
        {
            this.measurementRepository.NewMeasurementObservation(measurementList);
        }

        public void CreateMeasurementObservation(MeasurementObservation observation)
        {
            this.measurementRepository.NewMeasurementObservation(observation);
        }

        #endregion

        #region IPatientRepository members

        public Patient GetPatient(Guid id)
        {
            return this.patientRepository.Load(id);
        }


        #endregion
    }
}

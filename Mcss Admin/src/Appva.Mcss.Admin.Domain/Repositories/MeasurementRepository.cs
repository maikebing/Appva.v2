using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appva.Persistence;

namespace Appva.Mcss.Admin.Domain.Repositories
{
    public interface IMeasurementRepository : IRepository<MeasurementObservation>
    {
        IList<MeasurementObservation> GetMeasurementCategories(Guid patientId);
        void NewMeasurementObservation(MeasurementObservation measurementObservation);
    }
    public class MeasurementRepository : Repository<MeasurementObservation>, IMeasurementRepository
    {
        public MeasurementRepository(IPersistenceContext context) 
            : base(context)
        {
        }

        public IList<MeasurementObservation> GetMeasurementCategories(Guid patientId)
        {
            var list = this.Context.QueryOver<MeasurementObservation>()
                .Where(x => x.IsActive)
                .And(x => x.Patient.Id == patientId)
                .List();

            return list;
        }

        public void NewMeasurementObservation(MeasurementObservation measurementObservation)
        {
            this.Save(measurementObservation);
        }
    }
}

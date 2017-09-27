using Appva.Mcss.Admin.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appva.Persistence;

namespace Appva.Mcss.Admin.Domain.Repositories
{
    public interface IMeasurementObservationRepository : IRepository<MeasurementObservation>
    {

    }
    public class MeasurementObservationRepository : Repository<MeasurementObservation>, IMeasurementObservationRepository
    {
        public MeasurementObservationRepository(IPersistenceContext context) 
            : base(context)
        {
        }

        public IList<MeasurementObservation> GetMeasurementCategory(Guid patientId)
        {
            var list = this.Context.QueryOver<MeasurementObservation>()
                .Where(x => x.IsActive)
                .And(x => x.Patient.Id == patientId)
                .List();

            return list;
        }
    }
}

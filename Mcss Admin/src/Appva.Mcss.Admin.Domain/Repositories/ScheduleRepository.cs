

namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;
    using Appva.Repository;

    #endregion
    public interface IScheduleRepository : IRepository<Schedule>
    {
        #region Fields

        #endregion
    }
    public class ScheduleRepository : Repository<Schedule>
    {
        #region Constructor

        public ScheduleRepository(IPersistenceContext persistenceContext) 
            : base(persistenceContext)
        {
        }

        #endregion
    }
}

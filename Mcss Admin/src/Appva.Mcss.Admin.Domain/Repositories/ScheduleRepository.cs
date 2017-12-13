namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Persistence;

    #endregion

    public interface IScheduleRepository : 
        IUpdateRepository<Schedule>, 
        ISaveRepository<Schedule>, 
        IRepository<Schedule>
    {
    }

    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        #region Constructors.

        public ScheduleRepository(IPersistenceContext context) 
            : base (context) 
        {
        }

        #endregion
    }
}

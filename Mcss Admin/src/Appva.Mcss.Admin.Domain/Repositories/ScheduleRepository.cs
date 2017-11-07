namespace Appva.Mcss.Admin.Domain.Repositories
{
    #region Imports

    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories.Contracts;
    using Appva.Persistence;
    using Appva.Repository;
    using System;

    #endregion

    public interface IScheduleRepository : IIdentityRepository<Schedule>, 
        IUpdateRepository<Schedule>, 
        ISaveRepository<Schedule>, 
        IRepository
    {
        #region Fields

        Schedule Get(Guid id);

        #endregion
    }

    public class ScheduleRepository : IScheduleRepository
    {
        private readonly IPersistenceContext persistenceContext;

        #region Constructor

        public ScheduleRepository(IPersistenceContext persistenceContext) 
        {
            this.persistenceContext = persistenceContext;
        }

        public Schedule Get(Guid id)
        {
            return this.persistenceContext.Get<Schedule>(id);
        }

        public Schedule Find(Guid id)
        {
            return this.persistenceContext.Get<Schedule>(id);
        }

        public void Save(Schedule entity)
        {
            this.persistenceContext.Save<Schedule>(entity);
        }

        public void Update(Schedule entity)
        {
            entity.UpdatedAt = DateTime.Now;
            this.persistenceContext.Update<Schedule>(entity);
        }

        #endregion

        #region IScheduleRepository Members

        /// <inheritdoc />
        //public new void Update(Schedule schedule)
        //{
        //    schedule.UpdatedAt = DateTime.Now;
        //    this.PersistenceContext.Update<Schedule>(schedule);
        //}

        #endregion
    }
}

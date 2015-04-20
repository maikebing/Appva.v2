using System;
using System.Collections.Generic;
using NHibernate;

namespace Appva.Mcss.Web.Controllers {

    public interface IReportFilter<TRoot, TSubType> {
        void Filter(IQueryOver<TRoot, TSubType> query);
    }

}

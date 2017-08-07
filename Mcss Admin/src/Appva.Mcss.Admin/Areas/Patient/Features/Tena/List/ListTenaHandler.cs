

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region imports

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Models;
    using Appva.Mcss.Admin.Infrastructure;
    using Appva.Mcss.Web.ViewModels;

    #endregion

    /// <summary>
    /// 
    /// </summary>

    internal sealed class ListTenaHandler : RequestHandler<ListTena, ListTenaModel>
    {
        #region Variables

        private IPatientService patientService;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>

        public ListTenaHandler(IPatientService patientService)
        {
            this.patientService = patientService;
        }

        #endregion

        #region RequestHandler Overrides.

        public override ListTenaModel Handle(ListTena message)
        {
            return new ListTenaModel
            {
                Id = this.patientService.Get(message.Id).Id,
                TenaId = this.patientService.Get(message.Id).TenaId
            };
        }

        #endregion

    }
}
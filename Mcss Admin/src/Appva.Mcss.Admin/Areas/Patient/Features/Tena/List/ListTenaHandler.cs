

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
        private ITenaService tenaService;

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>

        public ListTenaHandler(IPatientService patientService, ITenaService tenaService)
        {
            this.patientService = patientService;
            this.tenaService = tenaService;
        }

        #endregion

        #region RequestHandler Overrides.

        public override ListTenaModel Handle(ListTena message)
        {
            var patientId = this.patientService.Get(message.Id).Id;
            var tenaId = this.patientService.Get(message.Id).TenaId;
            var tenaViewModel = new ListTenaModel();

            if (string.IsNullOrEmpty(tenaId)) {
                // activate TenaModel
            }
            else {
                // serve Data List
                var tenaObservationList = tenaService.GetTenaObservationPeriods(tenaId);
            }
            return tenaViewModel;           
        }

        #endregion

    }
}
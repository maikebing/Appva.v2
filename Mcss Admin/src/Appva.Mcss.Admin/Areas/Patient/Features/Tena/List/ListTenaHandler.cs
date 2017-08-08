

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
        private IPatientTransformer patientTransformer;
        
        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>

        public ListTenaHandler(IPatientService patientService, IPatientTransformer patientTransformer)
        {
            this.patientService = patientService;
            this.patientTransformer = patientTransformer;
        }

        #endregion

        #region RequestHandler Overrides.

        public override ListTenaModel Handle(ListTena message)
        {            
            return new ListTenaModel
            {
                patientViewModel = this.patientTransformer.ToPatient(this.patientService.Get(message.Id)),
                patient = this.patientService.Get(message.Id)
            };
        }

        #endregion

    }
}
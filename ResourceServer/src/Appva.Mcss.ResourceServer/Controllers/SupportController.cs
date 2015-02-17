// <copyright file="SupportController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.ResourceServer.Controllers
{
    #region Imports.

    using System.Linq;
    using System.Web.Http;
    using Appva.Mcss.Domain.Entities;
    using Appva.Mcss.ResourceServer.Application;
    using Appva.Mcss.ResourceServer.Application.Authorization;
    using Appva.Mcss.ResourceServer.Domain.Repositories;
    using Appva.Repository;

    #endregion

    /// <summary>
    /// Support endpoint.
    /// TODO: Is this even used????
    /// </summary>
    [RoutePrefix("v1/support")]
    public class SupportController : ApiController
    {
        #region Variables.

        /// <summary>
        /// The <see cref="ISettingRepository"/>.
        /// </summary>
        private readonly ISettingRepository settingRepository;
        
        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportController"/> class.
        /// </summary>
        /// <param name="settingRepository">The <see cref="ISettingRepository"/></param>
        public SupportController(ISettingRepository settingRepository)
        {
            this.settingRepository = settingRepository;
        }
        
        #endregion

        #region Routes.

        /// <summary>
        /// Returns a key value collection of setting.
        /// TODO: ??? Is this being used?
        /// </summary>
        /// <example>
        /// Returns JSON:
        /// {
        ///     { "foo": "bar" },
        ///     { "baz": "boz" }
        /// }
        /// </example>
        /// <param name="view">The view key</param>
        /// <returns>A key value collection of settings</returns>
        [AuthorizeToken(Scope.ReadWrite)]
        [HttpPost, Route("list")]
        public IHttpActionResult List(string view)
        {
            var pageable = Pageable<Setting>.Over().FilterBy(x => x.Active == true)
                .FilterBy(x => x.MachineName == "SomeNameSpace").Page(1).Size(100);
            var settings = this.settingRepository.List(pageable);
            var retval = settings.Entities.Select(x => new { Name = x.Name }).ToList<object>();
            return this.Ok(retval);
        }

        #endregion
    }
}

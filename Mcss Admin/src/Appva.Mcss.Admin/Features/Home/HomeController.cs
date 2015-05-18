// <copyright file="HomeController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Features.Home
{
    #region Imports.

    using System.Web.Mvc;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mvc.Messaging;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [Authorize, RoutePrefix("")]
    public class HomeController : Controller
    {
        #region Variables.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistenceContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="persistenceContext">The <see cref="IPersistenceContext"/></param>
        public HomeController(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        #endregion

        #region Routes.

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route]
        public ActionResult Index()
        {
            ViewData.Add("razor", RazorEmailMessage.CreateNew().Path("Features/Shared/EmailTemplates/AuthenticationEmail.cshtml").Model(new
            {
                Test = "johansalllarsson@appva.se"
            }).To("johansalllarsson@appva.se").Subject("Something").Build().Body);
            return View();
        }

        #endregion
    }
}
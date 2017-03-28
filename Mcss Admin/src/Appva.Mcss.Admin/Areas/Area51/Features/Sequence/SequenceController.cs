// <copyright file="SequenceController.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:richard.henriksson@appva.se">Richard Henriksson</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.Sequence
{
    #region Imports.

    using Appva.Mcss.Admin.Application.Common;
using Appva.Mcss.Admin.Areas.Area51.Models;
using Appva.Mcss.Admin.Infrastructure.Attributes;
using Appva.Mcss.Admin.Infrastructure.Models;
using Appva.Mcss.Admin.Models;
using Appva.Mvc.Security;
using System.Web.Mvc;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    [RouteArea("area51"), RoutePrefix("sequence")]
    [Permissions(Permissions.Area51.ReadValue)]
    public sealed class SequenceController : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceController"/> class.
        /// </summary>
        public SequenceController()
        {
        }

        #endregion

        #region Routes.

        [Route("listcorruptsequences")]
        [HttpGet, Dispatch(typeof(Parameterless<ListSequenceModel>))]
        public ActionResult ListCorruptSequences()
        {
            return this.View();
        }

        [Route("{id:guid}/updatedates")]
        [HttpPost, Dispatch("ListCorruptSequences", "Sequence")]
        public ActionResult UpdateStartAndEndDate(Identity<ListSequenceModel> request)
        {
            return this.View();
        }

        #endregion
    }
}
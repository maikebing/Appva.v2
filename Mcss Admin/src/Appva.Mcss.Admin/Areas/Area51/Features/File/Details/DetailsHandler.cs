// <copyright file="DetailsHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:kalle.jigfors@appva.se">Kalle Jigfors</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Area51.Features.File.Details
{
    using Admin.Models;
    using Application.Services;
    using Cqrs;
    using Domain.Entities;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Collections;
    using System.Reflection;
    using NPOI.OpenXmlFormats.Spreadsheet;
    using Web;
    using Application.Common;
    using OfficeOpenXml;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DetailsHandler : RequestHandler<Identity<DetailsModel>, DetailsModel>
    {
        #region Variables

        /// <summary>
        /// The <see cref="IFileService"/>
        /// </summary>
        private readonly IFileService fileService;
        private readonly IRoleService roleService;
        private readonly ITaxonomyService taxonomyService;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailsHandler"/> class.
        /// </summary>
        public DetailsHandler(IFileService fileService, ITaxonomyService taxonomyService, IRoleService roleService)
        {
            this.fileService = fileService;
            this.taxonomyService = taxonomyService;
            this.roleService = roleService;
        }

        /// <inheritdoc />
        public override DetailsModel Handle(Identity<DetailsModel> message)
        {
            var response = this.fileService.Find<XLS>(message.Id);

            return new DetailsModel
            {
                FileName = response.Name,
                Accounts = GetData(response),
                Taxons = TaxonomyHelper.SelectList(this.taxonomyService.List(TaxonomicSchema.Organization)),
                Titles = TitleHelper.SelectList(this.roleService.ListVisible())
            };
        }

        private IList<DetailsItemModel> GetData(XLS repsonse)
        {
            ExcelPackage package = null;

            using (var ms = new MemoryStream())
            {
                ms.Write(repsonse.Data, 0, repsonse.Data.Length);
                ms.Position = 0;
                package = new ExcelPackage(ms);
            }

            var workSheet = package.Workbook.Worksheets.First();
            var end = workSheet.Dimension.End;

            var retval = new List<DetailsItemModel>();

            for (int row = 5; row <= end.Row; row++)
            {
                retval.Add(new DetailsItemModel
                {
                    PersonalIdentityNumber = new PersonalIdentityNumber(workSheet.Cells[row, 1].Text),
                    FirstName = workSheet.Cells[row, 2].Text,
                    LastName = workSheet.Cells[row, 3].Text,
                    EmailAddress = workSheet.Cells[row, 4].Text,
                    HsaId = workSheet.Cells[row, 7].Text
                });
            }
            return retval;
        }
    }
}
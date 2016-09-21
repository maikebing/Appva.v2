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
            
            var rows = GetData(response);

            var temps = new List<string> { "  Importmall för medarbetare i MCSS", "Personnummer", "Förnamn", "Efternamn", "E-post", "Roll", "Organisationstillhörighet", "HSA-id", "(ex. 19010101-0001)", "(ex. Bertil)", "(ex. Svensson)", "(ex. bertil.svensson@boendet.se)", "(ex. Undersköterska)", "(ex. Solstrålen våning 1)", "(ex. TST1101100000-10R1001)" };

            foreach (var row in rows)
            {
                foreach (var coll in row.Cells)
                {
                    if (temps.Contains(coll.Value))
                    {
                        coll.Value = string.Empty;
                    }
                }
            }

            var retval = new DetailsModel
            {
                FileName = response.Name,
                Taxons = TaxonomyHelper.SelectList(this.taxonomyService.List(TaxonomicSchema.Organization)),
                Titles = TitleHelper.SelectList(this.roleService.ListVisible())
            };

            retval.Accounts = rows.Where(r => !r.IsEmpty()).ToList().Select(r => new Account
            {
                PersonalIdentityNumber = new PersonalIdentityNumber(r.Cells[0].Value),
                FirstName = r.Cells[1].Value,
                LastName = r.Cells[2].Value,
                FullName = string.Format("{0} {1}", r.Cells[1].Value, r.Cells[2].Value),
                EmailAddress = r.Cells[3].Value,
                HsaId = r.Cells[6].Value
            }).ToList();

            
            return retval;
        }

        private List<A> GetData(XLS repsonse)
        {
            XSSFWorkbook workbook = null;

            using (var ms = new MemoryStream())
            {
                ms.Write(repsonse.Data, 0, repsonse.Data.Length);
                ms.Position = 0;
                workbook = new XSSFWorkbook(ms);
            }

            var rows = new List<List<string>>();
            var testRows = new List<A>();

            ISheet sheet = workbook.GetSheetAt(0);

            MethodInfo methodInfo = sheet.GetType().GetMethod("GetCTWorksheet", BindingFlags.NonPublic | BindingFlags.Instance);
            var ct = (CT_Worksheet)methodInfo.Invoke(sheet, new object[] { });
            var dimen = ct.dimension.@ref.Split(':').Select(s => s.Remove(1)).ToList();

            int colCount = char.ToUpper(Convert.ToChar(dimen[1])) - 64;

            for (int i = 0; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                rows.Add(new List<string>());
                testRows.Add(new A { Row = i.ToString(), Cells = new List<B>() });

                for (int j = 0; j < colCount; j++)
                {
                    if (row != null)
                    {
                        ICell cell = row.GetCell(j);
                        if (cell != null)
                        {
                            rows[i].Add(string.IsNullOrWhiteSpace(cell.StringCellValue) ? string.Empty : cell.StringCellValue);
                            testRows[i].Cells.Add(new B { Value = string.IsNullOrWhiteSpace(cell.StringCellValue) ? string.Empty : cell.StringCellValue });
                        }
                        else
                        {
                            rows[i].Add(string.Empty);
                            testRows[i].Cells.Add(new B { Value = string.Empty });
                        }
                    }
                    else
                    {
                        rows[i].Add(string.Empty);
                        testRows[i].Cells.Add(new B { Value = string.Empty });
                    }
                }
            }
            return testRows;
        }
    }

    public class A
    {
        public string Row { get; set; }
        public IList<B> Cells { get; set; }

        public bool IsEmpty()
        {
            return this.Cells.All(c => c.Value == string.Empty);
        }
    }

    public class B
    {
        public string Value { get; set; }
    }
}
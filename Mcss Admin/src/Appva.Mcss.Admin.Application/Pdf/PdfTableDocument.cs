// <copyright file="PdfTableDocument.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Mcss.Admin.Application.Pdf.Prescriptions;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.VO;
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.Tables;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PdfTableDocument : AbstractPdfDocument
    {
        #region Variables.

        /// <summary>
        /// The high light color.
        /// </summary>
        private static readonly Color Highlight = new Color(236, 203, 203);

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfTableDocument"/> class.
        /// </summary>
        /// <param name="lookAndFeel">The <see cref="PdfLookAndFeel"/></param>
        private PdfTableDocument(PdfLookAndFeel lookAndFeel)
            : base(lookAndFeel)
        {
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="PdfTableDocument"/> class.
        /// </summary>
        /// <param name="lookAndFeel">The <see cref="PdfLookAndFeel"/></param>
        /// <returns>A new <see cref="PdfTableDocument"/> instance</returns>
        public static PdfTableDocument CreateNew(PdfLookAndFeel lookAndFeel)
        {
            return new PdfTableDocument(lookAndFeel);
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Processes the list of <c>PrescriptionList</c> and <c>References</c>.
        /// </summary>
        /// <param name="title">The title</param>
        /// <param name="period">The <see cref="Period"/></param>
        /// <param name="patient">The <see cref="PatientInformation"/></param>
        /// <param name="tasks">A list of <see cref="Task"/></param>
        /// <returns>The current <see cref="PdfScheduleDocument"/></returns>
        public PdfTableDocument Process(string title, Period period, PatientInformation patient, IList<Task> tasks)
        {
            var section = this.AddSection();
            this.CreateHeaderAndFooter(section, title, patient, period);
            this.CreateEntries(section, tasks);
            return this;
        }

        #endregion

        #region PdfDocument Overrides.

        /// <inheritdoc />
        protected override void PageSetup(PageSetup pageSetup)
        {
        }

        #endregion

        #region Private Methods.

        /// <summary>
        /// Creates the prescription entries.
        /// </summary>
        /// <param name="section">The current section</param>
        /// <param name="tasks">The current task list</param>
        private void CreateEntries(Section section, IList<Task> tasks)
        {
            var table = section.AddTable();
            table.Borders.Color = this.TableBorderColor;
            table.Borders.Width = AbstractPdfDocument.TableBorderWidth;
            var size = AbstractPdfDocument.PageWidth.Centimeter / 5;
            table.AddColumn(Unit.FromCentimeter(size));
            table.AddColumn(Unit.FromCentimeter(size));
            table.AddColumn(Unit.FromCentimeter(size));
            table.AddColumn(Unit.FromCentimeter(size));
            table.AddColumn(Unit.FromCentimeter(size));
            var headerRow = this.CreateHeaderRow(table);
            headerRow.Cells[0].AddParagraph("PREPARAT");
            headerRow.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            headerRow.Cells[1].AddParagraph("SKULLE GES TIDPUNKT");
            headerRow.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            headerRow.Cells[2].AddParagraph("SIGNERAD TIDPUNKT");
            headerRow.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            headerRow.Cells[3].AddParagraph("SIGNERAT AV");
            headerRow.Cells[3].Format.Alignment = ParagraphAlignment.Left;
            headerRow.Cells[4].AddParagraph("STATUS");
            headerRow.Cells[4].Format.Alignment = ParagraphAlignment.Left;
            headerRow.Borders.Bottom.Width = 1;
            foreach (var task in tasks)
            {
                var row = table.AddRow();
                row.Format.Alignment = ParagraphAlignment.Left;
                row.VerticalAlignment = VerticalAlignment.Center;
                row.TopPadding = AbstractPdfDocument.TableRowTopPadding;
                row.BottomPadding = AbstractPdfDocument.TableRowBottomPadding;
                row.Cells[0].AddParagraph(task.Name.ToUpper());
                row.Cells[1].AddParagraph(
                    string.Format(
                    "{0:yyyy-MM-dd KL. HH:mm} {1} MIN",
                    task.Scheduled,
                    task.RangeInMinutesBefore.Equals(task.RangeInMinutesAfter) ? "±" + task.RangeInMinutesBefore : "+" + task.RangeInMinutesAfter + " -" + task.RangeInMinutesBefore));
                if (task.OnNeedBasis)
                {
                    row.Cells[2].AddParagraph(string.Format("{0:yyyy-MM-dd 'KL.' HH:mm}", task.UpdatedAt));
                }
                else if (task.IsCompleted)
                {
                    row.Cells[2].AddParagraph(string.Format("{0:yyyy-MM-dd 'KL.' HH:mm}", task.CompletedDate));
                }
                else
                {
                    row.Cells[2].AddParagraph("-");
                }
                if (task.CompletedBy != null)
                {
                    row.Cells[3].AddParagraph(task.CompletedBy.FullName.ToUpper());
                }
                else
                {
                    row.Cells[3].AddParagraph("-");
                }
                if (!task.Delayed)
                {
                    if (task.StatusTaxon != null)
                    {
                        row.Cells[4].AddParagraph(task.StatusTaxon.Weight < 2 ? task.StatusTaxon.Name : "AVVIKELSE: " + task.StatusTaxon.Name.ToUpper());
                    }
                    else
                    {
                        if (task.Status.Equals(1))
                        {
                            row.Cells[4].AddParagraph("OK");
                        }
                        else if (task.Status.Equals(2))
                        {
                            row.Cells[4].AddParagraph("AVVIKELSE: DELVIS GIVEN");
                        }
                        else if (task.Status.Equals(3))
                        {
                            row.Cells[4].AddParagraph("AVVIKELSE: EJ GIVEN");
                        }
                        else if (task.Status.Equals(4))
                        {
                            row.Cells[4].AddParagraph("AVVIKELSE: KAN EJ TA");
                        }
                        else if (task.Status.Equals(5))
                        {
                            row.Cells[4].AddParagraph("AVVIKELSE: MEDSKICKAD");
                        }
                    }
                }
                else
                {
                    row.Cells[4].AddParagraph("AVVIKELSE");
                }
                if (task.Delayed || (task.Status > 1 && task.Status < 5) || (task.StatusTaxon != null ? task.StatusTaxon.Weight > 1 : false))
                {
                    /*
                    row.Cells[0].Shading.Color = Highlight;
                    row.Cells[1].Shading.Color = Highlight;
                    row.Cells[2].Shading.Color = Highlight;
                    row.Cells[3].Shading.Color = Highlight;
                    row.Cells[4].Shading.Color = Highlight;
                    */
                }
            }
        }

        #endregion
    }
}
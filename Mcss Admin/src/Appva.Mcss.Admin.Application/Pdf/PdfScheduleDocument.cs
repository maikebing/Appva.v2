// <copyright file="PdfScheduleDocument.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf
{
    #region Imports.

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.VO;
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.Tables;
    using Prescriptions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PdfScheduleDocument : AbstractPdfDocument
    {
        #region Variables.

        /// <summary>
        /// Whether or not the time column is visible.
        /// </summary>
        private readonly bool isTimeVisible;

        /// <summary>
        /// The header column texts.
        /// </summary>
        private readonly IList<string> columnTexts;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfScheduleDocument"/> class.
        /// </summary>
        /// <param name="lookAndFeel">The <see cref="PdfLookAndFeel"/></param>
        /// <param name="isTimeVisible">Whether or not the time column is visible, defaults to <c>true</c></param>
        /// <param name="columnTexts">Custom header column texts</param>
        private PdfScheduleDocument(PdfLookAndFeel lookAndFeel, bool isTimeVisible = true, IList<string> columnTexts = null)
            : base(lookAndFeel)
        {
            this.isTimeVisible = isTimeVisible;
            this.columnTexts = columnTexts ?? new List<string>
            {
                "INSATS",
                "KL",
                "DAG"
            };
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="PdfScheduleDocument"/> class.
        /// </summary>
        /// <param name="lookAndFeel">The <see cref="PdfLookAndFeel"/></param>
        /// <param name="isTimeVisible">Whether or not the time column is visible, defaults to <c>true</c></param>
        /// <param name="columnTexts">Custom header column texts</param>
        /// <returns>A new <see cref="PdfScheduleDocument"/> instance</returns>
        public static PdfScheduleDocument CreateNew(PdfLookAndFeel lookAndFeel, bool isTimeVisible = true, IList<string> columnTexts = null)
        {
            return new PdfScheduleDocument(lookAndFeel, isTimeVisible, columnTexts);
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Processes the list of <c>PrescriptionList</c> and <c>References</c>.
        /// </summary>
        /// <param name="lists">A list of <see cref="PrescriptionList"/></param>
        /// <param name="references">A list of <see cref="References"/></param>
        /// <returns>The current <see cref="PdfScheduleDocument"/></returns>
        public PdfScheduleDocument Process(IList<PrescriptionList> lists, IList<References> references)
        {
            foreach (var list in lists)
            {
                var section = this.AddSection();
                this.CreateHeaderAndFooter(section, list.Title, list.Patient, list.Period);
                this.CreateEntries(section, list);
                this.CreateSignatures(section, list);
            }
            this.CreateReferences(lists, references);
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
        /// <param name="list">The current list</param>
        private void CreateEntries(Section section, PrescriptionList list)
        {
            var table = section.AddTable();
            table.Borders.Color = this.TableBorderColor;
            table.Borders.Width = AbstractPdfDocument.TableBorderWidth;
            table.Rows.LeftIndent = 0;
            var daysInMonth = CultureInfo.CurrentCulture.Calendar.GetDaysInMonth(list.Period.Start.Year, list.Period.Start.Month);
            var columnSize = (PageWidth.Centimeter - 5.50 - (this.isTimeVisible ? 1.10 : 0)) / daysInMonth;
            var column1 = table.AddColumn(Unit.FromCentimeter(5.50));
            column1.Format.Alignment = ParagraphAlignment.Left;
            if (this.isTimeVisible)
            {
                var column2 = table.AddColumn(Unit.FromCentimeter(1.10));
                column2.Format.Alignment = ParagraphAlignment.Center;
            }
            for (var i = 0; i < daysInMonth; i++)
            {
                var column3 = table.AddColumn(Unit.FromCentimeter(columnSize));
                column3.Format.Alignment = ParagraphAlignment.Center;
            }
            var row1 = this.CreateHeaderRow(table);
            row1.Cells[0].AddParagraph(this.columnTexts[0]);
            row1.Cells[0].Format.Alignment = ParagraphAlignment.Left;
            if (this.isTimeVisible)
            {
                row1.Cells[1].AddParagraph(this.columnTexts[1]);
                row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            }
            var index = this.isTimeVisible ? 2 : 1;
            row1.Cells[index].AddParagraph(this.columnTexts[index]);
            row1.Cells[index].Format.Alignment = ParagraphAlignment.Left;
            row1.Cells[index].MergeRight = daysInMonth - 1;
            var row2 = this.CreateHeaderRow(table);
            row2.Borders.Bottom.Width = 1;
            if (this.isTimeVisible)
            {
                row2.Cells[0].MergeRight = 1;
            }
            for (var j = 1; j <= daysInMonth; j++)
            {
                if (this.isTimeVisible)
                {
                    row2.Cells[j + 1].AddParagraph(j.ToString());
                }
                else
                {
                    row2.Cells[j].AddParagraph(j.ToString());
                }
            }
            foreach (var prescription in list.Prescriptions)
            {
                var itemRow = table.AddRow();
                itemRow.VerticalAlignment = VerticalAlignment.Center;
                itemRow.TopPadding = AbstractPdfDocument.TableRowTopPadding;
                itemRow.BottomPadding = AbstractPdfDocument.TableRowBottomPadding;
                itemRow.Cells[0].Format.Alignment = ParagraphAlignment.Left;
                var paragraph = itemRow.Cells[0].AddParagraph(prescription.Name.ToUpper());
                if (prescription.Reference != null)
                {
                    paragraph.AddText(string.Format(" ({0})", prescription.Reference.Number + 1));
                }
                if (this.isTimeVisible)
                {
                    itemRow.Cells[1].AddParagraph(prescription.Time.HasValue ? string.Format("{0:HH:mm}", prescription.Time.Value.LocalDateTime) : "-");
                    itemRow.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                }
                for (var j = 1; j <= daysInMonth; j++)
                {
                    var key = this.isTimeVisible ? j + 1 : j;
                    itemRow.Cells[key].Format.Font.Size = 5;
                    if (prescription.Days.Contains(j))
                    {
                        itemRow.Cells[key].Shading.Color = Colors.White;
                        if (prescription.Symbols.ContainsKey(j))
                        {
                            itemRow.Cells[key].AddParagraph(prescription.Symbols[j]);
                        }
                    }
                    else
                    {
                        itemRow.Cells[key].Shading.Color = this.TableHeaderColor;
                        itemRow.Cells[key].Format.Font.Size = 8;
                        itemRow.Cells[key].AddParagraph("-");
                    }
                }
            }
            if (list.Symbols == null || list.Symbols.Count == 0)
            {
                return;
            }
            var symbolRow = table.AddRow();
            symbolRow.TopPadding = AbstractPdfDocument.TableRowTopPadding;
            symbolRow.BottomPadding = AbstractPdfDocument.TableRowBottomPadding;
            if (this.isTimeVisible)
            {
                symbolRow.Cells[0].MergeRight = 1;
            }
            symbolRow.Cells[0].Borders.Bottom.Visible = false;
            symbolRow.Cells[0].Borders.Left.Visible = false;
            symbolRow.Cells[2].MergeRight = daysInMonth - 1;
            symbolRow.Cells[2].Format.Font.Bold = false;
            symbolRow.Cells[2].Format.Alignment = ParagraphAlignment.Left;
            var paragraphSymbol = symbolRow.Cells[2].AddParagraph();
            paragraphSymbol.AddFormattedText("SYMBOLER  ", TextFormat.Bold);
            foreach (var symbol in list.Symbols)
            {
                paragraphSymbol.AddFormattedText(symbol.Key.ToUpper(), TextFormat.Italic);
                paragraphSymbol.AddText(": " + symbol.Value.ToUpper() + " ");
            }
        }

        /// <summary>
        /// Creates the signatures.
        /// </summary>
        /// <param name="section">The current section</param>
        /// <param name="list">THe current list</param>
        private void CreateSignatures(Section section, PrescriptionList list)
        {
            if (list.Signatures.Count == 0)
            {
                return;
            }
            var paragraph = section.AddParagraph("SIGNATURFÖRTYDLIGANDE");
            paragraph.Format.Font.Size = 10;
            paragraph.Format.SpaceBefore = Unit.FromCentimeter(0.75);
            paragraph.Format.SpaceAfter = Unit.FromCentimeter(0.75);
            paragraph.Format.KeepWithNext = true;
            var table = section.AddTable();
            table.Borders.Color = this.TableBorderColor;
            table.Borders.Width = AbstractPdfDocument.TableBorderWidth;
            table.Rows.LeftIndent = 0;
            var nameColumn = Unit.FromCentimeter(5.875);
            var signColumn = Unit.FromCentimeter(1.05);
            for (var i = 0; i < 4; i++)
            {
                table.AddColumn(nameColumn);
                table.AddColumn(signColumn);
            }
            var headerRow = this.CreateHeaderRow(table);
            headerRow.Borders.Bottom.Width = 1;
            headerRow.Cells[0].AddParagraph("NAMN");
            headerRow.Cells[1].AddParagraph("SIGN");
            headerRow.Cells[2].AddParagraph("NAMN");
            headerRow.Cells[3].AddParagraph("SIGN");
            headerRow.Cells[4].AddParagraph("NAMN");
            headerRow.Cells[5].AddParagraph("SIGN");
            headerRow.Cells[6].AddParagraph("NAMN");
            headerRow.Cells[7].AddParagraph("SIGN");
            var row = table.AddRow();
            var key = 0;
            for (var i = 0; i < list.Signatures.Count; i++)
            {
                if (key > 7)
                {
                    key = 0;
                    row = table.AddRow();
                }
                row.TopPadding = AbstractPdfDocument.TableRowTopPadding;
                row.BottomPadding = AbstractPdfDocument.TableRowBottomPadding;
                row.Cells[key].AddParagraph(list.Signatures[i].Name.ToUpper());
                row.Cells[key + 1].AddParagraph(list.Signatures[i].Initials.ToUpper());
                row.Cells[key + 1].Format.Alignment = ParagraphAlignment.Center;
                key += 2;
            }
        }

        /// <summary>
        /// Creates references.
        /// </summary>
        /// <param name="lists">The lists</param>
        /// <param name="references">The list of references</param>
        private void CreateReferences(IList<PrescriptionList> lists, IList<References> references)
        {
            if (references == null || references.Count == 0)
            {
                return;
            }
            var first = lists.First();
            var last = lists.Last();
            var section = this.AddSection();
            this.CreateHeaderAndFooter(section, "BESKRIVNINGAR" + " - " + first.Title, first.Patient, new Period(first.Period.Start, last.Period.End));
            foreach (var reference in references)
            {
                var referenceParagraph = section.AddParagraph();
                referenceParagraph.AddFormattedText(string.Format("{0}.", reference.Number + 1), TextFormat.Bold);
                referenceParagraph.AddTab();
                referenceParagraph.AddText(reference.Description);
                referenceParagraph.Format.LeftIndent = "1.5cm";
                referenceParagraph.Format.FirstLineIndent = "-1.5cm";
            }
        }

        #endregion
    }
}
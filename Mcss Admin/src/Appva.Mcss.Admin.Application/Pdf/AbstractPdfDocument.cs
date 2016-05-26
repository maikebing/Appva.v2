// <copyright file="AbstractPdfDocument.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Application.Pdf
{
    #region Imports.

    using System.IO;
    using Appva.Mcss.Admin.Application.Pdf.Prescriptions;
    using Appva.Mcss.Admin.Domain.VO;
    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.Tables;
    using MigraDoc.Rendering;
    using PdfSharp.Pdf;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public abstract class AbstractPdfDocument
    {
        #region Variables.

        /// <summary>
        /// A4 landscape orientation width in centimeters.
        /// </summary>
        protected static readonly Unit A4LandscapeWidth = Unit.FromCentimeter(29.7);

        /// <summary>
        /// The margin left centimeters.
        /// </summary>
        protected static readonly Unit MarginLeft = Unit.FromCentimeter(1.0);

        /// <summary>
        /// The margin right centimeters.
        /// </summary>
        protected static readonly Unit MarginRight = Unit.FromCentimeter(1.0);

        /// <summary>
        /// The margin top centimeters.
        /// </summary>
        protected static readonly Unit MarginTop = Unit.FromCentimeter(4.0);

        /// <summary>
        /// The margin bottom centimeters.
        /// </summary>
        protected static readonly Unit MarginBottom = Unit.FromCentimeter(1.0);

        /// <summary>
        /// The page width.
        /// </summary>
        protected static readonly Unit PageWidth = Unit.FromCentimeter(A4LandscapeWidth.Centimeter - (MarginLeft.Centimeter + MarginRight.Centimeter));

        /// <summary>
        /// The table border width.
        /// </summary>
        protected static readonly Unit TableBorderWidth = Unit.FromPoint(0.1);

        /// <summary>
        /// The table row top padding.
        /// </summary>
        protected static readonly Unit TableRowTopPadding = Unit.FromCentimeter(0.25);

        /// <summary>
        /// The table row bottom padding.
        /// </summary>
        protected static readonly Unit TableRowBottomPadding = Unit.FromCentimeter(0.25);

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPdfDocument"/> class.
        /// </summary>
        /// <param name="lookAndFeel">The <see cref="PdfLookAndFeel"/></param>
        protected AbstractPdfDocument(PdfLookAndFeel lookAndFeel)
        {
            this.Document = new Document();
            this.LookAndFeel = lookAndFeel;
            this.BackgroundColor = new Color(this.LookAndFeel.BackgroundColor.R, this.LookAndFeel.BackgroundColor.G, this.LookAndFeel.BackgroundColor.B);
            this.FontColor = new Color(this.LookAndFeel.FontColor.R, this.LookAndFeel.FontColor.G, this.LookAndFeel.FontColor.B);
            this.TableHeaderColor = new Color(this.LookAndFeel.TableHeaderColor.R, this.LookAndFeel.TableHeaderColor.G, this.LookAndFeel.TableHeaderColor.B);
            this.TableBorderColor = new Color(this.LookAndFeel.TableBorderColor.R, this.LookAndFeel.TableBorderColor.G, this.LookAndFeel.TableBorderColor.B);
            this.CreateStyles(this.Document);
        }

        #endregion

        #region Protected Properties.

        /// <summary>
        /// Returns the look and feel.
        /// </summary>
        protected Document Document
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the look and feel.
        /// </summary>
        protected PdfLookAndFeel LookAndFeel
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the background color.
        /// </summary>
        protected Color BackgroundColor
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the primary font color.
        /// </summary>
        protected Color FontColor
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the table header color.
        /// </summary>
        protected Color TableHeaderColor
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns the table border color.
        /// </summary>
        protected Color TableBorderColor
        {
            get;
            private set;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Saves the pdf to a byte array.
        /// </summary>
        /// <returns>A byte array</returns>
        public byte[] Save()
        {
            using (var stream = new MemoryStream())
            {
                var renderer = new PdfDocumentRenderer(true, PdfFontEmbedding.Always);
                renderer.Document = this.Document;
                renderer.RenderDocument();
                renderer.Save(stream, false);
                return stream.ToArray();
            }
        }

        #endregion

        #region Protected Methods.

        /// <summary>
        /// Adds a new <see cref="Section"/> to the document.
        /// </summary>
        /// <returns>A new <see cref="Section"/> instance</returns>
        protected Section AddSection()
        {
            var section = this.Document.AddSection();
            section.PageSetup = this.CreateNewPageSetup();
            return section;
        }

        /// <summary>
        /// Creates a header row.
        /// </summary>
        /// <param name="table">The table</param>
        /// <param name="isHeaderFormat">Optional header format</param>
        /// <returns>A new <see cref="Row"/> instance</returns>
        protected Row CreateHeaderRow(Table table, bool isHeaderFormat = true)
        {
            var row = table.AddRow();
            row.HeadingFormat = isHeaderFormat;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Format.Font.Color = this.FontColor;
            row.Shading.Color = this.TableHeaderColor;
            row.TopPadding = TableRowTopPadding;
            row.BottomPadding = TableRowBottomPadding;
            return row;
        }

        /// <summary>
        /// Creates header and footer.
        /// <example>
        /// <pre>
        ///     C1    C2    C3     C4    C5
        ///  --------------------------------
        ///  |  0  |  1     2      3     4  | R1
        ///  --------------------------------
        ///  |  0  |  1  |  2   |  3  |  4  | R2
        ///  --------------------------------
        ///  |  0  |  1  |  2   |  3  |  4  | R3
        ///  --------------------------------
        /// </pre> 
        /// </example>
        /// </summary>
        /// <param name="section">The current section</param>
        /// <param name="title">The title</param>
        /// <param name="patient">The patient information</param>
        /// <param name="period">The period</param>
        protected void CreateHeaderAndFooter(Section section, string title, PatientInformation patient, Period period)
        {
            var useLogotype = this.LookAndFeel.IsCustomLogotypeEnabled;
            var useFooter   = this.LookAndFeel.IsCustomFooterTextEnabled;
            var table = section.Headers.Primary.AddTable();
            table.LeftPadding = 0;
            table.RightPadding = 0;
            table.TopPadding = 0;
            table.BottomPadding = 0;
            table.AddColumn(Unit.FromCentimeter(5.5));
            table.AddColumn(Unit.FromCentimeter(13.95 - (useLogotype ? 2.75 : 0)));
            table.AddColumn(Unit.FromCentimeter(2.75));
            table.AddColumn(Unit.FromCentimeter(2.75));
            table.AddColumn(Unit.FromCentimeter(2.75));
            if (useLogotype)
            {
                table.AddColumn(Unit.FromCentimeter(2.75));
            }
            var row1 = table.AddRow();
            row1.BottomPadding = Unit.FromCentimeter(0.75);
            row1.Cells[0].MergeRight = 4;
            row1.Cells[0].AddParagraph(title.ToUpper());
            row1.Cells[0].Format.Font.Size = 13;
            row1.Cells[0].Format.Font.Bold = true;
            if (useLogotype)
            {
                row1.Cells[5].MergeDown = 2;
                var image = row1.Cells[5].AddImage(this.LookAndFeel.LogotypePath);
                image.Width = Unit.FromCentimeter(2.75);
                image.LockAspectRatio = true;
                row1.Cells[5].Format.Alignment = ParagraphAlignment.Right;
            }
            var row2 = table.AddRow();
            row2.Format.Font.Bold = true;
            row2.Cells[0].AddParagraph("PERSONNUMMER");
            row2.Cells[1].AddParagraph("NAMN");
            row2.Cells[2].AddParagraph("FR.O.M.");
            row2.Cells[3].AddParagraph("T.O.M.");
            row2.Cells[4].AddParagraph("SIDA");
            row2.Cells[4].Format.Alignment = useLogotype ? ParagraphAlignment.Left : ParagraphAlignment.Right;
            var row3 = table.AddRow();
            row3.Cells[0].AddParagraph(patient.PersonalIdentityNumber.ToString());
            row3.Cells[1].AddParagraph(patient.Name.ToUpper());
            row3.Cells[2].AddParagraph(string.Format("{0:yyyy-MM-dd}", period.Start));
            row3.Cells[3].AddParagraph(string.Format("{0:yyyy-MM-dd}", period.End));
            var paragraph = row3.Cells[4].AddParagraph();
            paragraph.AddPageField();
            paragraph.AddText(" AV ");
            paragraph.AddNumPagesField();
            paragraph.Format.Alignment = useLogotype ? ParagraphAlignment.Left : ParagraphAlignment.Right;
            row3.BottomPadding = Unit.FromCentimeter(0.75);
            if (useFooter)
            {
                var footerText = section.Footers.Primary.AddParagraph();
                footerText.AddText(this.LookAndFeel.FooterText);
                footerText.Format.Alignment = ParagraphAlignment.Center;
            }
        }

        /// <summary>
        /// Set up the default page configuration.
        /// </summary>
        /// <returns>The <see cref="PageSetup"/> instance</returns>
        protected virtual PageSetup CreateNewPageSetup()
        {
            var setup = this.Document.DefaultPageSetup.Clone();
            setup.LeftMargin     = MarginLeft;
            setup.RightMargin    = MarginRight;
            setup.TopMargin      = MarginTop;
            setup.BottomMargin   = MarginBottom;
            setup.PageFormat     = PageFormat.A4;
            setup.Orientation    = Orientation.Landscape;
            setup.HeaderDistance = Unit.FromCentimeter(1.0);
            setup.FooterDistance = Unit.FromCentimeter(1.0);
            this.PageSetup(setup);
            return setup;
        }

        /// <summary>
        /// Creates global document styles.
        /// </summary>
        /// <param name="document">The current document</param>
        protected virtual void CreateStyles(Document document)
        {
            document.Styles[StyleNames.Normal].Font.Name = "Verdana";
            document.Styles[StyleNames.Normal].Font.Size = 8;
            document.Styles[StyleNames.Normal].Font.Color = this.FontColor;
        }

        /// <summary>
        /// Set up the page configuration.
        /// </summary>
        /// <param name="pageSetup">The <see cref="PageSetup"/> clone</param>
        protected abstract void PageSetup(PageSetup pageSetup);

        #endregion
    }
}
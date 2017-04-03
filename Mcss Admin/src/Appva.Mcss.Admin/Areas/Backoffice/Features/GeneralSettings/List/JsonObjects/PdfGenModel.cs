using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appva.Mcss.Admin.Areas.Backoffice.JsonObjects.Pdf
{
    public class BackgroundColor
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }

    public class FontColor
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }

    public class TableHeaderColor
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }

    public class TableBorderColor
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }

    public class PdfGenObject
    {
        public string LogotypePath { get; set; }
        public bool IsCustomLogotypeEnabled { get; set; }
        public string FooterText { get; set; }
        public bool IsCustomFooterTextEnabled { get; set; }
        public BackgroundColor BackgroundColor { get; set; }
        public FontColor FontColor { get; set; }
        public TableHeaderColor TableHeaderColor { get; set; }
        public TableBorderColor TableBorderColor { get; set; }
    }
}
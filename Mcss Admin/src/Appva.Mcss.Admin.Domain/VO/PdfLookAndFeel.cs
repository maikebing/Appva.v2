// <copyright file="PdfLookAndFeel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using Appva.Common.Domain;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PdfLookAndFeel : ValueObject<PdfLookAndFeel>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfLookAndFeel"/> class.
        /// </summary>
        /// <param name="logotypePath">The custom logotype path, e.g. C://images//logo.png</param>
        /// <param name="footerText">The custom footer text</param>
        /// <param name="backgroundColor">The custom background color</param>
        /// <param name="fontColor">The custom font color</param>
        /// <param name="tableHeaderColor">The custom table header color</param>
        /// <param name="tableBorderColor">The custom table border color</param>
        [JsonConstructor]
        public PdfLookAndFeel(string logotypePath, string footerText, PdfColor backgroundColor, PdfColor fontColor, PdfColor tableHeaderColor, PdfColor tableBorderColor)
        {
            this.LogotypePath              = logotypePath;
            this.IsCustomLogotypeEnabled   = ! string.IsNullOrWhiteSpace(this.LogotypePath);
            this.FooterText                = footerText;
            this.IsCustomFooterTextEnabled = ! string.IsNullOrWhiteSpace(this.FooterText);
            this.BackgroundColor           = backgroundColor;
            this.FontColor                 = fontColor;
            this.TableHeaderColor          = tableHeaderColor;
            this.TableBorderColor          = tableBorderColor;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// A custom logotype path.
        /// </summary>
        [JsonProperty]
        public string LogotypePath
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether or not a custom logotype is enabled.
        /// </summary>
        [JsonProperty]
        public bool IsCustomLogotypeEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// The custom footer text.
        /// </summary>
        [JsonProperty]
        public string FooterText
        {
            get;
            private set;
        }

        /// <summary>
        /// Whether or not a custom footer text is enabled.
        /// </summary>
        [JsonProperty]
        public bool IsCustomFooterTextEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// The background color.
        /// </summary>
        [JsonProperty]
        public PdfColor BackgroundColor
        {
            get;
            private set;
        }

        /// <summary>
        /// The primary font color.
        /// </summary>
        [JsonProperty]
        public PdfColor FontColor
        {
            get;
            private set;
        }

        /// <summary>
        /// The table header color.
        /// </summary>
        [JsonProperty]
        public PdfColor TableHeaderColor
        {
            get;
            private set;
        }

        /// <summary>
        /// The table border color.
        /// </summary>
        [JsonProperty]
        public PdfColor TableBorderColor
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="PdfLookAndFeel"/> class.
        /// </summary>
        /// <param name="logotypePath">The custom logotype path, e.g. C://images//logo.png</param>
        /// <param name="footerText">The custom footer text</param>
        /// <param name="backgroundColor">The custom background color</param>
        /// <param name="fontColor">The custom font color</param>
        /// <param name="tableHeaderColor">The custom table header color</param>
        /// <param name="tableBorderColor">The custom table border color</param>
        /// <returns>A new <see cref="PdfLookAndFeel"/> instance</returns>
        public static PdfLookAndFeel CreateNew(string logotypePath, string footerText, PdfColor backgroundColor, PdfColor fontColor, PdfColor tableHeaderColor, PdfColor tableBorderColor)
        {
            return new PdfLookAndFeel(logotypePath, footerText, backgroundColor, fontColor, tableHeaderColor, tableBorderColor);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PdfLookAndFeel"/> class.
        /// </summary>
        /// <param name="logotypePath">The custom logotype path, e.g. C://images//logo.png</param>
        /// <param name="footerText">The custom footer text</param>
        /// <returns>A new <see cref="PdfLookAndFeel"/> instance</returns>
        public static PdfLookAndFeel CreateDefault(string logotypePath = null, string footerText = null)
        {
            //// Rgb value of hex #ffffff
            var white = PdfColor.CreateNew(255, 255, 255);
            //// Rgb value of hex #eaeff9
            var lightPurple = PdfColor.CreateNew(234, 239, 249);
            //// Rgb value of hex #445068
            var darkPurple = PdfColor.CreateNew(68, 80, 104);
            return new PdfLookAndFeel(logotypePath, footerText, white, darkPurple, lightPurple, darkPurple);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode += (this.LogotypePath == null) ? 0 : this.LogotypePath.GetHashCode();
            hashCode += (this.FooterText == null) ? 0 : this.FooterText.GetHashCode();
            hashCode += (this.BackgroundColor == null) ? 0 : this.BackgroundColor.GetHashCode();
            hashCode += (this.FontColor == null) ? 0 : this.FontColor.GetHashCode();
            hashCode += (this.TableHeaderColor == null) ? 0 : this.TableHeaderColor.GetHashCode();
            hashCode += (this.TableBorderColor == null) ? 0 : this.TableBorderColor.GetHashCode();
            return hashCode;
        }

        /// <inheritdoc />
        public override bool Equals(PdfLookAndFeel other)
        {
            return other != null
                && this.LogotypePath.Equals(other.LogotypePath)
                && this.FooterText.Equals(other.FooterText)
                && this.BackgroundColor.Equals(other.BackgroundColor)
                && this.FontColor.Equals(other.FontColor)
                && this.TableHeaderColor.Equals(other.TableHeaderColor)
                && this.TableBorderColor.Equals(other.TableBorderColor);
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PdfColor : ValueObject<PdfColor>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfColor"/> class.
        /// </summary>
        /// <param name="r">The value of red</param>
        /// <param name="g">The value of green</param>
        /// <param name="b">The value of blue</param>
        [JsonConstructor]
        public PdfColor(byte r, byte g, byte b)
        {
            this.R = r;
            this.G = g;
            this.B = b;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The value of red.
        /// </summary>
        [JsonProperty]
        public byte R
        {
            get;
            private set;
        }

        /// <summary>
        /// The value of green.
        /// </summary>
        [JsonProperty]
        public byte G
        {
            get;
            private set;
        }

        /// <summary>
        /// The value of blue.
        /// </summary>
        [JsonProperty]
        public byte B
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="PdfColor"/> class.
        /// </summary>
        /// <param name="r">The value of red</param>
        /// <param name="g">The value of green</param>
        /// <param name="b">The value of blue</param>
        /// <returns>A new <see cref="PdfColor"/> instance</returns>
        public static PdfColor CreateNew(byte r, byte g, byte b)
        {
            return new PdfColor(r, g, b);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return
                this.R.GetHashCode() +
                this.G.GetHashCode() +
                this.B.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(PdfColor other)
        {
            return other != null
                && this.R.Equals(other.R)
                && this.G.Equals(other.G)
                && this.B.Equals(other.B);
        }

        #endregion
    }
}
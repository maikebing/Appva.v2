// <copyright file="PdfLookAndFeel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Common.Domain;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class PdfLookAndFeel : ValueObject<PdfLookAndFeel>
    {
        #region Variables.

        /// <summary>
        /// A custom logotype path.
        /// </summary>
        [JsonProperty]
        private readonly string logotypePath;

        /// <summary>
        /// Whether or not a custom logotype is enabled.
        /// </summary>
        [JsonProperty]
        private readonly bool isCustomLogotypeEnabled;

        /// <summary>
        /// The custom footer text.
        /// </summary>
        [JsonProperty]
        private readonly string footerText;

        /// <summary>
        /// Whether or not a custom footer text is enabled.
        /// </summary>
        [JsonProperty]
        private readonly bool isCustomFooterTextEnabled;

        /// <summary>
        /// The background color.
        /// </summary>
        [JsonProperty]
        private readonly PdfColor backgroundColor;

        /// <summary>
        /// The primary font color.
        /// </summary>
        [JsonProperty]
        private readonly PdfColor fontColor;

        /// <summary>
        /// The table header color.
        /// </summary>
        [JsonProperty]
        private readonly PdfColor tableHeaderColor;

        /// <summary>
        /// The table border color.
        /// </summary>
        [JsonProperty]
        private readonly PdfColor tableBorderColor;

        #endregion

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
            this.logotypePath              = logotypePath;
            this.isCustomLogotypeEnabled   = ! string.IsNullOrWhiteSpace(this.logotypePath);
            this.footerText                = footerText;
            this.isCustomFooterTextEnabled = !string.IsNullOrWhiteSpace(this.footerText);
            this.backgroundColor           = backgroundColor;
            this.fontColor                 = fontColor;
            this.tableHeaderColor          = tableHeaderColor;
            this.tableBorderColor          = tableBorderColor;
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
        public static PdfLookAndFeel CreateDefault(string logotypePath, string footerText)
        {
            //// Rgb value of hex #ffffff
            var white = PdfColor.CreateNew(255, 255, 255);
            //// Rgb value of hex #eaeff9
            var lightPurple = PdfColor.CreateNew(234, 239, 249);
            //// Rgb value of hex #445068
            var darkPurple  = PdfColor.CreateNew(68, 80, 104);
            return new PdfLookAndFeel(
                logotypePath, 
                footerText,
                white,
                darkPurple,
                lightPurple,
                darkPurple);
        }

        #endregion

        #region Public Properties.

        /// <summary>
        /// Returns the logotype path.
        /// </summary>
        [JsonIgnore]
        public string LogotypePath
        {
            get
            {
                return this.logotypePath;
            }
        }

        /// <summary>
        /// Returns whether or not a custom logotype is enabled.
        /// </summary>
        [JsonIgnore]
        public bool IsCustomLogotypeEnabled
        {
            get
            {
                return this.isCustomLogotypeEnabled;
            }
        }

        /// <summary>
        /// Returns the footer text.
        /// </summary>
        [JsonIgnore]
        public string FooterText
        {
            get
            {
                return this.footerText;
            }
        }

        /// <summary>
        /// Returns whether or not a custom footer text is enabled.
        /// </summary>
        [JsonIgnore]
        public bool IsCustomFooterTextEnabled
        {
            get
            {
                return this.isCustomFooterTextEnabled;
            }
        }

        /// <summary>
        /// Returns the background color.
        /// </summary>
        [JsonIgnore]
        public PdfColor BackgroundColor
        {
            get
            {
                return this.backgroundColor;
            }
        }

        /// <summary>
        /// Returns the primary font color.
        /// </summary>
        [JsonIgnore]
        public PdfColor FontColor
        {
            get
            {
                return this.fontColor;
            }
        }

        /// <summary>
        /// Returns the table header color.
        /// </summary>
        [JsonIgnore]
        public PdfColor TableHeaderColor
        {
            get
            {
                return this.tableHeaderColor;
            }
        }

        /// <summary>
        /// Returns the table border color.
        /// </summary>
        [JsonIgnore]
        public PdfColor TableBorderColor
        {
            get
            {
                return this.tableBorderColor;
            }
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            var hashCode = 0;
            hashCode += (this.logotypePath == null)     ? 0 : this.logotypePath.GetHashCode();
            hashCode += (this.footerText == null)       ? 0 : this.footerText.GetHashCode();
            hashCode += (this.backgroundColor == null)  ? 0 : this.backgroundColor.GetHashCode();
            hashCode += (this.fontColor == null)        ? 0 : this.fontColor.GetHashCode();
            hashCode += (this.tableHeaderColor == null) ? 0 : this.tableHeaderColor.GetHashCode();
            hashCode += (this.tableBorderColor == null) ? 0 : this.tableBorderColor.GetHashCode();
            return hashCode;
        }

        /// <inheritdoc />
        public override bool Equals(PdfLookAndFeel other)
        {
            return other != null
                && this.logotypePath.Equals(other.LogotypePath)
                && this.footerText.Equals(other.FooterText)
                && this.backgroundColor.Equals(other.BackgroundColor)
                && this.fontColor.Equals(other.FontColor)
                && this.tableHeaderColor.Equals(other.TableHeaderColor)
                && this.tableBorderColor.Equals(other.TableBorderColor);
        }

        #endregion
    }

    public sealed class PdfColor : ValueObject<PdfColor>
    {
        #region Variables.

        /// <summary>
        /// The value of red.
        /// </summary>
        [JsonProperty]
        private readonly byte r;

        /// <summary>
        /// The value of green.
        /// </summary>
        [JsonProperty]
        private readonly byte g;

        /// <summary>
        /// The value of blue.
        /// </summary>
        [JsonProperty]
        private readonly byte b;

        #endregion

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
            this.r = r;
            this.g = g;
            this.b = b;
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

        #region Public Properties.

        /// <summary>
        /// Returns the value of red.
        /// </summary>
        [JsonIgnore]
        public byte R
        {
            get
            {
                return this.r;
            }
        }

        /// <summary>
        /// Returns the value of green.
        /// </summary>
        [JsonIgnore]
        public byte G
        {
            get
            {
                return this.g;
            }
        }

        /// <summary>
        /// Returns the value of blue.
        /// </summary>
        [JsonIgnore]
        public byte B
        {
            get
            {
                return this.b;
            }
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return 
                this.r.GetHashCode() +
                this.g.GetHashCode() + 
                this.b.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(PdfColor other)
        {
            return other != null
                && this.r.Equals(other.R)
                && this.g.Equals(other.G)
                && this.b.Equals(other.B);
        }

        #endregion
    }
}
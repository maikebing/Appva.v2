// <copyright file="FileConfiguration.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Domain.VO
{
    #region Imports.

    using System.Collections.Generic;
    using Appva.Common.Domain;
    using Newtonsoft.Json;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class FileConfiguration : ValueObject<FileConfiguration>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FileConfiguration"/> class.
        /// </summary>
        [JsonConstructor]
        private FileConfiguration(ImportPractitionerSettings importPractitionerSettings)
        {
            this.ImportPractitionerSettings = importPractitionerSettings;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The excel practitioner columns.
        /// </summary>
        [JsonProperty]
        public ImportPractitionerSettings ImportPractitionerSettings
        {
            get;
            private set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="FileConfiguration"/> class.
        /// </summary>
        /// <param name="importPractitionerSettings">The <see cref="ImportPractitionerSettings"/>.</param>
        /// <returns>A new <see cref="FileConfiguration"/> instance.</returns>
        public static FileConfiguration CreateNew(ImportPractitionerSettings importPractitionerSettings)
        {
            return new FileConfiguration(importPractitionerSettings);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="FileConfiguration"/> class.
        /// </summary>
        /// <returns>A new <see cref="FileConfiguration"/> instance.</returns>
        public static FileConfiguration CreateDefault()
        {
            var validColumns = new List<string>
            {
                "Personnummer",
                "Förnamn",
                "Efternamn",
                "E-post",
                "Roll",
                "Organisationstillhörighet",
                "HSA-id"
            };

            return new FileConfiguration(ImportPractitionerSettings.CreateNew(validColumns, 2, 4, 1, 3));
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.ImportPractitionerSettings.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(FileConfiguration other)
        {
            return other != null && this.ImportPractitionerSettings.Equals(other.ImportPractitionerSettings);
        }

        #endregion
    }

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ImportPractitionerSettings : ValueObject<ImportPractitionerSettings>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportPractitionerSettings"/> class.
        /// </summary>
        /// <param name="validColumns">The valid columns.</param>
        /// <param name="validateAtRow">Validate columns at the specified row.</param>
        /// <param name="readFromRow">Read data from the specified row.</param>
        /// <param name="skipRows">Specifies how many rows to skip before reading the first practitioner row.</param>
        /// <param name="previewRows">The number of rows that will be previewed.</param>
        [JsonConstructor]
        private ImportPractitionerSettings(IList<string> validColumns, int validateAtRow, int readFromRow, int skipRows, int previewRows)
        {
            this.ValidColumns = validColumns;
            this.ValidateAtRow = validateAtRow;
            this.ReadFromRow = readFromRow;
            this.SkipRows = skipRows;
            this.PreviewRows = previewRows;
        }

        #endregion

        #region Properties.

        /// <summary>
        /// The valid column data.
        /// </summary>
        [JsonProperty]
        public IList<string> ValidColumns
        {
            get;
            private set;
        }

        /// <summary>
        /// Validate cells at the specified row.
        /// </summary>
        [JsonProperty]
        public int ValidateAtRow
        {
            get;
            private set;
        }

        /// <summary>
        /// Reads data from the specified row.
        /// </summary>
        [JsonProperty]
        public int ReadFromRow
        {
            get;
            private set;
        }

        /// <summary>
        /// Specifies how many rows to skip before reading the first practitioner row.
        /// </summary>
        [JsonProperty]
        public int SkipRows
        {
            get;
            set;
        }

        /// <summary>
        /// The number of rows that will be previewed.
        /// </summary>
        [JsonProperty]
        public int PreviewRows
        {
            get;
            set;
        }

        #endregion

        #region Public Static Functions.

        /// <summary>
        /// Creates a new instance of the <see cref="ImportPractitionerSettings"/> class.
        /// </summary>
        /// <param name="validColumns">The valid columns.</param>
        /// <param name="validateAtRow">Validate columns at the specified row.</param>
        /// <param name="readFromRow">Read data from the specified row.</param>
        /// <param name="skipRows">Specifies how many rows to skip before reading the first practitioner row.</param>
        /// <param name="previewRows">The number of rows that will be previewed.</param>
        /// <returns>A new <see cref="ImportPractitionerSettings"/> instance.</returns>
        public static ImportPractitionerSettings CreateNew(IList<string> validColumns, int validateAtRow, int readFromRow, int skipRows, int previewRows)
        {
            return new ImportPractitionerSettings(validColumns, validateAtRow, readFromRow, skipRows, previewRows);
        }

        #endregion

        #region ValueObject Overrides.

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return
                this.ValidColumns.GetHashCode() +
                this.ValidateAtRow.GetHashCode() +
                this.ReadFromRow.GetHashCode() +
                this.PreviewRows.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(ImportPractitionerSettings other)
        {
            return other != null
                && this.ValidColumns.Equals(other.ValidColumns)
                && this.ValidateAtRow.Equals(other.ValidateAtRow)
                && this.ReadFromRow.Equals(other.ReadFromRow)
                && this.PreviewRows.Equals(other.PreviewRows);
        }

        #endregion
    }

    /// <summary>
    /// Temporary enum.
    /// </summary>
    public enum PractitionerColumns
    {
        PIN = 0,
        FirstName,
        LastName,
        Email,
        Roles,
        Organization,
        HSA
    }
}
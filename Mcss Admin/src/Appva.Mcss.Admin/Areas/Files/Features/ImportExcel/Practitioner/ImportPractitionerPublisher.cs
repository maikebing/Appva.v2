// <copyright file="ImportPractitionerPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Log.Handlers
{
    #region Imports.

    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Files.Excel;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ImportPractitionerPublisher : RequestHandler<ImportPractitionerModel, bool>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IRoleService"/>.
        /// </summary>
        private readonly IRoleService roleService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// A dictionary of invalid practitioner rows.
        /// </summary>
        private Dictionary<string, DataRow> invalidRows;

        /// <summary>
        /// The <see cref="Account"/>.
        /// </summary>
        private Account account;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="ImportPractitionerPublisher"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        /// <param name="accountService">The <see cref="IAccountService"/>.</param>
        /// <param name="roleService">The <see cref="IRoleService"/>.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/>.</param>
        public ImportPractitionerPublisher(IPersistenceContext persistence, IFileService fileService, IAccountService accountService, IRoleService roleService, ITaxonomyService taxonomyService)
        {
            this.persistence = persistence;
            this.fileService = fileService;
            this.accountService = accountService;
            this.roleService = roleService;
            this.taxonomyService = taxonomyService;
            this.invalidRows = new Dictionary<string, DataRow>();
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override bool Handle(ImportPractitionerModel message)
        {
            var file = this.fileService.Get(message.FileId);

            if (file == null)
            {
                return false;
            }

            var path = this.fileService.SaveToDisk(file.Name, file.Data);
            var data = ExcelReader.ReadPractitionersFromExcel(path, message.ValidateAtRow, message.ValidColumns, message.ReadFromRow);
            File.Delete(path);
            this.ImportData(data, message.ValidColumns, message.ExcludedRoles, message.IncludedRolesWithoutHsaId);

            return true;
        }

        #endregion

        #region Private methods.

        /// <summary>
        /// Imports data from the <see cref="DataTable"/>.
        /// </summary>
        /// <param name="data">The <see cref="DataTable"/>.</param>
        /// <param name="validColumns">The columns.</param>
        /// <param name="excludedRoles">The excluded roles.</param>
        /// <param name="includedRolesWithoutHsaId">Roles without HSA id.</param>
        private void ImportData(DataTable data, string[] validColumns, string excludedRoles, string includedRolesWithoutHsaId)
        {
            var roles = this.roleService.List();

            for (int i = 1; i < data.Rows.Count; i++)
            {
                this.account = new Account();

                for (int j = 0; j < data.Columns.Count; j++)
                {
                    var columnName = data.Rows[0][data.Columns[j]].ToString();
                    var columnValue = data.Rows[i][data.Columns[j]].ToString();
                    var row = data.Rows[j];
                    this.ValidatePersonalIdentityNumber(row, columnValue, "Ogiltigt personnummer.");
                    this.ValidateName(row, columnValue, (columnName == validColumns[1] ? "Förnamn" : "Efternamn") + " saknas.");
                    this.ValidateEmailAddress(row, columnValue, "Ogiltig e-postadress.");
                    this.ValidateRoles(roles, row, excludedRoles, columnValue, new string[] { "Exkluderad roll.", "Rollen finns ej i MCSS.", "Roll saknas." });
                    this.ValidateOrganizationNodes(row, columnValue, new string[] { "Organisation saknas.", "Organisationsnod hittades ej." });
                    this.ValidateHsaId(row, columnValue, includedRolesWithoutHsaId, "HSA-id saknas.");
                }

                if (this.invalidRows.Count > 0)
                {
                    continue;
                }

                // TODO: save the account if invalidRows.Count == 0.
            }
        }

        /// <summary>
        /// Validates the personal identity number.
        /// </summary>
        /// <param name="row">The current row.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reason">The reason if value is not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidatePersonalIdentityNumber(DataRow row, string columnValue, string reason)
        {
            var personalIdentityNumber = new PersonalIdentityNumber(columnValue);
            var isValid = personalIdentityNumber.IsValid();

            if (isValid && this.accountService.FindByPersonalIdentityNumber(personalIdentityNumber) != null)
            {
                return false;
            }
            else if (isValid == false)
            {
                this.invalidRows.Add(reason, row);
                return false;
            }
            else
            {
                this.account.PersonalIdentityNumber = personalIdentityNumber;
                return true;
            }
        }

        /// <summary>
        /// Validates the name.
        /// </summary>
        /// <param name="row">The current row.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reason">The reason if value is not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateName(DataRow row, string columnValue, string reason)
        {
            if (string.IsNullOrWhiteSpace(columnValue))
            {
                this.invalidRows.Add(reason, row);
                return false;
            }
            else
            {
                this.account.FirstName = columnValue.Trim().FirstToUpper();
                return true;
            }
        }

        /// <summary>
        /// Validates the email address.
        /// </summary>
        /// <param name="row">The current row.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reason">The reason if value is not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateEmailAddress(DataRow row, string columnValue, string reason)
        {
            MailAddress emailAddress = null;

            try
            {
                emailAddress = new MailAddress(columnValue);
            }
            catch
            {
                this.invalidRows.Add(reason, row);
                return false;
            }

            this.account.EmailAddress = emailAddress.Address;
            return true;
        }

        /// <summary>
        /// Validates the role.
        /// </summary>
        /// <param name="roles">A collection of roles.</param>
        /// <param name="row">The current row.</param>
        /// <param name="excludedRoles">The excluded rows.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reasons">The reasons if values are not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateRoles(IList<Role> roles, DataRow row, string excludedRoles, string columnValue, string[] reasons)
        {
            if (string.IsNullOrWhiteSpace(excludedRoles) == false && excludedRoles.ToLower().Contains(columnValue.ToLower()))
            {
                this.invalidRows.Add(reasons[0], row);
                return false;
            }
            if (roles.Where(x => x.Id == columnValue.ToGuid()).FirstOrDefault() == null)
            {
                this.invalidRows.Add(reasons[1], row);
                return false;
            }
            if (string.IsNullOrWhiteSpace(columnValue))
            {
                this.invalidRows.Add(reasons[2], row);
                return false;
            }

            this.account.Roles = new List<Role> { this.roleService.Find(columnValue.ToGuid()) };
            return true;
        }

        /// <summary>
        /// Validates the organization nodes.
        /// </summary>
        /// <param name="row">The current row.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reasons">The reasons if values are not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateOrganizationNodes(DataRow row, string columnValue, string[] reasons)
        {
            if (string.IsNullOrWhiteSpace(columnValue))
            {
                this.invalidRows.Add(reasons[0], row);
                return false;
            }

            var nodes = columnValue.Split(',');
            var node = this.taxonomyService.Get(nodes[nodes.Length - 1].Trim().FirstToUpper().ToGuid());

            if (node == null)
            {
                this.invalidRows.Add(reasons[1], row);
                return false;
            }

            this.account.Taxon = node;
            return true;
        }

        /// <summary>
        /// Validates the email address.
        /// </summary>
        /// <param name="row">The current row.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="includedRolesWithoutHsaId">Roles without HSA id.</param>
        /// <param name="reason">The reason if value is not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateHsaId(DataRow row, string columnValue, string includedRolesWithoutHsaId, string reason)
        {
            if (string.IsNullOrWhiteSpace(columnValue) && includedRolesWithoutHsaId.ToLower().Contains(columnValue.Trim().ToLower()) == false)
            {
                this.invalidRows.Add(reason, row);
            }

            this.account.HsaId = columnValue;
            return true;
        }

        #endregion
    }
}
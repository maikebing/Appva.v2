﻿/*
// <copyright file="PractitionerPreviewPublisher.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:emmanuel.hansson@appva.com">Emmanuel Hansson</a>
// </author>

namespace Appva.Mcss.Admin.Models.Handlers
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Files.Excel;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using Appva.Core.IO;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerPreviewPublisher : RequestHandler<PractitionerPreviewModel, PractitionerImportModel>
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
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// A list of invalid practitioner rows.
        /// </summary>
        private List<InvalidPractitionerData> invalidRows;

        /// <summary>
        /// The <see cref="Account"/>.
        /// </summary>
        private Account account;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerPreviewPublisher"/> class.
        /// </summary>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        /// <param name="accountService">The <see cref="IAccountService"/>.</param>
        /// <param name="roleService">The <see cref="IRoleService"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        public PractitionerPreviewPublisher(
            IPersistenceContext persistence, 
            IFileService fileService, 
            IAccountService accountService, 
            IRoleService roleService,
            ISettingsService settingsService)
        {
            this.persistence = persistence;
            this.fileService = fileService;
            this.accountService = accountService;
            this.roleService = roleService;
            this.settingsService = settingsService;
            this.invalidRows = new List<InvalidPractitionerData>();
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override PractitionerImportModel Handle(PractitionerPreviewModel message)
        {
            var model = new PractitionerImportModel();
            var settings = this.settingsService.Find(ApplicationSettings.FileConfiguration);
            var file = this.fileService.Get(message.FileId);

            if (file == null)
            {
                return model;
            }

            var path = this.fileService.SaveToDisk(file.Name, file.Data);
            int lastRow;
            var data = ExcelReader.ReadPractitioners(
                path,
                settings.ImportPractitionerSettings.ValidateAtRow, 
                settings.ImportPractitionerSettings.ValidColumns,
                settings.ImportPractitionerSettings.ReadFromRow,
                out lastRow
            );
            File.Delete(path);

            model.ImportedRowsCount = this.ImportData(data, settings.ImportPractitionerSettings.ValidColumns, message.ExcludedRoles, message.IncludedRolesWithoutHsaId);

            var templatePath = PathResolver.ResolveAppRelativePath("Templates\\PractitionerTemplate.xlsx");
            var invalidDataToExcel = ExcelWriter.Write(templatePath, this.invalidRows, message.ReadFromRow);
            model.FileId = this.Persist(invalidDataToExcel);
            model.FileName = file.Name;
            model.InvalidRowsCount = invalidRows.Count;
            return model;
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
        /// <returns>The number of imported rows.</returns>
        private int ImportData(DataTable data, IList<string> validColumns, string excludedRoles, string includedRolesWithoutHsaId)
        {
            int importedRowCount = 0;
            var roles = this.roleService.List();

            for (int i = 1; i < data.Rows.Count; i++)
            {
                this.account = new Account();
                var errors = new List<string>(data.Columns.Count);
                var role = string.Empty;

                for (int j = 0; j < data.Columns.Count; j++)
                {
                    var columnName = data.Rows[0][data.Columns[j]].ToString();
                    var columnValue = data.Rows[i][data.Columns[j]].ToString();

                    if (columnName == validColumns[0])
                    {
                        this.ValidatePersonalIdentityNumber(errors, columnValue, "Ogiltigt personnummer");
                    }
                    else if (columnName == validColumns[1] || columnName == validColumns[2])
                    {
                        this.ValidateName(errors, columnValue, (columnName == validColumns[1] ? "Förnamn" : "Efternamn") + " saknas");
                    }
                    else if (columnName == validColumns[3])
                    {
                        this.ValidateEmailAddress(errors, columnValue, "Ogiltig e-postadress");
                    }
                    else if (columnName == validColumns[4])
                    {
                        role = columnValue;
                        this.ValidateRoles(errors, excludedRoles, columnValue, new string[] { "Exkluderad roll", "Rollen finns ej i MCSS", "Roll saknas" });
                    }
                    else if (columnName == validColumns[5])
                    {
                        this.ValidateOrganizationNodes(errors, columnValue, new string[] { "Organisation saknas", "Organisationsnod hittades ej" });
                    }
                    else if (columnName == validColumns[6])
                    {
                        this.ValidateHsaId(errors, columnValue, role, includedRolesWithoutHsaId, "HSA-id saknas");
                    }
                }

                if (errors.Count > 0)
                {
                    this.MapRowToModel(data.Rows[i], errors);
                    continue;
                }

                // TODO: save the account if errors.Count == 0.
                importedRowCount++;
            }

            return importedRowCount;
        }

        /// <summary>
        /// Validates the personal identity number.
        /// </summary>
        /// <param name="errors">A list of validation errors.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reason">The reason if value is not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidatePersonalIdentityNumber(List<string> errors, string columnValue, string reason)
        {
            var personalIdentityNumber = new PersonalIdentityNumber(columnValue);
            var isValid = personalIdentityNumber.IsValid();

            if (isValid && this.accountService.FindByPersonalIdentityNumber(personalIdentityNumber) != null)
            {
                return false;
            }
            else if (isValid == false)
            {
                errors.Add(reason);
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
        /// <param name="errors">A list of validation errors.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reason">The reason if value is not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateName(List<string> errors, string columnValue, string reason)
        {
            if (string.IsNullOrWhiteSpace(columnValue))
            {
                errors.Add(reason);
                return false;
            }
            else
            {
                this.account.FirstName = columnValue.Trim().ToLower().FirstToUpper();
                return true;
            }
        }

        /// <summary>
        /// Validates the email address.
        /// </summary>
        /// <param name="errors">A list of validation errors.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reason">The reason if value is not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateEmailAddress(List<string> errors, string columnValue, string reason)
        {
            MailAddress emailAddress = null;

            try
            {
                emailAddress = new MailAddress(columnValue);
            }
            catch
            {
                errors.Add(reason);
                return false;
            }

            this.account.EmailAddress = emailAddress.Address;
            return true;
        }

        /// <summary>
        /// Validates the role.
        /// </summary>
        /// <param name="errors">A list of validation errors.</param>
        /// <param name="excludedRoles">The excluded rows.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reasons">The reasons if values are not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateRoles(List<string> errors, string excludedRoles, string columnValue, string[] reasons)
        {
            if (string.IsNullOrWhiteSpace(excludedRoles) == false && excludedRoles.ToLower().Contains(columnValue.ToLower()))
            {
                errors.Add(reasons[0]);
                return false;
            }
            var role = this.roleService.List().Where(x => x.Name.ToLower() == columnValue.Trim().ToLower()).FirstOrDefault();
            if (role == null)
            {
                errors.Add(reasons[1]);
                return false;
            }
            if (string.IsNullOrWhiteSpace(columnValue))
            {
                errors.Add(reasons[2]);
                return false;
            }

            this.account.Roles = new List<Role> { role };
            return true;
        }

        /// <summary>
        /// Validates the organization nodes.
        /// </summary>
        /// <param name="errors">A list of validation errors.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="reasons">The reasons if values are not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateOrganizationNodes(List<string> errors, string columnValue, string[] reasons)
        {
            if (string.IsNullOrWhiteSpace(columnValue))
            {
                errors.Add(reasons[0]);
                return false;
            }

            var inputNodes = columnValue.Split(',');
            var nodes = this.persistence.QueryOver<Taxon>()
                .JoinQueryOver<Taxonomy>(x => x.Taxonomy)
                    .Where(x => x.MachineName == TaxonomicSchema.Organization.Id)
                        .List();

            Taxon node = null;
            var ids = new List<Guid>();

            // TODO: Replace with a faster solution for finding organization nodes.
            // E.g. check if the last node contains the paths of the previous nodes.

            for (int i = 0; i < inputNodes.Length; i++)
            {
                var currentNode = inputNodes[i].Trim().ToLower().FirstToUpper();

                if(ids.Any())
                {
                    node = nodes.Where(x => x.Name == currentNode && ids.Contains(x.Parent.Id)).FirstOrDefault();
                }
                if(node == null)
                {
                    node = nodes.Where(x => x.Name == currentNode).FirstOrDefault();
                }
                if (i != inputNodes.Length - 1)
                {
                    ids = nodes.Where(x => x.Name == currentNode).Select(x => x.Id).ToList();
                }
            }

            if (node == null )
            {
                errors.Add(reasons[1]);
                return false;
            }

            this.account.Taxon = node;
            return true;
        }

        /// <summary>
        /// Validates the email address.
        /// </summary>
        /// <param name="errors">A list of validation errors.</param>
        /// <param name="columnValue">The column value.</param>
        /// <param name="role">The practitioner role.</param>
        /// <param name="includedRolesWithoutHsaId">Roles without HSA id.</param>
        /// <param name="reason">The reason if value is not valid.</param>
        /// <returns><see cref="bool"/>.</returns>
        private bool ValidateHsaId(List<string> errors, string columnValue, string role, string includedRolesWithoutHsaId, string reason)
        {
            if (string.IsNullOrWhiteSpace(columnValue) && includedRolesWithoutHsaId.ToLower().Contains(role.Trim().ToLower()) == false)
            {
                errors.Add(reason);
            }

            this.account.HsaId = columnValue;
            return true;
        }

        /// <summary>
        /// Maps row data to a new <see cref="InvalidPractitionerData"/> object.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="errors"></param>
        private void MapRowToModel(DataRow row, List<string> errors)
        {
            this.invalidRows.Add(new InvalidPractitionerData
            {
                PersonalIdentityNumber = row[0].ToString(),
                FirstName = row[1].ToString(),
                LastName = row[2].ToString(),
                EmailAddress = row[3].ToString(),
                Role = row[4].ToString(),
                OrganizationNode = row[5].ToString(),
                HsaId = row[6].ToString(),
                Errors = errors[0] + (errors.Count > 2 ? " och " + (errors.Count - 1) + " andra fel." : (errors.Count == 2 ? " och 1 annat fel." : "." ))
            });
        }

        /// <summary>
        /// Saves invalid practitioner rows.
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <returns>The file id.</returns>
        private Guid Persist(byte[] bytes)
        {
            var file = new DataFile
            {
                Name = string.Format("inläsningsfel-{0}.xlsx", DateTime.Now.ToShortDateString()),
                Data = bytes,
                Title = "Excel: inläsningsfel",
                Description = "Innehåller rader av medarbetare som inte kunde importeras till MCSS.",
                ContentType = "application/octet-stream"
            };

            this.persistence.Save(file);
            return file.Id;
        }

        #endregion
    }
}
*/
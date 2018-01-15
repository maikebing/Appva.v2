// <copyright file="PractitionerOrganizationHandler.cs" company="Appva AB">
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
    using Appva.Core.Extensions;
    using Appva.Cqrs;
    using Appva.Files.Excel;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Security.Identity;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Mcss.Web;
    using Appva.Persistence;
    using Newtonsoft.Json;
    using Appva.Mcss.Admin.Domain.VO;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PractitionerOrganizationHandler : RequestHandler<Identity<PractitionerOrganizationModel>, PractitionerOrganizationModel>
    {
        #region Fields.

        /// <summary>
        /// The <see cref="IFileService"/>.
        /// </summary>
        private readonly IFileService fileService;

        /// <summary>
        /// The <see cref="ISettingsService"/>.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The <see cref="ITaxonomyService"/>.
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        /// <summary>
        /// The <see cref="IIdentityService"/>.
        /// </summary>
        private readonly IIdentityService identityService;

        /// <summary>
        /// The <see cref="IAccountService"/>.
        /// </summary>
        private readonly IAccountService accountService;

        /// <summary>
        /// The <see cref="IPersistenceContext"/>.
        /// </summary>
        private readonly IPersistenceContext persistence;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="PractitionerOrganizationHandler"/> class.
        /// </summary>
        /// <param name="fileService">The <see cref="IFileService"/>.</param>
        /// <param name="settingsService">The <see cref="ISettingsService"/>.</param>
        /// <param name="taxonomyService">The <see cref="ITaxonomyService"/>.</param>
        /// <param name="identityService">The <see cref="IIdentityService"/>.</param>
        /// <param name="accountService">The <see cref="IAccountService"/>.</param>
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public PractitionerOrganizationHandler(
            IFileService fileService, 
            ISettingsService settingsService,
            ITaxonomyService taxonomyService,
            IIdentityService identityService,
            IAccountService accountService,
            IPersistenceContext persistence)
        {
            this.fileService     = fileService;
            this.settingsService = settingsService;
            this.taxonomyService = taxonomyService;
            this.identityService = identityService;
            this.accountService  = accountService;
            this.persistence     = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override PractitionerOrganizationModel Handle(Identity<PractitionerOrganizationModel> message)
        {
            var file       = this.fileService.Get(message.Id);
            var model      = new PractitionerOrganizationModel();
            var properties = JsonConvert.DeserializeObject<FileUploadProperties>(file.Properties);

            if (file == null ||
                properties.PractitionerImportProperties == null ||
                properties.PractitionerImportProperties.SelectedFirstRow.HasValue == false)
            {
                return model;
            }

            var id       = this.identityService.PrincipalId;
            var user     = this.accountService.Find(id);
            var settings = this.settingsService.Find(ApplicationSettings.FileConfiguration);
            var path     = this.fileService.SaveToDisk(file.Name, file.Data);
            var data     = ExcelReader.ReadPractitioners(
                path,
                settings.ImportPractitionerSettings.ValidateAtRow,
                settings.ImportPractitionerSettings.ValidColumns,
                out int lastRow,
                properties.PractitionerImportProperties.SelectedFirstRow.Value - 1,
                properties.PractitionerImportProperties.SelectedLastRow - 1
            );
            File.Delete(path);

            model.Id            = file.Id;
            model.Taxons        = TaxonomyHelper.CreateItems(user, null, this.taxonomyService.List(TaxonomicSchema.Organization));
            model.ImportedNodes = new List<string>();
            model.SelectedNodes = new List<string>();
            model.ParsedNodes   = new List<KeyValuePair<Guid, string>>();
            model.ImportedNodes = this.GetDistinctDataFrom(data, (int)PractitionerColumns.Organization).ToList();
            this.SetRolesToFileProperties(this.GetDistinctDataFrom(data, (int)PractitionerColumns.Roles).ToList(), properties, file);
            this.ParseNodes(model.ImportedNodes, model.ParsedNodes);
            return model;
        }

        #endregion

        #region Private methods.

        /// <summary>
        /// Gets unique rows by the specified index.
        /// </summary>
        /// <param name="data">The <see cref="DataTable"/>.</param>
        /// <param name="index">The column index.</param>
        /// <returns>A <see cref="IEnumerable{string}"/>.</returns>
        private IEnumerable<string> GetDistinctDataFrom(DataTable dataTable, int index)
        {
            var hashSet = new HashSet<string>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var column = dataTable.Rows[i][dataTable.Columns[index]].ToString();
                if (hashSet.Add(column))
                {
                    yield return column;
                }
            }
        }

        /// <summary>
        /// Stores imported roles to file properties.
        /// </summary>
        /// <param name="roles">The roles.</param>
        /// <param name="properties">The <see cref="FileUploadProperties"/>.</param>
        /// <param name="file">The <see cref="DataFile"/>.</param>
        private void SetRolesToFileProperties(IList<string> roles, FileUploadProperties properties, DataFile file)
        {
            var list = new List<KeyValuePair<string, Guid>>();
            foreach (var role in roles)
            {
                list.Add(new KeyValuePair<string, Guid>(role, Guid.Empty));
            }
            properties.PractitionerImportProperties.Roles = list;
            file.Properties = JsonConvert.SerializeObject(properties);
            this.fileService.Save(file);
        }

        /// <summary>
        /// Attempts to find matching organization nodes from the provided list.
        /// </summary>
        /// <param name="importedNodes">A collection of nodes from the imported file.</param>
        /// <param name="parsedNodes">The parsed nodes.</param>
        private void ParseNodes(IList<string> importedNodes, IList<KeyValuePair<Guid, string>> parsedNodes)
        {
            var ids = new List<Guid>();
            var nodes = this.Split(importedNodes, ',');
            var taxons = this.persistence.QueryOver<Taxon>()
                .JoinQueryOver(x => x.Taxonomy)
                    .Where(x => x.MachineName == TaxonomicSchema.Organization.Id)
                    .List();

            foreach (var nodeArray in nodes)
            {
                Taxon taxon = null;
                for (int i = 0; i < nodeArray.Length; i++)
                {
                    var node = nodeArray[i].Trim().ToLower().FirstToUpper();
                    if(ids.Any())
                    {
                        taxon = taxons.Where(x => x.Name == node && ids.Contains(x.Parent.Id)).FirstOrDefault();
                    }

                    if (taxon == null)
                    {
                        taxon = taxons.Where(x => x.Name == node).FirstOrDefault();
                    }

                    if (i != nodeArray.Length - 1)
                    {
                        ids = taxons.Where(x => x.Name == node).Select(x => x.Id).ToList();
                    }
                }

                if(taxon != null)
                {
                    parsedNodes.Add(new KeyValuePair<Guid, string>(taxon.Id, this.GetPathNameSequenceFrom(taxon.Path, taxons)));
                }
                else
                {
                    parsedNodes.Add(new KeyValuePair<Guid, string>(Guid.Empty, "<span class='previewed-node node-not-found'>Hittades ej</span>"));
                }
            }
        }

        /// <summary>
        /// Gets the full path name of a node.
        /// </summary>
        /// <param name="taxonPath">The taxon path.</param>
        /// <param name="taxons">A list of organization taxons.</param>
        /// <returns></returns>
        private string GetPathNameSequenceFrom(string taxonPath, IList<Taxon> taxons)
        {
            var pathNameSequence = new List<string>();
            var paths = taxonPath.Split('.');

            foreach (var path in paths)
            {
                Guid id = Guid.Empty;
                if(string.IsNullOrWhiteSpace(path) == false && Guid.TryParse(path, out id))
                {
                    pathNameSequence.Add(string.Format("<span class='previewed-node'>{0}</span>", taxons.Where(x => x.Id == id).First().Name));
                }
            }

            return string.Join("", pathNameSequence);
        }

        /// <summary>
        /// Split nodes extension.
        /// </summary>
        /// <param name="nodes">The list of nodes.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A <see cref="IList{string[]}"/>.</returns>
        private IList<string[]> Split(IList<string> nodes, char separator)
        {
            var list = new List<string[]>();

            foreach (var node in nodes)
            {
                list.Add(node.Split(separator));
            }

            return list;
        }

        #endregion
    }
}
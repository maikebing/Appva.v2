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
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Application.Services.Settings;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Models;
    using Appva.Persistence;
    using Newtonsoft.Json;

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
        /// <param name="persistence">The <see cref="IPersistenceContext"/>.</param>
        public PractitionerOrganizationHandler(IFileService fileService, ISettingsService settingsService, IPersistenceContext persistence)
        {
            this.fileService = fileService;
            this.settingsService = settingsService;
            this.persistence = persistence;
        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override PractitionerOrganizationModel Handle(Identity<PractitionerOrganizationModel> message)
        {
            var file = this.fileService.Get(message.Id);
            var model = new PractitionerOrganizationModel();
            var properties = JsonConvert.DeserializeObject<FileUploadProperties>(file.Properties);

            if (file == null ||
                properties.PractitionerImportProperties == null ||
                properties.PractitionerImportProperties.IsImportable == false)
            {
                return model;
            }

            var settings = this.settingsService.Find(ApplicationSettings.FileConfiguration);
            var path = this.fileService.SaveToDisk(file.Name, file.Data);
            var data = ExcelReader.ReadPractitioners(
                path,
                settings.ImportPractitionerSettings.ValidateAtRow,
                settings.ImportPractitionerSettings.ValidColumns,
                out int lastRow,
                properties.PractitionerImportProperties.SelectedFirstRow.Value,
                properties.PractitionerImportProperties.SelectedLastRow
            );
            File.Delete(path);

            model.Id = file.Id;
            model.Nodes = new List<string>();
            model.SelectedNodeNames = new List<string>();
            model.SelectedNodeIds = new List<Guid>();
            model.Nodes = this.GetNodesFrom(data, settings.ImportPractitionerSettings.ValidColumns.IndexOf(
                settings.ImportPractitionerSettings.ValidColumns[5])
            );
            this.ParseNodes(model.Nodes, model.SelectedNodeNames, model.SelectedNodeIds);
            return model;
        }

        #endregion

        #region Private methods.

        /// <summary>
        /// Gets a collection of unique organization nodes.
        /// </summary>
        /// <param name="data">The <see cref="DataTable"/>.</param>
        /// <param name="columnIndex">The column index.</param>
        /// <returns>A collection of unique organization nodes.</returns>
        private IList<string> GetNodesFrom(DataTable data, int columnIndex)
        {
            var nodes = new List<string>();

            for (var i = 1; i < data.Rows.Count; i++)
            {
                if(nodes.Contains(data.Rows[i][data.Columns[columnIndex]].ToString()) == false)
                {
                    nodes.Add(data.Rows[i][data.Columns[columnIndex]].ToString());
                }
            }

            return nodes;
        }

        /// <summary>
        /// Attempts to find matching organization nodes from the provided list.
        /// </summary>
        /// <param name="nodesFromFile">A collection of organization nodes.</param>
        /// <param name="selectedNodeIds">The selected node names.</param>
        /// <param name="selectedNodeNames">The selected node ids.</param>
        private void ParseNodes(IList<string> nodesFromFile, IList<string> selectedNodeNames, IList<Guid> selectedNodeIds)
        {
            var ids = new List<Guid>();
            var nodes = this.Split(nodesFromFile, ',');
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
                    selectedNodeNames.Add(this.GetPathNameSequenceFrom(taxon.Path, taxons));
                    selectedNodeIds.Add(taxon.Id);
                }
                else
                {
                    selectedNodeNames.Add("<span class='selected-node node-not-found'>Hittades ej</span>");
                    selectedNodeIds.Add(Guid.Empty);
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
            var pathArray = taxonPath.Split('.');

            foreach (var path in pathArray)
            {
                Guid id = Guid.Empty;
                if(string.IsNullOrWhiteSpace(path) == false && Guid.TryParse(path, out id))
                {
                    pathNameSequence.Add(string.Format("<span class='selected-node'>{0}</span>", taxons.Where(x => x.Id == id).First().Name));
                }
            }

            return string.Join(" ", pathNameSequence);
        }

        /// <summary>
        /// Split nodes.
        /// </summary>
        /// <param name="nodes">The node list.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>A collection of string arrays.</returns>
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
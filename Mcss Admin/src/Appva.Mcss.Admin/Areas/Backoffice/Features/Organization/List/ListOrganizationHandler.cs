// <copyright file="ListOrganizationHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Handlers
{
    #region Imports

    using Appva.Cqrs;
    using Appva.Mcss.Admin.Application.Common;
    using Appva.Mcss.Admin.Application.Models;
    using Appva.Mcss.Admin.Application.Services;
    using Appva.Mcss.Admin.Areas.Backoffice.Models;
    using Appva.Mcss.Admin.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class ListOrganizationHandler : RequestHandler<Parameterless<ListOrganizationModel>, ListOrganizationModel>
    {
        #region Fields

        /// <summary>
        /// The <see cref="ITaxonomyService"/>
        /// </summary>
        private readonly ITaxonomyService taxonomyService;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListOrganizationHandler"/> class.
        /// </summary>
        public ListOrganizationHandler(ITaxonomyService taxonomyService)
        {
            this.taxonomyService = taxonomyService;
        }

        #endregion

        #region RequestHandler overrides

        /// <inheritdoc />
        public override ListOrganizationModel Handle(Parameterless<ListOrganizationModel> message)
        {            
            var nodes = taxonomyService.List(TaxonomicSchema.Organization);
            var root = taxonomyService.Roots(TaxonomicSchema.Organization).First();

            return new ListOrganizationModel
            {
                Nodes = BuildHierarchy(root, nodes),
                Root = new Node<Guid>() { Id = root.Id, Name = root.Name }
            };
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns the flattened hierarchy in correct order.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="fullTree"></param>
        private IEnumerable<Node<Guid>> BuildHierarchy(ITaxon node, IList<ITaxon> fullTree)
        {
            var children = fullTree
                .Where(x => x.IsRoot == false && x.Parent.Equals(node)) // sust: Active == true does not exist on ITaxon, but existed in mvc3 projekt
                .OrderBy(x => x.Sort).ThenBy(x => x.Name);
            foreach (var child in children)
            {
                yield return new Node<Guid>
                {
                    Id = child.Id,
                    Name = child.Name,
                    Children = BuildHierarchy(child, fullTree)
                };
            }
        }

        #endregion
    }
}
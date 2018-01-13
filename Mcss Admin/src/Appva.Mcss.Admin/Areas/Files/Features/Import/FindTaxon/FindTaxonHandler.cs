// <copyright file="FindTaxonHandler.cs" company="Appva AB">
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
    using Appva.Cqrs;
    using Appva.Mcss.Admin.Models;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class FindTaxonHandler : RequestHandler<FindTaxonModel, IEnumerable<KeyValuePair<Guid, string>>>
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="FindTaxonHandler"/> class.
        /// </summary>
        public FindTaxonHandler()
        {

        }

        #endregion

        #region RequestHandler overrides.

        /// <inheritdoc />
        public override IEnumerable<KeyValuePair<Guid, string>> Handle(FindTaxonModel message)
        {
            return new List<KeyValuePair<Guid, string>>
            {
                new KeyValuePair<Guid, string>(Guid.NewGuid(), "Test.")
            };
        }

        #endregion
    }
}
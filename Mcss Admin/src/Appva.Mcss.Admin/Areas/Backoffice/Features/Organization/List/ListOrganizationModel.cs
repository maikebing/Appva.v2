// <copyright file="ListOrganizationModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:stefan.sundstrom@invativa.se">Stefan Sundström</a>
// </author>
namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ListOrganizationModel
    {
        #region Properties

        public Node<Guid> Root
        {
            get;
            set;
        }
        public IEnumerable<Node<Guid>> Nodes
        {
            get;
            set;
        }

        #endregion
    }

    public class Node<T>
    {
        public T Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public IEnumerable<Node<T>> Children
        {
            get;
            set;
        }
    }
}
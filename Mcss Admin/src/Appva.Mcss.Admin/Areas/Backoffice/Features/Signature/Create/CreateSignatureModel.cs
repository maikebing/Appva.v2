// <copyright file="CreateSignatureModel.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Areas.Backoffice.Models
{
    #region Imports.

    using Appva.Cqrs;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    #endregion

    public class CreateSignatureModel : IRequest<bool>
    {
        [DisplayName("Beskrivning")]
        public string Name
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public Guid Id
        {
            get;
            set;
        }

        public bool IsRoot
        {
            get;
            set;
        }

        public Dictionary<string, string> Images
        {
            get;
            set;
        }
    }
}
// <copyright file="ProfileController.cs" company="Appva AB">
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
    using System.Linq;
    using System.Web;
    #endregion

    public class UpdateProfileModel : IRequest<bool>
    {
        #region Properties.

        /// <summary>
        /// The profile Id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }

        /// <summary>
        /// The name
        /// </summary>
        /// 
        [DisplayName("Titel")]
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// The description
        /// </summary>
        [DisplayName("Beskrivning")]
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// The type
        /// </summary>
        [DisplayName("Symbol")]
        public string Type
        {
            get;
            set;
        }

        #endregion
    }
}
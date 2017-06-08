// <copyright file="Signatures.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:h4nsson@gmail.com">Emmanuel Hansson</a>
// </author>
// <author>
//     <a href="mailto:ziemanncarl@gmail.com">Carl Ziemann</a>
// </author>

namespace Appva.Mcss.Admin.Application.Common
{
    #region Imports.
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Appva.Mcss.Admin.Application.Models;
    #endregion

    public static class Signatures
    {
        #region Properties
        /// <summary>
        /// Icon for competed signature
        /// </summary>
        public static readonly string Completed = "icn-ok.png";

        /// <summary>
        /// Icon for sent signature
        /// </summary>
        public static readonly string Sent = "icn-sent.png";

        /// <summary>
        /// Icon for NotGiven signature
        /// </summary>
        public static readonly string NotGiven = "icn-none.png";

        /// <summary>
        /// Icon for cant recive signature
        /// </summary>
        public static readonly string CantRecive = "icn-nothx.png";

        /// <summary>
        /// Icon for part signature
        /// </summary>
        public static readonly string PartlyGiven = "icn-part.png";
        #endregion
    }
}

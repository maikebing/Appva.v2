// <copyright file="$safeitemrootname$.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:your@email.address">Your name</a></author>
namespace $rootnamespace$
{
    #region Imports.
        
    using System;
    using System.Collections.Generic;
    $if$ ($targetframeworkversion$ >= 3.5)using System.Linq;$endif$
    using System.Text;
    $if$ ($targetframeworkversion$ >= 4.5)using System.Threading.Tasks;$endif$
	using System.Web.Mvc;
	
    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class $safeitemrootname$ : Controller
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="$safeitemrootname$"/> class.
        /// </summary>
        public $safeitemrootname$()
        {
        }

        #endregion
		
		#region Routes.
		
		//// TODO: Add routes here!
		
		#endregion
    }
}
using System;
using System.Collections.Generic;

namespace Appva.Mcss.Admin.Application.Models
{
    
    /// <summary>
    /// Helper class for print schedule.
    /// </summary>
    public class PrintSchedule {

        #region Constructor.

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public PrintSchedule() {
            Sequences = new Dictionary<DateTime, IDictionary<string, PrintSequence>>();
            Signatures = new Dictionary<DateTime, Dictionary<string, string>>();
        }

        #endregion

        #region Public Fields.

        /// <summary>
        /// DateTime/PrintSequence holder for simplicity.
        /// </summary>
        public IDictionary<DateTime, IDictionary<string, PrintSequence>> Sequences { get; set; }

        /// <summary>
        /// List of signatures in Sequences
        /// </summary>
        public IDictionary<DateTime,Dictionary<string,string>> Signatures { get; set; }

        #endregion

    }

}

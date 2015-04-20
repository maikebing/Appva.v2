using System;
using System.Collections.Generic;
using Appva.Mcss.Admin.Domain.Entities;

namespace Appva.Mcss.Admin.Application.Models
{

    /// <summary>
    /// Helper class for print schedule.
    /// </summary>
    public class PrintSequence {

        #region Public Fields.

        /// <summary>
        /// The UID (name + time).
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// The visible name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The instruction to the sequence
        /// </summary>
        public string Instruction { get; set; }

        /// <summary>
        /// The time.
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// If the sequence should be on need basis
        /// </summary>
        public bool OnNeedBasis { get; set; }

        /// <summary>
        /// Days in a month which are active.
        /// </summary>
        public IDictionary<int, Task> Days { get; set; }

        #endregion

    }

}

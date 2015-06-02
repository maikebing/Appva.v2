// <copyright file="InventoryOld.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class InventoryOld
    {
        /// <summary>
        /// Keeping track of inventory stock additions.
        /// </summary>
        public virtual int? Increased
        {
            get;
            set;
        }

        /// <summary>
        /// Keeping track of inventory stock withdrawals.
        /// </summary>
        public virtual int? Reduced
        {
            get;
            set;
        }

        /// <summary>
        /// The inventory level.
        /// </summary>
        public virtual int? Level
        {
            get;
            set;
        }

        /// <summary>
        /// The recalculated level.
        /// </summary>
        public virtual int? RecalculatedLevel
        {
            get;
            set;
        }
    }
}
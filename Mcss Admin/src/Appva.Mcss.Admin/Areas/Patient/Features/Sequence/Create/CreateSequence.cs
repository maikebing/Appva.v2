// <copyright file="CreateSequence.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.Models
{
    #region Imports.

    using System;
    using Appva.Cqrs;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class CreateSequence : Identity<CreateSequenceForm>
    {
        /// <summary>
        /// The schedule ID.
        /// </summary>
        public Guid ScheduleId
        {
            get;
            set;
        }
    }
}
// <copyright file="DosageScheme.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Domain.Entities
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public class DosageScheme
    {
        #region Properties.

        public virtual IList<Dosage> Dosages
        {
            get;
            set;
        }

        public virtual int Period
        {
            get;
            set;
        }

        #endregion

        public Periodicity GetPeriodicity
        {
            get
            {
                var daysInPeriodWithDosage = this.Dosages.Select(x => x.DayInPeriod).Distinct().Count();
                if(daysInPeriodWithDosage == 1 && this.Period < 8)
                {
                    return (Periodicity)this.Period;
                }
                if(Period == 7)
                {
                    return Periodicity.WeeklyScheme;
                }

                return Periodicity.NotPeriodical;
            }
        }
    }

    public enum Periodicity
    {
        EveryDay        = 1,
        EverySecondDay  = 2,
        EveryThirdDay   = 3,
        EveryFourthDay  = 4,
        EveryFifthDay   = 5,
        EverySixtDay    = 6,
        EveryWeek       = 7,
        WeeklyScheme    = 8,
        NotPeriodical   = 9
    }
}
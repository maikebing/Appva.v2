// <copyright file="MedicationTransformer.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Mcss.Admin.Application.Transformers
{
    #region Imports.

    using Appva.Ehm.Models;
    using Appva.Mcss.Admin.Domain;
    using Appva.Mcss.Admin.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal static class MedicationTransformer
    {
        #region Static members.


        internal static Medication From(Ordination ehmOrdination)
        {
            var article       = new Article
            {
                ArticleNumber = ehmOrdination.ArticleNumber,
                Atc           = ehmOrdination.ArticleAtc,
                AtcCode       = ehmOrdination.ArticleAtcCode,
                Form          = ehmOrdination.ArticleForm,
                Name          = ehmOrdination.ArticleName,
                NplId         = ehmOrdination.NplId,
                NplPackId     = ehmOrdination.NplPackId,
                PackageType   = ehmOrdination.ArticlePackageType,
                PackageUnit   = ehmOrdination.ArticlePackageUnit,
                PackageSize   = ehmOrdination.ArticlePackageSize,
                Provided      = ehmOrdination.ArticleProvided,
                Stakeholder   = ehmOrdination.ArticleStakeholder,
                Strength      = ehmOrdination.ArticleStrength
            };
            var prescriber = new Prescriber 
            {
                Code          = ehmOrdination.PrescriberCode,
                FamilyName    = ehmOrdination.PrescriberFamilyName,
                GivenName     = ehmOrdination.PrescriberGivenName,
                WorkPlaceCode = ehmOrdination.PrescriberWorkPlaceCode
            };
            var dosageScheme = ehmOrdination.Dosing != null ? new Appva.Mcss.Admin.Domain.Entities.DosageScheme
            {
                Period = ehmOrdination.Dosing.PeriodLength,
                Dosages = ehmOrdination.Dosing.Dosages.Select(x =>
                    new Appva.Mcss.Admin.Domain.Entities.Dosage
                    {
                        Amount = x.Amount,
                        DayInPeriod = x.DayInPeriod,
                        Time = x.Time
                    }).ToList()
            } : null;
            return new Medication
            {
                Article             = article,
                DosageText1         = ehmOrdination.TreatmentDosageText1,
                DosageText2         = ehmOrdination.TreatmentDosageText2,
                OrdinationCreatedAt = ehmOrdination.OrdinationCreatedAt,
                OrdinationId        = ehmOrdination.Id,
                Prescriber          = prescriber,
                Purpose             = ehmOrdination.TreatmentPurpose,
                OrdinationStartsAt      = ehmOrdination.OrdinationStartsAt,
                Status              = ehmOrdination.Status,
                TreatmentEndsAt     = ehmOrdination.TreatmentEndsAt,
                TreatmentStartsAt   = ehmOrdination.TreatmentStartsAt,
                Type                = OrdinationTypeExtension.FromString(ehmOrdination.OrdinationType),
                OrdinationValidUntil    = ehmOrdination.OrdinationValidUntil,
                CanceledAt              = ehmOrdination.CanceledAt,
                CancellationComment     = ehmOrdination.CancellationComment,
                CancellationReason      = ehmOrdination.CancellationReason,
                CancellationReasonCode  = ehmOrdination.CancellationReasonCode,
                DiscontinuedAt          = ehmOrdination.DiscontinuedAt,
                DiscontinuedComment     = ehmOrdination.DiscontinuedComment,
                DiscontinuedType        = ehmOrdination.DiscontinuedType,
                DosageScheme            = dosageScheme,
                PreviousMedications     = ehmOrdination.PreviousOrdinations != null ? ehmOrdination.PreviousOrdinations.Select(x => From(x)).ToList() : new List<Medication>(),
                NumbersOfExpiditions    = ehmOrdination.NumbersOfExpiditions,
                RemainingExpiditions    = ehmOrdination.RemainingExpiditions,
                LastExpiditedAt         = ehmOrdination.LastExpiditedAt,
                LastExpiditedNplPackId  = ehmOrdination.LastExpiditedNplPackId,
                LastExpiditedAmount     = ehmOrdination.LastExpiditedAmount
            };
        }

        internal static IList<Medication> From(IList<Ordination> ehmOrdinations)
        {
            return ehmOrdinations.Select(x => From(x)).ToList();
        }

        #endregion
    }
}
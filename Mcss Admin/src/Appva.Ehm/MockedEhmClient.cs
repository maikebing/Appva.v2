// <copyright file="MockedEhmClient.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:$emailAddress$">$developer$</a>
// </author>
namespace Appva.Ehm
{
    #region Imports.

    using Appva.Ehm.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class MockedEhmClient : IEhmClient
    {
        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="MockedEhmClient"/> class.
        /// </summary>
        public MockedEhmClient()
        {
        }

        #endregion

        #region IEhmClient members.

        /// <inheritdoc />
        public async Task<IList<Ordination>> ListOrdinations(string forPatientUniqueId, User byUser)
        {
            return await Task.Run(() => MockOrdinationList());
        }

        #endregion

        #region Private static helpers

        private static IList<Ordination> MockOrdinationList()
        {
            var retval = new List<Ordination>();
            retval.Add(new Ordination
            {
                ArticleName             = "Alvedon",
                ArticleAtc              = "Paracetamol",
                ArticleAtcCode          = "23902303",
                ArticleForm             = "Tablett",
                ArticleNumber           = "123123",
                ArticlePackageSize      = 100,
                ArticlePackageType      = "Kartong",
                ArticlePackageUnit      = "St",
                ArticleProvided         = true,
                ArticleStakeholder      = "Apoteket kärnan",
                ArticleStrength         = "5mg",
                Id                      = 156165132,
                IsCommingOrdination     = false,
                LastExpiditedAmount     = 1,
                LastExpiditedAt         = new DateTime(2017,3,3),
                LastExpiditedNplPackId = "0234023324",
                NplId                   = "302324234",
                NplPackId               = "0923429043",
                NumbersOfExpiditions    = 3,
                OrdinationCreatedAt     = new DateTime(2017, 2, 2),
                OrdinationValidUntil    = new DateTime(2017, 12, 12),
                OrdinationStartsAt      = new DateTime(2017, 2, 22),
                PrescriberCode          = "3423424",
                PrescriberFamilyName    = "Svensson",
                PrescriberGivenName     = "Sven",
                PrescriberWorkPlaceCode = "Sjukhuset",
                Status                  = 205,
                TreatmentDosageText1    = "2 tabletter till frukost och middag 5 dagar i veckan",
                TreatmentPurpose        = "Mot värk",
                OrdinationType          = "D"
            });

            return retval;
        }

        #endregion
    }
}
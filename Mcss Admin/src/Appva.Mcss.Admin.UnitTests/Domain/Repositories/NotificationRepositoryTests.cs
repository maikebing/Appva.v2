// <copyright file="NotificationRepositoryTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Mcss.Admin.UnitTests.Domain.Repositories
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Appva.Mcss.Admin.Domain.Entities;
    using Appva.Mcss.Admin.Domain.Repositories;
    using Appva.Persistence;
    using Appva.Persistence.Tests;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class NotificationRepositoryTests : IClassFixture<SqlDatasource<NotificationRepositoryTestData>>
    {
        #region Variables.

        /// <summary>
        /// The <see cref="SqlDatasource{NotificationRepositoryTestData}"/>.
        /// </summary>
        private readonly SqlDatasource<NotificationRepositoryTestData> database;

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationRepositoryTests"/> class.
        /// </summary>
        /// <param name="database">The <see cref="SqlDatasource{NotificationRepositoryTestData}"/></param>
        public NotificationRepositoryTests(SqlDatasource<NotificationRepositoryTestData> database)
        {
            this.database = database;
        }

        #endregion

        #region Tests.

        /// <summary>
        /// Test:    Returns first by date in ascending order
        /// Expects: The notification is not the most recent but the oldest in the collection.
        /// </summary>
        [Fact]
        public void Should_ReturnFirstByDateInAscendingOrder_When_MoreThanOneItem()
        {
            var repository    = new NotificationRepository(this.database.PersistenceContext);
            var notifications = repository.List();
            var notification  = repository.GetFirstVisibleDashboardNotificationByAccount(NotificationRepositoryTestData.AccountId);
            Assert.Equal(4, notifications.TotalCount);
            Assert.True(notification != null);
            Assert.Equal(1899, notification.PublishedDate.Year);
        }

        #endregion
    }

    /// <summary>
    /// Test data for <see cref="NotificationRepositoryTests"/>.
    /// </summary>
    public sealed class NotificationRepositoryTestData : DataPopulation
    {
        #region Variables.

        /// <summary>
        /// The name format.
        /// </summary>
        public const string NotificationNameFormat = "Test {0:yyyy-MM-dd}";

        /// <summary>
        /// Represents the date january 1st 1900.
        /// </summary>
        public static readonly DateTime NineteenHundered = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);

        /// <summary>
        /// Represents the date january 1st 1899.
        /// </summary>
        public static readonly DateTime EighteenHundredNinetyNine = NineteenHundered.AddYears(-1);

        /// <summary>
        /// The <c>Account</c> ID.
        /// </summary>
        public static readonly Guid AccountId = new Guid("75549dac-4373-446c-a8a9-8bb0c91c693a");

        #endregion

        #region Constructor.

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationRepositoryTestData"/> class.
        /// </summary>
        /// <param name="context">The <see cref="IPersistenceContext"/></param>
        public NotificationRepositoryTestData(IPersistenceContext context)
            :base (context)
        {
        }

        #endregion

        #region Abstract Members.

        /// <inheritdoc />
        public override void Populate()
        {
            this.Save   (this.CreateAccount());
            this.SaveAll(this.CreateNotifications());
        }

        #endregion

        #region Private Data Initializations.

        /// <summary>
        /// Creates an <c>Account</c>
        /// </summary>
        /// <returns>A new instance of <c>Account</c></returns>
        private Account CreateAccount()
        {
            var account = Account.CreateForTest(AccountId);
            account.FirstName    = "John";
            account.LastName     = "Doe";
            account.FullName     = "John Doe";
            account.SymmetricKey = "JnGeba6GF9tmEq";
            account.PersonalIdentityNumber = new PersonalIdentityNumber("19010101-0101");
            return account;
        }

        /// <summary>
        /// Creates a collection of <c>DashboardNotification</c>
        /// </summary>
        /// <returns>A collection of <c>DashboardNotification</c></returns>
        private IList<DashboardNotification> CreateNotifications()
        {
            return new List<DashboardNotification> {
                new DashboardNotification
                {
                    Name                = string.Format(NotificationNameFormat, NineteenHundered),
                    NotificationType    = NotificationType.DashboardNotification,
                    Published           = true,
                    PublishedDate       = NineteenHundered,
                    UnPublishedDate     = DateTime.MaxValue,
                    IsActive            = true,
                    IsVisibleToEveryone = true,
                },
                new DashboardNotification
                {
                    Name                = string.Format(NotificationNameFormat, EighteenHundredNinetyNine),
                    NotificationType    = NotificationType.DashboardNotification,
                    Published           = true,
                    PublishedDate       = EighteenHundredNinetyNine,
                    UnPublishedDate     = null,
                    IsActive            = true,
                    IsVisibleToEveryone = true,
                },
                new DashboardNotification
                {
                    Name                = string.Format(NotificationNameFormat, NineteenHundered.AddYears(-100)),
                    NotificationType    = NotificationType.DashboardNotification,
                    Published           = false,
                    PublishedDate       = NineteenHundered.AddYears(-100),
                    UnPublishedDate     = null,
                    IsActive            = true,
                    IsVisibleToEveryone = true,
                },
                new DashboardNotification
                {
                    Name                = string.Format(NotificationNameFormat, DateTime.Now.AddDays(1)),
                    NotificationType    = NotificationType.DashboardNotification,
                    Published           = true,
                    PublishedDate       = DateTime.Now.AddDays(1),
                    UnPublishedDate     = null,
                    IsActive            = true,
                    IsVisibleToEveryone = true,
                }
            };
        }

        #endregion
    }
}

// <copyright file="ApiServiceTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:fredrik.andersson@appva.com">Fredrik Andersson</a>
// </author>
namespace Appva.Sca.UnitTests
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Sca;
    using Appva.Sca.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    #endregion

    /// <summary>
    /// Class ScaServiceTests.
    /// </summary>
    [TestClass]
    public class ApiServiceTests
    {
        /// <summary>
        /// Gets the resident test.
        /// </summary>
        [TestMethod]
        public void TestGetResident()
        {
            // arrange
            var service = new ApiService(new Uri("https://tenaidentifistage.sca.com/"));
            var credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("EABE6751-2ABD-4311-A794-70A833D31C31" + ":" + "C5C8DAEB-6C07-423D-82CF-8177C8CB9604"));
            service.SetCredentials(credentials);

            var testResident1 = new GetResidentModel { ExternalId = "8L2vJIUo" }; // Testsubject Alpha
            var testResident2 = new GetResidentModel { ExternalId = "VkURQBEo" }; // Testsubject Beta
            var testResident3 = new GetResidentModel { ExternalId = "4lU5CgQm" }; // Testsubject Gamma

            // act
            var response1 = service.GetResidentAsync(testResident1.ExternalId).Result;
            var response2 = service.GetResidentAsync(testResident2.ExternalId).Result;
            var response3 = service.GetResidentAsync(testResident3.ExternalId).Result;

            // assert
            Assert.AreEqual(testResident1.ExternalId, response1.ExternalId);
            Assert.AreEqual(testResident2.ExternalId, response2.ExternalId);
            Assert.AreEqual(testResident3.ExternalId, response3.ExternalId);
        }

        /// <summary>
        /// Tests the PostManualEvent.
        /// </summary>
        [TestMethod]
        public void TestPostManualEvent()
        {
            // arrange
            var service = new ApiService(new Uri("https://tenaidentifistage.sca.com/"));
            var credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("EABE6751-2ABD-4311-A794-70A833D31C31" + ":" + "C5C8DAEB-6C07-423D-82CF-8177C8CB9604"));
            service.SetCredentials(credentials);

            var testManualEvents1 = new List<PostManualEventModel>
            {
                new PostManualEventModel  // Testsubject Alpha
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "toilet",
                    ResidentId = "8L2vJIUo",
                    Timestamp = DateTime.Now.AddDays(-4).ToString(),
                    Active = true
                },
                new PostManualEventModel  // Testsubject Alpha
                {
                    Id = Guid.NewGuid().ToString(),
                    EventType = "leakage",
                    ResidentId = "8L2vJIUo",
                    Timestamp = DateTime.Now.AddDays(-2).ToString(),
                    Active = true
                }
            };

            // act
            var response1 = service.PostManualEventAsync(testManualEvents1).Result;

            // assert
            Assert.AreEqual(testManualEvents1.Count, response1.Count);
            Assert.AreEqual(testManualEvents1[0].Id, response1[0].Id);
            Assert.AreEqual("created", response1[0].ImportResult);
            Assert.AreEqual(testManualEvents1[1].Id, response1[1].Id);
            Assert.AreEqual("created", response1[1].ImportResult);
        }

        /// <summary>
        /// Tests the invalid credentials.
        /// </summary>
        [TestMethod]
        public void TestInvalidCredentials()
        {
            // arrange
            var service = new ApiService(new Uri("https://tenaidentifistage.sca.com/"));
            var credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("EABE6751-4311-A794-2ABD-70A833D31C31" + ":" + "C5C8DAEB-6C07-82CF-423D-8177C8CB9604"));
            service.SetCredentials(credentials);

            var testResident1 = new GetResidentModel { ExternalId = "8L2vJIUo" }; // Testsubject Alpha

            // act
            var response1 = service.GetResidentAsync(testResident1.ExternalId).Result;

            // assert
            Assert.IsNotNull(response1.Message);
            Assert.AreEqual("Ett fel inträffade. Var god försök igen.", response1.Message); // this might change over time.
        }

        /// <summary>
        /// Tests the base address.
        /// Vad händer om fel baseUrl är angiven?
        /// </summary>
        [TestMethod]
        public void TestInvalidBaseAddress()
        {
            // arrange
            var service = new ApiService(new Uri("https://tenaidentifistaeg.sca.se/")); //, "EABE6751-2ABD-4311-A794-70A833D31C31", "C5C8DAEB-6C07-423D-82CF-8177C8CB9604");
            var credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("EABE6751-4311-A794-2ABD-70A833D31C31" + ":" + "C5C8DAEB-6C07-82CF-423D-8177C8CB9604"));
            service.SetCredentials(credentials);

            var testResident1 = new GetResidentModel { ExternalId = "8L2vJIUo" }; // Testsubject Alpha

            // act
            var response = Task.Run(() => service.GetResidentAsync(testResident1.ExternalId)).Exception;

            // assert
            Assert.IsNull(response);
        }

        /// <summary>
        /// Tests changing the credentials.
        /// </summary>
        [TestMethod]
        public void TestChangingCredentials()
        {
            // arrange
            var service = new ApiService(new Uri("https://tenaidentifistage.sca.com/"));
            var old_credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("EABE6751-2ABD-A794-4311-70A833D31C31" + ":" + "C5C8DAEB-6C07-82CF-423D-8177C8CB9604"));
            var new_credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("EABE6751-2ABD-4311-A794-70A833D31C31" + ":" + "C5C8DAEB-6C07-423D-82CF-8177C8CB9604"));
            var testResident1 = new GetResidentModel { ExternalId = "8L2vJIUo" }; // Testsubject Alpha
            service.SetCredentials(old_credentials);

            // act
            service.SetCredentials(new_credentials);
            var response1 = service.GetResidentAsync(testResident1.ExternalId).Result;

            // assert
            Assert.AreEqual(testResident1.ExternalId, response1.ExternalId);
        }
    }
}

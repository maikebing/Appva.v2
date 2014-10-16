// <copyright file="ChecksumTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
namespace Appva.Test.Cryptography
{
    #region Import.

    using System.Security.Cryptography;
    using Appva.Cryptography;
    using Appva.Cryptography.HashAlgoritms;
    using Xunit;
    using Xunit.Extensions;

    #endregion

    /// <summary>
    /// Checksum tests.
    /// </summary>
    /// <author><a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a></author>
    public class ChecksumTests
    {
        #region Expectation.

        /// <summary>
        /// The plain text (unhashed value).
        /// </summary>
        private const string PlainText = "Foo";

        /// <summary>
        /// The MurmurHash 128 hash.
        /// </summary>
        private const string MurmurHash128 = "9b02fb1dee962f6a0bb065f3fec2c6a8";

        /// <summary>
        /// The MurmurHash 128 base64 hash.
        /// </summary>
        private const string MurmurHash128Base64 = "mwL7He6WL2oLsGXz/sLGqA==";

        /// <summary>
        /// The xxHash 32 bit hash.
        /// </summary>
        private const string XXHash32 = "3813187095";

        /// <summary>
        /// The xxHash 32 bit base64 hash.
        /// </summary>
        private const string XXHash32Base64 = "F55I4w==";

        #endregion

        #region Tests.

        #region MurmurHash 128

        /// <summary>
        /// Creates a MurmurHash 128 bit hash and verifies that the hash
        /// is equal to expected result.
        /// </summary>
        /// <param name="plainText">The plain text of the hash</param>
        [Theory]
        [InlineData(PlainText)]
        public void Checksum_MurmurHash128IsCorrectlyCreated_IsTrue(string plainText)
        {
            this.AssertThatCreatedHashedIsCorrect<MurmurHash128>(plainText, MurmurHash128);
            this.AssertThatCreatedHashedIsCorrect<MurmurHash128>(plainText, MurmurHash128Base64, HashFormat.Base64);
        }

        /// <summary>
        /// Asserts that an unhashed MurmurHash 128 bit hash equals the 
        /// hashed value.
        /// </summary>
        /// <param name="plainText">The plain text of the hash</param>
        [Theory]
        [InlineData(PlainText)]
        public void Checksum_MurmurHash128UnhashedEqualsHashed_IsTrue(string plainText)
        {
            this.AssertThatUnhashedAndHashedAreEqual<MurmurHash128>(plainText, MurmurHash128);
            this.AssertThatUnhashedAndHashedAreEqual<MurmurHash128>(plainText, MurmurHash128Base64, HashFormat.Base64);
        }

        #endregion

        #region XXHash 32

        /// <summary>
        /// Creates a xxHash 32 bit hash and verifies that the hash
        /// is equal to expected result.
        /// </summary>
        /// <param name="plainText">The plain text of the hash</param>
        [Theory]
        [InlineData(PlainText)]
        public void Checksum_XXHash32IsCorrectlyCreated_IsTrue(string plainText)
        {
            this.AssertThatCreatedHashedIsCorrect<XXHash32>(plainText, XXHash32);
            this.AssertThatCreatedHashedIsCorrect<XXHash32>(plainText, XXHash32Base64, HashFormat.Base64);
        }

        /// <summary>
        /// Asserts that an unhashed xxHash 32 bit hash equals the 
        /// hashed value.
        /// </summary>
        /// <param name="plainText">The plain text of the hash</param>
        [Theory]
        [InlineData(PlainText)]
        public void Checksum_XXHash32UnhashedEqualsHashed_IsTrue(string plainText)
        {
            this.AssertThatUnhashedAndHashedAreEqual<XXHash32>(plainText, XXHash32);
            this.AssertThatUnhashedAndHashedAreEqual<XXHash32>(plainText, XXHash32Base64, HashFormat.Base64);
        }

        #endregion

        #region Helpers.

        /// <summary>
        /// All tests are the same so wrap the equals assertment.
        /// </summary>
        /// <typeparam name="T">An instance of <see cref="HashAlgorithm"/></typeparam>
        /// <param name="plainText">The plain text of the hash</param>
        /// <param name="expected">The expected hash of the plain text</param>
        /// <param name="format">The format of the hash</param>
        private void AssertThatCreatedHashedIsCorrect<T>(string plainText, string expected, HashFormat format = HashFormat.None)
            where T : HashAlgorithm, new()
        {
            var nonCrypto = Checksum.Using<T>(value: plainText);
            string actual = format.Equals(HashFormat.Base64) ? nonCrypto.BuildAsBase64() : nonCrypto.Build();
            Assert.Equal(expected: expected, actual: actual);
        }

        /// <summary>
        /// All tests are the same so wrap the equals assertment.
        /// </summary>
        /// <typeparam name="T">An instance of <see cref="HashAlgorithm"/></typeparam>
        /// <param name="plainText">The plain text of the hash</param>
        /// <param name="expected">The expected hash of the plain text</param>
        /// <param name="format">The format of the hash</param>
        private void AssertThatUnhashedAndHashedAreEqual<T>(string plainText, string expected, HashFormat format = HashFormat.None)
            where T : HashAlgorithm, new()
        {
            var equals = Checksum.Assert<T>(unhashed: plainText)
                .Equals(hashed: expected, format: format);
            Assert.True(equals);
        }

        #endregion

        #endregion
    }
}

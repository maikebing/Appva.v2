// <copyright file="ChecksumTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Cryptography
{
    #region Import.

    using Appva.Cryptography;
    using Appva.Cryptography.HashAlgorithms;
    using Appva.Core.Extensions;
    using Xunit;

    #endregion

    /// <summary>
    /// Test suite for <see cref="Checksum"/>.
    /// </summary>
    public class ChecksumTests
    {
        #region Variables.

        /// <summary>
        /// The plain text (unhashed value).
        /// </summary>
        private const string PlainText = "Foo";

        /// <summary>
        /// The MurmurHash 128 hash.
        /// </summary>
        private const string MurmurHash128 = "9b02fb1dee962f6a0bb065f3fec2c6a8";

        /// <summary>
        /// The xxHash 32 bit hash.
        /// </summary>
        private const string XxHash32 = "3813187095";

        #endregion

        #region Tests.

        #region MurmurHash 128.

        /// <summary>
        /// Test: Creates a <see cref="MurmurHash128"/> bit hash.
        /// Expected Result: The created hash is equal to the expected result.
        /// </summary>
        [Fact]
        public void CreateMurmurHash128AsString_ExpectThatHashIsCorrect()
        {
            var actual = Checksum.Using<MurmurHash128>(PlainText).Build();
            Assert.Equal(MurmurHash128, actual);
        }

        /// <summary>
        /// Test: Creates a <see cref="MurmurHash128"/> bit hash as base64.
        /// Expected Result: The created hash is equal to the expected base64 result.
        /// </summary>
        [Fact]
        public void CreateMurmurHash128AsBase64_ExpectThatHashIsCorrect()
        {
            var actual = Checksum.Using<MurmurHash128>(PlainText).BuildAsBase64();
            Assert.Equal(MurmurHash128.ToBase64(), actual);
        }

        /// <summary>
        /// Test: That an unhashed <see cref="MurmurHash128"/> bit hash is equal to the 
        /// hashed value.
        /// Expected Result: The hashed values are equal.
        /// </summary>
        [Fact]
        public void UnhashedMurmurHash128EqualityCheck_ExpectThatEqualityIsTrue()
        {
            Assert.True(Checksum.Assert<MurmurHash128>(PlainText).Equals(MurmurHash128));
        }

        /// <summary>
        /// Test: That an unhashed <see cref="MurmurHash128"/> bit hash is equal to the 
        /// base64 hashed value.
        /// Expected Result: The hashed values are equal.
        /// </summary>
        [Fact]
        public void UnhashedBase64MurmurHash128EqualityCheck_ExpectThatEqualityIsTrue()
        {
            Assert.True(Checksum.Assert<MurmurHash128>(PlainText).Equals(MurmurHash128.ToBase64(), HashFormat.Base64));
        }

        #endregion

        #region XXHash 32.

        /// <summary>
        /// Test: Creates a <see cref="XxHash32"/> bit hash.
        /// Expected Result: The created hash is equal to the expected result.
        /// </summary>
        [Fact]
        public void CreateXxHash32AsString_ExpectThatHashIsCorrect()
        {
            var actual = Checksum.Using<XxHash32>(PlainText).Build();
            Assert.Equal(XxHash32, actual);
        }

        /// <summary>
        /// Test: Creates a <see cref="XxHash32"/> bit hash as base64.
        /// Expected Result: The created hash is equal to the expected base64 result.
        /// </summary>
        [Fact]
        public void CreateXxHash32AsBase64_ExpectThatHashIsCorrect()
        {
            var actual = Checksum.Using<XxHash32>(PlainText).BuildAsBase64();
            Assert.Equal(XxHash32.ToBase64(), actual);
        }

        /// <summary>
        /// Test: That an unhashed <see cref="XxHash32"/> bit hash is equal to the 
        /// hashed value.
        /// Expected Result: The hashed values are equal.
        /// </summary>
        [Fact]
        public void UnhashedXxHash32EqualityCheck_ExpectThatEqualityIsTrue()
        {
            Assert.True(Checksum.Assert<XxHash32>(PlainText).Equals(XxHash32));
        }

        /// <summary>
        /// Test: That an unhashed <see cref="XxHash32"/> bit hash is equal to the 
        /// base64 hashed value.
        /// Expected Result: The hashed values are equal.
        /// </summary>
        [Fact]
        public void UnhashedBase64XxHash32EqualityCheck_ExpectThatEqualityIsTrue()
        {
            Assert.True(Checksum.Assert<XxHash32>(PlainText).Equals(XxHash32.ToBase64(), HashFormat.Base64));
        }

        #endregion

        #endregion
    }
}

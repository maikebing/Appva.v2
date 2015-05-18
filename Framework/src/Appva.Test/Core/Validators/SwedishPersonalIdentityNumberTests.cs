// <copyright file="SwedishPersonalIdentityNumberTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Core.Validators
{
    #region Imports.

    using Appva.Core;
    using Xunit;
    using Xunit.Extensions;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class SwedishPersonalIdentityNumberTests
    {
        #region Tests.

        /// <summary>
        /// Test: Validate invalid 12 digit personal identity numbers.
        /// Expected results: All should be invalid.
        /// <list type="bullet">
        ///     <item>19640823-3235; incorrect checksum (should be 4)</item>
        ///     <item>20811111-1111; incorrect due to future date</item>
        ///     <item>18811111-1111; incorrect due to not valid century</item>
        ///     <item>19640823-32FX; incorrect due to non-alpha characters</item>
        ///     <item>19641383-3234; incorrect due to non-existing month (13)</item>
        /// </list>
        /// </summary>
        /// <param name="personalIdentityNumber">The string to check</param>
        [Theory]
        [InlineData("19640823-3235"), InlineData("20811111-1111")]
        [InlineData("18811111-1111"), InlineData("19640823-32FX"), InlineData("19641383-3234")]
        public void Invalid12DigitPersonalIdentityNumbers_ExpectIsInvalid(string personalIdentityNumber)
        {
            Assert.False(SwedishPersonalIdentityNumberValidator.IsValid(personalIdentityNumber));
        }

        /// <summary>
        /// Test: Validate valid 12 digit personal identity numbers.
        /// Expected results: All should be valid.
        /// <list type="bullet">
        ///     <item>19811111-1111; valid personal identity number</item>
        ///     <item>19640823-3234; valid personal identity number</item>
        ///     <item>19640883-3231; valid co-ordination number</item>
        /// </list>
        /// </summary>
        /// <param name="personalIdentityNumber">The string to check</param>
        [Theory]
        [InlineData("19811111-1111"), InlineData("19640823-3234"), InlineData("19640883-3231")]
        public void Valid12DigitPersonalIdentityNumbers_ExpectIsValid(string personalIdentityNumber)
        {
            Assert.True(SwedishPersonalIdentityNumberValidator.IsValid(personalIdentityNumber));
        }

        /// <summary>
        /// Test: Validate valid 10 digit personal identity numbers when it is disallowed.
        /// Expected results: All should be invalid due to 12 digit constraint.
        /// <list type="bullet">
        ///     <item>811111+1111; valid personal identity number</item>
        ///     <item>640823-3234; valid personal identity number</item>
        ///     <item>640883-3231; valid co-ordination number</item>
        /// </list>
        /// </summary>
        /// <param name="personalIdentityNumber">The string to check</param>
        [Theory]
        [InlineData("811111-1111"), InlineData("640823-3234"), InlineData("640883-3231")]
        public void Invalid10DigitPersonalIdentityNumbers_ExpectIsInvalid(string personalIdentityNumber)
        {
            Assert.False(SwedishPersonalIdentityNumberValidator.IsValid(personalIdentityNumber));
        }

        /// <summary>
        /// Test: Validate valid 10 digit personal identity numbers when it is allowed.
        /// Expected results: All should be valid.
        /// <list type="bullet">
        ///     <item>811111+1111; valid personal identity number</item>
        ///     <item>640823-3234; valid personal identity number</item>
        ///     <item>640883-3231; valid co-ordination number</item>
        /// </list>
        /// </summary>
        /// <param name="personalIdentityNumber">The string to check</param>
        [Theory]
        [InlineData("811111-1111"), InlineData("640823-3234"), InlineData("640883-3231")]
        public void Valid10DigitPersonalIdentityNumbers_ExpectIsValid(string personalIdentityNumber)
        {
            Assert.True(SwedishPersonalIdentityNumberValidator.IsValid(personalIdentityNumber, SwedishPersonalIdentityNumberCenturyOmittionMode.Allow));
        }

        #endregion
    }
}
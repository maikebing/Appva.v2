// <copyright file="ProtoBufTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Fhir
{
    #region Imports.

    using Appva.Fhir.Resources.Security;
    using Appva.Test.Tools;
    using Xunit;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    public sealed class ProtoBufTests
    {
        /// <summary>
        /// Test: Serialize an instance.
        /// Expected Result: The instance is properly serialized.
        /// </summary>
        [Fact]
        public void Serialize_ExpectInstanceIsSerializedCorrectly()
        {
            var expected = FhirUtils.CreateNewAuditEvent();
            var bytes = ProtoBufUtils.Serialize(expected);
            var actual = ProtoBufUtils.Deserialize<AuditEvent>(bytes);
            Assert.Equal(expected.Event.Type.Coding[0].Code.Value, actual.Event.Type.Coding[0].Code.Value);
        }
    }
}
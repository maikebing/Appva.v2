using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Appva.Mcss.Admin.Application.Models;

namespace Appva.Mcss.Admin.UnitTests
{
    [TestClass]
    public class MeasurementScaleTest
    {
        [TestMethod]
        public void TestMeliorScaleValidation()
        {
            // Arrange
            var value_to_test = "bigdoublea";
            var scale_to_test = "melior";
            var test_result = false;

            // Act
            test_result = MeasurementScale.HasValidValue(value_to_test, scale_to_test);

            // Assert
            Assert.AreEqual(true, test_result);
        }

        [TestMethod]
        public void TestWeightScaleValidation()
        {
            // Arrange
            var value_to_test = "83,61";
            var scale_to_test = "weight";
            var test_result = false;
            // Act
            test_result = MeasurementScale.HasValidValue(value_to_test, scale_to_test);

            // Assert
            Assert.AreEqual(true, test_result);
        }

        [TestMethod]
        public void TestBristolScaleValidation()
        {
            // Arrange
            var value_to_test = "4";
            var scale_to_test = "bristol";
            var test_result = false;

            // Act
            test_result = MeasurementScale.HasValidValue(value_to_test, scale_to_test);

            // Assert
            Assert.AreEqual(true, test_result);
        }

        [TestMethod]
        public void TestGetUnitForScale()
        {
            // Arrange
            var scale_to_test = "weight";
            var test_result = string.Empty;

            // Act
            test_result = MeasurementScale.GetUnitForScale(scale_to_test);

            // Assert
            Assert.AreEqual("kg", test_result);
        }

        [TestMethod]
        public void TestGetMeliorValue()
        {
            // Arrange
            var value_to_test = "bigtriplea";
            var test_result = string.Empty;

            // Act
            test_result = MeasurementScale.GetCommonValue(value_to_test);

            // Assert
            Assert.AreEqual("AAA", test_result);
        }
    }
}

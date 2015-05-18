// <copyright file="ValueObjectTests.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johansalllarsson@appva.se">Johan Säll Larsson</a>
// </author>
namespace Appva.Test.Common
{
    #region Imports.

    using System;
    using System.Collections.Generic;
    using Appva.Common.Domain;
    using Xunit;

    #endregion

    /// <summary>
    /// Test suite for <see cref="ValueObject{T}"/>
    /// </summary>
    public sealed class ValueObjectTests
    {
        #region Variables.

        /// <summary>
        /// Test value 'foo'.
        /// </summary>
        private const string Foo = "foo";

        /// <summary>
        /// Test value 'bar'.
        /// </summary>
        private const string Bar = "bar";

        /// <summary>
        /// The foo test VO.
        /// </summary>
        private static readonly TestValueObject FooVo = new TestValueObject(Foo);

        /// <summary>
        /// The bar test VO.
        /// </summary>
        private static readonly TestValueObject BarVo = new TestValueObject(Bar);

        #endregion

        #region Tests.

        #region IEquality.

        /// <summary>
        /// Test: Instantiate two different VO with the same value.
        /// Expected Result: The two VO:s are equal (.Equals).
        /// </summary>
        [Fact]
        public void OverridableEquals_ExpectAreEqual()
        {
            var newFooVo = new TestValueObject(Foo);
            Assert.True(FooVo.Equals(newFooVo));
        }

        /// <summary>
        /// Test: Instantiate two different VO with the different values.
        /// Expected Result: The two VO:s are not equal (.Equals).
        /// </summary>
        [Fact]
        public void OverridableEquals_ExpectAreNotEqual()
        {
            Assert.False(FooVo.Equals(BarVo));
        }

        #endregion

        #region Operator.

        /// <summary>
        /// Test: Instantiate two different VO with the same value.
        /// Expected Result: The two VO:s are equal (==).
        /// </summary>
        [Fact]
        public void OperatorEquals_ExpectAreEqual()
        {
            var newFooVo = new TestValueObject(Foo);
            Assert.True(FooVo == newFooVo);
        }

        /// <summary>
        /// Test: Instantiate two different VO with the different values.
        /// Expected Result: The two VO:s are not equal (==).
        /// </summary>
        [Fact]
        public void OperatorEquals_ExpectAreNotEqual()
        {
            Assert.False(FooVo == BarVo);
        }

        #endregion

        #region Dictionary.

        /// <summary>
        /// Test: Try to populate a dictionary with the same VO.
        /// Expected Result: Throw exception.
        /// </summary>
        [Fact]
        public void AddEntryToDictionary_ExpectItAlreadyExistAndThrowsException()
        {
            var newFooVo = new TestValueObject(Foo);
            var dictionary = new Dictionary<TestValueObject, string> { { newFooVo, Foo } };
            Assert.True(dictionary.ContainsKey(FooVo));
            Assert.Throws(
                typeof(ArgumentException), 
                () => 
                {
                    dictionary.Add(FooVo, Foo);
                });
        }

        /// <summary>
        /// Test: Try to populate a dictionary with a different VO as key.
        /// Expected Result: The entry is added.
        /// </summary>
        [Fact]
        public void AddEntryToDictionary_ExpectThatEntryIsAdded()
        {
            var newFooVo = new TestValueObject(Foo);
            var dictionary = new Dictionary<TestValueObject, string> { { newFooVo, Foo } };
            Assert.False(dictionary.ContainsKey(BarVo));
            dictionary.Add(BarVo, Foo);
            Assert.True(dictionary.ContainsKey(BarVo));
        }

        #endregion

        #endregion

        #region Private Classes.

        /// <summary>
        /// Internal Test VO.
        /// </summary>
        internal class TestValueObject : ValueObject<TestValueObject>
        {
            #region Variables.

            /// <summary>
            /// The actual value.
            /// </summary>
            private readonly string value;

            #endregion

            #region Constructor.

            /// <summary>
            /// Initializes a new instance of the <see cref="TestValueObject"/> class.
            /// </summary>
            /// <param name="value">The actual value</param>
            public TestValueObject(string value)
            {
                this.value = value;
            }

            #endregion

            #region Properties.

            /// <summary>
            /// Returns the value.
            /// </summary>
            public string Value
            {
                get
                {
                    return this.value;
                }
            }

            #endregion

            #region ValueObject Overrides.

            /// <inheritdoc />
            public override bool Equals(TestValueObject other)
            {
                if (other == null)
                {
                    return false;
                }
                if (string.IsNullOrEmpty(this.Value) || string.IsNullOrEmpty(other.Value))
                {
                    return false;
                }
                return this.Value.Equals(other.Value);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                return this.Value.GetHashCode();
            }

            #endregion
        }

        #endregion
    }
}
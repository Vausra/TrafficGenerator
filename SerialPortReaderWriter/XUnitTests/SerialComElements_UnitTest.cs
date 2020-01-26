// <copyright file="SerialComElements_UnitTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace XUnitTests
{
    using SerialPortReaderWriter;
    using Xunit;

    /// <summary>
    /// Test class for <see cref="SerialComElements"/>.
    /// </summary>
    public class SerialComElements_UnitTest
    {
        /// <summary>
        /// Test for reading a configuration file for response request key value pair
        /// </summary>
        [Fact]
        public void SerialComElements_GetHashCode()
        {
            // Arrange
            SerialComElements uutA = this.GenerateSerialComElement_GetSoftwareVersion();
            SerialComElements uutB = this.GenerateSerialComElement_GetSoftwareVersion();

            // Act
            int resultA = uutA.GetHashCode();
            int resultB = uutB.GetHashCode();

            // Assert
            Assert.Equal(resultA, resultB);
        }

        /// <summary>
        /// Test for reading a configuration file for response request key value pair
        /// </summary>
        [Fact]
        public void SerialComElements_Equals()
        {
            // Arrange
            SerialComElements uttA = this.GenerateSerialComElement_GetSoftwareVersion();
            SerialComElements uttB = this.GenerateSerialComElement_GetSoftwareVersion();
            bool expected = true;

            // Act
            bool result = uttA.Equals(uttB);

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Test for reading a configuration file for response request key value pair
        /// </summary>
        [Fact]
        public void SerialComElements_EqualsOperator()
        {
            // Arrange
            SerialComElements uttA = this.GenerateSerialComElement_GetSoftwareVersion();
            SerialComElements uttB = this.GenerateSerialComElement_GetSoftwareVersion();
            bool expected = true;

            // Act
            bool result = uttA == uttB;

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Test for reading a configuration file for response request key value pair
        /// </summary>
        [Fact]
        public void SerialComElements_NotEqualOperator()
        {
            // Arrange
            SerialComElements uttA = this.GenerateSerialComElement_GetSoftwareVersion();
            uttA.MessageName = "DifferentName";
            SerialComElements uttB = this.GenerateSerialComElement_GetSoftwareVersion();
            bool expected = true;

            // Act
            bool result = uttA != uttB;

            // Assert
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Generates test Data for a serial command named GetSoftwareVersion
        /// </summary>
        /// <returns>Initialized <see cref="SerialComElements"/> object</returns>
        private SerialComElements GenerateSerialComElement_GetSoftwareVersion()
        {
            return new SerialComElements
            {
                MessageName = "GetSoftwareVersion",
                MessageRequest = @"12345678",
                MessageResponse = @"12345678",
            };
        }
    }
}

// <copyright file="ResponseConfigFileReader_UnitTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace XUnitTests
{
    using System.Collections.Generic;
    using System.Linq;
    using SerialPortReaderWriter;
    using Xunit;

    /// <summary>
    /// Test class for <see cref="ResponseConfigurationReader"/>
    /// </summary>
    public class ResponseConfigFileReader_UnitTest
    {
        /// <summary>
        /// Test for reading a configuration file for response request key value pair
        /// </summary>
        [Fact]
        public void ReadConfigFile_Expected_Behaviour()
        {
            // Arrange
            var uut = new ResponseConfigurationReader();
            List<SerialComElements> expected = new List<SerialComElements>
            {
                this.GenerateSerialComElement_GetStatus(),
                this.GenerateSerialComElement_GetSoftwareVersion(),
            };

            // Act
            List<SerialComElements> result = uut.ReadConfigFile("MessageResponseConfig.json");

            // Assert
            foreach ((SerialComElements expectedElem, SerialComElements resultElem) in expected.Zip(result))
            {
                Assert.Equal(expectedElem.MessageName, resultElem.MessageName);
                Assert.Equal(expectedElem.MessageRequest, resultElem.MessageRequest);
                Assert.Equal(expectedElem.MessageResponse, resultElem.MessageResponse);
            }
        }

        /// <summary>
        /// Generates test Data for a serial command named GetStatus
        /// </summary>
        /// <returns>Initialized <see cref="SerialComElements"/> object</returns>
        private SerialComElements GenerateSerialComElement_GetStatus()
        {
            return new SerialComElements
            {
                MessageName = "GetStatus",
                MessageRequest = @"ðU¦",
                MessageResponse = @"ðU¦",
            };
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
                MessageRequest = @"ðU¦",
                MessageResponse = @"ðU¦",
            };
        }
    }
}

// TestsLazyJsonSerializerDateTime.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 08

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

using Lazy.Vinke.Json;
using Lazy.Vinke.Json.Properties;
using Lazy.Vinke.Tests.Json.Properties;

namespace Lazy.Vinke.Tests.Json
{
    [TestClass]
    public class TestsLazyJsonSerializerDateTime
    {
        [TestMethod]
        public void Serialize_Data_Null_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDateTime().Serialize(null);

            // Assert
            Assert.AreEqual(((LazyJsonString)jsonToken).Value, null);
        }

        [TestMethod]
        public void Serialize_Data_NotDateTime_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDateTime().Serialize("Lazy.Vinke.Tests.Json");

            // Assert
            Assert.AreEqual(((LazyJsonString)jsonToken).Value, null);
        }

        [TestMethod]
        public void Serialize_Options_Null_Success()
        {
            // Arrange
            DateTime dateTime = new DateTime(2023, 10, 8, 16, 20, 10);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDateTime().Serialize(dateTime);

            // Assert
            Assert.AreEqual(((LazyJsonString)jsonToken).Value, "2023-10-08T16:20:10:000Z");
        }

        [TestMethod]
        public void Serialize_Options_Empty_Success()
        {
            // Arrange
            DateTime dateTime = new DateTime(2023, 10, 8, 16, 20, 10);
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDateTime().Serialize(dateTime, jsonSerializerOptions);

            // Assert
            Assert.AreEqual(((LazyJsonString)jsonToken).Value, "2023-10-08T16:20:10:000Z");
        }

        [TestMethod]
        public void Serialize_Options_Valued_Success()
        {
            // Arrange
            DateTime dateTime = new DateTime(2023, 10, 8, 16, 20, 10);
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsDateTime>();

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDateTime().Serialize(dateTime, jsonSerializerOptions);

            // Assert
            Assert.AreEqual(((LazyJsonString)jsonToken).Value, "2023-10-08T16:20:10:000Z");
        }

        [TestMethod]
        public void Serialize_Options_OtherFormat_Success()
        {
            // Arrange
            DateTime dateTime = new DateTime(2023, 10, 8, 16, 20, 10);
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format = "MM/dd/yyyy HH:mm:ss";

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDateTime().Serialize(dateTime, jsonSerializerOptions);

            // Assert
            Assert.AreEqual(((LazyJsonString)jsonToken).Value, "10/08/2023 16:20:10");
        }

        [TestMethod]
        public void Serialize_Options_InvalidFormat_Exception()
        {
            // Arrange
            DateTime dateTime = new DateTime(2023, 10, 8, 16, 20, 10);
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsDateTime>().Format = "x";

            // Act
            Exception exception = null;
            try { new LazyJsonSerializerDateTime().Serialize(dateTime, jsonSerializerOptions); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.IsNotNull(exception);
        }
    }
}

// TestsLazyJsonDeserializerDateTime.cs
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
    public class TestsLazyJsonDeserializerDateTime
    {
        [TestMethod]
        public void Deserialize_Token_Null_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;

            // Act
            Object data = new LazyJsonDeserializerDateTime().Deserialize(jsonToken, typeof(DateTime));

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_Token_NotString_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = new LazyJsonBoolean(true);

            // Act
            Object data = new LazyJsonDeserializerDateTime().Deserialize(jsonToken, typeof(DateTime));

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_Token_ValueNull_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = new LazyJsonString();

            // Act
            Object data = new LazyJsonDeserializerDateTime().Deserialize(jsonToken, typeof(DateTime));

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_DataType_Null_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = new LazyJsonString("2023-10-08T16:20:10:000Z");

            // Act
            Object data = new LazyJsonDeserializerDateTime().Deserialize(jsonToken, null);

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_Options_Null_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = new LazyJsonString("2023-10-08T16:20:10:000Z");

            // Act
            Object data = new LazyJsonDeserializerDateTime().Deserialize(jsonToken, typeof(DateTime));

            // Assert
            Assert.AreEqual((DateTime)data, new DateTime(2023, 10, 8, 16, 20, 10));
        }

        [TestMethod]
        public void Deserialize_Options_Empty_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = new LazyJsonString("2023-10-08T16:20:10:000Z");
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();

            // Act
            Object data = new LazyJsonDeserializerDateTime().Deserialize(jsonToken, typeof(DateTime), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual((DateTime)data, new DateTime(2023, 10, 8, 16, 20, 10));
        }

        [TestMethod]
        public void Deserialize_Options_Valued_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = new LazyJsonString("2023-10-08T16:20:10:000Z");
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>();

            // Act
            Object data = new LazyJsonDeserializerDateTime().Deserialize(jsonToken, typeof(DateTime), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual((DateTime)data, new DateTime(2023, 10, 8, 16, 20, 10));
        }

        [TestMethod]
        public void Deserialize_Options_OtherFormat_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = new LazyJsonString("10/08/2023 16:20:10");
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Format = "MM/dd/yyyy HH:mm:ss";

            // Act
            Object data = new LazyJsonDeserializerDateTime().Deserialize(jsonToken, typeof(DateTime), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual((DateTime)data, new DateTime(2023, 10, 8, 16, 20, 10));
        }

        [TestMethod]
        public void Deserialize_Options_InvalidFormat_Exception()
        {
            // Arrange
            LazyJsonToken jsonToken = new LazyJsonString("10/08/2023 16:20:10");
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>().Format = "x";

            // Act
            Exception exception = null;
            try { new LazyJsonDeserializerDateTime().Deserialize(jsonToken, typeof(DateTime), jsonDeserializerOptions); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.IsNotNull(exception);
        }
    }
}

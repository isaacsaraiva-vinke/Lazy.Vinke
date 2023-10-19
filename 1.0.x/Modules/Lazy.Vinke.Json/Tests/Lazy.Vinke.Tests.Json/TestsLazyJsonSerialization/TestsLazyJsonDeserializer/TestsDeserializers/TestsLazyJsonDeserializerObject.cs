// TestsLazyJsonDeserializerObject.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 11

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
    public class TestsLazyJsonDeserializerObject
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;
            LazyJsonInteger jsonInteger = new LazyJsonInteger(0);

            // Act
            Object data1 = new LazyJsonDeserializerObject().Deserialize(jsonToken, typeof(Object));
            Object data2 = new LazyJsonDeserializerObject().Deserialize(jsonInteger, null);

            // Assert
            Assert.IsNull(data1);
            Assert.IsNull(data2);
        }

        [TestMethod]
        public void Deserialize_Type_NotObject_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger(0);

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonInteger, typeof(Int32));

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_TokenType_JsonNull_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonNull, typeof(Object));

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_TokenType_JsonInteger_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger(-1);

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonInteger, typeof(Object));

            // Assert
            Assert.AreEqual(data, (Int64)(-1));
        }

        [TestMethod]
        public void Deserialize_TokenType_JsonDecimal_Success()
        {
            // Arrange
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(-1.1m);

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonDecimal, typeof(Object));

            // Assert
            Assert.AreEqual(data, -1.1m);
        }

        [TestMethod]
        public void Deserialize_TokenType_JsonBoolean_Success()
        {
            // Arrange
            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(false);

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonBoolean, typeof(Object));

            // Assert
            Assert.AreEqual(data, false);
        }

        [TestMethod]
        public void Deserialize_TokenType_JsonArray_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(-101));

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonArray, typeof(Object));

            // Assert
            Assert.AreEqual(data.GetType(), typeof(Object[]));
            Assert.AreEqual(((Object[])data)[0], (Int64)(-101));
        }

        [TestMethod]
        public void Deserialize_TokenType_JsonObject_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Amount", new LazyJsonDecimal(-1.1m)));

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonObject, typeof(Object));

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_TokenType_JsonStringAsDateTime_Success()
        {
            // Arrange
            LazyJsonString jsonString = new LazyJsonString("2023-10-11T13:04:30:000Z");

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonString, typeof(Object));

            // Assert
            Assert.AreEqual(data.GetType(), typeof(DateTime));
            Assert.AreEqual(data, new DateTime(2023, 10, 11, 13, 04, 30));
        }

        [TestMethod]
        public void Deserialize_TokenType_JsonStringAsString_Success()
        {
            // Arrange
            LazyJsonString jsonString = new LazyJsonString("Lazy.Vinke.Tests.Json");

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonString, typeof(Object));

            // Assert
            Assert.AreEqual(data.GetType(), typeof(String));
            Assert.AreEqual(data, "Lazy.Vinke.Tests.Json");
        }
    }
}

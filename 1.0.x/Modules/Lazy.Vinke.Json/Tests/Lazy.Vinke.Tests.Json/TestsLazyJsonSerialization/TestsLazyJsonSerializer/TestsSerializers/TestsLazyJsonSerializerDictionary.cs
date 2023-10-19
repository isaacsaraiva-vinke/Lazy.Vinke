// TestsLazyJsonSerializerDictionary.cs
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
    public class TestsLazyJsonSerializerDictionary
    {
        [TestMethod]
        public void Serialize_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDictionary().Serialize(null);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Type_NotDictionary_Success()
        {
            // Arrange
            String test = "Lazy.Vinke.Tests.Json";

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDictionary().Serialize(test);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Type_Empty_Success()
        {
            // Arrange
            Dictionary<Int32, Decimal> integerDecimalDictionary = new Dictionary<Int32, Decimal>();

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDictionary().Serialize(integerDecimalDictionary);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 0);
        }

        [TestMethod]
        public void Serialize_Type_IntegerDecimal_Success()
        {
            // Arrange
            Dictionary<Int32, Decimal> integerDecimalDictionary = new Dictionary<Int32, Decimal>() { { 1, 1.1m }, { -101, -101.101m }, { -1, -1.1m } };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDictionary().Serialize(integerDecimalDictionary);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 3);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[0]).Length, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[0]).Value, 1);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[1]).Value, 1.1m);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[1]).Length, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[0]).Value, -101);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[1]).Value, -101.101m);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[2]).Length, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[2])[0]).Value, -1);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)((LazyJsonArray)jsonToken)[2])[1]).Value, -1.1m);
        }

        [TestMethod]
        public void Serialize_Type_StringBoolean_Success()
        {
            // Arrange
            Dictionary<String, Boolean> StringBooleanDictionary = new Dictionary<String, Boolean>() { { "Lazy", true }, { "Vinke", false }, { "Tests", true }, { "Json", false } };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDictionary().Serialize(StringBooleanDictionary);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 4);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[0]).Length, 2);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[0]).Value, "Lazy");
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[1]).Value, true);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[1]).Length, 2);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[0]).Value, "Vinke");
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[1]).Value, false);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[2]).Length, 2);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)jsonToken)[2])[0]).Value, "Tests");
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)((LazyJsonArray)jsonToken)[2])[1]).Value, true);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[3]).Length, 2);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)jsonToken)[3])[0]).Value, "Json");
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)((LazyJsonArray)jsonToken)[3])[1]).Value, false);
        }

        [TestMethod]
        public void Serialize_Type_ObjectKnown_Success()
        {
            // Arrange
            Dictionary<Object, Object> objectObjectDictionary = new Dictionary<Object, Object>() { { 1, "Lazy.Vinke.Tests.Json" }, { "TestedOn", new DateTime(2023, 10, 11, 18, 26, 30) } };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDictionary().Serialize(objectObjectDictionary);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 2);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[0]).Length, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[0]).Value, 1);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[1]).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[1]).Length, 2);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[0]).Value, "TestedOn");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[1]).Value, "2023-10-11T18:26:30:000Z");
        }
    }
}

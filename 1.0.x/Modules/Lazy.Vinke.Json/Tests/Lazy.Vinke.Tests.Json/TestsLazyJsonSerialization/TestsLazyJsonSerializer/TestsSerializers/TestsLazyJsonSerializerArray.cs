﻿// TestsLazyJsonSerializerArray.cs
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
    public class TestsLazyJsonSerializerArray
    {
        [TestMethod]
        public void Serialize_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerArray().Serialize(null);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Type_NotArray_Success()
        {
            // Arrange
            String test = "Lazy.Vinke.Tests.Json";

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerArray().Serialize(test);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Type_Empty_Success()
        {
            // Arrange
            Decimal[] decimalArray = new Decimal[] { };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerArray().Serialize(decimalArray);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 0);
        }

        [TestMethod]
        public void Serialize_Type_Integer_Success()
        {
            // Arrange
            Int32[] integerArray = new Int32[] { 1, 0, 1 };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerArray().Serialize(integerArray);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 3);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[0]).Value, 1);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[1]).Value, 0);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[2]).Value, 1);
        }

        [TestMethod]
        public void Serialize_Type_String_Success()
        {
            // Arrange
            String[] stringArray = new String[] { "Lazy", "Vinke", "Tests", "Json" };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerArray().Serialize(stringArray);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 4);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[0]).Value, "Lazy");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[1]).Value, "Vinke");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[2]).Value, "Tests");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[3]).Value, "Json");
        }

        [TestMethod]
        public void Serialize_Type_ObjectKnown_Success()
        {
            // Arrange
            Object[] objectArray = new Object[] { 1, "Vinke", -101.101m, true, null, new Int32[] { 101 }, new DateTime(2023, 10, 11, 08, 40, 00) };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerArray().Serialize(objectArray);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 7);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[0]).Value, 1);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[1]).Value, "Vinke");
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonToken)[2]).Value, -101.101m);
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)jsonToken)[3]).Value, true);
            Assert.AreEqual(((LazyJsonArray)jsonToken)[4].Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[5]).Length, 1);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[5])[0]).Value, 101);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[6]).Value, "2023-10-11T08:40:00:000Z");
        }
    }
}
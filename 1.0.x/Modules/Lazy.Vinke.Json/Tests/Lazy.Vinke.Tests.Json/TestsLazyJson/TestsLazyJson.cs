// TestsLazyJson.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, September 27

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Tests.Json
{
    [TestClass]
    public class TestsLazyJson
    {
        [TestMethod]
        public void Constructor_WithoutParameter_Single_Success()
        {
            // Arrange

            // Act
            LazyJson json = new LazyJson();

            // Assert
            Assert.AreEqual(json.Root.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Constructor_WithParameter_Null_Success()
        {
            // Arrange

            // Act
            LazyJson json = new LazyJson(null);

            // Assert
            Assert.AreEqual(json.Root.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Constructor_WithParameter_AllTypes_Success()
        {
            // Arrange

            // Act
            LazyJson jsonArray = new LazyJson(new LazyJsonArray());
            LazyJson jsonBoolean = new LazyJson(new LazyJsonBoolean(true));
            LazyJson jsonDecimal = new LazyJson(new LazyJsonDecimal(Decimal.MaxValue));
            LazyJson jsonInteger = new LazyJson(new LazyJsonInteger(Int64.MaxValue));
            LazyJson jsonNull = new LazyJson(new LazyJsonNull());
            LazyJson jsonObject = new LazyJson(new LazyJsonObject());
            LazyJson jsonString = new LazyJson(new LazyJsonString("Lazy Vinke Tests Json"));

            // Assert
            Assert.AreEqual(jsonArray.Root.Type, LazyJsonType.Array);
            Assert.AreEqual(jsonBoolean.Root.Type, LazyJsonType.Boolean);
            Assert.AreEqual(jsonDecimal.Root.Type, LazyJsonType.Decimal);
            Assert.AreEqual(jsonInteger.Root.Type, LazyJsonType.Integer);
            Assert.AreEqual(jsonNull.Root.Type, LazyJsonType.Null);
            Assert.AreEqual(jsonObject.Root.Type, LazyJsonType.Object);
            Assert.AreEqual(jsonString.Root.Type, LazyJsonType.String);
        }

        [TestMethod]
        public void SetPropertyRoot_TokenValued_AllTypes_Success()
        {
            // Arrange

            // Act
            LazyJson jsonArray = new LazyJson(); jsonArray.Root = new LazyJsonArray();
            LazyJson jsonBoolean = new LazyJson(); jsonBoolean.Root = new LazyJsonBoolean(true);
            LazyJson jsonDecimal = new LazyJson(); jsonDecimal.Root = new LazyJsonDecimal(Decimal.MaxValue);
            LazyJson jsonInteger = new LazyJson(); jsonInteger.Root = new LazyJsonInteger(Int64.MaxValue);
            LazyJson jsonNull = new LazyJson(); jsonNull.Root = new LazyJsonNull();
            LazyJson jsonObject = new LazyJson(); jsonObject.Root = new LazyJsonObject();
            LazyJson jsonString = new LazyJson(); jsonString.Root = new LazyJsonString("Lazy Vinke Tests Json");

            // Assert
            Assert.AreEqual(jsonArray.Root.Type, LazyJsonType.Array);
            Assert.AreEqual(jsonBoolean.Root.Type, LazyJsonType.Boolean);
            Assert.AreEqual(jsonDecimal.Root.Type, LazyJsonType.Decimal);
            Assert.AreEqual(jsonInteger.Root.Type, LazyJsonType.Integer);
            Assert.AreEqual(jsonNull.Root.Type, LazyJsonType.Null);
            Assert.AreEqual(jsonObject.Root.Type, LazyJsonType.Object);
            Assert.AreEqual(jsonString.Root.Type, LazyJsonType.String);
        }
    }
}

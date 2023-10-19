// TestsLazyJsonArray.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, September 24

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Tests.Json
{
    [TestClass]
    public class TestsLazyJsonArray
    {
        [TestMethod]
        public void Constructor_WithoutParameter_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Assert
            Assert.AreEqual(jsonArray.Type, LazyJsonType.Array);
            Assert.AreEqual(jsonArray.Length, 0);
        }

        [TestMethod]
        public void Add_TokenNull_Single_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            jsonArray.Add(null);

            // Assert
            Assert.AreEqual(jsonArray.Length, 1);
            Assert.AreEqual(jsonArray[0].Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Add_TokenValued_OneItem_Success()
        {
            // Arrange
            String itemValue = "Lazy Vinke Tests Json";
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            jsonArray.Add(new LazyJsonString(itemValue));

            // Assert
            Assert.AreEqual(jsonArray.Length, 1);
            Assert.AreEqual(((LazyJsonString)jsonArray[0]).Value, itemValue);
        }

        [TestMethod]
        public void Add_TokenValued_TwoItens_Success()
        {
            // Arrange
            Int64 item1Value = Int64.MaxValue;
            String item2Value = "Lazy Vinke Tests Json";
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            jsonArray.Add(new LazyJsonInteger(item1Value));
            jsonArray.Add(new LazyJsonString(item2Value));

            // Assert
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[0]).Value, item1Value);
            Assert.AreEqual(((LazyJsonString)jsonArray[1]).Value, item2Value);
        }

        [TestMethod]
        public void Add_TokenValued_AllTypes_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            jsonArray.Add(new LazyJsonArray());
            jsonArray.Add(new LazyJsonBoolean(false));
            jsonArray.Add(new LazyJsonDecimal(Decimal.MaxValue));
            jsonArray.Add(new LazyJsonInteger(Int64.MinValue));
            jsonArray.Add(new LazyJsonNull());
            jsonArray.Add(new LazyJsonObject());
            jsonArray.Add(new LazyJsonString("Lazy Vinke Tests Json"));

            // Assert
            Assert.AreEqual(jsonArray.Length, 7);
            Assert.AreEqual(jsonArray[0].Type, LazyJsonType.Array);
            Assert.AreEqual(jsonArray[1].Type, LazyJsonType.Boolean);
            Assert.AreEqual(jsonArray[2].Type, LazyJsonType.Decimal);
            Assert.AreEqual(jsonArray[3].Type, LazyJsonType.Integer);
            Assert.AreEqual(jsonArray[4].Type, LazyJsonType.Null);
            Assert.AreEqual(jsonArray[5].Type, LazyJsonType.Object);
            Assert.AreEqual(jsonArray[6].Type, LazyJsonType.String);
        }

        [TestMethod]
        public void RemoveAt_ValidIndex_OneItem_Success()
        {
            // Arrange
            String itemValue = "Lazy Vinke Tests Json";
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString(itemValue));

            // Act
            jsonArray.RemoveAt(0);

            // Assert
            Assert.AreEqual(jsonArray.Length, 0);
        }

        [TestMethod]
        public void RemoveAt_ValidIndex_TwoItens_Success()
        {
            // Arrange
            Int64 item1Value = Int64.MaxValue;
            String item2Value = "Lazy Vinke Tests Json";
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(item1Value));
            jsonArray.Add(new LazyJsonString(item2Value));

            // Act
            jsonArray.RemoveAt(1);

            // Assert
            Assert.AreEqual(jsonArray.Length, 1);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[0]).Value, item1Value);
        }

        [TestMethod]
        public void RemoveAt_InvalidIndex_Single_Success()
        {
            // Arrange
            Int64 item1Value = Int64.MaxValue;
            String item2Value = "Lazy Vinke Tests Json";
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(item1Value));
            jsonArray.Add(new LazyJsonString(item2Value));

            // Act
            jsonArray.RemoveAt(-1);
            jsonArray.RemoveAt(2);

            // Assert
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[0]).Value, item1Value);
            Assert.AreEqual(((LazyJsonString)jsonArray[1]).Value, item2Value);
        }

        [TestMethod]
        public void GetIndexer_Index_Overflow_Success()
        {
            // Arrange
            Int64 item1Value = Int64.MaxValue;
            String item2Value = "Lazy Vinke Tests Json";
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(item1Value));
            jsonArray.Add(new LazyJsonString(item2Value));

            // Act
            LazyJsonToken jsonToken1 = jsonArray[-1];
            LazyJsonToken jsonToken2 = jsonArray[2];

            // Assert
            Assert.IsNull(jsonToken1);
            Assert.IsNull(jsonToken2);
        }

        [TestMethod]
        public void SetIndexer_Index_Null_Success()
        {
            // Arrange
            Int64 itemValue = Int64.MaxValue;
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(itemValue));

            // Act
            jsonArray[0] = null;

            // Assert
            Assert.AreEqual(jsonArray.Length, 1);
            Assert.AreEqual(jsonArray[0].Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void SetIndexer_Index_Valued_Success()
        {
            // Arrange
            Int64 item1Value = Int64.MaxValue;
            String item2Value = "Lazy Vinke Tests Json";
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(item1Value));

            // Act
            jsonArray[0] = new LazyJsonString(item2Value);

            // Assert
            Assert.AreEqual(jsonArray.Length, 1);
            Assert.AreEqual(((LazyJsonString)jsonArray[0]).Value, item2Value);
        }

        [TestMethod]
        public void SetIndexer_Index_Overflow_Success()
        {
            // Arrange
            Int64 item1Value = Int64.MaxValue;
            String item2Value = "Lazy Vinke Tests Json";
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(item1Value));

            // Act
            jsonArray[-1] = new LazyJsonString(item2Value);
            jsonArray[1] = new LazyJsonString(item2Value);

            // Assert
            Assert.AreEqual(jsonArray.Length, 1);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[0]).Value, item1Value);
        }
    }
}

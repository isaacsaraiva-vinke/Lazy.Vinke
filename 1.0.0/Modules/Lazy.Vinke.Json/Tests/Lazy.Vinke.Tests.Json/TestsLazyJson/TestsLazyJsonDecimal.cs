// TestsLazyJsonDecimal.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, September 23

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using Lazy.Vinke.Json;

namespace Lazy.Vinke.Tests.Json
{
    [TestClass]
    public class TestsLazyJsonDecimal
    {
        [TestMethod]
        public void Constructor_WithoutParameter_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal();

            // Assert
            Assert.IsNull(jsonDecimal.Value);
            Assert.AreEqual(jsonDecimal.Type, LazyJsonType.Decimal);
        }

        [TestMethod]
        public void Constructor_WithParameter_Null_Success()
        {
            // Arrange

            // Act
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(null);

            // Assert
            Assert.IsNull(jsonDecimal.Value);
            Assert.AreEqual(jsonDecimal.Type, LazyJsonType.Decimal);
        }

        [TestMethod]
        public void Constructor_WithParameter_Zero_Success()
        {
            // Arrange

            // Act
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(0);

            // Assert
            Assert.AreEqual(jsonDecimal.Value, 0);
            Assert.AreEqual(jsonDecimal.Type, LazyJsonType.Decimal);
        }

        [TestMethod]
        public void Constructor_WithParameter_Positive_Success()
        {
            // Arrange

            // Act
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(Decimal.MaxValue);

            // Assert
            Assert.AreEqual(jsonDecimal.Value, Decimal.MaxValue);
            Assert.AreEqual(jsonDecimal.Type, LazyJsonType.Decimal);
        }

        [TestMethod]
        public void Constructor_WithParameter_Negative_Success()
        {
            // Arrange

            // Act
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(Decimal.MinValue);

            // Assert
            Assert.AreEqual(jsonDecimal.Value, Decimal.MinValue);
            Assert.AreEqual(jsonDecimal.Type, LazyJsonType.Decimal);
        }
    }
}

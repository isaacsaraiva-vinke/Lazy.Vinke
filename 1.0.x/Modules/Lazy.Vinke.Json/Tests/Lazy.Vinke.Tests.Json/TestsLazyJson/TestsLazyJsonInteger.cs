// TestsLazyJsonInteger.cs
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
    public class TestsLazyJsonInteger
    {
        [TestMethod]
        public void Constructor_WithoutParameter_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonInteger jsonInteger = new LazyJsonInteger();

            // Assert
            Assert.IsNull(jsonInteger.Value);
            Assert.AreEqual(jsonInteger.Type, LazyJsonType.Integer);
        }

        [TestMethod]
        public void Constructor_WithParameter_Null_Success()
        {
            // Arrange

            // Act
            LazyJsonInteger jsonInteger = new LazyJsonInteger(null);

            // Assert
            Assert.IsNull(jsonInteger.Value);
            Assert.AreEqual(jsonInteger.Type, LazyJsonType.Integer);
        }

        [TestMethod]
        public void Constructor_WithParameter_Zero_Success()
        {
            // Arrange

            // Act
            LazyJsonInteger jsonInteger = new LazyJsonInteger(0);

            // Assert
            Assert.AreEqual(jsonInteger.Value, 0);
            Assert.AreEqual(jsonInteger.Type, LazyJsonType.Integer);
        }

        [TestMethod]
        public void Constructor_WithParameter_Positive_Success()
        {
            // Arrange

            // Act
            LazyJsonInteger jsonInteger = new LazyJsonInteger(Int64.MaxValue);

            // Assert
            Assert.AreEqual(jsonInteger.Value, Int64.MaxValue);
            Assert.AreEqual(jsonInteger.Type, LazyJsonType.Integer);
        }

        [TestMethod]
        public void Constructor_WithParameter_Negative_Success()
        {
            // Arrange

            // Act
            LazyJsonInteger jsonInteger = new LazyJsonInteger(Int64.MinValue);

            // Assert
            Assert.AreEqual(jsonInteger.Value, Int64.MinValue);
            Assert.AreEqual(jsonInteger.Type, LazyJsonType.Integer);
        }
    }
}

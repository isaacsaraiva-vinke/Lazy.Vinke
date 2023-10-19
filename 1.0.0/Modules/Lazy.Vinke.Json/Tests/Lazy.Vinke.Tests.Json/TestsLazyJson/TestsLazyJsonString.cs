// TestsLazyJsonString.cs
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
    public class TestsLazyJsonString
    {
        [TestMethod]
        public void Constructor_WithoutParameter_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonString jsonString = new LazyJsonString();

            // Assert
            Assert.IsNull(jsonString.Value);
            Assert.AreEqual(jsonString.Type, LazyJsonType.String);
        }

        [TestMethod]
        public void Constructor_WithParameter_Null_Success()
        {
            // Arrange

            // Act
            LazyJsonString jsonString = new LazyJsonString(null);

            // Assert
            Assert.IsNull(jsonString.Value);
            Assert.AreEqual(jsonString.Type, LazyJsonType.String);
        }

        [TestMethod]
        public void Constructor_WithParameter_Empty_Success()
        {
            // Arrange

            // Act
            LazyJsonString jsonString = new LazyJsonString(String.Empty);

            // Assert
            Assert.AreEqual(jsonString.Value, String.Empty);
            Assert.AreEqual(jsonString.Type, LazyJsonType.String);
        }

        [TestMethod]
        public void Constructor_WithParameter_Whitespace_Success()
        {
            // Arrange

            // Act
            LazyJsonString jsonString = new LazyJsonString(" ");

            // Assert
            Assert.AreEqual(jsonString.Value, " ");
            Assert.AreEqual(jsonString.Type, LazyJsonType.String);
        }

        [TestMethod]
        public void Constructor_WithParameter_Valued_Success()
        {
            // Arrange
            String parameterString = "Lazy Vinke Tests Json";

            // Act
            LazyJsonString jsonString = new LazyJsonString(parameterString);

            // Assert
            Assert.AreEqual(jsonString.Value, parameterString);
            Assert.AreEqual(jsonString.Type, LazyJsonType.String);
        }
    }
}

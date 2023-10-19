// TestsLazyJsonBoolean.cs
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
    public class TestsLazyJsonBoolean
    {
        [TestMethod]
        public void Constructor_WithoutParameter_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean();

            // Assert
            Assert.IsNull(jsonBoolean.Value);
            Assert.AreEqual(jsonBoolean.Type, LazyJsonType.Boolean);
        }

        [TestMethod]
        public void Constructor_WithParameter_Null_Success()
        {
            // Arrange

            // Act
            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(null);

            // Assert
            Assert.IsNull(jsonBoolean.Value);
            Assert.AreEqual(jsonBoolean.Type, LazyJsonType.Boolean);
        }

        [TestMethod]
        public void Constructor_WithParameter_True_Success()
        {
            // Arrange

            // Act
            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(true);

            // Assert
            Assert.AreEqual(jsonBoolean.Value, true);
            Assert.AreEqual(jsonBoolean.Type, LazyJsonType.Boolean);
        }

        [TestMethod]
        public void Constructor_WithParameter_False_Success()
        {
            // Arrange

            // Act
            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(false);

            // Assert
            Assert.AreEqual(jsonBoolean.Value, false);
            Assert.AreEqual(jsonBoolean.Type, LazyJsonType.Boolean);
        }
    }
}

// TestsLazyJsonDeserializerOptions.cs
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
    public class TestsLazyJsonDeserializerOptions
    {
        [TestMethod]
        public void Item_DeserializerOptionsBase_Single_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();

            // Act
            LazyJsonDeserializerOptionsBase deserializerOptionsBase = deserializerOptions.Item<LazyJsonDeserializerOptionsBase>();

            // Assert
            Assert.IsNotNull(deserializerOptionsBase);
            Assert.IsTrue(deserializerOptions.Contains<LazyJsonDeserializerOptionsBase>());
        }

        [TestMethod]
        public void ItemIfContains_DeserializerOptionsBase_Single_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();

            // Act
            LazyJsonDeserializerOptionsBase deserializerOptionsBase = deserializerOptions.ItemIfContains<LazyJsonDeserializerOptionsBase>();

            // Assert
            Assert.IsNull(deserializerOptionsBase);
            Assert.IsFalse(deserializerOptions.Contains<LazyJsonDeserializerOptionsBase>());
        }

        [TestMethod]
        public void CurrentOrNew_DeserializerOptionsBase_Current_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();
            deserializerOptions.Item<LazyJsonDeserializerOptionsBase>();

            // Act
            LazyJsonDeserializerOptionsBase deserializerOptionsBase = LazyJsonDeserializerOptions.CurrentOrNew<LazyJsonDeserializerOptionsBase>(deserializerOptions);

            // Assert
            Assert.IsNotNull(deserializerOptionsBase);
            Assert.IsTrue(deserializerOptions.Contains<LazyJsonDeserializerOptionsBase>());
        }

        [TestMethod]
        public void CurrentOrNew_DeserializerOptionsBase_New_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions deserializerOptions = new LazyJsonDeserializerOptions();

            // Act
            LazyJsonDeserializerOptionsBase deserializerOptionsBase = LazyJsonDeserializerOptions.CurrentOrNew<LazyJsonDeserializerOptionsBase>(deserializerOptions);

            // Assert
            Assert.IsNotNull(deserializerOptionsBase);
            Assert.IsFalse(deserializerOptions.Contains<LazyJsonDeserializerOptionsBase>());
        }
    }
}

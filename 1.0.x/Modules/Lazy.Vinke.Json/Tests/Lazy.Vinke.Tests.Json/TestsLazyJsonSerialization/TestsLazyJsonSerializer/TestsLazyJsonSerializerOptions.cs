// TestsLazyJsonSerializerOptions.cs
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
    public class TestsLazyJsonSerializerOptions
    {
        [TestMethod]
        public void Item_SerializerOptionsBase_Single_Success()
        {
            // Arrange
            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();

            // Act
            LazyJsonSerializerOptionsBase serializerOptionsBase = serializerOptions.Item<LazyJsonSerializerOptionsBase>();

            // Assert
            Assert.IsNotNull(serializerOptionsBase);
            Assert.IsTrue(serializerOptions.Contains<LazyJsonSerializerOptionsBase>());
        }

        [TestMethod]
        public void ItemIfContains_SerializerOptionsBase_Single_Success()
        {
            // Arrange
            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();

            // Act
            LazyJsonSerializerOptionsBase serializerOptionsBase = serializerOptions.ItemIfContains<LazyJsonSerializerOptionsBase>();

            // Assert
            Assert.IsNull(serializerOptionsBase);
            Assert.IsFalse(serializerOptions.Contains<LazyJsonSerializerOptionsBase>());
        }

        [TestMethod]
        public void CurrentOrNew_SerializerOptionsBase_Current_Success()
        {
            // Arrange
            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();
            serializerOptions.Item<LazyJsonSerializerOptionsBase>();

            // Act
            LazyJsonSerializerOptionsBase serializerOptionsBase = LazyJsonSerializerOptions.CurrentOrNew<LazyJsonSerializerOptionsBase>(serializerOptions);

            // Assert
            Assert.IsNotNull(serializerOptionsBase);
            Assert.IsTrue(serializerOptions.Contains<LazyJsonSerializerOptionsBase>());
        }

        [TestMethod]
        public void CurrentOrNew_SerializerOptionsBase_New_Success()
        {
            // Arrange
            LazyJsonSerializerOptions serializerOptions = new LazyJsonSerializerOptions();

            // Act
            LazyJsonSerializerOptionsBase serializerOptionsBase = LazyJsonSerializerOptions.CurrentOrNew<LazyJsonSerializerOptionsBase>(serializerOptions);

            // Assert
            Assert.IsNotNull(serializerOptionsBase);
            Assert.IsFalse(serializerOptions.Contains<LazyJsonSerializerOptionsBase>());
        }
    }
}

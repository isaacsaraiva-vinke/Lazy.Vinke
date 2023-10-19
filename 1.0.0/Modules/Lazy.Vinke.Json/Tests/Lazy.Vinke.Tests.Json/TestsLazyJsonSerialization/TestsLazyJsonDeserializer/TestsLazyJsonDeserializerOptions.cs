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
    }
}

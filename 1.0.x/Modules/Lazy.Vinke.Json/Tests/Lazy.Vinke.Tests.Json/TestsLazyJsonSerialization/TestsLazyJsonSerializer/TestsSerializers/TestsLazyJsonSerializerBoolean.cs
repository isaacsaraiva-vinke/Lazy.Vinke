// TestsLazyJsonSerializerBoolean.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 07

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
    public class TestsLazyJsonSerializerBoolean
    {
        [TestMethod]
        public void Serialize_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerBoolean().Serialize(null);

            // Assert
            Assert.AreEqual(((LazyJsonBoolean)jsonToken).Value, null);
        }

        [TestMethod]
        public void Serialize_Boolean_Single_Success()
        {
            // Arrange
            Boolean dataNonNull = true;
            Nullable<Boolean> dataNullableNull = null;
            Nullable<Boolean> dataNullableValued = false;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerBoolean().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerBoolean().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerBoolean().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonBoolean)jsonTokenNonNull).Value, true);
            Assert.AreEqual(((LazyJsonBoolean)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonBoolean)jsonTokenNullableValued).Value, false);
        }
    }
}

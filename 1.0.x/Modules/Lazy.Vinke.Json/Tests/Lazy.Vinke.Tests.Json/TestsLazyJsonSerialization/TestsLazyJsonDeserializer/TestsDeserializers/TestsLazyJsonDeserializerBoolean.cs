// TestsLazyJsonDeserializerBoolean.cs
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
    public class TestsLazyJsonDeserializerBoolean
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;

            // Act
            Object data = new LazyJsonDeserializerBoolean().Deserialize(jsonToken, null);

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_Boolean_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonBoolean jsonBooleanNull = new LazyJsonBoolean(null);
            LazyJsonBoolean jsonBooleanValued = new LazyJsonBoolean(true);

            // Act
            Object dataTokenNullBoolean = new LazyJsonDeserializerBoolean().Deserialize(jsonNull, typeof(Boolean));
            Object dataTokenNullBooleanNullable = new LazyJsonDeserializerBoolean().Deserialize(jsonNull, typeof(Nullable<Boolean>));
            Object dataTokenBooleanNull = new LazyJsonDeserializerBoolean().Deserialize(jsonBooleanNull, typeof(Boolean));
            Object dataTokenBooleanValued = new LazyJsonDeserializerBoolean().Deserialize(jsonBooleanValued, typeof(Boolean));
            Object dataTokenBooleanNullableNull = new LazyJsonDeserializerBoolean().Deserialize(jsonBooleanNull, typeof(Nullable<Boolean>));
            Object dataTokenBooleanNullableValued = new LazyJsonDeserializerBoolean().Deserialize(jsonBooleanValued, typeof(Nullable<Boolean>));

            // Assert
            Assert.AreEqual(dataTokenNullBoolean, false);
            Assert.AreEqual(dataTokenNullBooleanNullable, null);
            Assert.AreEqual(dataTokenBooleanNull, false);
            Assert.AreEqual(dataTokenBooleanValued, true);
            Assert.AreEqual(dataTokenBooleanNullableNull, null);
            Assert.AreEqual(dataTokenBooleanNullableValued, true);
        }
    }
}

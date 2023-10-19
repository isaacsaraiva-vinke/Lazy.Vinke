// TestsLazyJsonDeserializerString.cs
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
    public class TestsLazyJsonDeserializerString
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;

            // Act
            Object data = new LazyJsonDeserializerString().Deserialize(jsonToken, null);

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_String_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonString jsonStringNull = new LazyJsonString(null);
            LazyJsonString jsonStringValued = new LazyJsonString("Lazy.Vinke.Tests.Json");

            // Act
            Object dataTokenNullString = new LazyJsonDeserializerString().Deserialize(jsonNull, typeof(String));
            Object dataTokenStringNull = new LazyJsonDeserializerString().Deserialize(jsonStringNull, typeof(String));
            Object dataTokenStringValued = new LazyJsonDeserializerString().Deserialize(jsonStringValued, typeof(String));

            // Assert
            Assert.AreEqual(dataTokenNullString, null);
            Assert.AreEqual(dataTokenStringNull, null);
            Assert.AreEqual(dataTokenStringValued, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void Deserialize_Char_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonString jsonStringNull = new LazyJsonString(null);
            LazyJsonString jsonStringValued = new LazyJsonString("J");

            // Act
            Object dataTokenNullChar = new LazyJsonDeserializerString().Deserialize(jsonNull, typeof(Char));
            Object dataTokenNullCharNullable = new LazyJsonDeserializerString().Deserialize(jsonNull, typeof(Nullable<Char>));
            Object dataTokenStringCharNull = new LazyJsonDeserializerString().Deserialize(jsonStringNull, typeof(Char));
            Object dataTokenStringCharValued = new LazyJsonDeserializerString().Deserialize(jsonStringValued, typeof(Char));
            Object dataTokenStringCharNullableNull = new LazyJsonDeserializerString().Deserialize(jsonStringNull, typeof(Nullable<Char>));
            Object dataTokenStringCharNullableValued = new LazyJsonDeserializerString().Deserialize(jsonStringValued, typeof(Nullable<Char>));

            // Assert
            Assert.AreEqual(dataTokenNullChar, '\0');
            Assert.AreEqual(dataTokenNullCharNullable, null);
            Assert.AreEqual(dataTokenStringCharNull, '\0');
            Assert.AreEqual(dataTokenStringCharValued, 'J');
            Assert.AreEqual(dataTokenStringCharNullableNull, null);
            Assert.AreEqual(dataTokenStringCharNullableValued, 'J');
        }
    }
}

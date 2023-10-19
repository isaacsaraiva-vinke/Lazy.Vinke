// TestsLazyJsonSerializerString.cs
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
    public class TestsLazyJsonSerializerString
    {
        [TestMethod]
        public void Serialize_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerString().Serialize(null);

            // Assert
            Assert.AreEqual(((LazyJsonString)jsonToken).Value, null);
        }

        [TestMethod]
        public void Serialize_String_Single_Success()
        {
            // Arrange
            String dataNonNull = "Lazy.Vinke.Tests.Json";
            String dataNullableNull = null;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerString().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerString().Serialize(dataNullableNull);

            // Assert
            Assert.AreEqual(((LazyJsonString)jsonTokenNonNull).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonString)jsonTokenNullableNull).Value, null);
        }

        [TestMethod]
        public void Serialize_Char_Single_Success()
        {
            // Arrange
            Char dataNull = '\0';
            Char dataNonNull = 'J';
            Nullable<Char> dataNullableNull = null;
            Nullable<Char> dataNullableValued = 'S';
            Nullable<Char> dataNullableValuedNull = '\0';

            // Act
            LazyJsonToken jsonTokenNull = new LazyJsonSerializerString().Serialize(dataNull);
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerString().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerString().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerString().Serialize(dataNullableValued);
            LazyJsonToken jsonTokenNullableValuedNull = new LazyJsonSerializerString().Serialize(dataNullableValuedNull);

            // Assert
            Assert.AreEqual(((LazyJsonString)jsonTokenNull).Value, null);
            Assert.AreEqual(((LazyJsonString)jsonTokenNonNull).Value, "J");
            Assert.AreEqual(((LazyJsonString)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonString)jsonTokenNullableValued).Value, "S");
            Assert.AreEqual(((LazyJsonString)jsonTokenNullableValuedNull).Value, null);
        }
    }
}

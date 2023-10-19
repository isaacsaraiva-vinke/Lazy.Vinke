// TestsLazyJsonSerializerDecimal.cs
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
    public class TestsLazyJsonSerializerDecimal
    {
        [TestMethod]
        public void Serialize_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDecimal().Serialize(null);

            // Assert
            Assert.AreEqual(((LazyJsonDecimal)jsonToken).Value, null);
        }

        [TestMethod]
        public void Serialize_Decimal_Single_Success()
        {
            // Arrange
            Decimal dataNonNull = Decimal.MaxValue;
            Nullable<Decimal> dataNullableNull = null;
            Nullable<Decimal> dataNullableValued = Decimal.MaxValue;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerDecimal().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerDecimal().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerDecimal().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenNonNull).Value, Decimal.MaxValue);
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenNullableValued).Value, Decimal.MaxValue);
        }

        [TestMethod]
        public void Serialize_Double_Single_Success()
        {
            // Arrange
            Double dataNonNull = 101.101d;
            Nullable<Double> dataNullableNull = null;
            Nullable<Double> dataNullableValued = 1001.1001d;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerDecimal().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerDecimal().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerDecimal().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenNonNull).Value, 101.101m);
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenNullableValued).Value, 1001.1001m);
        }

        [TestMethod]
        public void Serialize_Single_Single_Success()
        {
            // Arrange
            Single dataNonNull = 101.101f;
            Nullable<Single> dataNullableNull = null;
            Nullable<Single> dataNullableValued = 1.1f;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerDecimal().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerDecimal().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerDecimal().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenNonNull).Value, 101.101m);
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenNullableValued).Value, 1.1m);
        }
    }
}

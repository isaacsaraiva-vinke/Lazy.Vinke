// TestsLazyJsonDeserializerDecimal.cs
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
    public class TestsLazyJsonDeserializerDecimal
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;

            // Act
            Object data = new LazyJsonDeserializerDecimal().Deserialize(jsonToken, null);

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_Decimal_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonDecimal jsonDecimalNull = new LazyJsonDecimal(null);
            LazyJsonDecimal jsonDecimalValued = new LazyJsonDecimal(Decimal.MaxValue);

            // Act
            Object dataTokenNullDecimal = new LazyJsonDeserializerDecimal().Deserialize(jsonNull, typeof(Decimal));
            Object dataTokenNullDecimalNullable = new LazyJsonDeserializerDecimal().Deserialize(jsonNull, typeof(Nullable<Decimal>));
            Object dataTokenDecimalNull = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalNull, typeof(Decimal));
            Object dataTokenDecimalValued = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalValued, typeof(Decimal));
            Object dataTokenDecimalNullableNull = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalNull, typeof(Nullable<Decimal>));
            Object dataTokenDecimalNullableValued = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalValued, typeof(Nullable<Decimal>));

            // Assert
            Assert.AreEqual(dataTokenNullDecimal, 0.0m);
            Assert.AreEqual(dataTokenNullDecimalNullable, null);
            Assert.AreEqual(dataTokenDecimalNull, 0.0m);
            Assert.AreEqual(dataTokenDecimalValued, Decimal.MaxValue);
            Assert.AreEqual(dataTokenDecimalNullableNull, null);
            Assert.AreEqual(dataTokenDecimalNullableValued, Decimal.MaxValue);
        }

        [TestMethod]
        public void Deserialize_Double_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonDecimal jsonDecimalNull = new LazyJsonDecimal(null);
            LazyJsonDecimal jsonDecimalValued = new LazyJsonDecimal(1001.1001m);

            // Act
            Object dataTokenNullDouble = new LazyJsonDeserializerDecimal().Deserialize(jsonNull, typeof(Double));
            Object dataTokenNullDoubleNullable = new LazyJsonDeserializerDecimal().Deserialize(jsonNull, typeof(Nullable<Double>));
            Object dataTokenDecimalDoubleNull = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalNull, typeof(Double));
            Object dataTokenDecimalDoubleValued = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalValued, typeof(Double));
            Object dataTokenDecimalDoubleNullableNull = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalNull, typeof(Nullable<Double>));
            Object dataTokenDecimalDoubleNullableValued = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalValued, typeof(Nullable<Double>));

            // Assert
            Assert.AreEqual(dataTokenNullDouble, 0.0d);
            Assert.AreEqual(dataTokenNullDoubleNullable, null);
            Assert.AreEqual(dataTokenDecimalDoubleNull, 0.0d);
            Assert.AreEqual(dataTokenDecimalDoubleValued, 1001.1001d);
            Assert.AreEqual(dataTokenDecimalDoubleNullableNull, null);
            Assert.AreEqual(dataTokenDecimalDoubleNullableValued, 1001.1001d);
        }

        [TestMethod]
        public void Deserialize_Single_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonDecimal jsonDecimalNull = new LazyJsonDecimal(null);
            LazyJsonDecimal jsonDecimalValued = new LazyJsonDecimal(101.101m);

            // Act
            Object dataTokenNullSingle = new LazyJsonDeserializerDecimal().Deserialize(jsonNull, typeof(Single));
            Object dataTokenNullSingleNullable = new LazyJsonDeserializerDecimal().Deserialize(jsonNull, typeof(Nullable<Single>));
            Object dataTokenDecimalSingleNull = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalNull, typeof(Single));
            Object dataTokenDecimalSingleValued = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalValued, typeof(Single));
            Object dataTokenDecimalSingleNullableNull = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalNull, typeof(Nullable<Single>));
            Object dataTokenDecimalSingleNullableValued = new LazyJsonDeserializerDecimal().Deserialize(jsonDecimalValued, typeof(Nullable<Single>));

            // Assert
            Assert.AreEqual(dataTokenNullSingle, 0.0f);
            Assert.AreEqual(dataTokenNullSingleNullable, null);
            Assert.AreEqual(dataTokenDecimalSingleNull, 0.0f);
            Assert.AreEqual(dataTokenDecimalSingleValued, 101.101f);
            Assert.AreEqual(dataTokenDecimalSingleNullableNull, null);
            Assert.AreEqual(dataTokenDecimalSingleNullableValued, 101.101f);
        }
    }
}

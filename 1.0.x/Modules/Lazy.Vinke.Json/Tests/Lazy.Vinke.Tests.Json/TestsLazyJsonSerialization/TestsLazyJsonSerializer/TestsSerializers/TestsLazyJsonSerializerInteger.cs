// TestsLazyJsonSerializerInteger.cs
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
    public class TestsLazyJsonSerializerInteger
    {
        [TestMethod]
        public void Serialize_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerInteger().Serialize(null);

            // Assert
            Assert.AreEqual(((LazyJsonInteger)jsonToken).Value, null);
        }

        [TestMethod]
        public void Serialize_Int32_Single_Success()
        {
            // Arrange
            Int32 dataNonNull = Int32.MaxValue;
            Nullable<Int32> dataNullableNull = null;
            Nullable<Int32> dataNullableValued = Int32.MaxValue;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerInteger().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerInteger().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerInteger().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNonNull).Value, Int32.MaxValue);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableValued).Value, Int32.MaxValue);
        }

        [TestMethod]
        public void Serialize_Int16_Single_Success()
        {
            // Arrange
            Int16 dataNonNull = Int16.MaxValue;
            Nullable<Int16> dataNullableNull = null;
            Nullable<Int16> dataNullableValued = Int16.MaxValue;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerInteger().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerInteger().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerInteger().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNonNull).Value, Int16.MaxValue);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableValued).Value, Int16.MaxValue);
        }

        [TestMethod]
        public void Serialize_Int64_Single_Success()
        {
            // Arrange
            Int64 dataNonNull = Int64.MaxValue;
            Nullable<Int64> dataNullableNull = null;
            Nullable<Int64> dataNullableValued = Int64.MaxValue;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerInteger().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerInteger().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerInteger().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNonNull).Value, Int64.MaxValue);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableValued).Value, Int64.MaxValue);
        }

        [TestMethod]
        public void Serialize_Byte_Single_Success()
        {
            // Arrange
            Byte dataNonNull = Byte.MaxValue;
            Nullable<Byte> dataNullableNull = null;
            Nullable<Byte> dataNullableValued = Byte.MaxValue;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerInteger().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerInteger().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerInteger().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNonNull).Value, Byte.MaxValue);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableValued).Value, Byte.MaxValue);
        }

        [TestMethod]
        public void Serialize_SByte_Single_Success()
        {
            // Arrange
            SByte dataNonNull = SByte.MaxValue;
            Nullable<SByte> dataNullableNull = null;
            Nullable<SByte> dataNullableValued = SByte.MaxValue;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerInteger().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerInteger().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerInteger().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNonNull).Value, SByte.MaxValue);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableValued).Value, SByte.MaxValue);
        }

        [TestMethod]
        public void Serialize_UInt32_Single_Success()
        {
            // Arrange
            UInt32 dataNonNull = UInt32.MaxValue;
            Nullable<UInt32> dataNullableNull = null;
            Nullable<UInt32> dataNullableValued = UInt32.MaxValue;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerInteger().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerInteger().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerInteger().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNonNull).Value, UInt32.MaxValue);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableValued).Value, UInt32.MaxValue);
        }

        [TestMethod]
        public void Serialize_UInt16_Single_Success()
        {
            // Arrange
            UInt16 dataNonNull = UInt16.MaxValue;
            Nullable<UInt16> dataNullableNull = null;
            Nullable<UInt16> dataNullableValued = UInt16.MaxValue;

            // Act
            LazyJsonToken jsonTokenNonNull = new LazyJsonSerializerInteger().Serialize(dataNonNull);
            LazyJsonToken jsonTokenNullableNull = new LazyJsonSerializerInteger().Serialize(dataNullableNull);
            LazyJsonToken jsonTokenNullableValued = new LazyJsonSerializerInteger().Serialize(dataNullableValued);

            // Assert
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNonNull).Value, UInt16.MaxValue);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableNull).Value, null);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenNullableValued).Value, UInt16.MaxValue);
        }
    }
}

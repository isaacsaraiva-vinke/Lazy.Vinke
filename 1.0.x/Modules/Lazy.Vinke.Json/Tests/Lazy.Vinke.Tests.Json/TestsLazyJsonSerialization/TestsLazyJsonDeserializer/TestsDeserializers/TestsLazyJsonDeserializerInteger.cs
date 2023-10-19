// TestsLazyJsonDeserializerInteger.cs
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
    public class TestsLazyJsonDeserializerInteger
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;

            // Act
            Object data = new LazyJsonDeserializerInteger().Deserialize(jsonToken, null);

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_Int32_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonInteger jsonIntegerNull = new LazyJsonInteger(null);
            LazyJsonInteger jsonIntegerValued = new LazyJsonInteger(Int32.MaxValue);

            // Act
            Object dataTokenNullInt32 = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Int32));
            Object dataTokenNullInt32Nullable = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Nullable<Int32>));
            Object dataTokenIntegerInt32Null = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Int32));
            Object dataTokenIntegerInt32Valued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Int32));
            Object dataTokenIntegerInt32NullableNull = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Nullable<Int32>));
            Object dataTokenIntegerInt32NullableValued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Nullable<Int32>));

            // Assert
            Assert.AreEqual(dataTokenNullInt32, (Int32)0);
            Assert.AreEqual(dataTokenNullInt32Nullable, null);
            Assert.AreEqual(dataTokenIntegerInt32Null, (Int32)0);
            Assert.AreEqual(dataTokenIntegerInt32Valued, Int32.MaxValue);
            Assert.AreEqual(dataTokenIntegerInt32NullableNull, null);
            Assert.AreEqual(dataTokenIntegerInt32NullableValued, Int32.MaxValue);
        }

        [TestMethod]
        public void Deserialize_Int16_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonInteger jsonIntegerNull = new LazyJsonInteger(null);
            LazyJsonInteger jsonIntegerValued = new LazyJsonInteger(Int16.MaxValue);

            // Act
            Object dataTokenNullInt16 = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Int16));
            Object dataTokenNullInt16Nullable = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Nullable<Int16>));
            Object dataTokenIntegerInt16Null = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Int16));
            Object dataTokenIntegerInt16Valued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Int16));
            Object dataTokenIntegerInt16NullableNull = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Nullable<Int16>));
            Object dataTokenIntegerInt16NullableValued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Nullable<Int16>));

            // Assert
            Assert.AreEqual(dataTokenNullInt16, (Int16)0);
            Assert.AreEqual(dataTokenNullInt16Nullable, null);
            Assert.AreEqual(dataTokenIntegerInt16Null, (Int16)0);
            Assert.AreEqual(dataTokenIntegerInt16Valued, Int16.MaxValue);
            Assert.AreEqual(dataTokenIntegerInt16NullableNull, null);
            Assert.AreEqual(dataTokenIntegerInt16NullableValued, Int16.MaxValue);
        }

        [TestMethod]
        public void Deserialize_Int64_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonInteger jsonIntegerNull = new LazyJsonInteger(null);
            LazyJsonInteger jsonIntegerValued = new LazyJsonInteger(Int64.MaxValue);

            // Act
            Object dataTokenNullInt64 = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Int64));
            Object dataTokenNullInt64Nullable = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Nullable<Int64>));
            Object dataTokenIntegerInt64Null = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Int64));
            Object dataTokenIntegerInt64Valued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Int64));
            Object dataTokenIntegerInt64NullableNull = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Nullable<Int64>));
            Object dataTokenIntegerInt64NullableValued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Nullable<Int64>));

            // Assert
            Assert.AreEqual(dataTokenNullInt64, (Int64)0);
            Assert.AreEqual(dataTokenNullInt64Nullable, null);
            Assert.AreEqual(dataTokenIntegerInt64Null, (Int64)0);
            Assert.AreEqual(dataTokenIntegerInt64Valued, Int64.MaxValue);
            Assert.AreEqual(dataTokenIntegerInt64NullableNull, null);
            Assert.AreEqual(dataTokenIntegerInt64NullableValued, Int64.MaxValue);
        }

        [TestMethod]
        public void Deserialize_Byte_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonInteger jsonIntegerNull = new LazyJsonInteger(null);
            LazyJsonInteger jsonIntegerValued = new LazyJsonInteger(Byte.MaxValue);

            // Act
            Object dataTokenNullByte = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Byte));
            Object dataTokenNullByteNullable = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Nullable<Byte>));
            Object dataTokenIntegerByteNull = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Byte));
            Object dataTokenIntegerByteValued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Byte));
            Object dataTokenIntegerByteNullableNull = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Nullable<Byte>));
            Object dataTokenIntegerByteNullableValued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Nullable<Byte>));

            // Assert
            Assert.AreEqual(dataTokenNullByte, (Byte)0);
            Assert.AreEqual(dataTokenNullByteNullable, null);
            Assert.AreEqual(dataTokenIntegerByteNull, (Byte)0);
            Assert.AreEqual(dataTokenIntegerByteValued, Byte.MaxValue);
            Assert.AreEqual(dataTokenIntegerByteNullableNull, null);
            Assert.AreEqual(dataTokenIntegerByteNullableValued, Byte.MaxValue);
        }

        [TestMethod]
        public void Deserialize_SByte_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonInteger jsonIntegerNull = new LazyJsonInteger(null);
            LazyJsonInteger jsonIntegerValued = new LazyJsonInteger(SByte.MaxValue);

            // Act
            Object dataTokenNullSByte = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(SByte));
            Object dataTokenNullSByteNullable = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Nullable<SByte>));
            Object dataTokenIntegerSByteNull = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(SByte));
            Object dataTokenIntegerSByteValued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(SByte));
            Object dataTokenIntegerSByteNullableNull = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Nullable<SByte>));
            Object dataTokenIntegerSByteNullableValued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Nullable<SByte>));

            // Assert
            Assert.AreEqual(dataTokenNullSByte, (SByte)0);
            Assert.AreEqual(dataTokenNullSByteNullable, null);
            Assert.AreEqual(dataTokenIntegerSByteNull, (SByte)0);
            Assert.AreEqual(dataTokenIntegerSByteValued, SByte.MaxValue);
            Assert.AreEqual(dataTokenIntegerSByteNullableNull, null);
            Assert.AreEqual(dataTokenIntegerSByteNullableValued, SByte.MaxValue);
        }

        [TestMethod]
        public void Deserialize_UInt32_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonInteger jsonIntegerNull = new LazyJsonInteger(null);
            LazyJsonInteger jsonIntegerValued = new LazyJsonInteger(UInt32.MaxValue);

            // Act
            Object dataTokenNullUInt32 = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(UInt32));
            Object dataTokenNullUInt32Nullable = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Nullable<UInt32>));
            Object dataTokenIntegerUInt32Null = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(UInt32));
            Object dataTokenIntegerUInt32Valued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(UInt32));
            Object dataTokenIntegerUInt32NullableNull = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Nullable<UInt32>));
            Object dataTokenIntegerUInt32NullableValued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Nullable<UInt32>));

            // Assert
            Assert.AreEqual(dataTokenNullUInt32, (UInt32)0);
            Assert.AreEqual(dataTokenNullUInt32Nullable, null);
            Assert.AreEqual(dataTokenIntegerUInt32Null, (UInt32)0);
            Assert.AreEqual(dataTokenIntegerUInt32Valued, UInt32.MaxValue);
            Assert.AreEqual(dataTokenIntegerUInt32NullableNull, null);
            Assert.AreEqual(dataTokenIntegerUInt32NullableValued, UInt32.MaxValue);
        }

        [TestMethod]
        public void Deserialize_UInt16_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();
            LazyJsonInteger jsonIntegerNull = new LazyJsonInteger(null);
            LazyJsonInteger jsonIntegerValued = new LazyJsonInteger(UInt16.MaxValue);

            // Act
            Object dataTokenNullUInt16 = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(UInt16));
            Object dataTokenNullUInt16Nullable = new LazyJsonDeserializerInteger().Deserialize(jsonNull, typeof(Nullable<UInt16>));
            Object dataTokenIntegerUInt16Null = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(UInt16));
            Object dataTokenIntegerUInt16Valued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(UInt16));
            Object dataTokenIntegerUInt16NullableNull = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerNull, typeof(Nullable<UInt16>));
            Object dataTokenIntegerUInt16NullableValued = new LazyJsonDeserializerInteger().Deserialize(jsonIntegerValued, typeof(Nullable<UInt16>));

            // Assert
            Assert.AreEqual(dataTokenNullUInt16, (UInt16)0);
            Assert.AreEqual(dataTokenNullUInt16Nullable, null);
            Assert.AreEqual(dataTokenIntegerUInt16Null, (UInt16)0);
            Assert.AreEqual(dataTokenIntegerUInt16Valued, UInt16.MaxValue);
            Assert.AreEqual(dataTokenIntegerUInt16NullableNull, null);
            Assert.AreEqual(dataTokenIntegerUInt16NullableValued, UInt16.MaxValue);
        }
    }
}

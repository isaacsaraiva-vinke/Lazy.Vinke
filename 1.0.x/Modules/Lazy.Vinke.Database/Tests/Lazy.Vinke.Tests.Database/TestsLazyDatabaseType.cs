// TestsLazyDatabaseType.cs
//
// This file is integrated part of "Lazy Vinke Tests Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 22

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using Lazy.Vinke.Database;

namespace Lazy.Vinke.Tests.Database
{
    [TestClass]
    public class TestsLazyDatabaseType
    {
        [TestMethod]
        public void FromSystemType_LazyDbType_Single_Success()
        {
            // Arrange

            // Act
            LazyDbType dbTypeDBNull = LazyDatabaseType.FromSystemType(typeof(DBNull));
            LazyDbType dbTypeChar = LazyDatabaseType.FromSystemType(typeof(Char));
            LazyDbType dbTypeString = LazyDatabaseType.FromSystemType(typeof(String));
            LazyDbType dbTypeSByte = LazyDatabaseType.FromSystemType(typeof(SByte));
            LazyDbType dbTypeInt16 = LazyDatabaseType.FromSystemType(typeof(Int16));
            LazyDbType dbTypeInt32 = LazyDatabaseType.FromSystemType(typeof(Int32));
            LazyDbType dbTypeInt64 = LazyDatabaseType.FromSystemType(typeof(Int64));
            LazyDbType dbTypeByte = LazyDatabaseType.FromSystemType(typeof(Byte));
            LazyDbType dbTypeSingle = LazyDatabaseType.FromSystemType(typeof(Single));
            LazyDbType dbTypeDouble = LazyDatabaseType.FromSystemType(typeof(Double));
            LazyDbType dbTypeDecimal = LazyDatabaseType.FromSystemType(typeof(Decimal));
            LazyDbType dbTypeDateTime = LazyDatabaseType.FromSystemType(typeof(DateTime));
            LazyDbType dbTypeByteArray = LazyDatabaseType.FromSystemType(typeof(Byte[]));

            LazyDbType dbTypeDBNullUnmatch1 = LazyDatabaseType.FromSystemType(null);
            LazyDbType dbTypeDBNullUnmatch2 = LazyDatabaseType.FromSystemType(typeof(Int32[]));
            LazyDbType dbTypeDBNullUnmatch3 = LazyDatabaseType.FromSystemType(typeof(List<>));
            LazyDbType dbTypeDBNullUnmatch4 = LazyDatabaseType.FromSystemType(typeof(DataTable));
            LazyDbType dbTypeDBNullUnmatch5 = LazyDatabaseType.FromSystemType(typeof(DataSet));
            LazyDbType dbTypeDBNullUnmatch6 = LazyDatabaseType.FromSystemType(typeof(Array));

            // Assert
            Assert.AreEqual(dbTypeDBNull, LazyDbType.DBNull);
            Assert.AreEqual(dbTypeChar, LazyDbType.Char);
            Assert.AreEqual(dbTypeString, LazyDbType.VarChar);
            Assert.AreEqual(dbTypeSByte, LazyDbType.Byte);
            Assert.AreEqual(dbTypeInt16, LazyDbType.Int16);
            Assert.AreEqual(dbTypeInt32, LazyDbType.Int32);
            Assert.AreEqual(dbTypeInt64, LazyDbType.Int64);
            Assert.AreEqual(dbTypeByte, LazyDbType.UByte);
            Assert.AreEqual(dbTypeSingle, LazyDbType.Float);
            Assert.AreEqual(dbTypeDouble, LazyDbType.Double);
            Assert.AreEqual(dbTypeDecimal, LazyDbType.Decimal);
            Assert.AreEqual(dbTypeDateTime, LazyDbType.DateTime);
            Assert.AreEqual(dbTypeByteArray, LazyDbType.VarUByte);

            Assert.AreEqual(dbTypeDBNullUnmatch1, LazyDbType.DBNull);
            Assert.AreEqual(dbTypeDBNullUnmatch2, LazyDbType.DBNull);
            Assert.AreEqual(dbTypeDBNullUnmatch3, LazyDbType.DBNull);
            Assert.AreEqual(dbTypeDBNullUnmatch4, LazyDbType.DBNull);
            Assert.AreEqual(dbTypeDBNullUnmatch5, LazyDbType.DBNull);
            Assert.AreEqual(dbTypeDBNullUnmatch6, LazyDbType.DBNull);
        }
    }
}

// TestsLazyDbType.cs
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
    public class TestsLazyDbType
    {
        [TestMethod]
        public void LazyDbType_Value_Single_Success()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual((Int32)LazyDbType.DBNull, 0);
            Assert.AreEqual((Int32)LazyDbType.Char, 8);
            Assert.AreEqual((Int32)LazyDbType.VarChar, 12);
            Assert.AreEqual((Int32)LazyDbType.VarText, 14);
            Assert.AreEqual((Int32)LazyDbType.Byte, 20);
            Assert.AreEqual((Int32)LazyDbType.Int16, 22);
            Assert.AreEqual((Int32)LazyDbType.Int32, 24);
            Assert.AreEqual((Int32)LazyDbType.Int64, 26);
            Assert.AreEqual((Int32)LazyDbType.UByte, 32);
            Assert.AreEqual((Int32)LazyDbType.Float, 44);
            Assert.AreEqual((Int32)LazyDbType.Double, 46);
            Assert.AreEqual((Int32)LazyDbType.Decimal, 48);
            Assert.AreEqual((Int32)LazyDbType.DateTime, 56);
            Assert.AreEqual((Int32)LazyDbType.VarUByte, 72);
        }
    }
}

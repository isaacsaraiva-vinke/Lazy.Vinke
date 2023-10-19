// TestsLazyJsonSerializerOptionsGlobal.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 09

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
    public class TestsLazyJsonSerializerOptionsGlobal
    {
        [TestMethod]
        public void Add_Type_Null_Success()
        {
            // Arrange
            LazyJsonSerializerOptionsGlobal jsonSerializerOptionsGlobal = new LazyJsonSerializerOptionsGlobal();

            // Act
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerDateTime>(null);

            // Assert
            Assert.IsFalse(jsonSerializerOptionsGlobal.Contains(null));
            Assert.AreEqual(jsonSerializerOptionsGlobal.Get(null), null);
        }

        [TestMethod]
        public void Add_Type_DateTime_Success()
        {
            // Arrange
            LazyJsonSerializerOptionsGlobal jsonSerializerOptionsGlobal = new LazyJsonSerializerOptionsGlobal();

            // Act
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerDateTime>(typeof(DateTime));

            // Assert
            Assert.IsTrue(jsonSerializerOptionsGlobal.Contains(typeof(DateTime)));
            Assert.AreEqual(jsonSerializerOptionsGlobal.Get(typeof(DateTime)), typeof(LazyJsonSerializerDateTime));
        }

        [TestMethod]
        public void Add_Type_Integer_Success()
        {
            // Arrange
            LazyJsonSerializerOptionsGlobal jsonSerializerOptionsGlobal = new LazyJsonSerializerOptionsGlobal();

            // Act
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerInteger>(typeof(Int32));
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerInteger>(typeof(Int16));
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerInteger>(typeof(Int64));

            // Assert
            Assert.IsTrue(jsonSerializerOptionsGlobal.Contains(typeof(Int32)));
            Assert.AreEqual(jsonSerializerOptionsGlobal.Get(typeof(Int32)), typeof(LazyJsonSerializerInteger));
            Assert.IsTrue(jsonSerializerOptionsGlobal.Contains(typeof(Int16)));
            Assert.AreEqual(jsonSerializerOptionsGlobal.Get(typeof(Int16)), typeof(LazyJsonSerializerInteger));
            Assert.IsTrue(jsonSerializerOptionsGlobal.Contains(typeof(Int64)));
            Assert.AreEqual(jsonSerializerOptionsGlobal.Get(typeof(Int64)), typeof(LazyJsonSerializerInteger));
        }

        [TestMethod]
        public void Remove_Type_Null_Success()
        {
            // Arrange
            LazyJsonSerializerOptionsGlobal jsonSerializerOptionsGlobal = new LazyJsonSerializerOptionsGlobal();
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerDecimal>(typeof(Double));

            // Act
            jsonSerializerOptionsGlobal.Remove(null);

            // Assert
            Assert.IsTrue(jsonSerializerOptionsGlobal.Contains(typeof(Double)));
            Assert.AreEqual(jsonSerializerOptionsGlobal.Get(typeof(Double)), typeof(LazyJsonSerializerDecimal));
        }

        [TestMethod]
        public void Remove_Type_Decimal_Success()
        {
            // Arrange
            LazyJsonSerializerOptionsGlobal jsonSerializerOptionsGlobal = new LazyJsonSerializerOptionsGlobal();
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerDecimal>(typeof(Decimal));
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerDecimal>(typeof(Double));

            // Act
            jsonSerializerOptionsGlobal.Remove(typeof(Decimal));

            // Assert
            Assert.IsFalse(jsonSerializerOptionsGlobal.Contains(typeof(Decimal)));
            Assert.IsTrue(jsonSerializerOptionsGlobal.Contains(typeof(Double)));
            Assert.AreEqual(jsonSerializerOptionsGlobal.Get(typeof(Double)), typeof(LazyJsonSerializerDecimal));
        }
    }
}

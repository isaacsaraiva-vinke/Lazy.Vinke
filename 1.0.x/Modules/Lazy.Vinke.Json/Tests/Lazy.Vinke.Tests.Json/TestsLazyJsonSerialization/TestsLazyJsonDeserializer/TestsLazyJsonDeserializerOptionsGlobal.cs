// TestsLazyJsonDeserializerOptionsGlobal.cs
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
    public class TestsLazyJsonDeserializerOptionsGlobal
    {
        [TestMethod]
        public void Add_Type_Null_Success()
        {
            // Arrange
            LazyJsonDeserializerOptionsGlobal jsonDeserializerOptionsGlobal = new LazyJsonDeserializerOptionsGlobal();

            // Act
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerDateTime>(null);

            // Assert
            Assert.IsFalse(jsonDeserializerOptionsGlobal.Contains(null));
            Assert.AreEqual(jsonDeserializerOptionsGlobal.Get(null), null);
        }

        [TestMethod]
        public void Add_Type_DateTime_Success()
        {
            // Arrange
            LazyJsonDeserializerOptionsGlobal jsonDeserializerOptionsGlobal = new LazyJsonDeserializerOptionsGlobal();

            // Act
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerDateTime>(typeof(DateTime));

            // Assert
            Assert.IsTrue(jsonDeserializerOptionsGlobal.Contains(typeof(DateTime)));
            Assert.AreEqual(jsonDeserializerOptionsGlobal.Get(typeof(DateTime)), typeof(LazyJsonDeserializerDateTime));
        }

        [TestMethod]
        public void Add_Type_Integer_Success()
        {
            // Arrange
            LazyJsonDeserializerOptionsGlobal jsonDeserializerOptionsGlobal = new LazyJsonDeserializerOptionsGlobal();

            // Act
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerInteger>(typeof(Int32));
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerInteger>(typeof(Int16));
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerInteger>(typeof(Int64));

            // Assert
            Assert.IsTrue(jsonDeserializerOptionsGlobal.Contains(typeof(Int32)));
            Assert.AreEqual(jsonDeserializerOptionsGlobal.Get(typeof(Int32)), typeof(LazyJsonDeserializerInteger));
            Assert.IsTrue(jsonDeserializerOptionsGlobal.Contains(typeof(Int16)));
            Assert.AreEqual(jsonDeserializerOptionsGlobal.Get(typeof(Int16)), typeof(LazyJsonDeserializerInteger));
            Assert.IsTrue(jsonDeserializerOptionsGlobal.Contains(typeof(Int64)));
            Assert.AreEqual(jsonDeserializerOptionsGlobal.Get(typeof(Int64)), typeof(LazyJsonDeserializerInteger));
        }

        [TestMethod]
        public void Remove_Type_Null_Success()
        {
            // Arrange
            LazyJsonDeserializerOptionsGlobal jsonDeserializerOptionsGlobal = new LazyJsonDeserializerOptionsGlobal();
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerDecimal>(typeof(Double));

            // Act
            jsonDeserializerOptionsGlobal.Remove(null);

            // Assert
            Assert.IsTrue(jsonDeserializerOptionsGlobal.Contains(typeof(Double)));
            Assert.AreEqual(jsonDeserializerOptionsGlobal.Get(typeof(Double)), typeof(LazyJsonDeserializerDecimal));
        }

        [TestMethod]
        public void Remove_Type_Decimal_Success()
        {
            // Arrange
            LazyJsonDeserializerOptionsGlobal jsonDeserializerOptionsGlobal = new LazyJsonDeserializerOptionsGlobal();
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerDecimal>(typeof(Decimal));
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerDecimal>(typeof(Double));

            // Act
            jsonDeserializerOptionsGlobal.Remove(typeof(Decimal));

            // Assert
            Assert.IsFalse(jsonDeserializerOptionsGlobal.Contains(typeof(Decimal)));
            Assert.IsTrue(jsonDeserializerOptionsGlobal.Contains(typeof(Double)));
            Assert.AreEqual(jsonDeserializerOptionsGlobal.Get(typeof(Double)), typeof(LazyJsonDeserializerDecimal));
        }
    }
}

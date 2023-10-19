// TestsLazyJsonDeserializerOptionsDataTable.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 12

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
    public class TestsLazyJsonDeserializerOptionsDataTable
    {
        [TestMethod]
        public void Set_ColumnData_Single_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();

            // Act
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["SomeTable"].Columns["Id"].Set(new LazyJsonDeserializerInteger());
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["SomeTable"].Columns["Code"].Set(new LazyJsonDeserializerString());
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["SomeOtherTable"].Columns["Amount"].Set(new LazyJsonDeserializerDecimal());

            // Assert
            Assert.AreEqual(jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["SomeTable"].Columns["Id"].Deserializer.GetType(), typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["SomeTable"].Columns["Code"].Deserializer.GetType(), typeof(LazyJsonDeserializerString));
            Assert.AreEqual(jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>()["SomeOtherTable"].Columns["Amount"].Deserializer.GetType(), typeof(LazyJsonDeserializerDecimal));
        }
    }
}

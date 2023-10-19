// TestsLazyJsonSerializerOptionsDataTable.cs
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
    public class TestsLazyJsonSerializerOptionsDataTable
    {
        [TestMethod]
        public void Set_ColumnData_Single_Success()
        {
            // Arrange
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();

            // Act
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsDataTable>()["SomeTable"].Columns["Id"].Set(new LazyJsonSerializerInteger());
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsDataTable>()["SomeTable"].Columns["Code"].Set(new LazyJsonSerializerString());
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsDataTable>()["SomeOtherTable"].Columns["Amount"].Set(new LazyJsonSerializerDecimal());

            // Assert
            Assert.AreEqual(jsonSerializerOptions.Item<LazyJsonSerializerOptionsDataTable>()["SomeTable"].Columns["Id"].Serializer.GetType(), typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerOptions.Item<LazyJsonSerializerOptionsDataTable>()["SomeTable"].Columns["Code"].Serializer.GetType(), typeof(LazyJsonSerializerString));
            Assert.AreEqual(jsonSerializerOptions.Item<LazyJsonSerializerOptionsDataTable>()["SomeOtherTable"].Columns["Amount"].Serializer.GetType(), typeof(LazyJsonSerializerDecimal));
        }
    }
}

// TestsLazyJsonSerializerDataSet.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 16

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
    public class TestsLazyJsonSerializerDataSet
    {
        [TestMethod]
        public void Serialize_Empty_Single_Success()
        {
            // Arrange
            DataSet dataSet = new DataSet("NewDataSet");

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataSet().Serialize(dataSet);

            // Assert
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonToken)["Name"].Token).Value, "NewDataSet");
            Assert.AreEqual(((LazyJsonArray)((LazyJsonObject)jsonToken)["Tables"].Token).Length, 0);
        }

        [TestMethod]
        public void Serialize_DefaultTypes_TwoTables_Success()
        {
            // Arrange
            DataTable dataTableX = new DataTable("DataTableX");
            DataTable dataTableY = new DataTable("DataTableY");
            DataSet dataSet = new DataSet("NewDataSet");
            dataSet.Tables.Add(dataTableX);
            dataSet.Tables.Add(dataTableY);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataSet().Serialize(dataSet);

            // Assert
            LazyJsonObject jsonObjectDataSet = (LazyJsonObject)jsonToken;
            LazyJsonArray jsonArrayDataTables = (LazyJsonArray)jsonObjectDataSet["Tables"].Token;
            LazyJsonObject jsonObjectDataTableXValue = (LazyJsonObject)((LazyJsonObject)jsonArrayDataTables[0])["Value"].Token;
            LazyJsonObject jsonObjectDataTableYValue = (LazyJsonObject)((LazyJsonObject)jsonArrayDataTables[1])["Value"].Token;

            Assert.AreEqual(((LazyJsonString)jsonObjectDataSet["Name"].Token).Value, "NewDataSet");
            Assert.AreEqual(jsonArrayDataTables.Length, 2);
            Assert.IsNull(((LazyJsonObject)jsonArrayDataTables[0])["Type"]);
            Assert.IsNull(((LazyJsonObject)jsonArrayDataTables[1])["Type"]);
            Assert.AreEqual(((LazyJsonString)jsonObjectDataTableXValue["Name"].Token).Value, "DataTableX");
            Assert.AreEqual(((LazyJsonString)jsonObjectDataTableYValue["Name"].Token).Value, "DataTableY");
        }

        [TestMethod]
        public void Serialize_InheritTypes_TwoTables_Success()
        {
            // Arrange
            DataTable dataTableX = new TestsSamplesLazyJsonSerializerDataTableSimple("DataTableX");
            DataTable dataTableY = new TestsSamplesLazyJsonSerializerDataTableSimple("DataTableY");
            DataSet dataSet = new TestsSamplesLazyJsonSerializerDataSetSimple("NewDataSet");
            dataSet.Tables.Add(dataTableX);
            dataSet.Tables.Add(dataTableY);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataSet().Serialize(dataSet);

            // Assert
            LazyJsonObject jsonObjectDataSet = (LazyJsonObject)jsonToken;
            LazyJsonArray jsonArrayDataTables = (LazyJsonArray)jsonObjectDataSet["Tables"].Token;
            LazyJsonObject jsonObjectDataTableX = (LazyJsonObject)jsonArrayDataTables[0];
            LazyJsonObject jsonObjectDataTableY = (LazyJsonObject)jsonArrayDataTables[1];

            Assert.AreEqual(((LazyJsonString)jsonObjectDataSet["Name"].Token).Value, "NewDataSet");
            Assert.AreEqual(jsonArrayDataTables.Length, 2);
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectDataTableX["Type"].Token)["Assembly"].Token).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectDataTableX["Type"].Token)["Namespace"].Token).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectDataTableX["Type"].Token)["Class"].Token).Value, "TestsSamplesLazyJsonSerializerDataTableSimple");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectDataTableX["Value"].Token)["Name"].Token).Value, "DataTableX");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectDataTableY["Type"].Token)["Assembly"].Token).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectDataTableY["Type"].Token)["Namespace"].Token).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectDataTableY["Type"].Token)["Class"].Token).Value, "TestsSamplesLazyJsonSerializerDataTableSimple");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectDataTableY["Value"].Token)["Name"].Token).Value, "DataTableY");
        }
    }
}

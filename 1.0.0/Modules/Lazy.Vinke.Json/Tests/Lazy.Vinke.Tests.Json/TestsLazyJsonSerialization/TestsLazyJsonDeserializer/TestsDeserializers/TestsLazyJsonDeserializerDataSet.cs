// TestsLazyJsonDeserializerDataSet.cs
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
    public class TestsLazyJsonDeserializerDataSet
    {
        [TestMethod]
        public void Deserialize_Empty_Single_Success()
        {
            // Arrange
            LazyJsonObject jsonToken = new LazyJsonObject();
            jsonToken.Add(new LazyJsonProperty("Name", new LazyJsonString("NewDataSet")));
            jsonToken.Add(new LazyJsonProperty("Tables", new LazyJsonArray()));

            // Act
            Object data = new LazyJsonDeserializerDataSet().Deserialize(jsonToken, typeof(DataSet));

            // Assert
            Assert.AreEqual(((DataSet)data).DataSetName, "NewDataSet");
            Assert.AreEqual(((DataSet)data).Tables.Count, 0);
        }

        [TestMethod]
        public void Deserialize_DefaultTypes_TwoTables_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableX = new LazyJsonObject();
            jsonObjectDataTableX.Add(new LazyJsonProperty("Name", new LazyJsonString("DataTableX")));

            LazyJsonObject jsonObjectDataTable0 = new LazyJsonObject();
            jsonObjectDataTable0.Add(new LazyJsonProperty("Value", jsonObjectDataTableX));

            LazyJsonObject jsonObjectDataTableY = new LazyJsonObject();
            jsonObjectDataTableY.Add(new LazyJsonProperty("Name", new LazyJsonString("DataTableY")));

            LazyJsonObject jsonObjectDataTable1 = new LazyJsonObject();
            jsonObjectDataTable1.Add(new LazyJsonProperty("Value", jsonObjectDataTableY));

            LazyJsonArray jsonArrayDataTables = new LazyJsonArray();
            jsonArrayDataTables.Add(jsonObjectDataTable0);
            jsonArrayDataTables.Add(jsonObjectDataTable1);

            LazyJsonObject jsonObjectDataSet = new LazyJsonObject();
            jsonObjectDataSet.Add(new LazyJsonProperty("Name", new LazyJsonString("NewDataSet")));
            jsonObjectDataSet.Add(new LazyJsonProperty("Tables", jsonArrayDataTables));

            // Act
            Object data = new LazyJsonDeserializerDataSet().Deserialize(jsonObjectDataSet, typeof(DataSet));

            // Assert
            Assert.AreEqual(((DataSet)data).DataSetName, "NewDataSet");
            Assert.AreEqual(((DataSet)data).Tables.Count, 2);
            Assert.AreEqual(((DataSet)data).Tables[0].TableName, "DataTableX");
            Assert.AreEqual(((DataSet)data).Tables[1].TableName, "DataTableY");
        }

        [TestMethod]
        public void Deserialize_InheritTypes_TwoTables_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectInheritDataTableType = new LazyJsonObject();
            jsonObjectInheritDataTableType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("Lazy.Vinke.Tests.Json")));
            jsonObjectInheritDataTableType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("Lazy.Vinke.Tests.Json")));
            jsonObjectInheritDataTableType.Add(new LazyJsonProperty("Class", new LazyJsonString("TestsSamplesLazyJsonDeserializerDataTableSimple")));

            LazyJsonObject jsonObjectDataTableX = new LazyJsonObject();
            jsonObjectDataTableX.Add(new LazyJsonProperty("Name", new LazyJsonString("DataTableX")));

            LazyJsonObject jsonObjectDataTable0 = new LazyJsonObject();
            jsonObjectDataTable0.Add(new LazyJsonProperty("Type", jsonObjectInheritDataTableType));
            jsonObjectDataTable0.Add(new LazyJsonProperty("Value", jsonObjectDataTableX));

            LazyJsonObject jsonObjectDataTableY = new LazyJsonObject();
            jsonObjectDataTableY.Add(new LazyJsonProperty("Name", new LazyJsonString("DataTableY")));

            LazyJsonObject jsonObjectDataTable1 = new LazyJsonObject();
            jsonObjectDataTable1.Add(new LazyJsonProperty("Type", jsonObjectInheritDataTableType));
            jsonObjectDataTable1.Add(new LazyJsonProperty("Value", jsonObjectDataTableY));

            LazyJsonArray jsonArrayDataTables = new LazyJsonArray();
            jsonArrayDataTables.Add(jsonObjectDataTable0);
            jsonArrayDataTables.Add(jsonObjectDataTable1);

            LazyJsonObject jsonObjectDataSet = new LazyJsonObject();
            jsonObjectDataSet.Add(new LazyJsonProperty("Name", new LazyJsonString("NewDataSet")));
            jsonObjectDataSet.Add(new LazyJsonProperty("Tables", jsonArrayDataTables));

            // Act
            Object data = new LazyJsonDeserializerDataSet().Deserialize(jsonObjectDataSet, typeof(TestsSamplesLazyJsonSerializerDataSetSimple));

            // Assert
            Assert.AreEqual(data.GetType(), typeof(TestsSamplesLazyJsonSerializerDataSetSimple));
            Assert.AreEqual(((DataSet)data).DataSetName, "NewDataSet");
            Assert.AreEqual(((DataSet)data).Tables.Count, 2);
            Assert.AreEqual(((DataSet)data).Tables[0].GetType(), typeof(TestsSamplesLazyJsonDeserializerDataTableSimple));
            Assert.AreEqual(((DataSet)data).Tables[0].GetType(), typeof(TestsSamplesLazyJsonDeserializerDataTableSimple));
            Assert.AreEqual(((DataSet)data).Tables[0].TableName, "DataTableX");
            Assert.AreEqual(((DataSet)data).Tables[1].TableName, "DataTableY");
        }
    }
}

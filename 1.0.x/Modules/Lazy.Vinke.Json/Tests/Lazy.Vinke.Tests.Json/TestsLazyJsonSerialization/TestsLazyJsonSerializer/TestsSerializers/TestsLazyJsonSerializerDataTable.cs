// TestsLazyJsonSerializerDataTable.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 14

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
    public class TestsLazyJsonSerializerDataTable
    {
        [TestMethod]
        public void Serialize_Type_ColumnString_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnString");
            dataTable.Columns.Add("ColumnString", typeof(String));
            dataTable.Columns.Add("ColumnStringArray", typeof(String[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnString"] = DBNull.Value;
            dataTable.Rows[0]["ColumnStringArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnString"] = "Lazy.Vinke.Tests.Json";
            dataTable.Rows[0]["ColumnStringArray"] = new String[] { "Lazy", "Vinke", "Tests", "Json" };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnString = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnString"].Token;
            LazyJsonObject jsonObjectColumnStringArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnStringArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnString");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnString["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnString["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnString["Class"].Token).Value, "String");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnStringArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnStringArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnStringArray["Class"].Token).Value, "String[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonString)jsonObjectRowValuesOriginal["ColumnString"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnStringArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonString)jsonObjectRowValuesCurrent["ColumnString"].Token).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnStringArray"].Token)[0]).Value, "Lazy");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnStringArray"].Token)[1]).Value, "Vinke");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnStringArray"].Token)[2]).Value, "Tests");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnStringArray"].Token)[3]).Value, "Json");
        }

        [TestMethod]
        public void Serialize_Type_ColumnInt32_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnInt32");
            dataTable.Columns.Add("ColumnInt32", typeof(Int32));
            dataTable.Columns.Add("ColumnInt32Array", typeof(Int32[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnInt32"] = DBNull.Value;
            dataTable.Rows[0]["ColumnInt32Array"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnInt32"] = 32;
            dataTable.Rows[0]["ColumnInt32Array"] = new Int32[] { 1, 2, 4, 8 };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnInt32 = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnInt32"].Token;
            LazyJsonObject jsonObjectColumnInt32Array = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnInt32Array"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnInt32");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt32["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt32["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt32["Class"].Token).Value, "Int32");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt32Array["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt32Array["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt32Array["Class"].Token).Value, "Int32[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesOriginal["ColumnInt32"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnInt32Array"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesCurrent["ColumnInt32"].Token).Value, 32);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt32Array"].Token)[0]).Value, 1);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt32Array"].Token)[1]).Value, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt32Array"].Token)[2]).Value, 4);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt32Array"].Token)[3]).Value, 8);
        }

        [TestMethod]
        public void Serialize_Type_ColumnDecimal_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnDecimal");
            dataTable.Columns.Add("ColumnDecimal", typeof(Decimal));
            dataTable.Columns.Add("ColumnDecimalArray", typeof(Decimal[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnDecimal"] = DBNull.Value;
            dataTable.Rows[0]["ColumnDecimalArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnDecimal"] = 101.101m;
            dataTable.Rows[0]["ColumnDecimalArray"] = new Decimal[] { 1.1m, 101.101m, -1.1m, -101.101m };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnDecimal = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnDecimal"].Token;
            LazyJsonObject jsonObjectColumnDecimalArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnDecimalArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnDecimal");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDecimal["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDecimal["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDecimal["Class"].Token).Value, "Decimal");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDecimalArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDecimalArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDecimalArray["Class"].Token).Value, "Decimal[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonDecimal)jsonObjectRowValuesOriginal["ColumnDecimal"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnDecimalArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonDecimal)jsonObjectRowValuesCurrent["ColumnDecimal"].Token).Value, 101.101m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDecimalArray"].Token)[0]).Value, 1.1m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDecimalArray"].Token)[1]).Value, 101.101m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDecimalArray"].Token)[2]).Value, -1.1m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDecimalArray"].Token)[3]).Value, -101.101m);
        }

        [TestMethod]
        public void Serialize_Type_ColumnDateTime_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnDateTime");
            dataTable.Columns.Add("ColumnDateTime", typeof(DateTime));
            dataTable.Columns.Add("ColumnDateTimeArray", typeof(DateTime[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnDateTime"] = DBNull.Value;
            dataTable.Rows[0]["ColumnDateTimeArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnDateTime"] = new DateTime(2023, 10, 16, 15, 23, 30);
            dataTable.Rows[0]["ColumnDateTimeArray"] = new DateTime[] { new DateTime(2023, 10, 15, 15, 23, 30), new DateTime(2023, 10, 17, 15, 23, 30) };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnDateTime = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnDateTime"].Token;
            LazyJsonObject jsonObjectColumnDateTimeArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnDateTimeArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnDateTime");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDateTime["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDateTime["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDateTime["Class"].Token).Value, "DateTime");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDateTimeArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDateTimeArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDateTimeArray["Class"].Token).Value, "DateTime[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonString)jsonObjectRowValuesOriginal["ColumnDateTime"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnDateTimeArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonString)jsonObjectRowValuesCurrent["ColumnDateTime"].Token).Value, "2023-10-16T15:23:30:000Z");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDateTimeArray"].Token)[0]).Value, "2023-10-15T15:23:30:000Z");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDateTimeArray"].Token)[1]).Value, "2023-10-17T15:23:30:000Z");
        }

        [TestMethod]
        public void Serialize_Type_ColumnBoolean_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnBoolean");
            dataTable.Columns.Add("ColumnBoolean", typeof(Boolean));
            dataTable.Columns.Add("ColumnBooleanArray", typeof(Boolean[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnBoolean"] = DBNull.Value;
            dataTable.Rows[0]["ColumnBooleanArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnBoolean"] = true;
            dataTable.Rows[0]["ColumnBooleanArray"] = new Boolean[] { false, true };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnBoolean = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnBoolean"].Token;
            LazyJsonObject jsonObjectColumnBooleanArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnBooleanArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnBoolean");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnBoolean["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnBoolean["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnBoolean["Class"].Token).Value, "Boolean");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnBooleanArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnBooleanArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnBooleanArray["Class"].Token).Value, "Boolean[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonBoolean)jsonObjectRowValuesOriginal["ColumnBoolean"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnBooleanArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonBoolean)jsonObjectRowValuesCurrent["ColumnBoolean"].Token).Value, true);
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnBooleanArray"].Token)[0]).Value, false);
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnBooleanArray"].Token)[1]).Value, true);
        }

        [TestMethod]
        public void Serialize_Type_ColumnInt16_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnInt16");
            dataTable.Columns.Add("ColumnInt16", typeof(Int16));
            dataTable.Columns.Add("ColumnInt16Array", typeof(Int16[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnInt16"] = DBNull.Value;
            dataTable.Rows[0]["ColumnInt16Array"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnInt16"] = 16;
            dataTable.Rows[0]["ColumnInt16Array"] = new Int16[] { 1, 2, 4, 8 };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnInt16 = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnInt16"].Token;
            LazyJsonObject jsonObjectColumnInt16Array = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnInt16Array"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnInt16");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt16["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt16["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt16["Class"].Token).Value, "Int16");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt16Array["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt16Array["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt16Array["Class"].Token).Value, "Int16[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesOriginal["ColumnInt16"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnInt16Array"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesCurrent["ColumnInt16"].Token).Value, (Int16)16);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt16Array"].Token)[0]).Value, (Int16)1);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt16Array"].Token)[1]).Value, (Int16)2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt16Array"].Token)[2]).Value, (Int16)4);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt16Array"].Token)[3]).Value, (Int16)8);
        }

        [TestMethod]
        public void Serialize_Type_ColumnInt64_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnInt64");
            dataTable.Columns.Add("ColumnInt64", typeof(Int64));
            dataTable.Columns.Add("ColumnInt64Array", typeof(Int64[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnInt64"] = DBNull.Value;
            dataTable.Rows[0]["ColumnInt64Array"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnInt64"] = 64;
            dataTable.Rows[0]["ColumnInt64Array"] = new Int64[] { 1024, 2048, 4098, 8192 };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnInt64 = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnInt64"].Token;
            LazyJsonObject jsonObjectColumnInt64Array = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnInt64Array"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnInt64");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt64["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt64["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt64["Class"].Token).Value, "Int64");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt64Array["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt64Array["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnInt64Array["Class"].Token).Value, "Int64[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesOriginal["ColumnInt64"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnInt64Array"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesCurrent["ColumnInt64"].Token).Value, (Int64)64);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt64Array"].Token)[0]).Value, (Int64)1024);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt64Array"].Token)[1]).Value, (Int64)2048);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt64Array"].Token)[2]).Value, (Int64)4098);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnInt64Array"].Token)[3]).Value, (Int64)8192);
        }

        [TestMethod]
        public void Serialize_Type_ColumnDouble_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnDouble");
            dataTable.Columns.Add("ColumnDouble", typeof(Double));
            dataTable.Columns.Add("ColumnDoubleArray", typeof(Double[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnDouble"] = DBNull.Value;
            dataTable.Rows[0]["ColumnDoubleArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnDouble"] = 101.101d;
            dataTable.Rows[0]["ColumnDoubleArray"] = new Double[] { 1.1d, 101.101d, -1.1d, -101.101d };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnDouble = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnDouble"].Token;
            LazyJsonObject jsonObjectColumnDoubleArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnDoubleArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnDouble");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDouble["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDouble["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDouble["Class"].Token).Value, "Double");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDoubleArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDoubleArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDoubleArray["Class"].Token).Value, "Double[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonDecimal)jsonObjectRowValuesOriginal["ColumnDouble"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnDoubleArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonDecimal)jsonObjectRowValuesCurrent["ColumnDouble"].Token).Value, 101.101m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDoubleArray"].Token)[0]).Value, 1.1m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDoubleArray"].Token)[1]).Value, 101.101m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDoubleArray"].Token)[2]).Value, -1.1m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDoubleArray"].Token)[3]).Value, -101.101m);
        }

        [TestMethod]
        public void Serialize_Type_ColumnSingle_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnSingle");
            dataTable.Columns.Add("ColumnSingle", typeof(Single));
            dataTable.Columns.Add("ColumnSingleArray", typeof(Single[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnSingle"] = DBNull.Value;
            dataTable.Rows[0]["ColumnSingleArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnSingle"] = 101.101f;
            dataTable.Rows[0]["ColumnSingleArray"] = new Single[] { 1.1f, 101.101f, -1.1f, -101.101f };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnSingle = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnSingle"].Token;
            LazyJsonObject jsonObjectColumnSingleArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnSingleArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnSingle");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSingle["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSingle["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSingle["Class"].Token).Value, "Single");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSingleArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSingleArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSingleArray["Class"].Token).Value, "Single[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonDecimal)jsonObjectRowValuesOriginal["ColumnSingle"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnSingleArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonDecimal)jsonObjectRowValuesCurrent["ColumnSingle"].Token).Value, 101.101m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnSingleArray"].Token)[0]).Value, 1.1m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnSingleArray"].Token)[1]).Value, 101.101m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnSingleArray"].Token)[2]).Value, -1.1m);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnSingleArray"].Token)[3]).Value, -101.101m);
        }

        [TestMethod]
        public void Serialize_Type_ColumnChar_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnChar");
            dataTable.Columns.Add("ColumnChar", typeof(Char));
            dataTable.Columns.Add("ColumnCharArray", typeof(Char[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnChar"] = DBNull.Value;
            dataTable.Rows[0]["ColumnCharArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnChar"] = 'J';
            dataTable.Rows[0]["ColumnCharArray"] = new Char[] { 'J', 'S', 'O', 'N' };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnChar = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnChar"].Token;
            LazyJsonObject jsonObjectColumnCharArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnCharArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnChar");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnChar["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnChar["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnChar["Class"].Token).Value, "Char");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnCharArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnCharArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnCharArray["Class"].Token).Value, "Char[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonString)jsonObjectRowValuesOriginal["ColumnChar"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnCharArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonString)jsonObjectRowValuesCurrent["ColumnChar"].Token).Value, "J");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnCharArray"].Token)[0]).Value, "J");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnCharArray"].Token)[1]).Value, "S");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnCharArray"].Token)[2]).Value, "O");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnCharArray"].Token)[3]).Value, "N");
        }

        [TestMethod]
        public void Serialize_Type_ColumnByte_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnByte");
            dataTable.Columns.Add("ColumnByte", typeof(Byte));
            dataTable.Columns.Add("ColumnByteArray", typeof(Byte[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnByte"] = DBNull.Value;
            dataTable.Rows[0]["ColumnByteArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnByte"] = 64;
            dataTable.Rows[0]["ColumnByteArray"] = new Byte[] { 8, 16, 24, 32 };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnByte = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnByte"].Token;
            LazyJsonObject jsonObjectColumnByteArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnByteArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnByte");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnByte["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnByte["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnByte["Class"].Token).Value, "Byte");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnByteArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnByteArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnByteArray["Class"].Token).Value, "Byte[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesOriginal["ColumnByte"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnByteArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesCurrent["ColumnByte"].Token).Value, (Byte)64);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnByteArray"].Token)[0]).Value, (Byte)8);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnByteArray"].Token)[1]).Value, (Byte)16);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnByteArray"].Token)[2]).Value, (Byte)24);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnByteArray"].Token)[3]).Value, (Byte)32);
        }

        [TestMethod]
        public void Serialize_Type_ColumnSByte_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnSByte");
            dataTable.Columns.Add("ColumnSByte", typeof(SByte));
            dataTable.Columns.Add("ColumnSByteArray", typeof(SByte[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnSByte"] = DBNull.Value;
            dataTable.Rows[0]["ColumnSByteArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnSByte"] = -64;
            dataTable.Rows[0]["ColumnSByteArray"] = new SByte[] { -8, -16, -24, -32 };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnSByte = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnSByte"].Token;
            LazyJsonObject jsonObjectColumnSByteArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnSByteArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnSByte");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSByte["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSByte["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSByte["Class"].Token).Value, "SByte");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSByteArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSByteArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnSByteArray["Class"].Token).Value, "SByte[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesOriginal["ColumnSByte"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnSByteArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesCurrent["ColumnSByte"].Token).Value, (SByte)(-64));
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnSByteArray"].Token)[0]).Value, (SByte)(-8));
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnSByteArray"].Token)[1]).Value, (SByte)(-16));
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnSByteArray"].Token)[2]).Value, (SByte)(-24));
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnSByteArray"].Token)[3]).Value, (SByte)(-32));
        }

        [TestMethod]
        public void Serialize_Type_ColumnUInt32_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnUInt32");
            dataTable.Columns.Add("ColumnUInt32", typeof(UInt32));
            dataTable.Columns.Add("ColumnUInt32Array", typeof(UInt32[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnUInt32"] = DBNull.Value;
            dataTable.Rows[0]["ColumnUInt32Array"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnUInt32"] = 64;
            dataTable.Rows[0]["ColumnUInt32Array"] = new UInt32[] { 32, 64, 128, 256 };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnUInt32 = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnUInt32"].Token;
            LazyJsonObject jsonObjectColumnUInt32Array = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnUInt32Array"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnUInt32");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt32["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt32["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt32["Class"].Token).Value, "UInt32");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt32Array["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt32Array["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt32Array["Class"].Token).Value, "UInt32[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesOriginal["ColumnUInt32"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnUInt32Array"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesCurrent["ColumnUInt32"].Token).Value, (UInt32)64);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnUInt32Array"].Token)[0]).Value, (UInt32)32);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnUInt32Array"].Token)[1]).Value, (UInt32)64);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnUInt32Array"].Token)[2]).Value, (UInt32)128);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnUInt32Array"].Token)[3]).Value, (UInt32)256);
        }

        [TestMethod]
        public void Serialize_Type_ColumnUInt16_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnUInt16");
            dataTable.Columns.Add("ColumnUInt16", typeof(UInt16));
            dataTable.Columns.Add("ColumnUInt16Array", typeof(UInt16[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnUInt16"] = DBNull.Value;
            dataTable.Rows[0]["ColumnUInt16Array"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnUInt16"] = 64;
            dataTable.Rows[0]["ColumnUInt16Array"] = new UInt16[] { 16, 24, 32, 48 };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnUInt16 = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnUInt16"].Token;
            LazyJsonObject jsonObjectColumnUInt16Array = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnUInt16Array"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnUInt16");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt16["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt16["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt16["Class"].Token).Value, "UInt16");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt16Array["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt16Array["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnUInt16Array["Class"].Token).Value, "UInt16[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesOriginal["ColumnUInt16"].Token).Value, null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnUInt16Array"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonInteger)jsonObjectRowValuesCurrent["ColumnUInt16"].Token).Value, (UInt16)64);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnUInt16Array"].Token)[0]).Value, (UInt16)16);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnUInt16Array"].Token)[1]).Value, (UInt16)24);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnUInt16Array"].Token)[2]).Value, (UInt16)32);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnUInt16Array"].Token)[3]).Value, (UInt16)48);
        }

        [TestMethod]
        public void Serialize_Type_ColumnDataTable_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnDataTable");
            dataTable.Columns.Add("ColumnDataTable", typeof(DataTable));
            dataTable.Columns.Add("ColumnDataTableArray", typeof(DataTable[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnDataTable"] = DBNull.Value;
            dataTable.Rows[0]["ColumnDataTableArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            DataTable dataTableIntegerCol = new DataTable("EmptyDataTable");
            dataTableIntegerCol.Columns.Add("ColumnInt32", typeof(Int32));

            dataTable.Rows[0]["ColumnDataTable"] = dataTableIntegerCol;
            dataTable.Rows[0]["ColumnDataTableArray"] = new DataTable[] { dataTableIntegerCol, dataTableIntegerCol };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnDataTable = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnDataTable"].Token;
            LazyJsonObject jsonObjectColumnDataTableArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnDataTableArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnDataTable");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDataTable["Assembly"].Token).Value, "System.Data.Common");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDataTable["Namespace"].Token).Value, "System.Data");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDataTable["Class"].Token).Value, "DataTable");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDataTableArray["Assembly"].Token).Value, "System.Data.Common");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDataTableArray["Namespace"].Token).Value, "System.Data");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnDataTableArray["Class"].Token).Value, "DataTable[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnDataTable"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnDataTableArray"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectRowValuesCurrent["ColumnDataTable"].Token)["Name"].Token).Value, "EmptyDataTable");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonObject)jsonObjectRowValuesCurrent["ColumnDataTable"].Token)["Columns"].Token)["ColumnInt32"].Token)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonObject)jsonObjectRowValuesCurrent["ColumnDataTable"].Token)["Columns"].Token)["ColumnInt32"].Token)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonObject)jsonObjectRowValuesCurrent["ColumnDataTable"].Token)["Columns"].Token)["ColumnInt32"].Token)["Class"].Token).Value, "Int32");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDataTableArray"].Token)[0])["Name"].Token).Value, "EmptyDataTable");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDataTableArray"].Token)[1])["Name"].Token).Value, "EmptyDataTable");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDataTableArray"].Token)[0])["Columns"].Token)["ColumnInt32"].Token)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDataTableArray"].Token)[0])["Columns"].Token)["ColumnInt32"].Token)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDataTableArray"].Token)[0])["Columns"].Token)["ColumnInt32"].Token)["Class"].Token).Value, "Int32");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDataTableArray"].Token)[1])["Columns"].Token)["ColumnInt32"].Token)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDataTableArray"].Token)[1])["Columns"].Token)["ColumnInt32"].Token)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnDataTableArray"].Token)[1])["Columns"].Token)["ColumnInt32"].Token)["Class"].Token).Value, "Int32");
        }

        [TestMethod]
        public void Serialize_Type_ColumnType_Success()
        {
            // Arrange
            DataTable dataTable = new DataTable("Type_ColumnType");
            dataTable.Columns.Add("ColumnType", typeof(Type));
            dataTable.Columns.Add("ColumnTypeArray", typeof(Type[]));

            dataTable.Rows.Add(dataTable.NewRow());

            dataTable.Rows[0]["ColumnType"] = DBNull.Value;
            dataTable.Rows[0]["ColumnTypeArray"] = DBNull.Value;

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnType"] = typeof(LazyJsonArray);
            dataTable.Rows[0]["ColumnTypeArray"] = new Type[] { typeof(String), typeof(Type) };

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerDataTable().Serialize(dataTable);

            // Assert
            LazyJsonProperty jsonPropertyName = ((LazyJsonObject)jsonToken)["Name"];
            LazyJsonProperty jsonPropertyColumns = ((LazyJsonObject)jsonToken)["Columns"];
            LazyJsonObject jsonObjectColumnType = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnType"].Token;
            LazyJsonObject jsonObjectColumnTypeArray = (LazyJsonObject)((LazyJsonObject)jsonPropertyColumns.Token)["ColumnTypeArray"].Token;
            LazyJsonProperty jsonPropertyRow = ((LazyJsonObject)jsonToken)["Rows"];
            LazyJsonProperty jsonPropertyRowState = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["State"];
            LazyJsonProperty jsonPropertyRowValues = ((LazyJsonObject)((LazyJsonArray)jsonPropertyRow.Token)[0])["Values"];
            LazyJsonProperty jsonPropertyRowValuesOriginal = ((LazyJsonObject)jsonPropertyRowValues.Token)["Original"];
            LazyJsonProperty jsonPropertyRowValuesCurrent = ((LazyJsonObject)jsonPropertyRowValues.Token)["Current"];
            LazyJsonObject jsonObjectRowValuesOriginal = (LazyJsonObject)jsonPropertyRowValuesOriginal.Token;
            LazyJsonObject jsonObjectRowValuesCurrent = (LazyJsonObject)jsonPropertyRowValuesCurrent.Token;

            Assert.AreEqual(((LazyJsonString)jsonPropertyName.Token).Value, "Type_ColumnType");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnType["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnType["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnType["Class"].Token).Value, "Type");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnTypeArray["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnTypeArray["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectColumnTypeArray["Class"].Token).Value, "Type[]");
            Assert.AreEqual(((LazyJsonString)jsonPropertyRowState.Token).Value, "Modified");
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnType"].Token.Type, LazyJsonType.Null);
            Assert.AreEqual(jsonObjectRowValuesOriginal["ColumnTypeArray"].Token.Type, LazyJsonType.Null);

            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectRowValuesCurrent["ColumnType"].Token)["Assembly"].Token).Value, "Lazy.Vinke.Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectRowValuesCurrent["ColumnType"].Token)["Namespace"].Token).Value, "Lazy.Vinke.Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObjectRowValuesCurrent["ColumnType"].Token)["Class"].Token).Value, "LazyJsonArray");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnTypeArray"].Token)[0])["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnTypeArray"].Token)[0])["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnTypeArray"].Token)[0])["Class"].Token).Value, "String");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnTypeArray"].Token)[1])["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnTypeArray"].Token)[1])["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonArray)jsonObjectRowValuesCurrent["ColumnTypeArray"].Token)[1])["Class"].Token).Value, "Type");
        }
    }
}

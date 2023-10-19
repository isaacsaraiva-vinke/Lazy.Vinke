// TestsLazyJsonDeserializerDataTable.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 15

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
    public class TestsLazyJsonDeserializerDataTable
    {
        [TestMethod]
        public void Deserialize_Type_ColumnString_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnString", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnStringArray", new LazyJsonNull()));

            LazyJsonString jsonCurrentColumnString = new LazyJsonString("Lazy.Vinke.Tests.Json");
            LazyJsonArray jsonCurrentColumnStringArray = new LazyJsonArray();
            jsonCurrentColumnStringArray.Add(new LazyJsonString("Lazy"));
            jsonCurrentColumnStringArray.Add(new LazyJsonString("Vinke"));
            jsonCurrentColumnStringArray.Add(new LazyJsonString("Tests"));
            jsonCurrentColumnStringArray.Add(new LazyJsonString("Json"));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnString", jsonCurrentColumnString));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnStringArray", jsonCurrentColumnStringArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectString = new LazyJsonObject();
            jsonObjectString.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectString.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectString.Add(new LazyJsonProperty("Class", new LazyJsonString("String")));
            LazyJsonObject jsonObjectStringArray = new LazyJsonObject();
            jsonObjectStringArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectStringArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectStringArray.Add(new LazyJsonProperty("Class", new LazyJsonString("String[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnString", jsonObjectString));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnStringArray", jsonObjectStringArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnString")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnString");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnString"].DataType, typeof(String));
            Assert.AreEqual(dataTable.Columns["ColumnStringArray"].DataType, typeof(String[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnString"], "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((String[])dataTable.Rows[0]["ColumnStringArray"])[0], "Lazy");
            Assert.AreEqual(((String[])dataTable.Rows[0]["ColumnStringArray"])[1], "Vinke");
            Assert.AreEqual(((String[])dataTable.Rows[0]["ColumnStringArray"])[2], "Tests");
            Assert.AreEqual(((String[])dataTable.Rows[0]["ColumnStringArray"])[3], "Json");
        }

        [TestMethod]
        public void Deserialize_Type_ColumnInt32_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnInt32", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnInt32Array", new LazyJsonNull()));

            LazyJsonInteger jsonCurrentColumnInt32 = new LazyJsonInteger(101);
            LazyJsonArray jsonCurrentColumnInt32Array = new LazyJsonArray();
            jsonCurrentColumnInt32Array.Add(new LazyJsonInteger(1));
            jsonCurrentColumnInt32Array.Add(new LazyJsonInteger(2));
            jsonCurrentColumnInt32Array.Add(new LazyJsonInteger(4));
            jsonCurrentColumnInt32Array.Add(new LazyJsonInteger(8));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnInt32", jsonCurrentColumnInt32));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnInt32Array", jsonCurrentColumnInt32Array));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectInt32 = new LazyJsonObject();
            jsonObjectInt32.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectInt32.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectInt32.Add(new LazyJsonProperty("Class", new LazyJsonString("Int32")));
            LazyJsonObject jsonObjectInt32Array = new LazyJsonObject();
            jsonObjectInt32Array.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectInt32Array.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectInt32Array.Add(new LazyJsonProperty("Class", new LazyJsonString("Int32[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnInt32", jsonObjectInt32));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnInt32Array", jsonObjectInt32Array));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnInt32")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnInt32");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnInt32"].DataType, typeof(Int32));
            Assert.AreEqual(dataTable.Columns["ColumnInt32Array"].DataType, typeof(Int32[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnInt32"], 101);
            Assert.AreEqual(((Int32[])dataTable.Rows[0]["ColumnInt32Array"])[0], 1);
            Assert.AreEqual(((Int32[])dataTable.Rows[0]["ColumnInt32Array"])[1], 2);
            Assert.AreEqual(((Int32[])dataTable.Rows[0]["ColumnInt32Array"])[2], 4);
            Assert.AreEqual(((Int32[])dataTable.Rows[0]["ColumnInt32Array"])[3], 8);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnDecimal_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnDecimal", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnDecimalArray", new LazyJsonNull()));

            LazyJsonDecimal jsonCurrentColumnDecimal = new LazyJsonDecimal(101.101m);
            LazyJsonArray jsonCurrentColumnDecimalArray = new LazyJsonArray();
            jsonCurrentColumnDecimalArray.Add(new LazyJsonDecimal(1.1m));
            jsonCurrentColumnDecimalArray.Add(new LazyJsonDecimal(101.101m));
            jsonCurrentColumnDecimalArray.Add(new LazyJsonDecimal(-101.101m));
            jsonCurrentColumnDecimalArray.Add(new LazyJsonDecimal(-1.1m));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnDecimal", jsonCurrentColumnDecimal));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnDecimalArray", jsonCurrentColumnDecimalArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectDecimal = new LazyJsonObject();
            jsonObjectDecimal.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectDecimal.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectDecimal.Add(new LazyJsonProperty("Class", new LazyJsonString("Decimal")));
            LazyJsonObject jsonObjectDecimalArray = new LazyJsonObject();
            jsonObjectDecimalArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectDecimalArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectDecimalArray.Add(new LazyJsonProperty("Class", new LazyJsonString("Decimal[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnDecimal", jsonObjectDecimal));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnDecimalArray", jsonObjectDecimalArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnDecimal")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnDecimal");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnDecimal"].DataType, typeof(Decimal));
            Assert.AreEqual(dataTable.Columns["ColumnDecimalArray"].DataType, typeof(Decimal[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnDecimal"], 101.101m);
            Assert.AreEqual(((Decimal[])dataTable.Rows[0]["ColumnDecimalArray"])[0], 1.1m);
            Assert.AreEqual(((Decimal[])dataTable.Rows[0]["ColumnDecimalArray"])[1], 101.101m);
            Assert.AreEqual(((Decimal[])dataTable.Rows[0]["ColumnDecimalArray"])[2], -101.101m);
            Assert.AreEqual(((Decimal[])dataTable.Rows[0]["ColumnDecimalArray"])[3], -1.1m);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnDateTime_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnDateTime", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnDateTimeArray", new LazyJsonNull()));

            LazyJsonString jsonCurrentColumnDateTime = new LazyJsonString("2023-10-16T14:20:30:000Z");
            LazyJsonArray jsonCurrentColumnDateTimeArray = new LazyJsonArray();
            jsonCurrentColumnDateTimeArray.Add(new LazyJsonString("2023-10-14T14:20:30:000Z"));
            jsonCurrentColumnDateTimeArray.Add(new LazyJsonString("2023-10-15T14:30:30:000Z"));
            jsonCurrentColumnDateTimeArray.Add(new LazyJsonString("2023-10-16T14:40:30:000Z"));
            jsonCurrentColumnDateTimeArray.Add(new LazyJsonString("2023-10-17T14:50:30:000Z"));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnDateTime", jsonCurrentColumnDateTime));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnDateTimeArray", jsonCurrentColumnDateTimeArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectDateTime = new LazyJsonObject();
            jsonObjectDateTime.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectDateTime.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectDateTime.Add(new LazyJsonProperty("Class", new LazyJsonString("DateTime")));
            LazyJsonObject jsonObjectDateTimeArray = new LazyJsonObject();
            jsonObjectDateTimeArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectDateTimeArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectDateTimeArray.Add(new LazyJsonProperty("Class", new LazyJsonString("DateTime[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnDateTime", jsonObjectDateTime));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnDateTimeArray", jsonObjectDateTimeArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnDateTime")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnDateTime");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnDateTime"].DataType, typeof(DateTime));
            Assert.AreEqual(dataTable.Columns["ColumnDateTimeArray"].DataType, typeof(DateTime[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnDateTime"], new DateTime(2023, 10, 16, 14, 20, 30));
            Assert.AreEqual(((DateTime[])dataTable.Rows[0]["ColumnDateTimeArray"])[0], new DateTime(2023, 10, 14, 14, 20, 30));
            Assert.AreEqual(((DateTime[])dataTable.Rows[0]["ColumnDateTimeArray"])[1], new DateTime(2023, 10, 15, 14, 30, 30));
            Assert.AreEqual(((DateTime[])dataTable.Rows[0]["ColumnDateTimeArray"])[2], new DateTime(2023, 10, 16, 14, 40, 30));
            Assert.AreEqual(((DateTime[])dataTable.Rows[0]["ColumnDateTimeArray"])[3], new DateTime(2023, 10, 17, 14, 50, 30));
        }

        [TestMethod]
        public void Deserialize_Type_ColumnBoolean_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnBoolean", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnBooleanArray", new LazyJsonNull()));

            LazyJsonBoolean jsonCurrentColumnBoolean = new LazyJsonBoolean(true);
            LazyJsonArray jsonCurrentColumnBooleanArray = new LazyJsonArray();
            jsonCurrentColumnBooleanArray.Add(new LazyJsonBoolean(false));
            jsonCurrentColumnBooleanArray.Add(new LazyJsonBoolean(true));
            jsonCurrentColumnBooleanArray.Add(new LazyJsonBoolean(true));
            jsonCurrentColumnBooleanArray.Add(new LazyJsonBoolean(false));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnBoolean", jsonCurrentColumnBoolean));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnBooleanArray", jsonCurrentColumnBooleanArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectBoolean = new LazyJsonObject();
            jsonObjectBoolean.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectBoolean.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectBoolean.Add(new LazyJsonProperty("Class", new LazyJsonString("Boolean")));
            LazyJsonObject jsonObjectBooleanArray = new LazyJsonObject();
            jsonObjectBooleanArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectBooleanArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectBooleanArray.Add(new LazyJsonProperty("Class", new LazyJsonString("Boolean[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnBoolean", jsonObjectBoolean));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnBooleanArray", jsonObjectBooleanArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnBoolean")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnBoolean");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnBoolean"].DataType, typeof(Boolean));
            Assert.AreEqual(dataTable.Columns["ColumnBooleanArray"].DataType, typeof(Boolean[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnBoolean"], true);
            Assert.AreEqual(((Boolean[])dataTable.Rows[0]["ColumnBooleanArray"])[0], false);
            Assert.AreEqual(((Boolean[])dataTable.Rows[0]["ColumnBooleanArray"])[1], true);
            Assert.AreEqual(((Boolean[])dataTable.Rows[0]["ColumnBooleanArray"])[2], true);
            Assert.AreEqual(((Boolean[])dataTable.Rows[0]["ColumnBooleanArray"])[3], false);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnInt16_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnInt16", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnInt16Array", new LazyJsonNull()));

            LazyJsonInteger jsonCurrentColumnInt16 = new LazyJsonInteger(101);
            LazyJsonArray jsonCurrentColumnInt16Array = new LazyJsonArray();
            jsonCurrentColumnInt16Array.Add(new LazyJsonInteger(2));
            jsonCurrentColumnInt16Array.Add(new LazyJsonInteger(4));
            jsonCurrentColumnInt16Array.Add(new LazyJsonInteger(8));
            jsonCurrentColumnInt16Array.Add(new LazyJsonInteger(16));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnInt16", jsonCurrentColumnInt16));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnInt16Array", jsonCurrentColumnInt16Array));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectInt16 = new LazyJsonObject();
            jsonObjectInt16.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectInt16.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectInt16.Add(new LazyJsonProperty("Class", new LazyJsonString("Int16")));
            LazyJsonObject jsonObjectInt16Array = new LazyJsonObject();
            jsonObjectInt16Array.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectInt16Array.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectInt16Array.Add(new LazyJsonProperty("Class", new LazyJsonString("Int16[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnInt16", jsonObjectInt16));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnInt16Array", jsonObjectInt16Array));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnInt16")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnInt16");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnInt16"].DataType, typeof(Int16));
            Assert.AreEqual(dataTable.Columns["ColumnInt16Array"].DataType, typeof(Int16[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnInt16"], (Int16)101);
            Assert.AreEqual(((Int16[])dataTable.Rows[0]["ColumnInt16Array"])[0], 2);
            Assert.AreEqual(((Int16[])dataTable.Rows[0]["ColumnInt16Array"])[1], 4);
            Assert.AreEqual(((Int16[])dataTable.Rows[0]["ColumnInt16Array"])[2], 8);
            Assert.AreEqual(((Int16[])dataTable.Rows[0]["ColumnInt16Array"])[3], 16);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnInt64_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnInt64", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnInt64Array", new LazyJsonNull()));

            LazyJsonInteger jsonCurrentColumnInt64 = new LazyJsonInteger(512);
            LazyJsonArray jsonCurrentColumnInt64Array = new LazyJsonArray();
            jsonCurrentColumnInt64Array.Add(new LazyJsonInteger(1024));
            jsonCurrentColumnInt64Array.Add(new LazyJsonInteger(2048));
            jsonCurrentColumnInt64Array.Add(new LazyJsonInteger(4096));
            jsonCurrentColumnInt64Array.Add(new LazyJsonInteger(8192));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnInt64", jsonCurrentColumnInt64));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnInt64Array", jsonCurrentColumnInt64Array));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectInt64 = new LazyJsonObject();
            jsonObjectInt64.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectInt64.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectInt64.Add(new LazyJsonProperty("Class", new LazyJsonString("Int64")));
            LazyJsonObject jsonObjectInt64Array = new LazyJsonObject();
            jsonObjectInt64Array.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectInt64Array.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectInt64Array.Add(new LazyJsonProperty("Class", new LazyJsonString("Int64[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnInt64", jsonObjectInt64));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnInt64Array", jsonObjectInt64Array));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnInt64")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnInt64");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnInt64"].DataType, typeof(Int64));
            Assert.AreEqual(dataTable.Columns["ColumnInt64Array"].DataType, typeof(Int64[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnInt64"], (Int64)512);
            Assert.AreEqual(((Int64[])dataTable.Rows[0]["ColumnInt64Array"])[0], 1024);
            Assert.AreEqual(((Int64[])dataTable.Rows[0]["ColumnInt64Array"])[1], 2048);
            Assert.AreEqual(((Int64[])dataTable.Rows[0]["ColumnInt64Array"])[2], 4096);
            Assert.AreEqual(((Int64[])dataTable.Rows[0]["ColumnInt64Array"])[3], 8192);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnDouble_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnDouble", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnDoubleArray", new LazyJsonNull()));

            LazyJsonDecimal jsonCurrentColumnDouble = new LazyJsonDecimal(1.1m);
            LazyJsonArray jsonCurrentColumnDoubleArray = new LazyJsonArray();
            jsonCurrentColumnDoubleArray.Add(new LazyJsonDecimal(1.01m));
            jsonCurrentColumnDoubleArray.Add(new LazyJsonDecimal(-1.01m));
            jsonCurrentColumnDoubleArray.Add(new LazyJsonDecimal(-101.1m));
            jsonCurrentColumnDoubleArray.Add(new LazyJsonDecimal(101.1m));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnDouble", jsonCurrentColumnDouble));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnDoubleArray", jsonCurrentColumnDoubleArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectDouble = new LazyJsonObject();
            jsonObjectDouble.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectDouble.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectDouble.Add(new LazyJsonProperty("Class", new LazyJsonString("Double")));
            LazyJsonObject jsonObjectDoubleArray = new LazyJsonObject();
            jsonObjectDoubleArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectDoubleArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectDoubleArray.Add(new LazyJsonProperty("Class", new LazyJsonString("Double[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnDouble", jsonObjectDouble));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnDoubleArray", jsonObjectDoubleArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnDouble")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnDouble");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnDouble"].DataType, typeof(Double));
            Assert.AreEqual(dataTable.Columns["ColumnDoubleArray"].DataType, typeof(Double[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnDouble"], 1.1d);
            Assert.AreEqual(((Double[])dataTable.Rows[0]["ColumnDoubleArray"])[0], 1.01d);
            Assert.AreEqual(((Double[])dataTable.Rows[0]["ColumnDoubleArray"])[1], -1.01d);
            Assert.AreEqual(((Double[])dataTable.Rows[0]["ColumnDoubleArray"])[2], -101.1d);
            Assert.AreEqual(((Double[])dataTable.Rows[0]["ColumnDoubleArray"])[3], 101.1d);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnSingle_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnSingle", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnSingleArray", new LazyJsonNull()));

            LazyJsonDecimal jsonCurrentColumnSingle = new LazyJsonDecimal(1.1m);
            LazyJsonArray jsonCurrentColumnSingleArray = new LazyJsonArray();
            jsonCurrentColumnSingleArray.Add(new LazyJsonDecimal(1.01m));
            jsonCurrentColumnSingleArray.Add(new LazyJsonDecimal(-1.01m));
            jsonCurrentColumnSingleArray.Add(new LazyJsonDecimal(-101.1m));
            jsonCurrentColumnSingleArray.Add(new LazyJsonDecimal(101.1m));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnSingle", jsonCurrentColumnSingle));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnSingleArray", jsonCurrentColumnSingleArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectSingle = new LazyJsonObject();
            jsonObjectSingle.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectSingle.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectSingle.Add(new LazyJsonProperty("Class", new LazyJsonString("Single")));
            LazyJsonObject jsonObjectSingleArray = new LazyJsonObject();
            jsonObjectSingleArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectSingleArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectSingleArray.Add(new LazyJsonProperty("Class", new LazyJsonString("Single[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnSingle", jsonObjectSingle));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnSingleArray", jsonObjectSingleArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnSingle")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnSingle");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnSingle"].DataType, typeof(Single));
            Assert.AreEqual(dataTable.Columns["ColumnSingleArray"].DataType, typeof(Single[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnSingle"], 1.1f);
            Assert.AreEqual(((Single[])dataTable.Rows[0]["ColumnSingleArray"])[0], 1.01f);
            Assert.AreEqual(((Single[])dataTable.Rows[0]["ColumnSingleArray"])[1], -1.01f);
            Assert.AreEqual(((Single[])dataTable.Rows[0]["ColumnSingleArray"])[2], -101.1f);
            Assert.AreEqual(((Single[])dataTable.Rows[0]["ColumnSingleArray"])[3], 101.1f);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnChar_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnChar", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnCharArray", new LazyJsonNull()));

            LazyJsonString jsonCurrentColumnChar = new LazyJsonString("J");
            LazyJsonArray jsonCurrentColumnCharArray = new LazyJsonArray();
            jsonCurrentColumnCharArray.Add(new LazyJsonString("J"));
            jsonCurrentColumnCharArray.Add(new LazyJsonString("S"));
            jsonCurrentColumnCharArray.Add(new LazyJsonString("O"));
            jsonCurrentColumnCharArray.Add(new LazyJsonString("N"));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnChar", jsonCurrentColumnChar));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnCharArray", jsonCurrentColumnCharArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectChar = new LazyJsonObject();
            jsonObjectChar.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectChar.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectChar.Add(new LazyJsonProperty("Class", new LazyJsonString("Char")));
            LazyJsonObject jsonObjectCharArray = new LazyJsonObject();
            jsonObjectCharArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectCharArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectCharArray.Add(new LazyJsonProperty("Class", new LazyJsonString("Char[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnChar", jsonObjectChar));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnCharArray", jsonObjectCharArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnChar")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnChar");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnChar"].DataType, typeof(Char));
            Assert.AreEqual(dataTable.Columns["ColumnCharArray"].DataType, typeof(Char[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnChar"], 'J');
            Assert.AreEqual(((Char[])dataTable.Rows[0]["ColumnCharArray"])[0], 'J');
            Assert.AreEqual(((Char[])dataTable.Rows[0]["ColumnCharArray"])[1], 'S');
            Assert.AreEqual(((Char[])dataTable.Rows[0]["ColumnCharArray"])[2], 'O');
            Assert.AreEqual(((Char[])dataTable.Rows[0]["ColumnCharArray"])[3], 'N');
        }

        [TestMethod]
        public void Deserialize_Type_ColumnByte_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnByte", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnByteArray", new LazyJsonNull()));

            LazyJsonInteger jsonCurrentColumnByte = new LazyJsonInteger(8);
            LazyJsonArray jsonCurrentColumnByteArray = new LazyJsonArray();
            jsonCurrentColumnByteArray.Add(new LazyJsonInteger(16));
            jsonCurrentColumnByteArray.Add(new LazyJsonInteger(32));
            jsonCurrentColumnByteArray.Add(new LazyJsonInteger(64));
            jsonCurrentColumnByteArray.Add(new LazyJsonInteger(24));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnByte", jsonCurrentColumnByte));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnByteArray", jsonCurrentColumnByteArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectByte = new LazyJsonObject();
            jsonObjectByte.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectByte.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectByte.Add(new LazyJsonProperty("Class", new LazyJsonString("Byte")));
            LazyJsonObject jsonObjectByteArray = new LazyJsonObject();
            jsonObjectByteArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectByteArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectByteArray.Add(new LazyJsonProperty("Class", new LazyJsonString("Byte[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnByte", jsonObjectByte));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnByteArray", jsonObjectByteArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnByte")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnByte");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnByte"].DataType, typeof(Byte));
            Assert.AreEqual(dataTable.Columns["ColumnByteArray"].DataType, typeof(Byte[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnByte"], (Byte)8);
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["ColumnByteArray"])[0], (Byte)16);
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["ColumnByteArray"])[1], (Byte)32);
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["ColumnByteArray"])[2], (Byte)64);
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["ColumnByteArray"])[3], (Byte)24);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnSByte_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnSByte", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnSByteArray", new LazyJsonNull()));

            LazyJsonInteger jsonCurrentColumnSByte = new LazyJsonInteger(-8);
            LazyJsonArray jsonCurrentColumnSByteArray = new LazyJsonArray();
            jsonCurrentColumnSByteArray.Add(new LazyJsonInteger(-16));
            jsonCurrentColumnSByteArray.Add(new LazyJsonInteger(-32));
            jsonCurrentColumnSByteArray.Add(new LazyJsonInteger(-64));
            jsonCurrentColumnSByteArray.Add(new LazyJsonInteger(-24));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnSByte", jsonCurrentColumnSByte));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnSByteArray", jsonCurrentColumnSByteArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectSByte = new LazyJsonObject();
            jsonObjectSByte.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectSByte.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectSByte.Add(new LazyJsonProperty("Class", new LazyJsonString("SByte")));
            LazyJsonObject jsonObjectSByteArray = new LazyJsonObject();
            jsonObjectSByteArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectSByteArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectSByteArray.Add(new LazyJsonProperty("Class", new LazyJsonString("SByte[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnSByte", jsonObjectSByte));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnSByteArray", jsonObjectSByteArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnSByte")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnSByte");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnSByte"].DataType, typeof(SByte));
            Assert.AreEqual(dataTable.Columns["ColumnSByteArray"].DataType, typeof(SByte[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnSByte"], (SByte)(-8));
            Assert.AreEqual(((SByte[])dataTable.Rows[0]["ColumnSByteArray"])[0], (SByte)(-16));
            Assert.AreEqual(((SByte[])dataTable.Rows[0]["ColumnSByteArray"])[1], (SByte)(-32));
            Assert.AreEqual(((SByte[])dataTable.Rows[0]["ColumnSByteArray"])[2], (SByte)(-64));
            Assert.AreEqual(((SByte[])dataTable.Rows[0]["ColumnSByteArray"])[3], (SByte)(-24));
        }

        [TestMethod]
        public void Deserialize_Type_ColumnUInt32_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnUInt32", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnUInt32Array", new LazyJsonNull()));

            LazyJsonInteger jsonCurrentColumnUInt32 = new LazyJsonInteger(8);
            LazyJsonArray jsonCurrentColumnUInt32Array = new LazyJsonArray();
            jsonCurrentColumnUInt32Array.Add(new LazyJsonInteger(16));
            jsonCurrentColumnUInt32Array.Add(new LazyJsonInteger(32));
            jsonCurrentColumnUInt32Array.Add(new LazyJsonInteger(64));
            jsonCurrentColumnUInt32Array.Add(new LazyJsonInteger(24));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnUInt32", jsonCurrentColumnUInt32));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnUInt32Array", jsonCurrentColumnUInt32Array));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectUInt32 = new LazyJsonObject();
            jsonObjectUInt32.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectUInt32.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectUInt32.Add(new LazyJsonProperty("Class", new LazyJsonString("UInt32")));
            LazyJsonObject jsonObjectUInt32Array = new LazyJsonObject();
            jsonObjectUInt32Array.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectUInt32Array.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectUInt32Array.Add(new LazyJsonProperty("Class", new LazyJsonString("UInt32[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnUInt32", jsonObjectUInt32));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnUInt32Array", jsonObjectUInt32Array));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnUInt32")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnUInt32");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnUInt32"].DataType, typeof(UInt32));
            Assert.AreEqual(dataTable.Columns["ColumnUInt32Array"].DataType, typeof(UInt32[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnUInt32"], (UInt32)8);
            Assert.AreEqual(((UInt32[])dataTable.Rows[0]["ColumnUInt32Array"])[0], (UInt32)16);
            Assert.AreEqual(((UInt32[])dataTable.Rows[0]["ColumnUInt32Array"])[1], (UInt32)32);
            Assert.AreEqual(((UInt32[])dataTable.Rows[0]["ColumnUInt32Array"])[2], (UInt32)64);
            Assert.AreEqual(((UInt32[])dataTable.Rows[0]["ColumnUInt32Array"])[3], (UInt32)24);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnUInt16_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnUInt16", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnUInt16Array", new LazyJsonNull()));

            LazyJsonInteger jsonCurrentColumnUInt16 = new LazyJsonInteger(8);
            LazyJsonArray jsonCurrentColumnUInt16Array = new LazyJsonArray();
            jsonCurrentColumnUInt16Array.Add(new LazyJsonInteger(16));
            jsonCurrentColumnUInt16Array.Add(new LazyJsonInteger(32));
            jsonCurrentColumnUInt16Array.Add(new LazyJsonInteger(64));
            jsonCurrentColumnUInt16Array.Add(new LazyJsonInteger(24));

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnUInt16", jsonCurrentColumnUInt16));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnUInt16Array", jsonCurrentColumnUInt16Array));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectUInt16 = new LazyJsonObject();
            jsonObjectUInt16.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectUInt16.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectUInt16.Add(new LazyJsonProperty("Class", new LazyJsonString("UInt16")));
            LazyJsonObject jsonObjectUInt16Array = new LazyJsonObject();
            jsonObjectUInt16Array.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectUInt16Array.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectUInt16Array.Add(new LazyJsonProperty("Class", new LazyJsonString("UInt16[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnUInt16", jsonObjectUInt16));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnUInt16Array", jsonObjectUInt16Array));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnUInt16")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnUInt16");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnUInt16"].DataType, typeof(UInt16));
            Assert.AreEqual(dataTable.Columns["ColumnUInt16Array"].DataType, typeof(UInt16[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnUInt16"], (UInt16)8);
            Assert.AreEqual(((UInt16[])dataTable.Rows[0]["ColumnUInt16Array"])[0], (UInt16)16);
            Assert.AreEqual(((UInt16[])dataTable.Rows[0]["ColumnUInt16Array"])[1], (UInt16)32);
            Assert.AreEqual(((UInt16[])dataTable.Rows[0]["ColumnUInt16Array"])[2], (UInt16)64);
            Assert.AreEqual(((UInt16[])dataTable.Rows[0]["ColumnUInt16Array"])[3], (UInt16)24);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnDataTable_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnDataTable", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnDataTableArray", new LazyJsonNull()));

            LazyJsonObject jsonCurrentColumnDataTableIntegerCol = new LazyJsonObject();
            jsonCurrentColumnDataTableIntegerCol.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonCurrentColumnDataTableIntegerCol.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonCurrentColumnDataTableIntegerCol.Add(new LazyJsonProperty("Class", new LazyJsonString("Int32")));
            LazyJsonObject jsonCurrentColumnDataTableCol = new LazyJsonObject();
            jsonCurrentColumnDataTableCol.Add(new LazyJsonProperty("ColumnInt32", jsonCurrentColumnDataTableIntegerCol));

            LazyJsonObject jsonCurrentColumnDataTable = new LazyJsonObject();
            jsonCurrentColumnDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("EmptyDataTable")));
            jsonCurrentColumnDataTable.Add(new LazyJsonProperty("Columns", jsonCurrentColumnDataTableCol));
            jsonCurrentColumnDataTable.Add(new LazyJsonProperty("Rows", new LazyJsonArray()));
            LazyJsonArray jsonCurrentColumnDataTableArray = new LazyJsonArray();
            jsonCurrentColumnDataTableArray.Add(jsonCurrentColumnDataTable);
            jsonCurrentColumnDataTableArray.Add(jsonCurrentColumnDataTable);

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnDataTable", jsonCurrentColumnDataTable));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnDataTableArray", jsonCurrentColumnDataTableArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectColDataTable = new LazyJsonObject();
            jsonObjectColDataTable.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Data.Common")));
            jsonObjectColDataTable.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Data")));
            jsonObjectColDataTable.Add(new LazyJsonProperty("Class", new LazyJsonString("DataTable")));
            LazyJsonObject jsonObjectColDataTableArray = new LazyJsonObject();
            jsonObjectColDataTableArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Data.Common")));
            jsonObjectColDataTableArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Data")));
            jsonObjectColDataTableArray.Add(new LazyJsonProperty("Class", new LazyJsonString("DataTable[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnDataTable", jsonObjectColDataTable));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnDataTableArray", jsonObjectColDataTableArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnDataTable")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnDataTable");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnDataTable"].DataType, typeof(DataTable));
            Assert.AreEqual(dataTable.Columns["ColumnDataTableArray"].DataType, typeof(DataTable[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(((DataTable)dataTable.Rows[0]["ColumnDataTable"]).TableName, "EmptyDataTable");
            Assert.AreEqual(((DataTable)dataTable.Rows[0]["ColumnDataTable"]).Columns.Count, 1);
            Assert.AreEqual(((DataTable)dataTable.Rows[0]["ColumnDataTable"]).Rows.Count, 0);
            Assert.AreEqual(((DataTable[])dataTable.Rows[0]["ColumnDataTableArray"])[0].TableName, "EmptyDataTable");
            Assert.AreEqual(((DataTable[])dataTable.Rows[0]["ColumnDataTableArray"])[0].Columns.Count, 1);
            Assert.AreEqual(((DataTable[])dataTable.Rows[0]["ColumnDataTableArray"])[0].Rows.Count, 0);
            Assert.AreEqual(((DataTable[])dataTable.Rows[0]["ColumnDataTableArray"])[1].TableName, "EmptyDataTable");
            Assert.AreEqual(((DataTable[])dataTable.Rows[0]["ColumnDataTableArray"])[1].Columns.Count, 1);
            Assert.AreEqual(((DataTable[])dataTable.Rows[0]["ColumnDataTableArray"])[1].Rows.Count, 0);
        }

        [TestMethod]
        public void Deserialize_Type_ColumnType_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectDataTableRowValuesOriginal = new LazyJsonObject();
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnType", new LazyJsonNull()));
            jsonObjectDataTableRowValuesOriginal.Add(new LazyJsonProperty("ColumnTypeArray", new LazyJsonNull()));

            LazyJsonObject jsonCurrentColumnTypeInt32 = new LazyJsonObject();
            jsonCurrentColumnTypeInt32.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonCurrentColumnTypeInt32.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonCurrentColumnTypeInt32.Add(new LazyJsonProperty("Class", new LazyJsonString("Int32")));

            LazyJsonObject jsonCurrentColumnTypeDataTable = new LazyJsonObject();
            jsonCurrentColumnTypeDataTable.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Data.Common")));
            jsonCurrentColumnTypeDataTable.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Data")));
            jsonCurrentColumnTypeDataTable.Add(new LazyJsonProperty("Class", new LazyJsonString("DataTable")));

            LazyJsonObject jsonCurrentColumnTypeLazyJsonInteger = new LazyJsonObject();
            jsonCurrentColumnTypeLazyJsonInteger.Add(new LazyJsonProperty("Assembly", new LazyJsonString("Lazy.Vinke.Json")));
            jsonCurrentColumnTypeLazyJsonInteger.Add(new LazyJsonProperty("Namespace", new LazyJsonString("Lazy.Vinke.Json")));
            jsonCurrentColumnTypeLazyJsonInteger.Add(new LazyJsonProperty("Class", new LazyJsonString("LazyJsonInteger")));

            LazyJsonArray jsonCurrentColumnTypeArray = new LazyJsonArray();
            jsonCurrentColumnTypeArray.Add(jsonCurrentColumnTypeDataTable);
            jsonCurrentColumnTypeArray.Add(jsonCurrentColumnTypeLazyJsonInteger);

            LazyJsonObject jsonObjectDataTableRowValuesCurrent = new LazyJsonObject();
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnType", jsonCurrentColumnTypeInt32));
            jsonObjectDataTableRowValuesCurrent.Add(new LazyJsonProperty("ColumnTypeArray", jsonCurrentColumnTypeArray));

            LazyJsonObject jsonObjectDataTableRowValues = new LazyJsonObject();
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataTableRowValuesOriginal));
            jsonObjectDataTableRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataTableRowValuesCurrent));
            LazyJsonObject jsonObjectDataTableRow = new LazyJsonObject();
            jsonObjectDataTableRow.Add(new LazyJsonProperty("State", new LazyJsonString("Modified")));
            jsonObjectDataTableRow.Add(new LazyJsonProperty("Values", jsonObjectDataTableRowValues));
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();
            jsonArrayDataTableRows.Add(jsonObjectDataTableRow);

            LazyJsonObject jsonObjectType = new LazyJsonObject();
            jsonObjectType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectType.Add(new LazyJsonProperty("Class", new LazyJsonString("Type")));
            LazyJsonObject jsonObjectTypeArray = new LazyJsonObject();
            jsonObjectTypeArray.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectTypeArray.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectTypeArray.Add(new LazyJsonProperty("Class", new LazyJsonString("Type[]")));

            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnType", jsonObjectType));
            jsonObjectDataTableColumns.Add(new LazyJsonProperty("ColumnTypeArray", jsonObjectTypeArray));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", new LazyJsonString("Type_ColumnType")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            // Act
            DataTable dataTable = (DataTable)new LazyJsonDeserializerDataTable().Deserialize(jsonObjectDataTable, typeof(DataTable));

            // Assert
            Assert.AreEqual(dataTable.TableName, "Type_ColumnType");
            Assert.AreEqual(dataTable.Columns.Count, 2);
            Assert.AreEqual(dataTable.Columns["ColumnType"].DataType, typeof(Type));
            Assert.AreEqual(dataTable.Columns["ColumnTypeArray"].DataType, typeof(Type[]));
            Assert.AreEqual(dataTable.Rows.Count, 1);
            Assert.AreEqual(dataTable.Rows[0]["ColumnType"], typeof(Int32));
            Assert.AreEqual(((Type[])dataTable.Rows[0]["ColumnTypeArray"])[0], typeof(DataTable));
            Assert.AreEqual(((Type[])dataTable.Rows[0]["ColumnTypeArray"])[1], typeof(LazyJsonInteger));
        }
    }
}

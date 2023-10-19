// LazyJsonSerializerDataTable.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 14

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerDataTable : LazyJsonSerializerBase
    {
        #region Variables
        #endregion Variables

        #region Constructors
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Serialize an object to a json token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The json token</returns>
        public override LazyJsonToken Serialize(Object data, LazyJsonSerializerOptions jsonSerializerOptions = null)
        {
            if (data != null && data is DataTable)
            {
                DataTable dataTable = (DataTable)data;

                LazyJsonSerializerOptionsDataTableColumnCollection jsonSerializerOptionsDataTableColumns = null;
                if (jsonSerializerOptions?.Contains<LazyJsonSerializerOptionsDataTable>() == true && jsonSerializerOptions.Item<LazyJsonSerializerOptionsDataTable>().DataTableCollection.ContainsKey(dataTable.TableName) == true)
                    jsonSerializerOptionsDataTableColumns = jsonSerializerOptions.Item<LazyJsonSerializerOptionsDataTable>().DataTableCollection[dataTable.TableName].Columns;

                return SerializeDataTable(dataTable, jsonSerializerOptions, jsonSerializerOptionsDataTableColumns);
            }

            return new LazyJsonNull();
        }

        /// <summary>
        /// Serialize a data table to a json object
        /// </summary>
        /// <param name="dataTable">The data table</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <param name="jsonSerializerOptionsDataTableColumns">The json data table columns serializer options</param>
        /// <returns>The json object</returns>
        private LazyJsonObject SerializeDataTable(DataTable dataTable, LazyJsonSerializerOptions jsonSerializerOptions, LazyJsonSerializerOptionsDataTableColumnCollection jsonSerializerOptionsDataTableColumns)
        {
            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();

            LazyJsonString jsonStringDataTableName = new LazyJsonString(dataTable.TableName);
            jsonObjectDataTable.Add(new LazyJsonProperty("Name", jsonStringDataTableName));

            Dictionary<String, LazyJsonSerializerBase> jsonSerializerDictionary = null;
            LazyJsonObject jsonObjectDataTableColumns = SerializeDataTableColumns(dataTable.Columns, jsonSerializerOptions, jsonSerializerOptionsDataTableColumns, out jsonSerializerDictionary);
            jsonObjectDataTable.Add(new LazyJsonProperty("Columns", jsonObjectDataTableColumns));

            LazyJsonArray jsonArrayDataTableRows = SerializeDataTableRows(dataTable.Rows, jsonSerializerOptions, jsonSerializerOptionsDataTableColumns, jsonSerializerDictionary);
            jsonObjectDataTable.Add(new LazyJsonProperty("Rows", jsonArrayDataTableRows));

            return jsonObjectDataTable;
        }

        /// <summary>
        /// Serialize a data table column collection to a json object
        /// </summary>
        /// <param name="dataTableColumns">The data table column collection</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <param name="jsonSerializerOptionsDataTableColumns">The json data table columns serializer options</param>
        /// <param name="jsonSerializerDictionary">The json serializer dictionary</param>
        /// <returns>The json object</returns>
        private LazyJsonObject SerializeDataTableColumns(DataColumnCollection dataTableColumns, LazyJsonSerializerOptions jsonSerializerOptions, LazyJsonSerializerOptionsDataTableColumnCollection jsonSerializerOptionsDataTableColumns, out Dictionary<String, LazyJsonSerializerBase> jsonSerializerDictionary)
        {
            LazyJsonObject jsonObjectDataTableColumns = new LazyJsonObject();
            LazyJsonSerializerType jsonSerializerColumnType = new LazyJsonSerializerType();

            jsonSerializerDictionary = new Dictionary<String, LazyJsonSerializerBase>();

            foreach (DataColumn dataColumn in dataTableColumns)
            {
                LazyJsonSerializerBase jsonSerializer = null;

                if (jsonSerializerOptionsDataTableColumns?.ColumnDataCollection.ContainsKey(dataColumn.ColumnName) == true && jsonSerializerOptionsDataTableColumns.ColumnDataCollection[dataColumn.ColumnName].Serializer != null)
                {
                    jsonSerializer = jsonSerializerOptionsDataTableColumns.ColumnDataCollection[dataColumn.ColumnName].Serializer;
                }
                else
                {
                    Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(dataColumn.DataType, jsonSerializerOptions);

                    if (jsonSerializerType != null)
                        jsonSerializer = (LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType);
                }

                if (jsonSerializer != null)
                {
                    jsonObjectDataTableColumns.Add(new LazyJsonProperty(dataColumn.ColumnName, jsonSerializerColumnType.Serialize(dataColumn.DataType, jsonSerializerOptions)));
                    jsonSerializerDictionary.Add(dataColumn.ColumnName, jsonSerializer);
                }
            }

            return jsonObjectDataTableColumns;
        }

        /// <summary>
        /// Serialize a data table row collection to a json array
        /// </summary>
        /// <param name="dataTableRows">The data table row collection</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <param name="jsonSerializerOptionsDataTableColumns">The json data table columns serializer options</param>
        /// <param name="jsonSerializerDictionary">The json serializer dictionary</param>
        /// <returns>The json array</returns>
        private LazyJsonArray SerializeDataTableRows(DataRowCollection dataTableRows, LazyJsonSerializerOptions jsonSerializerOptions, LazyJsonSerializerOptionsDataTableColumnCollection jsonSerializerOptionsDataTableColumns, Dictionary<String, LazyJsonSerializerBase> jsonSerializerDictionary)
        {
            LazyJsonArray jsonArrayDataTableRows = new LazyJsonArray();

            foreach (DataRow dataRow in dataTableRows)
            {
                LazyJsonObject jsonObjectDataRow = new LazyJsonObject();

                // State
                jsonObjectDataRow.Add(new LazyJsonProperty("State", new LazyJsonString(Enum.GetName(typeof(DataRowState), dataRow.RowState))));

                // Values
                LazyJsonObject jsonObjectDataRowValues = new LazyJsonObject();
                jsonObjectDataRow.Add(new LazyJsonProperty("Values", jsonObjectDataRowValues));

                // Values Original
                LazyJsonObject jsonObjectDataRowValuesOriginal = null;
                if (dataRow.RowState == DataRowState.Modified || dataRow.RowState == DataRowState.Deleted)
                {
                    jsonObjectDataRowValuesOriginal = new LazyJsonObject();
                    foreach (KeyValuePair<String, LazyJsonSerializerBase> keyValuePair in jsonSerializerDictionary)
                        jsonObjectDataRowValuesOriginal.Add(new LazyJsonProperty(keyValuePair.Key, keyValuePair.Value.Serialize(dataRow[keyValuePair.Key, DataRowVersion.Original], jsonSerializerOptions)));
                }
                jsonObjectDataRowValues.Add(new LazyJsonProperty("Original", jsonObjectDataRowValuesOriginal != null ? jsonObjectDataRowValuesOriginal : new LazyJsonNull()));

                // Values Current
                LazyJsonObject jsonObjectDataRowValuesCurrent = null;
                if (dataRow.RowState != DataRowState.Deleted)
                {
                    jsonObjectDataRowValuesCurrent = new LazyJsonObject();
                    foreach (KeyValuePair<String, LazyJsonSerializerBase> keyValuePair in jsonSerializerDictionary)
                        jsonObjectDataRowValuesCurrent.Add(new LazyJsonProperty(keyValuePair.Key, keyValuePair.Value.Serialize(dataRow[keyValuePair.Key, DataRowVersion.Current], jsonSerializerOptions)));
                }
                jsonObjectDataRowValues.Add(new LazyJsonProperty("Current", jsonObjectDataRowValuesCurrent != null ? jsonObjectDataRowValuesCurrent : new LazyJsonNull()));

                jsonArrayDataTableRows.Add(jsonObjectDataRow);
            }

            return jsonArrayDataTableRows;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

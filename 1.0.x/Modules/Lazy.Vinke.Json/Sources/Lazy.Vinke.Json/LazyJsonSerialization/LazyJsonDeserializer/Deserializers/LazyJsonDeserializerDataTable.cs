// LazyJsonDeserializerDataTable.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 15

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerDataTable : LazyJsonDeserializerBase
    {
        #region Variables
        #endregion Variables

        #region Constructors
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Deserialize the json token to an object
        /// </summary>
        /// <param name="jsonToken">The json token</param>
        /// <param name="dataType">The type of the object</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The deserialized object</returns>
        public override Object Deserialize(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Object && dataType != null && dataType.IsAssignableTo(typeof(DataTable)) == true)
            {
                LazyJsonProperty jsonPropertyTokenExtractor = null;

                DataTable dataTable = (DataTable)Activator.CreateInstance(dataType);

                LazyJsonObject jsonObjectDataTable = (LazyJsonObject)jsonToken;

                jsonPropertyTokenExtractor = jsonObjectDataTable["Name"];
                if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.String)
                {
                    LazyJsonString jsonStringDataTableName = (LazyJsonString)jsonPropertyTokenExtractor.Token;

                    if (jsonStringDataTableName.Value != null)
                        dataTable.TableName = jsonStringDataTableName.Value;
                }

                jsonPropertyTokenExtractor = jsonObjectDataTable["Columns"];
                if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.Object)
                {
                    LazyJsonObject jsonObjectDataTableColumns = (LazyJsonObject)jsonPropertyTokenExtractor.Token;

                    if (jsonObjectDataTableColumns.Count > 0)
                    {
                        LazyJsonDeserializerOptionsDataTableColumnCollection jsonDeserializerOptionsDataTableColumns = null;
                        if (jsonDeserializerOptions?.Contains<LazyJsonDeserializerOptionsDataTable>() == true && jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>().DataTableCollection.ContainsKey(dataTable.TableName) == true)
                            jsonDeserializerOptionsDataTableColumns = jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDataTable>().DataTableCollection[dataTable.TableName].Columns;

                        Dictionary<String, Tuple<Type, LazyJsonDeserializerBase>> jsonDeserializerDictionary = null;
                        DeserializeDataTableColumns(dataTable, jsonObjectDataTableColumns, jsonDeserializerOptions, jsonDeserializerOptionsDataTableColumns, out jsonDeserializerDictionary);

                        jsonPropertyTokenExtractor = jsonObjectDataTable["Rows"];
                        if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.Array)
                        {
                            LazyJsonArray jsonArrayDataTableRows = (LazyJsonArray)jsonPropertyTokenExtractor.Token;

                            if (jsonArrayDataTableRows.Length > 0)
                                DeserializeDataTableRows(dataTable, jsonArrayDataTableRows, jsonDeserializerOptions, jsonDeserializerOptionsDataTableColumns, jsonDeserializerDictionary);
                        }
                    }
                }

                return dataTable;
            }

            return null;
        }

        /// <summary>
        /// Deserialize the json object data table columns
        /// </summary>
        /// <param name="dataTable">The data table</param>
        /// <param name="jsonObjectDataTableColumns">The json object data table columns</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <param name="jsonDeserializerOptionsDataTableColumns">The json data table columns deserializer options</param>
        /// <param name="jsonDeserializerDictionary">The json deserializer dictionary</param>
        private void DeserializeDataTableColumns(DataTable dataTable, LazyJsonObject jsonObjectDataTableColumns, LazyJsonDeserializerOptions jsonDeserializerOptions, LazyJsonDeserializerOptionsDataTableColumnCollection jsonDeserializerOptionsDataTableColumns, out Dictionary<String, Tuple<Type, LazyJsonDeserializerBase>> jsonDeserializerDictionary)
        {
            LazyJsonDeserializerType jsonDeserializerColumnType = new LazyJsonDeserializerType();

            jsonDeserializerDictionary = new Dictionary<String, Tuple<Type, LazyJsonDeserializerBase>>();

            for (int index = 0; index < jsonObjectDataTableColumns.Count; index++)
            {
                Type dataType = null;
                LazyJsonDeserializerBase jsonDeserializer = null;

                LazyJsonProperty jsonPropertyColumn = jsonObjectDataTableColumns[index];

                if (jsonDeserializerOptionsDataTableColumns?.ColumnDataCollection.ContainsKey(jsonPropertyColumn.Name) == true && jsonDeserializerOptionsDataTableColumns.ColumnDataCollection[jsonPropertyColumn.Name].Deserializer != null)
                {
                    jsonDeserializer = jsonDeserializerOptionsDataTableColumns.ColumnDataCollection[jsonPropertyColumn.Name].Deserializer;
                }
                else
                {
                    dataType = (Type)jsonDeserializerColumnType.Deserialize(jsonPropertyColumn, typeof(Type), jsonDeserializerOptions);

                    if (dataType != null)
                    {
                        Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(dataType, jsonDeserializerOptions);

                        if (jsonDeserializerType != null)
                            jsonDeserializer = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);
                    }
                }

                if (jsonDeserializer != null)
                {
                    dataTable.Columns.Add(jsonPropertyColumn.Name, dataType);

                    Type dataTypeNullable = dataType.IsValueType == true ? typeof(Nullable<>).MakeGenericType(dataType) : dataType;
                    jsonDeserializerDictionary.Add(jsonPropertyColumn.Name, new Tuple<Type, LazyJsonDeserializerBase>(dataTypeNullable, jsonDeserializer));
                }
            }
        }

        /// <summary>
        /// Deserialize the json array data table rows
        /// </summary>
        /// <param name="dataTable">The data table</param>
        /// <param name="jsonArrayDataTableRows">The json array data table rows</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <param name="jsonDeserializerOptionsDataTableColumns">The json data table columns deserializer options</param>
        /// <param name="jsonDeserializerDictionary">The json deserializer dictionary</param>
        private void DeserializeDataTableRows(DataTable dataTable, LazyJsonArray jsonArrayDataTableRows, LazyJsonDeserializerOptions jsonDeserializerOptions, LazyJsonDeserializerOptionsDataTableColumnCollection jsonDeserializerOptionsDataTableColumns, Dictionary<String, Tuple<Type, LazyJsonDeserializerBase>> jsonDeserializerDictionary)
        {
            LazyJsonProperty jsonPropertyTokenExtractor = null;

            for (int index = 0; index < jsonArrayDataTableRows.Length; index++)
            {
                if (jsonArrayDataTableRows[index].Type == LazyJsonType.Object)
                {
                    LazyJsonObject jsonObjectDataTableRow = (LazyJsonObject)jsonArrayDataTableRows[index];

                    DataRow dataRow = dataTable.NewRow();
                    dataTable.Rows.Add(dataRow);
                    dataRow.AcceptChanges();

                    // State
                    DataRowState dataRowState = DataRowState.Unchanged;

                    jsonPropertyTokenExtractor = jsonObjectDataTableRow["State"];
                    if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.String)
                    {
                        LazyJsonString jsonString = (LazyJsonString)jsonPropertyTokenExtractor.Token;

                        if (jsonString.Value != null)
                        {
                            switch (jsonString.Value.ToLower())
                            {
                                case "added": dataRowState = DataRowState.Added; break;
                                case "modified": dataRowState = DataRowState.Modified; break;
                                case "deleted": dataRowState = DataRowState.Deleted; break;
                            }
                        }
                    }

                    // Values
                    jsonPropertyTokenExtractor = jsonObjectDataTableRow["Values"];
                    if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.Object)
                    {
                        LazyJsonObject jsonObjectDataTableRowValues = (LazyJsonObject)jsonPropertyTokenExtractor.Token;

                        // Values Original
                        if (dataRowState == DataRowState.Modified || dataRowState == DataRowState.Deleted)
                        {
                            jsonPropertyTokenExtractor = jsonObjectDataTableRowValues["Original"];
                            if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.Object)
                            {
                                LazyJsonObject jsonObjectDataTableRowValuesOriginal = (LazyJsonObject)jsonPropertyTokenExtractor.Token;

                                foreach (KeyValuePair<String, Tuple<Type, LazyJsonDeserializerBase>> keyValuePair in jsonDeserializerDictionary)
                                {
                                    jsonPropertyTokenExtractor = jsonObjectDataTableRowValuesOriginal[keyValuePair.Key];
                                    if (jsonPropertyTokenExtractor != null)
                                    {
                                        Object value = keyValuePair.Value.Item2.Deserialize(jsonPropertyTokenExtractor, keyValuePair.Value.Item1, jsonDeserializerOptions);
                                        dataRow[keyValuePair.Key] = value != null ? value : DBNull.Value;
                                    }
                                }

                                dataRow.AcceptChanges();
                            }
                        }

                        // Values Current
                        if (dataRowState != DataRowState.Deleted)
                        {
                            jsonPropertyTokenExtractor = jsonObjectDataTableRowValues["Current"];
                            if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.Object)
                            {
                                LazyJsonObject jsonObjectDataTableRowValuesCurrent = (LazyJsonObject)jsonPropertyTokenExtractor.Token;

                                foreach (KeyValuePair<String, Tuple<Type, LazyJsonDeserializerBase>> keyValuePair in jsonDeserializerDictionary)
                                {
                                    jsonPropertyTokenExtractor = jsonObjectDataTableRowValuesCurrent[keyValuePair.Key];
                                    if (jsonPropertyTokenExtractor != null)
                                    {
                                        Object value = keyValuePair.Value.Item2.Deserialize(jsonPropertyTokenExtractor, keyValuePair.Value.Item1, jsonDeserializerOptions);
                                        dataRow[keyValuePair.Key] = value != null ? value : DBNull.Value;
                                    }
                                }
                            }
                        }
                    }

                    switch (dataRowState)
                    {
                        case DataRowState.Added: dataRow.AcceptChanges(); dataRow.SetAdded(); break;
                        case DataRowState.Deleted: dataRow.AcceptChanges(); dataRow.Delete(); break;
                        case DataRowState.Unchanged: dataRow.AcceptChanges(); break;
                    }
                }
            }
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

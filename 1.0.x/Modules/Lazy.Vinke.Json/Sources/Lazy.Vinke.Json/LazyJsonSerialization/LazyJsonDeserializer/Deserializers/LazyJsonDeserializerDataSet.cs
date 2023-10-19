// LazyJsonDeserializerDataSet.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 16

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerDataSet : LazyJsonDeserializerBase
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
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Object && dataType != null && dataType.IsAssignableTo(typeof(DataSet)) == true)
            {
                LazyJsonProperty jsonPropertyTokenExtractor = null;

                DataSet dataSet = (DataSet)Activator.CreateInstance(dataType);

                LazyJsonObject jsonObjectDataSet = (LazyJsonObject)jsonToken;

                jsonPropertyTokenExtractor = jsonObjectDataSet["Name"];
                if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.String)
                {
                    LazyJsonString jsonStringDataSetName = (LazyJsonString)jsonPropertyTokenExtractor.Token;

                    if (jsonStringDataSetName != null && jsonStringDataSetName.Value != null)
                        dataSet.DataSetName = jsonStringDataSetName.Value;
                }

                jsonPropertyTokenExtractor = jsonObjectDataSet["Tables"];
                if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.Array)
                {
                    LazyJsonArray jsonArrayDataSetTables = (LazyJsonArray)jsonPropertyTokenExtractor.Token;

                    if (jsonArrayDataSetTables != null)
                    {
                        for (int index = 0; index < jsonArrayDataSetTables.Length; index++)
                        {
                            Type dataTableType = typeof(DataTable);

                            LazyJsonObject jsonObjectDataTable = (LazyJsonObject)jsonArrayDataSetTables[index];

                            jsonPropertyTokenExtractor = jsonObjectDataTable["Type"];
                            if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.Object)
                                dataTableType = (Type)new LazyJsonDeserializerType().Deserialize(jsonPropertyTokenExtractor, typeof(Type), jsonDeserializerOptions);

                            jsonPropertyTokenExtractor = jsonObjectDataTable["Value"];
                            if (jsonPropertyTokenExtractor != null && jsonPropertyTokenExtractor.Token.Type == LazyJsonType.Object)
                            {
                                Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(dataTableType, jsonDeserializerOptions);

                                if (jsonDeserializerType == null)
                                    jsonDeserializerType = typeof(LazyJsonDeserializerDataTable);

                                LazyJsonDeserializerBase jsonDeserializer = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);

                                Object data = jsonDeserializer.Deserialize(jsonPropertyTokenExtractor, dataTableType, jsonDeserializerOptions);

                                if (data != null && data is DataTable)
                                {
                                    DataTable dataTable = (DataTable)data;

                                    if (dataSet.Tables.Contains(dataTable.TableName) == false)
                                        dataSet.Tables.Add(dataTable);
                                }
                            }
                        }
                    }
                }

                return dataSet;
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

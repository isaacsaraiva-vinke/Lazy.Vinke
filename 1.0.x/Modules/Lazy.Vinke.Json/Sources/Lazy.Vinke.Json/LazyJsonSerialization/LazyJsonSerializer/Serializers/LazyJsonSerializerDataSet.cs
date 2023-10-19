// LazyJsonSerializerDataSet.cs
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
    public class LazyJsonSerializerDataSet : LazyJsonSerializerBase
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
            if (data != null && data is DataSet)
            {
                DataSet dataSet = (DataSet)data;

                LazyJsonObject jsonObjectDataSet = new LazyJsonObject();

                jsonObjectDataSet.Add(new LazyJsonProperty("Name", new LazyJsonString(dataSet.DataSetName)));

                LazyJsonArray jsonArrayDataSetTables = new LazyJsonArray();
                jsonObjectDataSet.Add(new LazyJsonProperty("Tables", jsonArrayDataSetTables));

                foreach (DataTable dataTable in dataSet.Tables)
                {
                    Type dataTableType = dataTable.GetType();

                    Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(dataTableType, jsonSerializerOptions);

                    if (jsonSerializerType == null)
                        jsonSerializerType = typeof(LazyJsonSerializerDataTable);

                    LazyJsonSerializerBase jsonSerializer = (LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType);

                    LazyJsonObject jsonObjectDataTable = new LazyJsonObject();

                    if (dataTableType != typeof(DataTable))
                        jsonObjectDataTable.Add(new LazyJsonProperty("Type", new LazyJsonSerializerType().Serialize(dataTableType, jsonSerializerOptions)));

                    jsonObjectDataTable.Add(new LazyJsonProperty("Value", jsonSerializer.Serialize(dataTable, jsonSerializerOptions)));

                    jsonArrayDataSetTables.Add(jsonObjectDataTable);
                }

                return jsonObjectDataSet;
            }

            return new LazyJsonNull();
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

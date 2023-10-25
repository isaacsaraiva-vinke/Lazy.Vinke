// LazyJsonSerializerArray.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 11

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerArray : LazyJsonSerializerBase
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
            if (data != null)
            {
                Type dataType = data.GetType();

                if (dataType.IsArray == true)
                {
                    Array dataArray = (data as Array);
                    LazyJsonArray jsonArray = new LazyJsonArray();

                    Type dataArrayElementType = dataType.GetElementType();

                    LazyJsonSerializerBase jsonSerializer = null;
                    LazyJsonSerializeTokenEventHandler jsonSerializeTokenEventHandler = null;
                    LazyJsonSerializer.SelectSerializeTokenEventHandler(dataArrayElementType, out jsonSerializer, out jsonSerializeTokenEventHandler, jsonSerializerOptions);

                    foreach (Object item in dataArray)
                        jsonArray.Add(jsonSerializeTokenEventHandler(item, jsonSerializerOptions));

                    return jsonArray;
                }
            }

            return new LazyJsonNull();
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

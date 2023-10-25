// LazyJsonDeserializerArray.cs
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
    public class LazyJsonDeserializerArray : LazyJsonDeserializerBase
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
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Array && dataType != null && dataType.IsArray == true)
            {
                LazyJsonArray jsonArray = (LazyJsonArray)jsonToken;

                Type dataArrayElementType = dataType.GetElementType();
                Array dataArray = Array.CreateInstance(dataArrayElementType, jsonArray.Length);

                LazyJsonDeserializerBase jsonDeserializer = null;
                LazyJsonDeserializeTokenEventHandler jsonDeserializeTokenEventHandler = null;
                LazyJsonDeserializer.SelectDeserializeTokenEventHandler(dataArrayElementType, out jsonDeserializer, out jsonDeserializeTokenEventHandler, jsonDeserializerOptions);

                for (int index = 0; index < jsonArray.Length; index++)
                    dataArray.SetValue(jsonDeserializeTokenEventHandler(jsonArray[index], dataArrayElementType, jsonDeserializerOptions), index);

                return dataArray;
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

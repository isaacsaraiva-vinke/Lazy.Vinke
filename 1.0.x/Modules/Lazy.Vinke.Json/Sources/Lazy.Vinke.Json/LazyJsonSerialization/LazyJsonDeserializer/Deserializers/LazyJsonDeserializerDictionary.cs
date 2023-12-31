﻿// LazyJsonDeserializerDictionary.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 11

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerDictionary : LazyJsonDeserializerBase
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
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Array && dataType != null && dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                LazyJsonArray jsonArray = (LazyJsonArray)jsonToken;

                Object dataDictionary = Activator.CreateInstance(dataType);
                MethodInfo methodInfoAdd = dataType.GetMethods().First(x => x.Name == "Add");

                LazyJsonDeserializerBase jsonDeserializerKeys = null;
                LazyJsonDeserializeTokenEventHandler jsonDeserializeTokenEventHandlerKeys = null;
                LazyJsonDeserializer.SelectDeserializeTokenEventHandler(dataType.GenericTypeArguments[0], out jsonDeserializerKeys, out jsonDeserializeTokenEventHandlerKeys, jsonDeserializerOptions);

                LazyJsonDeserializerBase jsonDeserializerValues = null;
                LazyJsonDeserializeTokenEventHandler jsonDeserializeTokenEventHandlerValues = null;
                LazyJsonDeserializer.SelectDeserializeTokenEventHandler(dataType.GenericTypeArguments[1], out jsonDeserializerValues, out jsonDeserializeTokenEventHandlerValues, jsonDeserializerOptions);

                for (int index = 0; index < jsonArray.Length; index++)
                {
                    if (jsonArray[index].Type == LazyJsonType.Array)
                    {
                        LazyJsonArray jsonArrayKeyValuePair = (LazyJsonArray)jsonArray[index];

                        if (jsonArrayKeyValuePair.Length == 2)
                        {
                            Object key = jsonDeserializeTokenEventHandlerKeys(jsonArrayKeyValuePair[0], dataType.GenericTypeArguments[0], jsonDeserializerOptions);
                            Object value = jsonDeserializeTokenEventHandlerValues(jsonArrayKeyValuePair[1], dataType.GenericTypeArguments[1], jsonDeserializerOptions);

                            methodInfoAdd.Invoke(dataDictionary, new Object[] { key, value });
                        }
                    }
                }

                return dataDictionary;
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

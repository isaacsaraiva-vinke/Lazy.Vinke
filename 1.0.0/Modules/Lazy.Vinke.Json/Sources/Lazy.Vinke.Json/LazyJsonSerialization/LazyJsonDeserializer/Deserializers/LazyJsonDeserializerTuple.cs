// LazyJsonDeserializerTuple.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 19

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerTuple : LazyJsonDeserializerBase
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
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Array && dataType != null && dataType.IsGenericType == true && dataType.IsAssignableTo(typeof(ITuple)) == true)
            {
                LazyJsonArray jsonArray = (LazyJsonArray)jsonToken;

                if (jsonArray.Length == dataType.GenericTypeArguments.Length && jsonArray.Length > 0)
                {
                    Object[] tupleValuesArray = new Object[jsonArray.Length];

                    for (int index = 0; index < jsonArray.Length; index++)
                    {
                        Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(dataType.GenericTypeArguments[index], jsonDeserializerOptions);

                        if (jsonDeserializerType != null)
                        {
                            LazyJsonDeserializerBase jsonDeserializer = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);
                            tupleValuesArray[index] = jsonDeserializer.Deserialize(jsonArray[index], dataType.GenericTypeArguments[index], jsonDeserializerOptions);
                        }
                        else
                        {
                            tupleValuesArray[index] = LazyJsonDeserializer.DeserializeToken(jsonArray[index], dataType.GenericTypeArguments[index], jsonDeserializerOptions);
                        }
                    }

                    return Activator.CreateInstance(dataType, tupleValuesArray);
                }
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

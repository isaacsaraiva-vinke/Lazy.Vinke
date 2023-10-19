// LazyJsonDeserializerList.cs
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
    public class LazyJsonDeserializerList : LazyJsonDeserializerBase
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
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Array && dataType != null && dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(List<>))
            {
                LazyJsonArray jsonArray = (LazyJsonArray)jsonToken;

                Object dataList = Activator.CreateInstance(dataType);
                MethodInfo methodInfoAdd = dataType.GetMethods().First(x => x.Name == "Add");

                LazyJsonDeserializerBase jsonDeserializer = null;
                LazyJsonDeserializeTokenEventHandler jsonDeserializeTokenEventHandler = null;

                Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(dataType.GenericTypeArguments[0], jsonDeserializerOptions);

                if (jsonDeserializerType != null)
                {
                    jsonDeserializer = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);
                    jsonDeserializeTokenEventHandler = new LazyJsonDeserializeTokenEventHandler(jsonDeserializer.Deserialize);
                }
                else
                {
                    jsonDeserializeTokenEventHandler = new LazyJsonDeserializeTokenEventHandler(LazyJsonDeserializer.DeserializeToken);
                }

                for (int index = 0; index < jsonArray.Length; index++)
                    methodInfoAdd.Invoke(dataList, new Object[] { jsonDeserializeTokenEventHandler(jsonArray[index], dataType.GenericTypeArguments[0], jsonDeserializerOptions) });

                return dataList;
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

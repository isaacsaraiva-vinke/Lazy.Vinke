// LazyJsonSerializerDictionary.cs
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
    public class LazyJsonSerializerDictionary : LazyJsonSerializerBase
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

                if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                {
                    LazyJsonArray jsonArray = new LazyJsonArray();

                    Object collection = null;
                    Int32 count = (Int32)dataType.GetProperties().First(x => x.Name == "Count").GetValue(data);

                    Array keysArray = Array.CreateInstance(dataType.GenericTypeArguments[0], count);
                    collection = dataType.GetProperties().First(x => x.Name == "Keys").GetValue(data);
                    collection.GetType().GetMethods().First(x => x.Name == "CopyTo").Invoke(collection, new Object[] { keysArray, 0 });

                    Array valuesArray = Array.CreateInstance(dataType.GenericTypeArguments[1], count);
                    collection = dataType.GetProperties().First(x => x.Name == "Values").GetValue(data);
                    collection.GetType().GetMethods().First(x => x.Name == "CopyTo").Invoke(collection, new Object[] { valuesArray, 0 });

                    LazyJsonSerializerBase jsonSerializerKeys = null;
                    LazyJsonSerializeTokenEventHandler jsonSerializeTokenEventHandlerKeys = null;
                    LazyJsonSerializer.SelectSerializeTokenEventHandler(dataType.GenericTypeArguments[0], out jsonSerializerKeys, out jsonSerializeTokenEventHandlerKeys, jsonSerializerOptions);

                    LazyJsonSerializerBase jsonSerializerValues = null;
                    LazyJsonSerializeTokenEventHandler jsonSerializeTokenEventHandlerValues = null;
                    LazyJsonSerializer.SelectSerializeTokenEventHandler(dataType.GenericTypeArguments[1], out jsonSerializerValues, out jsonSerializeTokenEventHandlerValues, jsonSerializerOptions);

                    for (int index = 0; index < count; index++)
                    {
                        LazyJsonArray jsonArrayKeyValuePair = new LazyJsonArray();
                        jsonArrayKeyValuePair.Add(jsonSerializeTokenEventHandlerKeys(keysArray.GetValue(index), jsonSerializerOptions));
                        jsonArrayKeyValuePair.Add(jsonSerializeTokenEventHandlerValues(valuesArray.GetValue(index), jsonSerializerOptions));
                        jsonArray.Add(jsonArrayKeyValuePair);
                    }

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

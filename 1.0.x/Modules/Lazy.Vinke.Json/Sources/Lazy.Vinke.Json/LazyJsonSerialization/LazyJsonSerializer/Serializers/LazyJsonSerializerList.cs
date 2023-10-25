// LazyJsonSerializerList.cs
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
    public class LazyJsonSerializerList : LazyJsonSerializerBase
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

                if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    LazyJsonArray jsonArray = new LazyJsonArray();

                    Int32 count = (Int32)dataType.GetProperties().First(x => x.Name == "Count").GetValue(data);
                    PropertyInfo propertyInfoIndexer = dataType.GetProperties().First(x => x.GetIndexParameters().Length == 1 && x.GetIndexParameters()[0].ParameterType == typeof(Int32));

                    LazyJsonSerializerBase jsonSerializer = null;
                    LazyJsonSerializeTokenEventHandler jsonSerializeTokenEventHandler = null;
                    LazyJsonSerializer.SelectSerializeTokenEventHandler(dataType.GenericTypeArguments[0], out jsonSerializer, out jsonSerializeTokenEventHandler, jsonSerializerOptions);

                    for (int index = 0; index < count; index++)
                        jsonArray.Add(jsonSerializeTokenEventHandler(propertyInfoIndexer.GetValue(data, new Object[] { index }), jsonSerializerOptions));

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

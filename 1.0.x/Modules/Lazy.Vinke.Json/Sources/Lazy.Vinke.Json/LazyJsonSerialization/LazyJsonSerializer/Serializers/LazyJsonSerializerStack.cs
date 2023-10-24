// LazyJsonSerializerStack.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 24

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerStack : LazyJsonSerializerBase
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

                if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(Stack<>))
                {
                    Int32 count = (Int32)dataType.GetProperties().First(x => x.Name == "Count").GetValue(data);
                    MethodInfo methodInfoPop = dataType.GetMethods().First(x => x.Name == "Pop");

                    LazyJsonArray jsonArray = new LazyJsonArray();

                    for (int index = 0; index < count; index++)
                        jsonArray.Add(new LazyJsonNull());

                    LazyJsonSerializerBase jsonSerializer = null;
                    LazyJsonSerializeTokenEventHandler jsonSerializeTokenEventHandler = null;

                    Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(dataType.GenericTypeArguments[0], jsonSerializerOptions);

                    if (jsonSerializerType != null)
                    {
                        jsonSerializer = (LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType);
                        jsonSerializeTokenEventHandler = new LazyJsonSerializeTokenEventHandler(jsonSerializer.Serialize);
                    }
                    else
                    {
                        jsonSerializeTokenEventHandler = new LazyJsonSerializeTokenEventHandler(LazyJsonSerializer.SerializeToken);
                    }

                    LazyJsonSerializerOptions options = jsonSerializerOptions != null ? jsonSerializerOptions : new LazyJsonSerializerOptions();
                    LazyJsonSerializerOptionsStack optionsStack = options.Contains<LazyJsonSerializerOptionsStack>() == true ? options.Item<LazyJsonSerializerOptionsStack>() : new LazyJsonSerializerOptionsStack();

                    if (optionsStack.WriteReverse == true)
                    {
                        for (int index = (count - 1); index >= 0; index--)
                            jsonArray[index] = jsonSerializeTokenEventHandler(methodInfoPop.Invoke(data, null), jsonSerializerOptions);
                    }
                    else
                    {
                        for (int index = 0; index < count; index++)
                            jsonArray[index] = jsonSerializeTokenEventHandler(methodInfoPop.Invoke(data, null), jsonSerializerOptions);
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

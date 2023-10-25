// LazyJsonDeserializerStack.cs
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
    public class LazyJsonDeserializerStack : LazyJsonDeserializerBase
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
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Array && dataType != null && dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(Stack<>))
            {
                LazyJsonArray jsonArray = (LazyJsonArray)jsonToken;

                Object dataStack = Activator.CreateInstance(dataType);
                MethodInfo methodInfoPush = dataType.GetMethods().First(x => x.Name == "Push");

                LazyJsonDeserializerBase jsonDeserializer = null;
                LazyJsonDeserializeTokenEventHandler jsonDeserializeTokenEventHandler = null;
                LazyJsonDeserializer.SelectDeserializeTokenEventHandler(dataType.GenericTypeArguments[0], out jsonDeserializer, out jsonDeserializeTokenEventHandler, jsonDeserializerOptions);

                LazyJsonDeserializerOptions options = jsonDeserializerOptions != null ? jsonDeserializerOptions : new LazyJsonDeserializerOptions();
                LazyJsonDeserializerOptionsStack optionsStack = options.Contains<LazyJsonDeserializerOptionsStack>() == true ? options.Item<LazyJsonDeserializerOptionsStack>() : new LazyJsonDeserializerOptionsStack();

                if (optionsStack.ReadReverse == true)
                {
                    for (int index = (jsonArray.Length - 1); index >= 0; index--)
                        methodInfoPush.Invoke(dataStack, new Object[] { jsonDeserializeTokenEventHandler(jsonArray[index], dataType.GenericTypeArguments[0], jsonDeserializerOptions) });
                }
                else
                {
                    for (int index = 0; index < jsonArray.Length; index++)
                        methodInfoPush.Invoke(dataStack, new Object[] { jsonDeserializeTokenEventHandler(jsonArray[index], dataType.GenericTypeArguments[0], jsonDeserializerOptions) });
                }

                return dataStack;
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

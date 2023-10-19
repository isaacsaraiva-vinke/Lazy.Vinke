// LazyJsonDeserializerType.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 15

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerType : LazyJsonDeserializerBase
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
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Object && dataType != null && dataType == typeof(Type))
            {
                try
                {
                    LazyJsonObject jsonObject = (LazyJsonObject)jsonToken;
                    String typeAssembly = ((LazyJsonString)jsonObject["Assembly"].Token).Value;
                    String typeNamespace = ((LazyJsonString)jsonObject["Namespace"].Token).Value;
                    String typeClass = ((LazyJsonString)jsonObject["Class"].Token).Value;

                    Type type = Assembly.Load(typeAssembly).GetType(typeNamespace + "." + typeClass);

                    if (type.IsGenericType == true)
                    {
                        LazyJsonProperty jsonPropertyArgumentTypes = jsonObject["Arguments"];
                        if (jsonPropertyArgumentTypes != null && jsonPropertyArgumentTypes.Token.Type == LazyJsonType.Array)
                            type = DeserializeGenericType((LazyJsonArray)jsonPropertyArgumentTypes.Token, type, jsonDeserializerOptions);
                    }

                    return type;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Deserialize the json argument type array to a generic type
        /// </summary>
        /// <param name="jsonArrayArgumentTypes">The json argument type array</param>
        /// <param name="genericTypeDefinition">The generic type definition</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The generic type</returns>
        private Type DeserializeGenericType(LazyJsonArray jsonArrayArgumentTypes, Type genericTypeDefinition, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            Type[] argumentTypeArray = new Type[jsonArrayArgumentTypes.Length];

            for (int index = 0; index < jsonArrayArgumentTypes.Length; index++)
                argumentTypeArray[index] = (Type)Deserialize(jsonArrayArgumentTypes[index], typeof(Type), jsonDeserializerOptions);

            return genericTypeDefinition.MakeGenericType(argumentTypeArray);
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

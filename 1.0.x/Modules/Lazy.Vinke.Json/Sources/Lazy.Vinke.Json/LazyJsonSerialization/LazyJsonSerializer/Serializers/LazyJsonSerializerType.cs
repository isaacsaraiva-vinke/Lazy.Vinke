// LazyJsonSerializerType.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 15

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerType : LazyJsonSerializerBase
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
            if (data != null && data.GetType().IsAssignableTo(typeof(Type)) == true)
            {
                Type type = (Type)data;

                LazyJsonObject jsonObject = new LazyJsonObject();
                jsonObject.Add(new LazyJsonProperty("Assembly", new LazyJsonString(type.Assembly.GetName().Name)));
                jsonObject.Add(new LazyJsonProperty("Namespace", new LazyJsonString(type.Namespace)));
                jsonObject.Add(new LazyJsonProperty("Class", new LazyJsonString(type.Name)));

                if (type.IsGenericType == true)
                    jsonObject.Add(new LazyJsonProperty("Arguments", SerializeGenericType(type, jsonSerializerOptions)));

                return jsonObject;
            }

            return new LazyJsonNull();
        }

        /// <summary>
        /// Serialize a generic type arguments to a json argument type array
        /// </summary>
        /// <param name="genericType">The generic type</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The json argument type array</returns>
        private LazyJsonArray SerializeGenericType(Type genericType, LazyJsonSerializerOptions jsonSerializerOptions = null)
        {
            LazyJsonArray jsonArrayArgumentTypes = new LazyJsonArray();
            foreach (Type argumentType in genericType.GetGenericArguments())
                jsonArrayArgumentTypes.Add(Serialize(argumentType, jsonSerializerOptions));

            return jsonArrayArgumentTypes;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

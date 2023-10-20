// LazyJsonDeserializerObject.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 20

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerObject : LazyJsonDeserializerBase
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
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Object && dataType != null && dataType == typeof(Object))
            {
                LazyJsonProperty jsonPropertyType = ((LazyJsonObject)jsonToken)["Type"];
                LazyJsonProperty jsonPropertyValue = ((LazyJsonObject)jsonToken)["Value"];

                if (jsonPropertyType != null && jsonPropertyValue != null)
                {
                    Type unwrappedType = (Type)new LazyJsonDeserializerType().Deserialize(jsonPropertyType, typeof(Type), jsonDeserializerOptions);

                    if (unwrappedType != null)
                        return LazyJsonDeserializer.DeserializeProperty(jsonPropertyValue, unwrappedType, jsonDeserializerOptions);
                }
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

// LazyJsonDeserializerObject.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 11

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
            if (jsonToken != null && dataType != null && dataType == typeof(Object))
            {
                /* dataType is useless here because its an "Object" type */

                if (jsonToken.Type == LazyJsonType.Null) return null;
                if (jsonToken.Type == LazyJsonType.Integer) return new LazyJsonDeserializerInteger().Deserialize(jsonToken, typeof(Int64), jsonDeserializerOptions);
                if (jsonToken.Type == LazyJsonType.Decimal) return new LazyJsonDeserializerDecimal().Deserialize(jsonToken, typeof(Decimal), jsonDeserializerOptions);
                if (jsonToken.Type == LazyJsonType.Boolean) return new LazyJsonDeserializerBoolean().Deserialize(jsonToken, typeof(Boolean), jsonDeserializerOptions);
                if (jsonToken.Type == LazyJsonType.Array) return new LazyJsonDeserializerArray().Deserialize(jsonToken, typeof(Object[]), jsonDeserializerOptions);

                /* Unable to deserialize token of type "JsonObject" and dataType "Object" */
                if (jsonToken.Type == LazyJsonType.Object) return null;

                if (jsonToken.Type == LazyJsonType.String)
                {
                    Object deserializedValue = new LazyJsonDeserializerDateTime().Deserialize(jsonToken, typeof(DateTime), jsonDeserializerOptions);

                    if (deserializedValue == null)
                        deserializedValue = new LazyJsonDeserializerString().Deserialize(jsonToken, typeof(String), jsonDeserializerOptions);

                    return deserializedValue;
                }
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

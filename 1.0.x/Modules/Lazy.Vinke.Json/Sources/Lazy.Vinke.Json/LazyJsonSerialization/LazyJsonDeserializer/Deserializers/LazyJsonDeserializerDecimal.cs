// LazyJsonDeserializerDecimal.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 07

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerDecimal : LazyJsonDeserializerBase
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
            if (jsonToken != null && dataType != null)
            {
                if (jsonToken.Type == LazyJsonType.Null)
                {
                    if (dataType == typeof(Decimal)) return 0.0m;
                    if (dataType == typeof(Double)) return 0.0d;
                    if (dataType == typeof(Single)) return 0.0f;
                    if (dataType == typeof(Nullable<Decimal>)) return null;
                    if (dataType == typeof(Nullable<Double>)) return null;
                    if (dataType == typeof(Nullable<Single>)) return null;
                }
                else if (jsonToken.Type == LazyJsonType.Decimal)
                {
                    LazyJsonDecimal jsonDecimal = (LazyJsonDecimal)jsonToken;

                    if (dataType == typeof(Decimal)) return jsonDecimal.Value == null ? 0.0m : Convert.ToDecimal(jsonDecimal.Value);
                    if (dataType == typeof(Double)) return jsonDecimal.Value == null ? 0.0d : Convert.ToDouble(jsonDecimal.Value);
                    if (dataType == typeof(Single)) return jsonDecimal.Value == null ? 0.0f : (Single)jsonDecimal.Value;
                    if (dataType == typeof(Nullable<Decimal>)) return jsonDecimal.Value == null ? null : Convert.ToDecimal(jsonDecimal.Value);
                    if (dataType == typeof(Nullable<Double>)) return jsonDecimal.Value == null ? null : Convert.ToDouble(jsonDecimal.Value);
                    if (dataType == typeof(Nullable<Single>)) return jsonDecimal.Value == null ? null : (Single)jsonDecimal.Value;
                }
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

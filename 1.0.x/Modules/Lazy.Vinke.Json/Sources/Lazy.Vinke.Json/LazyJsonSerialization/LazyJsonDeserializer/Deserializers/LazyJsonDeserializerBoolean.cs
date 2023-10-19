// LazyJsonDeserializerBoolean.cs
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
    public class LazyJsonDeserializerBoolean : LazyJsonDeserializerBase
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
                    if (dataType == typeof(Boolean)) return false;
                    if (dataType == typeof(Nullable<Boolean>)) return null;
                }
                else if (jsonToken.Type == LazyJsonType.Boolean)
                {
                    LazyJsonBoolean jsonBoolean = (LazyJsonBoolean)jsonToken;

                    if (dataType == typeof(Boolean)) return jsonBoolean.Value == null ? false : Convert.ToBoolean(jsonBoolean.Value);
                    if (dataType == typeof(Nullable<Boolean>)) return jsonBoolean.Value;
                }
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

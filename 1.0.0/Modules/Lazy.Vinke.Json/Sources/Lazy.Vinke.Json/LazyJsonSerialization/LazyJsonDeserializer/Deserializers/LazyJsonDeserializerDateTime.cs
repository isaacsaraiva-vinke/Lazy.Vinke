// LazyJsonDeserializerDateTime.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 08

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerDateTime : LazyJsonDeserializerBase
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
            if (jsonToken != null && dataType != null && (dataType == typeof(DateTime) || dataType == typeof(Nullable<DateTime>)))
            {
                if (jsonToken.Type == LazyJsonType.String)
                {
                    LazyJsonString jsonString = (LazyJsonString)jsonToken;

                    if (jsonString.Value != null)
                    {
                        if (jsonDeserializerOptions == null)
                            jsonDeserializerOptions = new LazyJsonDeserializerOptions();

                        LazyJsonDeserializerOptionsDateTime jsonDeserializerOptionsDateTime = jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsDateTime>();

                        DateTime dateTime = DateTime.MinValue;

                        if (DateTime.TryParseExact(jsonString.Value, jsonDeserializerOptionsDateTime.Format, jsonDeserializerOptionsDateTime.CultureInfo, jsonDeserializerOptionsDateTime.DateTimeStyles, out dateTime) == true)
                            return dateTime;
                    }
                }
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

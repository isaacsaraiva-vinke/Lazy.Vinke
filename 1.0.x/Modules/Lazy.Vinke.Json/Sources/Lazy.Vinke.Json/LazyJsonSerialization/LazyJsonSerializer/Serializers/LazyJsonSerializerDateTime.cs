// LazyJsonSerializerDateTime.cs
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
    public class LazyJsonSerializerDateTime : LazyJsonSerializerBase
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
                if (data.GetType() == typeof(DateTime))
                {
                    LazyJsonSerializerOptions options = jsonSerializerOptions != null ? jsonSerializerOptions : new LazyJsonSerializerOptions();
                    LazyJsonSerializerOptionsDateTime optionsDateTime = options.Contains<LazyJsonSerializerOptionsDateTime>() == true ? options.Item<LazyJsonSerializerOptionsDateTime>() : new LazyJsonSerializerOptionsDateTime();

                    return new LazyJsonString(((DateTime)data).ToString(optionsDateTime.Format));
                }
            }

            return new LazyJsonString(null);
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

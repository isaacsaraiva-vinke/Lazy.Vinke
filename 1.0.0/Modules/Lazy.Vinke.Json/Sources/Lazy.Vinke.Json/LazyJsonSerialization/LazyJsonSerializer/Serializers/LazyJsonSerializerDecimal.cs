// LazyJsonSerializerDecimal.cs
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
    public class LazyJsonSerializerDecimal : LazyJsonSerializerBase
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

                if (dataType == typeof(Decimal)) return new LazyJsonDecimal(Convert.ToDecimal(data));
                if (dataType == typeof(Double)) return new LazyJsonDecimal(Convert.ToDecimal(data));
                if (dataType == typeof(Single)) return new LazyJsonDecimal(Convert.ToDecimal(data));
            }

            return new LazyJsonDecimal(null);
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

﻿// LazyJsonSerializerInteger.cs
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
    public class LazyJsonSerializerInteger : LazyJsonSerializerBase
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

                if (dataType == typeof(Int32)) return new LazyJsonInteger(Convert.ToInt64(data));
                if (dataType == typeof(Int16)) return new LazyJsonInteger(Convert.ToInt64(data));
                if (dataType == typeof(Int64)) return new LazyJsonInteger(Convert.ToInt64(data));
                if (dataType == typeof(Byte)) return new LazyJsonInteger(Convert.ToInt64(data));
                if (dataType == typeof(SByte)) return new LazyJsonInteger(Convert.ToInt64(data));
                if (dataType == typeof(UInt32)) return new LazyJsonInteger(Convert.ToInt64(data));
                if (dataType == typeof(UInt16)) return new LazyJsonInteger(Convert.ToInt64(data));
            }

            return new LazyJsonInteger(null);
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

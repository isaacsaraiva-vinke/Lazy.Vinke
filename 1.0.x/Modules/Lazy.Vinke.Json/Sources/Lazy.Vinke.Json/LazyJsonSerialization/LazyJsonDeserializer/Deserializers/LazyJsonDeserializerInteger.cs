// LazyJsonDeserializerInteger.cs
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
    public class LazyJsonDeserializerInteger : LazyJsonDeserializerBase
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
                    if (dataType == typeof(Int32)) return (Int32)0;
                    if (dataType == typeof(Int16)) return (Int16)0;
                    if (dataType == typeof(Int64)) return (Int64)0;
                    if (dataType == typeof(Byte)) return (Byte)0;
                    if (dataType == typeof(SByte)) return (SByte)0;
                    if (dataType == typeof(UInt32)) return (UInt32)0;
                    if (dataType == typeof(UInt16)) return (UInt16)0;
                    if (dataType == typeof(Nullable<Int32>)) return null;
                    if (dataType == typeof(Nullable<Int16>)) return null;
                    if (dataType == typeof(Nullable<Int64>)) return null;
                    if (dataType == typeof(Nullable<Byte>)) return null;
                    if (dataType == typeof(Nullable<SByte>)) return null;
                    if (dataType == typeof(Nullable<UInt32>)) return null;
                    if (dataType == typeof(Nullable<UInt16>)) return null;
                }
                else if (jsonToken.Type == LazyJsonType.Integer)
                {
                    LazyJsonInteger jsonInteger = (LazyJsonInteger)jsonToken;

                    if (dataType == typeof(Int32)) return jsonInteger.Value == null ? (Int32)0 : Convert.ToInt32(jsonInteger.Value);
                    if (dataType == typeof(Int16)) return jsonInteger.Value == null ? (Int16)0 : Convert.ToInt16(jsonInteger.Value);
                    if (dataType == typeof(Int64)) return jsonInteger.Value == null ? (Int64)0 : Convert.ToInt64(jsonInteger.Value);
                    if (dataType == typeof(Byte)) return jsonInteger.Value == null ? (Byte)0 : Convert.ToByte(jsonInteger.Value);
                    if (dataType == typeof(SByte)) return jsonInteger.Value == null ? (SByte)0 : Convert.ToSByte(jsonInteger.Value);
                    if (dataType == typeof(UInt32)) return jsonInteger.Value == null ? (UInt32)0 : Convert.ToUInt32(jsonInteger.Value);
                    if (dataType == typeof(UInt16)) return jsonInteger.Value == null ? (UInt16)0 : Convert.ToUInt16(jsonInteger.Value);
                    if (dataType == typeof(Nullable<Int32>)) return jsonInteger.Value == null ? null : Convert.ToInt32(jsonInteger.Value);
                    if (dataType == typeof(Nullable<Int16>)) return jsonInteger.Value == null ? null : Convert.ToInt16(jsonInteger.Value);
                    if (dataType == typeof(Nullable<Int64>)) return jsonInteger.Value == null ? null : Convert.ToInt64(jsonInteger.Value);
                    if (dataType == typeof(Nullable<Byte>)) return jsonInteger.Value == null ? null : Convert.ToByte(jsonInteger.Value);
                    if (dataType == typeof(Nullable<SByte>)) return jsonInteger.Value == null ? null : Convert.ToSByte(jsonInteger.Value);
                    if (dataType == typeof(Nullable<UInt32>)) return jsonInteger.Value == null ? null : Convert.ToUInt32(jsonInteger.Value);
                    if (dataType == typeof(Nullable<UInt16>)) return jsonInteger.Value == null ? null : Convert.ToUInt16(jsonInteger.Value);
                }
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

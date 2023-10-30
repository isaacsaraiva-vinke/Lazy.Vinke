// LazyDatabaseType.cs
//
// This file is integrated part of "Lazy Vinke Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 23

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Database
{
    public static class LazyDatabaseType
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Convert the system type to the lazy database type
        /// </summary>
        /// <param name="systemType">The system type</param>
        /// <returns>The lazy database type</returns>
        public static LazyDbType FromSystemType(Type systemType)
        {
            if (systemType != null)
            {
                if (systemType == typeof(DBNull)) return LazyDbType.DBNull;
                if (systemType == typeof(Char)) return LazyDbType.Char;
                if (systemType == typeof(String)) return LazyDbType.VarChar;
                if (systemType == typeof(SByte)) return LazyDbType.Byte;
                if (systemType == typeof(Int16)) return LazyDbType.Int16;
                if (systemType == typeof(Int32)) return LazyDbType.Int32;
                if (systemType == typeof(Int64)) return LazyDbType.Int64;
                if (systemType == typeof(Byte)) return LazyDbType.UByte;
                if (systemType == typeof(Single)) return LazyDbType.Float;
                if (systemType == typeof(Double)) return LazyDbType.Double;
                if (systemType == typeof(Decimal)) return LazyDbType.Decimal;
                if (systemType == typeof(DateTime)) return LazyDbType.DateTime;
                if (systemType == typeof(Byte[])) return LazyDbType.VarUByte;
            }

            return LazyDbType.DBNull;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

// LazyDbType.cs
//
// This file is integrated part of "Lazy Vinke Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 08

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Data
{
    public enum LazyDbType
    {
        DBNull = 0,
        // Reserved: 1-7
        Char = 8,
        // Reserved: 9-11
        VarChar = 12,
        // Reserved: 13
        VarText = 14,
        // Reserved: 15-19
        Byte = 20,
        // Reserved: 21
        Int16 = 22,
        // Reserved: 23
        Int32 = 24,
        // Reserved: 25
        Int64 = 26,
        // Reserved: 27
        // Reserved: 28-31
        UByte = 32,
        // Reserved: 33-39
        // Reserved: 40-43
        Float = 44,
        // Reserved: 45
        Double = 46,
        // Reserved: 47
        Decimal = 48,
        // Reserved: 49
        // Reserved: 50-51
        // Reserved: 52-55
        DateTime = 56,
        // Reserved: 57-63
        // Reserved: 64-71
        VarUByte = 72,
        // Reserved: 73-79
    }
}

// LazyJsonType.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, September 23

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public enum LazyJsonType
    {
        Null,
        Boolean,
        Integer,
        Decimal,
        String,
        Object,
        Array
    }
}

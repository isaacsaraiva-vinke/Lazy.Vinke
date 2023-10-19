// LazyJsonDecimal.cs
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
    public class LazyJsonDecimal : LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonDecimal()
        {
            this.Value = null;
            this.Type = LazyJsonType.Decimal;
        }

        public LazyJsonDecimal(Decimal? value)
        {
            this.Value = value;
            this.Type = LazyJsonType.Decimal;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Decimal? Value { get; set; }

        #endregion Properties
    }
}

// LazyJsonBoolean.cs
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
    public class LazyJsonBoolean : LazyJsonToken
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonBoolean()
        {
            this.Value = null;
            this.Type = LazyJsonType.Boolean;
        }

        public LazyJsonBoolean(Boolean? value)
        {
            this.Value = value;
            this.Type = LazyJsonType.Boolean;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Boolean? Value { get; set; }

        #endregion Properties
    }
}

// LazyJsonDeserializerOptionsStack.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 24

using System;
using System.IO;
using System.Data;
using System.Globalization;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerOptionsStack : LazyJsonDeserializerOptionsBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptionsStack()
        {
            this.ReadReverse = false;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Boolean ReadReverse { get; set; }

        #endregion Properties
    }
}

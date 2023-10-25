﻿// LazyJsonSerializerOptionsStack.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 24

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonSerializerOptionsStack : LazyJsonSerializerOptionsBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonSerializerOptionsStack()
        {
            this.WriteReverse = true;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Boolean WriteReverse { get; set; }

        #endregion Properties
    }
}

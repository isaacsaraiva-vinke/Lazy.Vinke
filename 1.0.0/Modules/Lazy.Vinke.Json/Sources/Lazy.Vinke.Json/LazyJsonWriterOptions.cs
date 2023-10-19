// LazyJsonWriterOptions.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 06

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonWriterOptions
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonWriterOptions()
        {
            this.Indent = false;
            this.IndentEmptyArray = false;
            this.IndentEmptyObject = false;
            this.IndentSize = 4;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Boolean Indent { get; set; }

        public Boolean IndentEmptyArray { get; set; }

        public Boolean IndentEmptyObject { get; set; }

        public Int32 IndentSize { get; set; }

        #endregion Properties
    }
}

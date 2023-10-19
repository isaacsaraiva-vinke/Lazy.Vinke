// LazyJson.cs
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
    public class LazyJson
    {
        #region Variables

        private LazyJsonToken root;

        #endregion Variables

        #region Constructors

        public LazyJson()
        {
            this.Root = new LazyJsonNull();
        }

        public LazyJson(LazyJsonToken jsonToken)
        {
            this.Root = jsonToken;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public LazyJsonToken Root
        {
            get { return this.root; }
            set { this.root = value != null ? value : new LazyJsonNull(); }
        }

        #endregion Properties
    }
}

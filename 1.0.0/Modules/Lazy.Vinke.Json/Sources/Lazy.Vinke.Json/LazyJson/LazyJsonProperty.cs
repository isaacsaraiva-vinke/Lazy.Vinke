// LazyJsonProperty.cs
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
    public class LazyJsonProperty
    {
        #region Consts

        public const string UNNAMED_PROPERTY = "UNNAMED_PROPERTY";

        #endregion Consts

        #region Variables

        private String name;
        private LazyJsonToken token;

        #endregion Variables

        #region Constructors

        public LazyJsonProperty(String name, LazyJsonToken jsonToken)
        {
            this.Name = name;
            this.Token = jsonToken;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Name
        {
            get { return this.name; }
            set { this.name = value != null ? value : UNNAMED_PROPERTY; }
        }

        public LazyJsonToken Token
        {
            get { return this.token; }
            set { this.token = value != null ? value : new LazyJsonNull(); }
        }

        #endregion Properties
    }
}

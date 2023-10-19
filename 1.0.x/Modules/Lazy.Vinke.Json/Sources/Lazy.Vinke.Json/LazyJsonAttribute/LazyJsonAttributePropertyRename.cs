// LazyJsonAttributePropertyRename.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 08

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    [AttributeUsage(AttributeTargets.Property)]
    public class LazyJsonAttributePropertyRename : LazyJsonAttributeProperty
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonAttributePropertyRename(String name)
        {
            this.Name = name;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Name { get; private set; }

        #endregion Properties
    }
}

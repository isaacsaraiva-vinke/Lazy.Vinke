// LazyJsonAttributeTypeSerializer.cs
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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class LazyJsonAttributeTypeSerializer : LazyJsonAttributeType
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonAttributeTypeSerializer(Type type)
            : base(type)
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

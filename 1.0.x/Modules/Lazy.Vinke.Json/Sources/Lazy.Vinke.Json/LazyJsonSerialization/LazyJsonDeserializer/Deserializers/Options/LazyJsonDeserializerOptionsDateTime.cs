// LazyJsonDeserializerOptionsDateTime.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 08

using System;
using System.IO;
using System.Data;
using System.Globalization;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerOptionsDateTime : LazyJsonDeserializerOptionsBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptionsDateTime()
        {
            this.Format = "yyyy-MM-ddTHH:mm:ss:fffZ";
            this.CultureInfo = CultureInfo.InvariantCulture;
            this.DateTimeStyles = DateTimeStyles.AdjustToUniversal;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public String Format { get; set; }

        public CultureInfo CultureInfo { get; set; }

        public DateTimeStyles DateTimeStyles { get; set; }

        #endregion Properties
    }
}

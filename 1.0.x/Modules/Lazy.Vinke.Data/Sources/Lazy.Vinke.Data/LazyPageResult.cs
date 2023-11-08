// LazyPageResult.cs
//
// This file is integrated part of "Lazy Vinke Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 08

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Data
{
    public class LazyPageResult
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyPageResult()
        {
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Int32 PageNum { get; set; }

        public Int32 PageSize { get; set; }

        public Int32 PageItems { get; set; }

        public Int32 PageCount { get; set; }

        public Int32 CurrentCount { get; set; }

        public Int32 TotalCount { get; set; }

        public Boolean HasNextPage { get; set; }

        public DataTable DataTable { get; set; }

        #endregion Properties
    }
}

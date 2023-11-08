// LazyPageData.cs
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
    public class LazyPageData
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyPageData()
        {
            this.PageNum = 1;
            this.PageSize = 512;
            this.OrderBy = "1"; // Means first column
        }

        public LazyPageData(Int32 pageNum, Int32 pageSize, String orderBy)
        {
            this.PageNum = pageNum;
            this.PageSize = pageSize;
            this.OrderBy = orderBy;
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public Int32 PageNum { get; set; }

        public Int32 PageSize { get; set; }

        public String OrderBy { get; set; }

        #endregion Properties
    }
}

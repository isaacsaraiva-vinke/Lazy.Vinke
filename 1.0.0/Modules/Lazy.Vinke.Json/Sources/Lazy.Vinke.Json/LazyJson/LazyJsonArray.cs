// LazyJsonArray.cs
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
    public class LazyJsonArray : LazyJsonToken
    {
        #region Variables

        private List<LazyJsonToken> tokenList;

        #endregion Variables

        #region Constructors

        public LazyJsonArray()
        {
            this.tokenList = new List<LazyJsonToken>();
            this.Type = LazyJsonType.Array;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Add a new token to the array
        /// </summary>
        /// <param name="jsonToken">The json token</param>
        public void Add(LazyJsonToken jsonToken)
        {
            this.tokenList.Add(jsonToken != null ? jsonToken : new LazyJsonNull());
        }

        /// <summary>
        /// Remove a token at the given index
        /// </summary>
        /// <param name="index">The index of the token to be removed</param>
        public void RemoveAt(Int32 index)
        {
            if (index >= 0 && index < this.tokenList.Count)
                this.tokenList.RemoveAt(index);
        }

        #endregion Methods

        #region Properties

        public Int32 Length { get { return this.tokenList.Count; } }

        #endregion Properties

        #region Indexers

        public LazyJsonToken this[Int32 index]
        {
            get
            {
                if (index >= 0 && index < this.tokenList.Count)
                    return this.tokenList[index];

                return null;
            }
            set
            {
                if (index >= 0 && index < this.tokenList.Count)
                    this.tokenList[index] = value != null ? value : new LazyJsonNull();
            }
        }

        #endregion Indexers
    }
}

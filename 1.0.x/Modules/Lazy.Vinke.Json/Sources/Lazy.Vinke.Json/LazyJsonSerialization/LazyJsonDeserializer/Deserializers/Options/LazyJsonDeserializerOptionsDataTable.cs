// LazyJsonDeserializerOptionsDataTable.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 12

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public class LazyJsonDeserializerOptionsDataTable : LazyJsonDeserializerOptionsBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptionsDataTable()
        {
            this.DataTableCollection = new Dictionary<String, LazyJsonDeserializerOptionsDataTableColumn>();
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        internal Dictionary<String, LazyJsonDeserializerOptionsDataTableColumn> DataTableCollection { get; private set; }

        #endregion Properties

        #region Indexers

        public LazyJsonDeserializerOptionsDataTableColumn this[String tableName]
        {
            get
            {
                if (this.DataTableCollection.ContainsKey(tableName) == false)
                    this.DataTableCollection.Add(tableName, new LazyJsonDeserializerOptionsDataTableColumn());

                return this.DataTableCollection[tableName];
            }
        }

        #endregion Indexers
    }

    public class LazyJsonDeserializerOptionsDataTableColumn
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptionsDataTableColumn()
        {
            this.Columns = new LazyJsonDeserializerOptionsDataTableColumnCollection();
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public LazyJsonDeserializerOptionsDataTableColumnCollection Columns { get; private set; }

        #endregion Properties
    }

    public class LazyJsonDeserializerOptionsDataTableColumnCollection
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptionsDataTableColumnCollection()
        {
            this.ColumnDataCollection = new Dictionary<String, LazyJsonDeserializerOptionsDataTableColumnData>();
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        internal Dictionary<String, LazyJsonDeserializerOptionsDataTableColumnData> ColumnDataCollection { get; private set; }

        #endregion Properties

        #region Indexers

        public LazyJsonDeserializerOptionsDataTableColumnData this[String columnName]
        {
            get
            {
                if (this.ColumnDataCollection.ContainsKey(columnName) == false)
                    this.ColumnDataCollection.Add(columnName, new LazyJsonDeserializerOptionsDataTableColumnData());

                return this.ColumnDataCollection[columnName];
            }
        }

        #endregion Indexers
    }

    public class LazyJsonDeserializerOptionsDataTableColumnData
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonDeserializerOptionsDataTableColumnData()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Set column data
        /// </summary>
        /// <param name="jsonDeserializer">The json deserializer</param>
        public void Set(LazyJsonDeserializerBase jsonDeserializer)
        {
            this.Deserializer = jsonDeserializer;
        }

        #endregion Methods

        #region Properties

        public LazyJsonDeserializerBase Deserializer { get; private set; }

        #endregion Properties
    }
}

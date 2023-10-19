// LazyJsonSerializerOptionsDataTable.cs
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
    public class LazyJsonSerializerOptionsDataTable : LazyJsonSerializerOptionsBase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonSerializerOptionsDataTable()
        {
            this.DataTableCollection = new Dictionary<String, LazyJsonSerializerOptionsDataTableColumn>();
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        internal Dictionary<String, LazyJsonSerializerOptionsDataTableColumn> DataTableCollection { get; private set; }

        #endregion Properties

        #region Indexers

        public LazyJsonSerializerOptionsDataTableColumn this[String tableName]
        {
            get
            {
                if (this.DataTableCollection.ContainsKey(tableName) == false)
                    this.DataTableCollection.Add(tableName, new LazyJsonSerializerOptionsDataTableColumn());

                return this.DataTableCollection[tableName];
            }
        }

        #endregion Indexers
    }

    public class LazyJsonSerializerOptionsDataTableColumn
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonSerializerOptionsDataTableColumn()
        {
            this.Columns = new LazyJsonSerializerOptionsDataTableColumnCollection();
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        public LazyJsonSerializerOptionsDataTableColumnCollection Columns { get; private set; }

        #endregion Properties
    }

    public class LazyJsonSerializerOptionsDataTableColumnCollection
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonSerializerOptionsDataTableColumnCollection()
        {
            this.ColumnDataCollection = new Dictionary<String, LazyJsonSerializerOptionsDataTableColumnData>();
        }

        #endregion Constructors

        #region Methods
        #endregion Methods

        #region Properties

        internal Dictionary<String, LazyJsonSerializerOptionsDataTableColumnData> ColumnDataCollection { get; private set; }

        #endregion Properties

        #region Indexers

        public LazyJsonSerializerOptionsDataTableColumnData this[String columnName]
        {
            get
            {
                if (this.ColumnDataCollection.ContainsKey(columnName) == false)
                    this.ColumnDataCollection.Add(columnName, new LazyJsonSerializerOptionsDataTableColumnData());

                return this.ColumnDataCollection[columnName];
            }
        }

        #endregion Indexers
    }

    public class LazyJsonSerializerOptionsDataTableColumnData
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyJsonSerializerOptionsDataTableColumnData()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Set column data
        /// </summary>
        /// <param name="jsonSerializer">The json serializer</param>
        public void Set(LazyJsonSerializerBase jsonSerializer)
        {
            this.Serializer = jsonSerializer;
        }

        #endregion Methods

        #region Properties

        public LazyJsonSerializerBase Serializer { get; private set; }

        #endregion Properties
    }
}

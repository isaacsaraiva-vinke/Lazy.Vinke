// LazyDatabase.cs
//
// This file is integrated part of "Lazy Vinke Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using Lazy.Vinke.Database.Properties;

namespace Lazy.Vinke.Database
{
    public abstract class LazyDatabase
    {
        #region Variables
        #endregion Variables

        #region Constructors

        public LazyDatabase()
        {
        }

        public LazyDatabase(String connectionString, String connectionOwner = null)
        {
            this.ConnectionString = connectionString;
            this.ConnectionOwner = connectionOwner;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Open the connection with the database
        /// </summary>
        public abstract void OpenConnection();

        /// <summary>
        /// Close the connection with the database
        /// </summary>
        public abstract void CloseConnection();

        /// <summary>
        /// Begin a new transaction
        /// </summary>
        public abstract void BeginTransaction();

        /// <summary>
        /// Commit current transaction
        /// </summary>
        public abstract void CommitTransaction();

        /// <summary>
        /// Rollback current transaction
        /// </summary>
        public abstract void RollbackTransaction();

        /// <summary>
        /// Create new instance of the current database type
        /// </summary>
        /// <param name="connectionOwner">The owner of the new instance</param>
        /// <returns>The new database instance</returns>
        public virtual LazyDatabase CreateNew(String connectionOwner = null)
        {
            return (LazyDatabase)Activator.CreateInstance(this.GetType(), new Object[] { this.ConnectionString, connectionOwner });
        }

        /// <summary>
        /// Execute the sql statement
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="values">The sql statement parameters values</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Execute(String sql, Object[] values)
        {
            return Execute(sql, values, ConvertSystemTypeToLazyDbType(values));
        }

        /// <summary>
        /// Execute the sql statement
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="values">The sql statement parameters values</param>
        /// <param name="dbTypes">The sql statement parameters types</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Execute(String sql, Object[] values, LazyDbType[] dbTypes)
        {
            return Execute(sql, values, dbTypes, LazyDatabaseStatement.Parameter.Extract(sql, this.SqlParameterChar));
        }

        /// <summary>
        /// Execute the sql statement
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="values">The sql statement parameters values</param>
        /// <param name="dbTypes">The sql statement parameters types</param>
        /// <param name="parameters">The sql statement parameters names</param>
        /// <returns>The number of affected records</returns>
        public abstract Int32 Execute(String sql, Object[] values, LazyDbType[] dbTypes, String[] parameters);

        /// <summary>
        /// Execute the stored procedure
        /// </summary>
        /// <param name="name">The stored procedure name</param>
        /// <param name="values">The stored procedure parameters values</param>
        /// <param name="dbTypes">The stored procedure parameters types</param>
        /// <param name="parameters">The stored procedure parameters names</param>
        public abstract void ExecuteProcedure(String name, Object[] values, LazyDbType[] dbTypes, String[] parameters);

        /// <summary>
        /// Query single value from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <returns>The value found</returns>
        public virtual Object QueryValue(String sql, Object[] values)
        {
            return QueryValue(sql, values, ConvertSystemTypeToLazyDbType(values));
        }

        /// <summary>
        /// Query single value from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The value found</returns>
        public virtual Object QueryValue(String sql, Object[] values, LazyDbType[] dbTypes)
        {
            return QueryValue(sql, values, dbTypes, LazyDatabaseStatement.Parameter.Extract(sql, this.SqlParameterChar));
        }

        /// <summary>
        /// Query single value from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <param name="parameters">The sql query parameters names</param>
        /// <returns>The value found</returns>
        public abstract Object QueryValue(String sql, Object[] values, LazyDbType[] dbTypes, String[] parameters);

        /// <summary>
        /// Query for record existance on table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <returns>The record existance</returns>
        public virtual Boolean QueryFind(String sql, Object[] values)
        {
            return QueryFind(sql, values, ConvertSystemTypeToLazyDbType(values));
        }

        /// <summary>
        /// Query for record existance on table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The record existance</returns>
        public virtual Boolean QueryFind(String sql, Object[] values, LazyDbType[] dbTypes)
        {
            return QueryFind(sql, values, dbTypes, LazyDatabaseStatement.Parameter.Extract(sql, this.SqlParameterChar));
        }

        /// <summary>
        /// Query for record existance on table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <param name="parameters">The sql query parameters names</param>
        /// <returns>The record existance</returns>
        public abstract Boolean QueryFind(String sql, Object[] values, LazyDbType[] dbTypes, String[] parameters);

        /// <summary>
        /// Query single record from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="values">The sql query parameters values</param>
        /// <returns>The first record found</returns>
        public virtual DataRow QueryRecord(String sql, String tableName, Object[] values)
        {
            return QueryRecord(sql, tableName, values, ConvertSystemTypeToLazyDbType(values));
        }

        /// <summary>
        /// Query single record from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The first record found</returns>
        public virtual DataRow QueryRecord(String sql, String tableName, Object[] values, LazyDbType[] dbTypes)
        {
            return QueryRecord(sql, tableName, values, dbTypes, LazyDatabaseStatement.Parameter.Extract(sql, this.SqlParameterChar));
        }

        /// <summary>
        /// Query single record from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <param name="parameters">The sql query parameters names</param>
        /// <returns>The first record found</returns>
        public abstract DataRow QueryRecord(String sql, String tableName, Object[] values, LazyDbType[] dbTypes, String[] parameters);

        /// <summary>
        /// Query multiple records from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="values">The sql query parameters values</param>
        /// <returns>The records found</returns>
        public virtual DataTable QueryTable(String sql, String tableName, Object[] values)
        {
            return QueryTable(sql, tableName, values, ConvertSystemTypeToLazyDbType(values));
        }

        /// <summary>
        /// Query multiple records from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The records found</returns>
        public virtual DataTable QueryTable(String sql, String tableName, Object[] values, LazyDbType[] dbTypes)
        {
            return QueryTable(sql, tableName, values, dbTypes, LazyDatabaseStatement.Parameter.Extract(sql, this.SqlParameterChar));
        }

        /// <summary>
        /// Query multiple records from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <param name="parameters">The sql query parameters names</param>
        /// <returns>The records found</returns>
        public abstract DataTable QueryTable(String sql, String tableName, Object[] values, LazyDbType[] dbTypes, String[] parameters);

        /// <summary>
        /// Query paged records from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="queryPageData">The query page data</param>
        /// <param name="values">The sql query parameters values</param>
        /// <returns>The paged records found</returns>
        public virtual LazyQueryPageResult QueryPage(String sql, String tableName, LazyQueryPageData queryPageData, Object[] values)
        {
            return QueryPage(sql, tableName, queryPageData, values, ConvertSystemTypeToLazyDbType(values));
        }

        /// <summary>
        /// Query paged records from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="queryPageData">The query page data</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The paged records found</returns>
        public virtual LazyQueryPageResult QueryPage(String sql, String tableName, LazyQueryPageData queryPageData, Object[] values, LazyDbType[] dbTypes)
        {
            return QueryPage(sql, tableName, queryPageData, values, dbTypes, LazyDatabaseStatement.Parameter.Extract(sql, this.SqlParameterChar));
        }

        /// <summary>
        /// Query paged records from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="queryPageData">The query page data</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <param name="parameters">The sql query parameters names</param>
        /// <returns>The paged records found</returns>
        public abstract LazyQueryPageResult QueryPage(String sql, String tableName, LazyQueryPageData queryPageData, Object[] values, LazyDbType[] dbTypes, String[] parameters);

        /// <summary>
        /// Select records from table filtering by data row primary key columns
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="dataRow">The data row to be used on the filter</param>
        /// <param name="dataRowState">The data row state to be considered</param>
        /// <param name="returnFields">The fields to be returned from table</param>
        /// <returns>The records found</returns>
        public virtual DataTable Select(String tableName, DataRow dataRow, DataRowState dataRowState = DataRowState.Unchanged, String[] returnFields = null)
        {
            ValidateParameters(tableName, dataRow);

            (Object[] values, LazyDbType[] dbTypes, String[] fields) = Select(dataRow, dataRowState);

            return Select(tableName, values, dbTypes, fields, returnFields);
        }

        /// <summary>
        /// Select paged records from table filtering by data row primary key columns
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="queryPageData">The query page data</param>
        /// <param name="dataRow">The data row to be used on the filter</param>
        /// <param name="dataRowState">The data row state to be considered</param>
        /// <param name="returnFields">The fields to be returned from table</param>
        /// <returns>The paged records found</returns>
        public virtual LazyQueryPageResult Select(String tableName, LazyQueryPageData queryPageData, DataRow dataRow, DataRowState dataRowState = DataRowState.Unchanged, String[] returnFields = null)
        {
            ValidateParameters(tableName, dataRow);

            (Object[] values, LazyDbType[] dbTypes, String[] fields) = Select(dataRow, dataRowState);

            return Select(tableName, queryPageData, values, dbTypes, fields, returnFields);
        }

        /// <summary>
        /// Select records from table filtering by array collection
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="values">The values array</param>
        /// <param name="dbTypes">The types array</param>
        /// <param name="fields">The fields array</param>
        /// <param name="returnFields">The fields to be returned from table</param>
        /// <returns>The records found</returns>
        public abstract DataTable Select(String tableName, Object[] values, LazyDbType[] dbTypes, String[] fields, String[] returnFields = null);

        /// <summary>
        /// Select paged records from table filtering by array collection
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="queryPageData">The query page data</param>
        /// <param name="values">The values array</param>
        /// <param name="dbTypes">The types array</param>
        /// <param name="fields">The fields array</param>
        /// <param name="returnFields">The fields to be returned from table</param>
        /// <returns>The paged records found</returns>
        public abstract LazyQueryPageResult Select(String tableName, LazyQueryPageData queryPageData, Object[] values, LazyDbType[] dbTypes, String[] fields, String[] returnFields = null);

        /// <summary>
        /// Validate parameters
        /// </summary>
        /// <param name="sql">The sql statement</param>
        protected virtual void ValidateParameters(String sql)
        {
            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrWhiteSpace(sql) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
        }

        /// <summary>
        /// Validate parameters
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="tableName">The record table name</param>
        protected virtual void ValidateParameters(String sql, String tableName)
        {
            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrWhiteSpace(sql) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);

            if (tableName == null)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);
        }

        /// <summary>
        /// Validate parameters
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="queryPageData">The query page data</param>
        protected virtual void ValidateParameters(String sql, String tableName, LazyQueryPageData queryPageData)
        {
            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrWhiteSpace(sql) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);

            if (tableName == null)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);

            if (queryPageData == null)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataNull);

            if (queryPageData.PageNum < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataPageNumLowerThanOne);

            if (queryPageData.PageSize < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataPageSizeLowerThanOne);

            if (String.IsNullOrWhiteSpace(queryPageData.OrderBy) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataOrderByNullOrEmpty);
        }

        /// <summary>
        /// Validate parameters
        /// </summary>
        /// <param name="values">The sql statement parameters values</param>
        /// <param name="dbTypes">The sql statement parameters types</param>
        /// <param name="parameters">The sql statement parameters names</param>
        protected virtual void ValidateParameters(Object[] values, LazyDbType[] dbTypes, String[] parameters)
        {
            if (values == null && (dbTypes != null || parameters != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);

            if (dbTypes == null && (values != null || parameters != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);

            if (parameters == null && (values != null || dbTypes != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);

            if (values != null && dbTypes != null && parameters != null && (values.Length != dbTypes.Length || values.Length != parameters.Length))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        /// <summary>
        /// Validate parameters
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="dataRow">The data row</param>
        protected virtual void ValidateParameters(String tableName, DataRow dataRow)
        {
            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (tableName.Contains(" ") == true) /* Avoid sends subqueries as table name */
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);

            if (dataRow == null)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionDataRowNull);
        }

        /// <summary>
        /// Validate parameters
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="dataRow">The data row</param>
        protected virtual void ValidateParameters(String tableName, Object[] values, LazyDbType[] dbTypes, String[] fields)
        {
            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (tableName.Contains(" ") == true) /* Avoid sends subqueries as table name */
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);

            if (values == null && (dbTypes != null || fields != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);

            if (dbTypes == null && (values != null || fields != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);

            if (fields == null && (values != null || dbTypes != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);

            if (values != null && dbTypes != null && fields != null && (values.Length != dbTypes.Length || values.Length != fields.Length))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
        }

        /// <summary>
        /// Convert the system values array to lazy database type array
        /// </summary>
        /// <param name="values">The system values array</param>
        /// <returns>The lazy database type array</returns>
        protected virtual LazyDbType[] ConvertSystemTypeToLazyDbType(Object[] values)
        {
            if (values != null)
            {
                LazyDbType[] dbTypes = new LazyDbType[values.Length];

                for (int index = 0; index < values.Length; index++)
                {
                    if (values[index] == null)
                    {
                        dbTypes[index] = LazyDbType.DBNull;
                    }
                    else
                    {
                        dbTypes[index] = LazyDatabaseType.FromSystemType(values[index].GetType());
                    }
                }

                return dbTypes;
            }

            return null;
        }

        /// <summary>
        /// Convert the lazy database type to the dbms type
        /// </summary>
        /// <param name="dbType">The lazy database type</param>
        /// <returns>The dbms type</returns>
        protected abstract Int32 ConvertLazyDbTypeToDbmsType(LazyDbType dbType);

        /// <summary>
        /// Select parameters arrays from data row
        /// </summary>
        /// <param name="dataRow">The data row</param>
        /// <param name="dataRowState">The data row state</param>
        /// <returns>The parameters arrays</returns>
        private (Object[], LazyDbType[], String[]) Select(DataRow dataRow, DataRowState dataRowState)
        {
            Object[] values = null;
            LazyDbType[] dbTypes = null;
            String[] fields = null;

            if (dataRowState.HasFlag(dataRow.RowState) == true)
            {
                if (dataRow.Table.PrimaryKey != null && dataRow.Table.PrimaryKey.Length > 0)
                {
                    values = new Object[dataRow.Table.PrimaryKey.Length];
                    dbTypes = new LazyDbType[dataRow.Table.PrimaryKey.Length];
                    fields = new String[dataRow.Table.PrimaryKey.Length];

                    for (int index = 0; index < dataRow.Table.PrimaryKey.Length; index++)
                    {
                        String columnName = dataRow.Table.PrimaryKey[index].ColumnName;
                        values[index] = (dataRowState == DataRowState.Modified || dataRowState == DataRowState.Deleted) ? dataRow[columnName, DataRowVersion.Original] : dataRow[columnName];
                        dbTypes[index] = LazyDatabaseType.FromSystemType(dataRow.Table.Columns[columnName].DataType);
                        fields[index] = columnName;
                    }
                }
            }

            return (values, dbTypes, fields);
        }

        #endregion Methods

        #region Properties

        public Char SqlParameterChar { get; set; } = LazyDatabaseStatement.Parameter.DefaultParameterChar;

        public String ConnectionString { get; set; }

        public String ConnectionOwner { get; private set; }

        public abstract Char DbmsParameterChar { get; protected set; }

        public abstract ConnectionState ConnectionState { get; }

        public abstract Boolean InTransaction { get; }

        #endregion Properties
    }
}

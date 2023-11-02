// LazyDatabaseMySql.cs
//
// This file is integrated part of "Lazy Vinke Database MySql" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;

namespace Lazy.Vinke.Database.MySql
{
    public class LazyDatabaseMySql : LazyDatabase
    {
        #region Variables

        private MySqlCommand sqlCommand;
        private MySqlConnection sqlConnection;
        private MySqlDataAdapter sqlDataAdapter;
        private MySqlTransaction sqlTransaction;

        #endregion Variables

        #region Constructors

        public LazyDatabaseMySql()
        {
        }

        public LazyDatabaseMySql(String connectionString, String connectionOwner = null)
            : base(connectionString, connectionOwner)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Open the connection with the database
        /// </summary>
        public override void OpenConnection()
        {
            #region Validations

            if (this.ConnectionState == ConnectionState.Open)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionAlreadyOpen);

            if (String.IsNullOrWhiteSpace(this.ConnectionString) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionStringNullOrEmpty);

            #endregion Validations

            this.sqlConnection = new MySqlConnection(this.ConnectionString);
            this.sqlCommand = new MySqlCommand();
            this.sqlCommand.Connection = this.sqlConnection;
            this.sqlDataAdapter = new MySqlDataAdapter();

            this.sqlConnection.Open();
        }

        /// <summary>
        /// Close the connection with the database
        /// </summary>
        public override void CloseConnection()
        {
            #region Validations

            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionAlreadyClose);

            #endregion Validations

            this.sqlConnection.Close();

            this.sqlTransaction = null;
            this.sqlDataAdapter = null;
            this.sqlCommand = null;
            this.sqlConnection = null;
        }

        /// <summary>
        /// Begin a new transaction
        /// </summary>
        public override void BeginTransaction()
        {
            #region Validations

            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (this.InTransaction == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTransactionAlreadyOpen);

            #endregion Validations

            this.sqlTransaction = this.sqlConnection.BeginTransaction();
        }

        /// <summary>
        /// Commit current transaction
        /// </summary>
        public override void CommitTransaction()
        {
            #region Validations

            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (this.InTransaction == false)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTransactionNotOpen);

            #endregion Validations

            this.sqlTransaction.Commit();
            this.sqlTransaction = null;
        }

        /// <summary>
        /// Rollback current transaction
        /// </summary>
        public override void RollbackTransaction()
        {
            #region Validations

            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (this.InTransaction == false)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTransactionNotOpen);

            #endregion Validations

            this.sqlTransaction.Rollback();
            this.sqlTransaction = null;
        }

        /// <summary>
        /// Execute the sql statement
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="values">The sql statement parameters values</param>
        /// <param name="dbTypes">The sql statement parameters types</param>
        /// <returns>The number of affected records</returns>
        public virtual Int32 Execute(String sql, Object[] values, MySqlDbType[] dbTypes)
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
        public virtual Int32 Execute(String sql, Object[] values, MySqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbTypes[index]);
                    sqlParameter.Value = values[index] != null ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            return this.sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute the sql statement
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="values">The sql statement parameters values</param>
        /// <param name="dbTypes">The sql statement parameters types</param>
        /// <param name="parameters">The sql statement parameters names</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Execute(String sql, Object[] values, LazyDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbType);
                    sqlParameter.Value = (values[index] != null && dbTypes[index] != LazyDbType.DBNull) ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            return this.sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute the stored procedure
        /// </summary>
        /// <param name="name">The stored procedure name</param>
        /// <param name="values">The stored procedure parameters values</param>
        /// <param name="dbTypes">The stored procedure parameters types</param>
        /// <param name="parameters">The stored procedure parameters names</param>
        public virtual void ExecuteProcedure(String name, Object[] values, MySqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(name);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbTypes[index]);
                    sqlParameter.Value = values[index] != null ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            this.sqlCommand.CommandText = name;
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
            this.sqlCommand.Transaction = this.sqlTransaction;

            this.sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Execute the stored procedure
        /// </summary>
        /// <param name="name">The stored procedure name</param>
        /// <param name="values">The stored procedure parameters values</param>
        /// <param name="dbTypes">The stored procedure parameters types</param>
        /// <param name="parameters">The stored procedure parameters names</param>
        public override void ExecuteProcedure(String name, Object[] values, LazyDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(name);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbType);
                    sqlParameter.Value = (values[index] != null && dbTypes[index] != LazyDbType.DBNull) ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            this.sqlCommand.CommandText = name;
            this.sqlCommand.CommandType = CommandType.StoredProcedure;
            this.sqlCommand.Transaction = this.sqlTransaction;

            this.sqlCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Query single value from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The value found</returns>
        public virtual Object QueryValue(String sql, Object[] values, MySqlDbType[] dbTypes)
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
        public virtual Object QueryValue(String sql, Object[] values, MySqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbTypes[index]);
                    sqlParameter.Value = values[index] != null ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable();
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0][0];

            return null;
        }

        /// <summary>
        /// Query single value from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <param name="parameters">The sql query parameters names</param>
        /// <returns>The value found</returns>
        public override Object QueryValue(String sql, Object[] values, LazyDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbType);
                    sqlParameter.Value = (values[index] != null && dbTypes[index] != LazyDbType.DBNull) ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable();
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0][0];

            return null;
        }

        /// <summary>
        /// Query for record existance on table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The record existance</returns>
        public virtual Boolean QueryFind(String sql, Object[] values, MySqlDbType[] dbTypes)
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
        public virtual Boolean QueryFind(String sql, Object[] values, MySqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbTypes[index]);
                    sqlParameter.Value = values[index] != null ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable();
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            return dataTable.Rows.Count > 0;
        }

        /// <summary>
        /// Query for record existance on table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <param name="parameters">The sql query parameters names</param>
        /// <returns>The record existance</returns>
        public override Boolean QueryFind(String sql, Object[] values, LazyDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbType);
                    sqlParameter.Value = (values[index] != null && dbTypes[index] != LazyDbType.DBNull) ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable();
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            return dataTable.Rows.Count > 0;
        }

        /// <summary>
        /// Query single record from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The first record found</returns>
        public virtual DataRow QueryRecord(String sql, String tableName, Object[] values, MySqlDbType[] dbTypes)
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
        public virtual DataRow QueryRecord(String sql, String tableName, Object[] values, MySqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, tableName);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbTypes[index]);
                    sqlParameter.Value = values[index] != null ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0];

            return null;
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
        public override DataRow QueryRecord(String sql, String tableName, Object[] values, LazyDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, tableName);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbType);
                    sqlParameter.Value = (values[index] != null && dbTypes[index] != LazyDbType.DBNull) ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
                return dataTable.Rows[0];

            return null;
        }

        /// <summary>
        /// Query multiple records from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The records found</returns>
        public virtual DataTable QueryTable(String sql, String tableName, Object[] values, MySqlDbType[] dbTypes)
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
        public virtual DataTable QueryTable(String sql, String tableName, Object[] values, MySqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, tableName);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbTypes[index]);
                    sqlParameter.Value = values[index] != null ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            return dataTable;
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
        public override DataTable QueryTable(String sql, String tableName, Object[] values, LazyDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, tableName);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbType);
                    sqlParameter.Value = (values[index] != null && dbTypes[index] != LazyDbType.DBNull) ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// Query paged records from table
        /// </summary>
        /// <param name="sql">The sql query</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="queryPageData">The query page data</param>
        /// <param name="values">The sql query parameters values</param>
        /// <param name="dbTypes">The sql query parameters types</param>
        /// <returns>The records found</returns>
        public virtual LazyQueryPageResult QueryPage(String sql, String tableName, LazyQueryPageData queryPageData, Object[] values, MySqlDbType[] dbTypes)
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
        /// <returns>The records found</returns>
        public virtual LazyQueryPageResult QueryPage(String sql, String tableName, LazyQueryPageData queryPageData, Object[] values, MySqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, tableName, queryPageData);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbTypes[index]);
                    sqlParameter.Value = values[index] != null ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            Int32 offset = (queryPageData.PageNum - 1) * queryPageData.PageSize;

            sql = String.Format("select count(*) over(), T.* from ({0}) T order by {1} limit {2} offset {3}", sql, queryPageData.OrderBy, queryPageData.PageSize, offset);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            LazyQueryPageResult queryPageResult = new LazyQueryPageResult();
            queryPageResult.PageNum = queryPageData.PageNum;
            queryPageResult.PageSize = queryPageData.PageSize;
            queryPageResult.PageItems = dataTable.Rows.Count;
            queryPageResult.CurrentCount = dataTable.Rows.Count > 0 ? offset + dataTable.Rows.Count : 0;
            queryPageResult.TotalCount = dataTable.Rows.Count > 0 ? Convert.ToInt32(dataTable.Rows[0][0]) : 0;
            queryPageResult.PageCount = Convert.ToInt32(Math.Ceiling(((Decimal)queryPageResult.TotalCount) / ((Decimal)queryPageResult.PageSize)));
            queryPageResult.HasNextPage = queryPageResult.CurrentCount < queryPageResult.TotalCount;
            queryPageResult.DataTable = dataTable;

            dataTable.Columns.RemoveAt(0);

            return queryPageResult;
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
        /// <returns>The records found</returns>
        public override LazyQueryPageResult QueryPage(String sql, String tableName, LazyQueryPageData queryPageData, Object[] values, LazyDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, tableName, queryPageData);

            ValidateParameters(values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    MySqlDbType dbType = (MySqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    MySqlParameter sqlParameter = new MySqlParameter(parameters[index], dbType);
                    sqlParameter.Value = (values[index] != null && dbTypes[index] != LazyDbType.DBNull) ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            Int32 offset = (queryPageData.PageNum - 1) * queryPageData.PageSize;

            sql = String.Format("select count(*) over(), T.* from ({0}) T order by {1} limit {2} offset {3}", sql, queryPageData.OrderBy, queryPageData.PageSize, offset);

            this.sqlCommand.CommandText = sql;
            this.sqlCommand.CommandType = CommandType.Text;
            this.sqlCommand.Transaction = this.sqlTransaction;

            DataTable dataTable = new DataTable(tableName);
            this.sqlDataAdapter.SelectCommand = this.sqlCommand;
            this.sqlDataAdapter.Fill(dataTable);

            LazyQueryPageResult queryPageResult = new LazyQueryPageResult();
            queryPageResult.PageNum = queryPageData.PageNum;
            queryPageResult.PageSize = queryPageData.PageSize;
            queryPageResult.PageItems = dataTable.Rows.Count;
            queryPageResult.CurrentCount = dataTable.Rows.Count > 0 ? offset + dataTable.Rows.Count : 0;
            queryPageResult.TotalCount = dataTable.Rows.Count > 0 ? Convert.ToInt32(dataTable.Rows[0][0]) : 0;
            queryPageResult.PageCount = Convert.ToInt32(Math.Ceiling(((Decimal)queryPageResult.TotalCount) / ((Decimal)queryPageResult.PageSize)));
            queryPageResult.HasNextPage = queryPageResult.CurrentCount < queryPageResult.TotalCount;
            queryPageResult.DataTable = dataTable;

            dataTable.Columns.RemoveAt(0);

            return queryPageResult;
        }

        /// <summary>
        /// Validate parameters
        /// </summary>
        /// <param name="values">The sql statement parameters values</param>
        /// <param name="dbTypes">The sql statement parameters types</param>
        /// <param name="parameters">The sql statement parameters names</param>
        protected virtual void ValidateParameters(Object[] values, MySqlDbType[] dbTypes, String[] parameters)
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
        /// Convert the lazy database type to the dbms type
        /// </summary>
        /// <param name="dbType">The lazy database type</param>
        /// <returns>The dbms type</returns>
        protected override Int32 ConvertLazyDbTypeToDbmsType(LazyDbType dbType)
        {
            if (dbType != LazyDbType.DBNull)
            {
                if (dbType == LazyDbType.Char) return (Int32)MySqlDbType.VarChar;
                if (dbType == LazyDbType.VarChar) return (Int32)MySqlDbType.VarString;
                if (dbType == LazyDbType.VarText) return (Int32)MySqlDbType.LongText;
                if (dbType == LazyDbType.Byte) return (Int32)MySqlDbType.Byte;
                if (dbType == LazyDbType.Int16) return (Int32)MySqlDbType.Int16;
                if (dbType == LazyDbType.Int32) return (Int32)MySqlDbType.Int32;
                if (dbType == LazyDbType.Int64) return (Int32)MySqlDbType.Int64;
                if (dbType == LazyDbType.UByte) return (Int32)MySqlDbType.UByte;
                if (dbType == LazyDbType.Float) return (Int32)MySqlDbType.Float;
                if (dbType == LazyDbType.Double) return (Int32)MySqlDbType.Double;
                if (dbType == LazyDbType.Decimal) return (Int32)MySqlDbType.Decimal;
                if (dbType == LazyDbType.DateTime) return (Int32)MySqlDbType.DateTime;
                if (dbType == LazyDbType.VarUByte) return (Int32)MySqlDbType.Blob;
            }

            return (Int32)MySqlDbType.VarString;
        }

        #endregion Methods

        #region Properties

        public override Char DbmsParameterChar { get; protected set; } = '@';

        public override ConnectionState ConnectionState
        {
            get { return this.sqlConnection != null && this.sqlConnection.State == ConnectionState.Open ? ConnectionState.Open : ConnectionState.Closed; }
        }

        public override Boolean InTransaction
        {
            get { return this.sqlTransaction != null; }
        }

        #endregion Properties
    }
}

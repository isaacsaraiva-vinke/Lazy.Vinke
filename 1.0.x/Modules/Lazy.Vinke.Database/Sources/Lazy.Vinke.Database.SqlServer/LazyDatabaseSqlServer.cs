// LazyDatabaseSqlServer.cs
//
// This file is integrated part of "Lazy Vinke Database SqlServer" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;

namespace Lazy.Vinke.Database.SqlServer
{
    public class LazyDatabaseSqlServer : LazyDatabase
    {
        #region Variables

        private SqlCommand sqlCommand;
        private SqlConnection sqlConnection;
        private SqlDataAdapter sqlDataAdapter;
        private SqlTransaction sqlTransaction;

        #endregion Variables

        #region Constructors

        public LazyDatabaseSqlServer()
        {
        }

        public LazyDatabaseSqlServer(String connectionString, String connectionOwner = null)
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

            this.sqlConnection = new SqlConnection(this.ConnectionString);
            this.sqlCommand = new SqlCommand();
            this.sqlCommand.Connection = this.sqlConnection;
            this.sqlDataAdapter = new SqlDataAdapter();

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
        public virtual Int32 Execute(String sql, Object[] values, SqlDbType[] dbTypes)
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
        public virtual Int32 Execute(String sql, Object[] values, SqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbTypes[index]);
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
            ValidateParameters(sql, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlDbType dbType = (SqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbType);
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
        public virtual void ExecuteProcedure(String name, Object[] values, SqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(name, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbTypes[index]);
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
            ValidateParameters(name, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlDbType dbType = (SqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbType);
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
        public virtual Object QueryValue(String sql, Object[] values, SqlDbType[] dbTypes)
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
        public virtual Object QueryValue(String sql, Object[] values, SqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbTypes[index]);
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
            ValidateParameters(sql, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlDbType dbType = (SqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbType);
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
        public virtual Boolean QueryFind(String sql, Object[] values, SqlDbType[] dbTypes)
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
        public virtual Boolean QueryFind(String sql, Object[] values, SqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbTypes[index]);
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
            ValidateParameters(sql, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlDbType dbType = (SqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbType);
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
        public virtual DataRow QueryRecord(String sql, String tableName, Object[] values, SqlDbType[] dbTypes)
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
        public virtual DataRow QueryRecord(String sql, String tableName, Object[] values, SqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, tableName, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbTypes[index]);
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
            ValidateParameters(sql, tableName, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlDbType dbType = (SqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbType);
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
        public virtual DataTable QueryTable(String sql, String tableName, Object[] values, SqlDbType[] dbTypes)
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
        public virtual DataTable QueryTable(String sql, String tableName, Object[] values, SqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, tableName, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbTypes[index]);
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
            ValidateParameters(sql, tableName, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlDbType dbType = (SqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbType);
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
        public virtual LazyQueryPageResult QueryPage(String sql, String tableName, LazyQueryPageData queryPageData, Object[] values, SqlDbType[] dbTypes)
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
        public virtual LazyQueryPageResult QueryPage(String sql, String tableName, LazyQueryPageData queryPageData, Object[] values, SqlDbType[] dbTypes, String[] parameters)
        {
            ValidateParameters(sql, tableName, queryPageData, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbTypes[index]);
                    sqlParameter.Value = values[index] != null ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            Int32 offset = (queryPageData.PageNum - 1) * queryPageData.PageSize;

            sql = String.Format("select count(*) over(), T.* from ({0}) T order by {1} offset {2} row fetch next {3} row only", sql, queryPageData.OrderBy, offset, queryPageData.PageSize);

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
            ValidateParameters(sql, tableName, queryPageData, values, dbTypes, parameters);

            this.sqlCommand.Parameters.Clear();

            if (values != null)
            {
                for (int index = 0; index < values.Length; index++)
                {
                    SqlDbType dbType = (SqlDbType)ConvertLazyDbTypeToDbmsType(dbTypes[index]);
                    SqlParameter sqlParameter = new SqlParameter(parameters[index], dbType);
                    sqlParameter.Value = (values[index] != null && dbTypes[index] != LazyDbType.DBNull) ? values[index] : DBNull.Value;

                    this.sqlCommand.Parameters.Add(sqlParameter);
                }
            }

            if (this.SqlParameterChar != this.DbmsParameterChar)
                sql = LazyDatabaseStatement.Transform.Replace(sql, this.SqlParameterChar, this.DbmsParameterChar);

            Int32 offset = (queryPageData.PageNum - 1) * queryPageData.PageSize;

            sql = String.Format("select count(*) over(), T.* from ({0}) T order by {1} offset {2} row fetch next {3} row only", sql, queryPageData.OrderBy, offset, queryPageData.PageSize);

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
        /// Select records from table filtering by array collection
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="values">The values array</param>
        /// <param name="dbTypes">The types array</param>
        /// <param name="fields">The fields array</param>
        /// <param name="returnFields">The return fields array</param>
        /// <returns>The records found</returns>
        public override DataTable Select(String tableName, Object[] values, LazyDbType[] dbTypes, String[] fields, String[] returnFields = null)
        {
            #region Validations

            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (tableName.Contains(" ") == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);

            if (values == null && (dbTypes != null || fields != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            if (dbTypes == null && (values != null || fields != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            if (fields == null && (values != null || dbTypes != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            if (values != null && dbTypes != null && fields != null && (values.Length != dbTypes.Length || values.Length != fields.Length))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            #endregion Validations

            String sql = SelectQueryFrom(tableName, fields, returnFields);

            return QueryTable(sql, tableName, values, dbTypes, fields);
        }

        /// <summary>
        /// Select paged records from table filtering by array collection
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="queryPageData">The query page data</param>
        /// <param name="values">The values array</param>
        /// <param name="dbTypes">The types array</param>
        /// <param name="fields">The fields array</param>
        /// <param name="returnFields">The return fields array</param>
        /// <returns>The paged records found</returns>
        public override LazyQueryPageResult Select(String tableName, LazyQueryPageData queryPageData, Object[] values, LazyDbType[] dbTypes, String[] fields, String[] returnFields = null)
        {
            #region Validations

            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (tableName.Contains(" ") == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);

            if (values == null && (dbTypes != null || fields != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            if (dbTypes == null && (values != null || fields != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            if (fields == null && (values != null || dbTypes != null))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            if (values != null && dbTypes != null && fields != null && (values.Length != dbTypes.Length || values.Length != fields.Length))
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            #endregion Validations

            String sql = SelectQueryFrom(tableName, fields, returnFields);

            return QueryPage(sql, tableName, queryPageData, values, dbTypes, fields);
        }

        /// <summary>
        /// Insert values array on table
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="values">The values array</param>
        /// <param name="dbTypes">The types array</param>
        /// <param name="fields">The fields array</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Insert(String tableName, Object[] values, LazyDbType[] dbTypes, String[] fields)
        {
            #region Validations

            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (tableName.Contains(" ") == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);

            if (values == null || values.Length < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLength);

            if (dbTypes == null || dbTypes.Length < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTypesNullOrZeroLength);

            if (fields == null || fields.Length < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLength);

            if (values.Length != dbTypes.Length || values.Length != fields.Length)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            #endregion Validations

            String sql = InsertStatementFrom(tableName, fields);

            return Execute(sql, values, dbTypes, fields);
        }

        /// <summary>
        /// Update values array on table
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="values">The values array</param>
        /// <param name="dbTypes">The types array</param>
        /// <param name="fields">The fields array</param>
        /// <param name="keyValues">The key values array</param>
        /// <param name="keyDbTypes">The key types array</param>
        /// <param name="keyFields">The key fields array</param>
        /// <returns>The number of affected records</returns>
        public override Int32 Update(String tableName, Object[] values, LazyDbType[] dbTypes, String[] fields, Object[] keyValues, LazyDbType[] keyDbTypes, String[] keyFields)
        {
            #region Validations

            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrEmpty(tableName) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);

            if (tableName.Contains(" ") == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);

            if (values == null || values.Length < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLength);

            if (dbTypes == null || dbTypes.Length < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTypesNullOrZeroLength);

            if (fields == null || fields.Length < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLength);

            if (values.Length != dbTypes.Length || values.Length != fields.Length)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            if (keyValues == null || keyValues.Length < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLength);

            if (keyDbTypes == null || keyDbTypes.Length < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTypesNullOrZeroLength);

            if (keyFields == null || keyFields.Length < 1)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLength);

            if (keyValues.Length != keyDbTypes.Length || keyValues.Length != keyFields.Length)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);

            #endregion Validations

            String sql = UpdateStatementFrom(tableName, fields, keyFields);

            keyFields = keyFields.Select(x => { return "key" + x; }).ToArray();

            Object[] mergedValues = values.Concat(keyValues).ToArray();
            LazyDbType[] mergedDbTypes = dbTypes.Concat(keyDbTypes).ToArray();
            String[] mergedFields = fields.Concat(keyFields).ToArray();

            return Execute(sql, mergedValues, mergedDbTypes, mergedFields);
        }

        /// <summary>
        /// Validate parameters
        /// </summary>
        /// <param name="sql">The sql statement</param>
        /// <param name="values">The sql statement parameters values</param>
        /// <param name="dbTypes">The sql statement parameters types</param>
        /// <param name="parameters">The sql statement parameters names</param>
        protected virtual void ValidateParameters(String sql, Object[] values, SqlDbType[] dbTypes, String[] parameters)
        {
            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrWhiteSpace(sql) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);

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
        /// <param name="sql">The sql statement</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="values">The sql statement parameters values</param>
        /// <param name="dbTypes">The sql statement parameters types</param>
        /// <param name="parameters">The sql statement parameters names</param>
        protected virtual void ValidateParameters(String sql, String tableName, Object[] values, SqlDbType[] dbTypes, String[] parameters)
        {
            if (this.ConnectionState == ConnectionState.Closed)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);

            if (String.IsNullOrWhiteSpace(sql) == true)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);

            if (tableName == null)
                throw new Exception(LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);

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
        /// <param name="sql">The sql statement</param>
        /// <param name="tableName">The record table name</param>
        /// <param name="queryPageData">The query page data</param>
        /// <param name="values">The sql statement parameters values</param>
        /// <param name="dbTypes">The sql statement parameters types</param>
        /// <param name="parameters">The sql statement parameters names</param>
        protected virtual void ValidateParameters(String sql, String tableName, LazyQueryPageData queryPageData, Object[] values, SqlDbType[] dbTypes, String[] parameters)
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
                if (dbType == LazyDbType.Char) return (Int32)SqlDbType.Char;
                if (dbType == LazyDbType.VarChar) return (Int32)SqlDbType.VarChar;
                if (dbType == LazyDbType.VarText) return (Int32)SqlDbType.Text;
                if (dbType == LazyDbType.Byte) return (Int32)SqlDbType.SmallInt;
                if (dbType == LazyDbType.Int16) return (Int32)SqlDbType.SmallInt;
                if (dbType == LazyDbType.Int32) return (Int32)SqlDbType.Int;
                if (dbType == LazyDbType.Int64) return (Int32)SqlDbType.BigInt;
                if (dbType == LazyDbType.UByte) return (Int32)SqlDbType.TinyInt;
                if (dbType == LazyDbType.Float) return (Int32)SqlDbType.Real;
                if (dbType == LazyDbType.Double) return (Int32)SqlDbType.Float;
                if (dbType == LazyDbType.Decimal) return (Int32)SqlDbType.Decimal;
                if (dbType == LazyDbType.DateTime) return (Int32)SqlDbType.DateTime;
                if (dbType == LazyDbType.VarUByte) return (Int32)SqlDbType.Image;
            }

            return (Int32)SqlDbType.VarChar;
        }

        /// <summary>
        /// Generate sql select query
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="fields">The fields array</param>
        /// <param name="returnFields">The return fields array</param>
        /// <returns>The sql select query</returns>
        private String SelectQueryFrom(String tableName, String[] fields, String[] returnFields)
        {
            String parameterString = String.Empty;
            String returnFieldString = String.Empty;

            if (fields != null)
            {
                for (int index = 0; index < fields.Length; index++)
                {
                    if (String.IsNullOrWhiteSpace(fields[index]) == false)
                        parameterString += fields[index] + " = " + this.DbmsParameterChar + fields[index] + " and ";
                }
                if (parameterString.EndsWith(" and ") == true)
                    parameterString = " where " + parameterString.Remove(parameterString.Length - 5, 5);
            }

            if (returnFields != null)
            {
                for (int index = 0; index < returnFields.Length; index++)
                {
                    if (String.IsNullOrWhiteSpace(returnFields[index]) == false)
                        returnFieldString += returnFields[index] + ",";
                }
                if (returnFieldString.EndsWith(",") == true)
                    returnFieldString = returnFieldString.Remove(returnFieldString.Length - 1, 1);
            }

            if (returnFieldString == String.Empty)
                returnFieldString = "*";

            return String.Format("select {0} from {1}{2}", returnFieldString, tableName, parameterString);
        }

        /// <summary>
        /// Generate sql insert statement
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="fields">The fields array</param>
        /// <returns>The sql insert statement</returns>
        private String InsertStatementFrom(String tableName, String[] fields)
        {
            String fieldString = String.Empty;
            String parameterString = String.Empty;

            for (int index = 0; index < fields.Length; index++)
            {
                if (String.IsNullOrWhiteSpace(fields[index]) == false)
                {
                    fieldString += fields[index] + ",";
                    parameterString += this.DbmsParameterChar + fields[index] + ",";
                }
            }

            if (fieldString.EndsWith(",") == true)
                fieldString = fieldString.Remove(fieldString.Length - 1, 1);

            if (parameterString.EndsWith(",") == true)
                parameterString = parameterString.Remove(parameterString.Length - 1, 1);

            return String.Format("insert into {0} ({1}) values ({2})", tableName, fieldString, parameterString);
        }

        /// <summary>
        /// Generate sql update statement
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="fields">The fields array</param>
        /// <param name="keyFields">The key fields array</param>
        /// <returns>The sql update statement</returns>
        private String UpdateStatementFrom(String tableName, String[] fields, String[] keyFields)
        {
            String fieldString = String.Empty;
            String keyFieldString = String.Empty;

            for (int index = 0; index < fields.Length; index++)
            {
                if (String.IsNullOrWhiteSpace(fields[index]) == false)
                    fieldString += fields[index] + " = " + this.DbmsParameterChar + fields[index] + ",";
            }
            if (fieldString.EndsWith(",") == true)
                fieldString = fieldString.Remove(fieldString.Length - 1, 1);

            for (int index = 0; index < keyFields.Length; index++)
            {
                if (String.IsNullOrWhiteSpace(keyFields[index]) == false)
                    keyFieldString += keyFields[index] + " = " + this.DbmsParameterChar + "key" + keyFields[index] + ",";
            }
            if (keyFieldString.EndsWith(",") == true)
                keyFieldString = keyFieldString.Remove(keyFieldString.Length - 1, 1);

            return String.Format("update {0} set {1} where {2}", tableName, fieldString, keyFieldString);
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

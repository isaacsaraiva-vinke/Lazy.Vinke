// TestsLazyDatabaseDelete.cs
//
// This file is integrated part of "Lazy Vinke Tests Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 04

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;

namespace Lazy.Vinke.Tests.Database
{
    public class TestsLazyDatabaseDelete
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void Delete_Validations_DataRow_Exception()
        {
            // Arrange
            String tableName = "TestsDelete";
            String subQuery = "(select * from TestsDelete)";

            DataTable dataTableNoColumns = new DataTable(tableName);
            dataTableNoColumns.Rows.Add(dataTableNoColumns.NewRow());

            DataTable dataTableNoPrimaryKey = new DataTable(tableName);
            dataTableNoPrimaryKey.Columns.Add("Id", typeof(Int32));
            dataTableNoPrimaryKey.Rows.Add(1);

            DataTable dataTable = new DataTable(tableName);
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"] };
            dataTable.Rows.Add(1);

            DataRow dataRowNoColumns = dataTableNoColumns.Rows[0];
            DataRow dataRowNoPrimaryKey = dataTableNoPrimaryKey.Rows[0];
            DataRow dataRow = dataTable.Rows[0];

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionDataRowNull = null;
            Exception exceptionDataRowNoColumns = null;
            Exception exceptionDataRowNoPrimaryKey = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Delete(tableName, dataRow); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Delete(null, dataRow); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Delete(subQuery, dataRow); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Delete(tableName, null); } catch (Exception exp) { exceptionDataRowNull = exp; }
            try { this.Database.Delete(tableName, dataRowNoColumns); } catch (Exception exp) { exceptionDataRowNoColumns = exp; }
            try { this.Database.Delete(tableName, dataRowNoPrimaryKey); } catch (Exception exp) { exceptionDataRowNoPrimaryKey = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionDataRowNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowNull);
            Assert.AreEqual(exceptionDataRowNoColumns.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowColumnsMissing);
            Assert.AreEqual(exceptionDataRowNoPrimaryKey.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowPrimaryKeyColumnsMissing);
        }

        public virtual void Delete_Validations_Arrays_Exception()
        {
            // Arrange
            String tableName = "TestsDelete";
            String subQuery = "(select * from TestsDelete)";

            Object[] keyValues = new Object[] { 1, 1 };
            LazyDbType[] keyDbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.Int32 };
            String[] keyFields = new String[] { "IdMaster", "IdChild" };

            Object[] keyValuesLess = new Object[] { 1 };
            LazyDbType[] keyDbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] keyFieldsLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionKeyValuesNullButOthers = null;
            Exception exceptionKeyDbTypesNullButOthers = null;
            Exception exceptionKeyFieldsNullButOthers = null;
            Exception exceptionKeyValuesLessButOthers = null;
            Exception exceptionKeyDbTypesLessButOthers = null;
            Exception exceptionKeyFieldsLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Delete(tableName, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Delete(null, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Delete(subQuery, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Delete(tableName, null, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesNullButOthers = exp; }
            try { this.Database.Delete(tableName, keyValues, null, keyFields); } catch (Exception exp) { exceptionKeyDbTypesNullButOthers = exp; }
            try { this.Database.Delete(tableName, keyValues, keyDbTypes, null); } catch (Exception exp) { exceptionKeyFieldsNullButOthers = exp; }

            try { this.Database.Delete(tableName, keyValuesLess, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesLessButOthers = exp; }
            try { this.Database.Delete(tableName, keyValues, keyDbTypesLess, keyFields); } catch (Exception exp) { exceptionKeyDbTypesLessButOthers = exp; }
            try { this.Database.Delete(tableName, keyValues, keyDbTypes, keyFieldsLess); } catch (Exception exp) { exceptionKeyFieldsLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionKeyValuesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesNullOrZeroLength);
            Assert.AreEqual(exceptionKeyDbTypesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyTypesNullOrZeroLength);
            Assert.AreEqual(exceptionKeyFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionKeyValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
        }

        public virtual void Delete_DataRow_Deleted_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsDelete";
            String sqlSelectFind = "select 1 from " + tableName + " where Id between 1000 and 3000";
            String sqlDelete = "delete from " + tableName + " where Id between 1000 and 3000";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Columns.Add("Name", typeof(String));
            dataTable.Columns.Add("Description", typeof(String));
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"] };
            dataTable.Rows.Add(1000, "Name 1000", "Description 1000");
            dataTable.Rows.Add(2000, "Name 2000", "Description 2000");

            this.Database.Insert(tableName, dataTable.Rows[0]);
            this.Database.Insert(tableName, dataTable.Rows[1]);

            dataTable.AcceptChanges();

            dataTable.Rows[0].Delete();
            dataTable.Rows[1].Delete();

            // Act
            Boolean existsRecordsBeforeDelete = this.Database.QueryFind(sqlSelectFind, null);
            rowsAffected += this.Database.Delete(tableName, dataTable.Rows[0]);
            rowsAffected += this.Database.Delete(tableName, dataTable.Rows[1]);
            Boolean existsRecordsAfterDelete = this.Database.QueryFind(sqlSelectFind, null);

            // Assert
            Assert.AreEqual(rowsAffected, 2);
            Assert.AreEqual(existsRecordsBeforeDelete, true);
            Assert.AreEqual(existsRecordsAfterDelete, false);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

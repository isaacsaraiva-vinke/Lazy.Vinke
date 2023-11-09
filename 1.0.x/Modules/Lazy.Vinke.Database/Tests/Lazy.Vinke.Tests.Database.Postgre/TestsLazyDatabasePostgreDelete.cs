// TestsLazyDatabasePostgreDelete.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Postgre" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 04

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Npgsql;
using NpgsqlTypes;

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.Postgre;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.Postgre
{
    [TestClass]
    public class TestsLazyDatabasePostgreDelete : TestsLazyDatabaseDelete
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Delete_Validations_DbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsDelete";
            String subQuery = "(select * from TestsDelete)";

            Object[] keyValues = new Object[] { 1, 1 };
            NpgsqlDbType[] keyDbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer };
            String[] keyFields = new String[] { "IdMaster", "IdChild" };

            Object[] keyValuesLess = new Object[] { 1 };
            NpgsqlDbType[] keyDbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Integer };
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

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.Delete(tableName, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.Delete(null, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databasePostgre.Delete(subQuery, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databasePostgre.Delete(tableName, null, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesNullButOthers = exp; }
            try { databasePostgre.Delete(tableName, keyValues, null, keyFields); } catch (Exception exp) { exceptionKeyDbTypesNullButOthers = exp; }
            try { databasePostgre.Delete(tableName, keyValues, keyDbTypes, null); } catch (Exception exp) { exceptionKeyFieldsNullButOthers = exp; }

            try { databasePostgre.Delete(tableName, keyValuesLess, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesLessButOthers = exp; }
            try { databasePostgre.Delete(tableName, keyValues, keyDbTypesLess, keyFields); } catch (Exception exp) { exceptionKeyDbTypesLessButOthers = exp; }
            try { databasePostgre.Delete(tableName, keyValues, keyDbTypes, keyFieldsLess); } catch (Exception exp) { exceptionKeyFieldsLessButOthers = exp; }

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

        [TestMethod]
        public virtual void Delete_Arrays_Single_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsDelete";
            String sqlSelectFind = "select 1 from " + tableName + " where Id between 4000 and 5000";
            String sqlDelete = "delete from " + tableName + " where Id between 4000 and 5000";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            String[] fields = new String[] { "Id", "Name", "Description" };
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar, NpgsqlDbType.Varchar };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { 4000, "Name 4000", "Description 4000" },
                new Object[] { 5000, "Name 5000", "Description 5000" }
            };

            String[] keyFields = new String[] { "Id" };
            NpgsqlDbType[] keyDbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer };
            List<Object[]> keyValuesList = new List<Object[]>() {
                new Object[] { 4000 },
                new Object[] { 5000 }
            };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            databasePostgre.Insert(tableName, valuesList[0], dbTypes, fields);
            databasePostgre.Insert(tableName, valuesList[1], dbTypes, fields);

            // Act
            Boolean existsRecordsBeforeDelete = databasePostgre.QueryFind(sqlSelectFind, null);
            rowsAffected += databasePostgre.Delete(tableName, keyValuesList[0], keyDbTypes, keyFields);
            rowsAffected += databasePostgre.Delete(tableName, keyValuesList[1], keyDbTypes, keyFields);
            Boolean existsRecordsAfterDelete = databasePostgre.QueryFind(sqlSelectFind, null);

            // Assert
            Assert.AreEqual(rowsAffected, 2);
            Assert.AreEqual(existsRecordsBeforeDelete, true);
            Assert.AreEqual(existsRecordsAfterDelete, false);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void Delete_Validations_DataRow_Exception()
        {
            base.Delete_Validations_DataRow_Exception();
        }

        [TestMethod]
        public override void Delete_Validations_LazyDbTypeArrays_Exception()
        {
            base.Delete_Validations_LazyDbTypeArrays_Exception();
        }

        [TestMethod]
        public override void Delete_DataRow_Deleted_Success()
        {
            base.Delete_DataRow_Deleted_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

// TestsLazyDatabaseSqlServerIndate.cs
//
// This file is integrated part of "Lazy Vinke Tests Database SqlServer" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 06

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.SqlServer;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.SqlServer
{
    [TestClass]
    public class TestsLazyDatabaseSqlServerIndate : TestsLazyDatabaseIndate
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Indate_Validations_DbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsIndate";
            String subQuery = "(select * from TestsIndate)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar };
            String[] fields = new String[] { "Id", "Name" };
            String[] keyFields = new String[] { "Id" };

            Object[] valuesLess = new Object[] { 1 };
            SqlDbType[] dbTypesLess = new SqlDbType[] { SqlDbType.Decimal };
            String[] fieldsLess = new String[] { "Amount" };

            String[] keyFieldsNotMatch1 = new String[] { "Code" };
            String[] keyFieldsNotMatch2 = new String[] { "Id", "Code" };
            String[] keyFieldsNotMatch3 = new String[] { "Code", "Id" };

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionValuesNullButOthers = null;
            Exception exceptionDbTypesNullButOthers = null;
            Exception exceptionDbFieldsNullButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbFieldsLessButOthers = null;
            Exception exceptionKeyFieldsNullButOthers = null;
            Exception exceptionKeyFieldsNotMatch1 = null;
            Exception exceptionKeyFieldsNotMatch2 = null;
            Exception exceptionKeyFieldsNotMatch3 = null;

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.CloseConnection();

            try { databaseSqlServer.Indate(tableName, values, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.Indate(null, values, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseSqlServer.Indate(subQuery, values, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databaseSqlServer.Indate(tableName, null, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { databaseSqlServer.Indate(tableName, values, null, fields, keyFields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { databaseSqlServer.Indate(tableName, values, dbTypes, null, keyFields); } catch (Exception exp) { exceptionDbFieldsNullButOthers = exp; }
            try { databaseSqlServer.Indate(tableName, values, dbTypes, fields, null); } catch (Exception exp) { exceptionKeyFieldsNullButOthers = exp; }

            try { databaseSqlServer.Indate(tableName, valuesLess, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.Indate(tableName, values, dbTypesLess, fields, keyFields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.Indate(tableName, values, dbTypes, fieldsLess, keyFields); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }

            try { databaseSqlServer.Indate(tableName, values, dbTypes, fields, keyFieldsNotMatch1); } catch (Exception exp) { exceptionKeyFieldsNotMatch1 = exp; }
            try { databaseSqlServer.Indate(tableName, values, dbTypes, fields, keyFieldsNotMatch2); } catch (Exception exp) { exceptionKeyFieldsNotMatch2 = exp; }
            try { databaseSqlServer.Indate(tableName, values, dbTypes, fields, keyFieldsNotMatch3); } catch (Exception exp) { exceptionKeyFieldsNotMatch3 = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionValuesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLength);
            Assert.AreEqual(exceptionDbTypesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionTypesNullOrZeroLength);
            Assert.AreEqual(exceptionDbFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionKeyFieldsNotMatch1.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNotPresentInFields);
            Assert.AreEqual(exceptionKeyFieldsNotMatch2.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNotPresentInFields);
            Assert.AreEqual(exceptionKeyFieldsNotMatch3.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNotPresentInFields);
        }

        [TestMethod]
        public virtual void Indate_Arrays_Single_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsIndate";
            String testCode = "Indate_Arrays_Single_Success";
            String sqlDelete = "delete from " + tableName + " where TestCode = '" + testCode + "'";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            String[] fields = new String[] { "TestCode", "Id", "Item" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.VarChar, SqlDbType.Int, SqlDbType.VarChar };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { testCode, 1, "Lazy" },
                new Object[] { testCode, 2, "Vinke" },
                new Object[] { testCode, 3, "Tests" },
                new Object[] { testCode, 4, "Database" }
            };

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            databaseSqlServer.Insert(tableName, valuesList[0], dbTypes, fields);
            databaseSqlServer.Insert(tableName, valuesList[1], dbTypes, fields);
            databaseSqlServer.Insert(tableName, valuesList[2], dbTypes, fields);
            databaseSqlServer.Insert(tableName, valuesList[3], dbTypes, fields);

            valuesList = new List<Object[]>() {
                new Object[] { testCode, 3, "Isaac" },
                new Object[] { testCode, 4, "Bezerra" },
                new Object[] { testCode, 5, "Saraiva" }
            };

            String[] keyFields = new String[] { "TestCode", "Id" };

            // Act
            rowsAffected += databaseSqlServer.Indate(tableName, valuesList[0], dbTypes, fields, keyFields);
            rowsAffected += databaseSqlServer.Indate(tableName, valuesList[1], dbTypes, fields, keyFields);
            rowsAffected += databaseSqlServer.Indate(tableName, valuesList[2], dbTypes, fields, keyFields);

            // Assert
            Assert.AreEqual(rowsAffected, 3);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void Indate_Validations_DataRow_Exception()
        {
            base.Indate_Validations_DataRow_Exception();
        }

        [TestMethod]
        public override void Indate_Validations_LazyDbTypeArrays_Exception()
        {
            base.Indate_Validations_LazyDbTypeArrays_Exception();
        }

        [TestMethod]
        public override void Indate_DataRow_Added_Success()
        {
            base.Indate_DataRow_Added_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

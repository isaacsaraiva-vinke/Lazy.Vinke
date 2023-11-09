// TestsLazyDatabaseSqlServerUpsert.cs
//
// This file is integrated part of "Lazy Vinke Tests Database SqlServer" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 07

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
    public class TestsLazyDatabaseSqlServerUpsert : TestsLazyDatabaseUpsert
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Upsert_Validations_DbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsUpsert";
            String subQuery = "(select * from TestsUpsert)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] keyValues = new Object[] { 1, 1 };
            SqlDbType[] keyDbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.Int };
            String[] keyFields = new String[] { "IdMaster", "IdChild" };

            Object[] valuesLess = new Object[] { 1 };
            SqlDbType[] dbTypesLess = new SqlDbType[] { SqlDbType.Decimal };
            String[] fieldsLess = new String[] { "Amount" };

            Object[] keyValuesLess = new Object[] { 1 };
            SqlDbType[] keyDbTypesLess = new SqlDbType[] { SqlDbType.Int };
            String[] keyFieldsLess = new String[] { "Id" };

            String[] keyFieldsNotMatch1 = new String[] { "Code", "Date" };
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
            Exception exceptionKeyValuesNullButOthers = null;
            Exception exceptionKeyDbTypesNullButOthers = null;
            Exception exceptionKeyFieldsNullButOthers = null;
            Exception exceptionKeyValuesLessButOthers = null;
            Exception exceptionKeyDbTypesLessButOthers = null;
            Exception exceptionKeyFieldsLessButOthers = null;
            Exception exceptionKeyFieldsNotMatch1 = null;
            Exception exceptionKeyFieldsNotMatch2 = null;
            Exception exceptionKeyFieldsNotMatch3 = null;

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.CloseConnection();

            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.Upsert(null, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseSqlServer.Upsert(subQuery, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databaseSqlServer.Upsert(tableName, null, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, null, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, null, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsNullButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, null, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesNullButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, keyValues, null, keyFields); } catch (Exception exp) { exceptionKeyDbTypesNullButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, null); } catch (Exception exp) { exceptionKeyFieldsNullButOthers = exp; }

            try { databaseSqlServer.Upsert(tableName, valuesLess, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypesLess, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fieldsLess, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, keyValuesLess, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesLessButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypesLess, keyFields); } catch (Exception exp) { exceptionKeyDbTypesLessButOthers = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsLess); } catch (Exception exp) { exceptionKeyFieldsLessButOthers = exp; }

            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsNotMatch1); } catch (Exception exp) { exceptionKeyFieldsNotMatch1 = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsNotMatch2); } catch (Exception exp) { exceptionKeyFieldsNotMatch2 = exp; }
            try { databaseSqlServer.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsNotMatch3); } catch (Exception exp) { exceptionKeyFieldsNotMatch3 = exp; }

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
            Assert.AreEqual(exceptionKeyValuesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesNullOrZeroLength);
            Assert.AreEqual(exceptionKeyDbTypesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyTypesNullOrZeroLength);
            Assert.AreEqual(exceptionKeyFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionKeyValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyFieldsNotMatch1.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNotPresentInFields);
            Assert.AreEqual(exceptionKeyFieldsNotMatch2.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNotPresentInFields);
            Assert.AreEqual(exceptionKeyFieldsNotMatch3.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNotPresentInFields);
        }

        [TestMethod]
        public override void Upsert_Validations_DataRow_Exception()
        {
            base.Upsert_Validations_DataRow_Exception();
        }

        [TestMethod]
        public override void Upsert_Validations_LazyDbTypeArrays_Exception()
        {
            base.Upsert_Validations_LazyDbTypeArrays_Exception();
        }

        [TestMethod]
        public override void Upsert_DataRow_Modified_Success()
        {
            base.Upsert_DataRow_Modified_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

// TestsLazyDatabaseSqlServerUpdate.cs
//
// This file is integrated part of "Lazy Vinke Tests Database SqlServer" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 04

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
    public class TestsLazyDatabaseSqlServerUpdate : TestsLazyDatabaseUpdate
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Update_Validations_DbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsUpdate";
            String subQuery = "(select * from TestsUpdate)";

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

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.CloseConnection();

            try { databaseSqlServer.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.Update(null, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseSqlServer.Update(subQuery, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databaseSqlServer.Update(tableName, null, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, null, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, dbTypes, null, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsNullButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, dbTypes, fields, null, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesNullButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, dbTypes, fields, keyValues, null, keyFields); } catch (Exception exp) { exceptionKeyDbTypesNullButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypes, null); } catch (Exception exp) { exceptionKeyFieldsNullButOthers = exp; }

            try { databaseSqlServer.Update(tableName, valuesLess, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, dbTypesLess, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, dbTypes, fieldsLess, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, dbTypes, fields, keyValuesLess, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesLessButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypesLess, keyFields); } catch (Exception exp) { exceptionKeyDbTypesLessButOthers = exp; }
            try { databaseSqlServer.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsLess); } catch (Exception exp) { exceptionKeyFieldsLessButOthers = exp; }

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
        }

        [TestMethod]
        public virtual void Update_Arrays_ValuesAndKeys_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsUpdate";
            String sqlDelete = "delete from " + tableName + " where Id between 4000 and 6000";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            String[] fields = new String[] { "Id", "ColumnVarChar", "ColumnDecimal", "ColumnDateTime", "ColumnByte", "ColumnChar" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar, SqlDbType.Decimal, SqlDbType.DateTime, SqlDbType.SmallInt, SqlDbType.Char };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { 4000, "Item 4000", 4000.0m, new DateTime(2023, 11, 09, 17, 25, 30), 2, '1' },
                new Object[] { 5000, "Item 5000", 5000.0m, new DateTime(2023, 11, 09, 17, 25, 30), 4, '0' }
            };

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            databaseSqlServer.Insert(tableName, valuesList[0], dbTypes, fields);
            databaseSqlServer.Insert(tableName, valuesList[1], dbTypes, fields);

            valuesList = new List<Object[]>() {
                new Object[] { 4004, "Item 4004", 4004.4m, new DateTime(2023, 11, 09, 17, 55, 30), 16, '0' },
                new Object[] { 5005, "Item 5005", 5005.5m, new DateTime(2023, 11, 09, 17, 55, 30), 32, '1' }
            };

            String[] keyFields = new String[] { "Id" };
            SqlDbType[] keyDbTypes = new SqlDbType[] { SqlDbType.Int };
            List<Object[]> keyValuesList = new List<Object[]>() {
                new Object[] { 4000 },
                new Object[] { 5000 }
            };

            // Act
            rowsAffected += databaseSqlServer.Update(tableName, valuesList[0], dbTypes, fields, keyValuesList[0], keyDbTypes, keyFields);
            rowsAffected += databaseSqlServer.Update(tableName, valuesList[1], dbTypes, fields, keyValuesList[1], keyDbTypes, keyFields);

            // Assert
            Assert.AreEqual(rowsAffected, 2);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void Update_Validations_DataRow_Exception()
        {
            base.Update_Validations_DataRow_Exception();
        }

        [TestMethod]
        public override void Update_Validations_LazyDbTypeArrays_Exception()
        {
            base.Update_Validations_LazyDbTypeArrays_Exception();
        }

        [TestMethod]
        public override void Update_DataRow_Modified_Success()
        {
            base.Update_DataRow_Modified_Success();
        }

        [TestMethod]
        public override void Update_DataRow_ModifiedOnlyKeys_Success()
        {
            base.Update_DataRow_ModifiedOnlyKeys_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

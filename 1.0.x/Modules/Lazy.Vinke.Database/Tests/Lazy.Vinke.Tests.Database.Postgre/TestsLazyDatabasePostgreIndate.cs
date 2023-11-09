// TestsLazyDatabasePostgreIndate.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Postgre" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 06

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
    public class TestsLazyDatabasePostgreIndate : TestsLazyDatabaseIndate
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Indate_Validations_DbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsIndate";
            String subQuery = "(select * from TestsIndate)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar };
            String[] fields = new String[] { "Id", "Name" };
            String[] keyFields = new String[] { "Id" };

            Object[] valuesLess = new Object[] { 1 };
            NpgsqlDbType[] dbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Numeric };
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

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.Indate(tableName, values, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.Indate(null, values, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databasePostgre.Indate(subQuery, values, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databasePostgre.Indate(tableName, null, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { databasePostgre.Indate(tableName, values, null, fields, keyFields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { databasePostgre.Indate(tableName, values, dbTypes, null, keyFields); } catch (Exception exp) { exceptionDbFieldsNullButOthers = exp; }
            try { databasePostgre.Indate(tableName, values, dbTypes, fields, null); } catch (Exception exp) { exceptionKeyFieldsNullButOthers = exp; }

            try { databasePostgre.Indate(tableName, valuesLess, dbTypes, fields, keyFields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databasePostgre.Indate(tableName, values, dbTypesLess, fields, keyFields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databasePostgre.Indate(tableName, values, dbTypes, fieldsLess, keyFields); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }

            try { databasePostgre.Indate(tableName, values, dbTypes, fields, keyFieldsNotMatch1); } catch (Exception exp) { exceptionKeyFieldsNotMatch1 = exp; }
            try { databasePostgre.Indate(tableName, values, dbTypes, fields, keyFieldsNotMatch2); } catch (Exception exp) { exceptionKeyFieldsNotMatch2 = exp; }
            try { databasePostgre.Indate(tableName, values, dbTypes, fields, keyFieldsNotMatch3); } catch (Exception exp) { exceptionKeyFieldsNotMatch3 = exp; }

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
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Varchar, NpgsqlDbType.Integer, NpgsqlDbType.Varchar };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { testCode, 1, "Lazy" },
                new Object[] { testCode, 2, "Vinke" },
                new Object[] { testCode, 3, "Tests" },
                new Object[] { testCode, 4, "Database" }
            };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            databasePostgre.Insert(tableName, valuesList[0], dbTypes, fields);
            databasePostgre.Insert(tableName, valuesList[1], dbTypes, fields);
            databasePostgre.Insert(tableName, valuesList[2], dbTypes, fields);
            databasePostgre.Insert(tableName, valuesList[3], dbTypes, fields);

            valuesList = new List<Object[]>() {
                new Object[] { testCode, 3, "Isaac" },
                new Object[] { testCode, 4, "Bezerra" },
                new Object[] { testCode, 5, "Saraiva" }
            };

            String[] keyFields = new String[] { "TestCode", "Id" };

            // Act
            rowsAffected += databasePostgre.Indate(tableName, valuesList[0], dbTypes, fields, keyFields);
            rowsAffected += databasePostgre.Indate(tableName, valuesList[1], dbTypes, fields, keyFields);
            rowsAffected += databasePostgre.Indate(tableName, valuesList[2], dbTypes, fields, keyFields);

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

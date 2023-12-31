﻿// TestsLazyDatabaseOracleInsert.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Oracle" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 04

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.Oracle;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.Oracle
{
    [TestClass]
    public class TestsLazyDatabaseOracleInsert : TestsLazyDatabaseInsert
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseOracle(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Insert_Validations_DbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsInsert";
            String subQuery = "(select * from TestsInsert)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            OracleDbType[] dbTypesLess = new OracleDbType[] { OracleDbType.Decimal };
            String[] fieldsLess = new String[] { "Amount" };

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionValuesNullButOthers = null;
            Exception exceptionDbTypesNullButOthers = null;
            Exception exceptionFieldsNullButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionFieldsLessButOthers = null;

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            databaseOracle.CloseConnection();

            try { databaseOracle.Insert(tableName, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            databaseOracle.OpenConnection();

            try { databaseOracle.Insert(null, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseOracle.Insert(subQuery, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databaseOracle.Insert(tableName, null, dbTypes, fields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { databaseOracle.Insert(tableName, values, null, fields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { databaseOracle.Insert(tableName, values, dbTypes, null); } catch (Exception exp) { exceptionFieldsNullButOthers = exp; }

            try { databaseOracle.Insert(tableName, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseOracle.Insert(tableName, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseOracle.Insert(tableName, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionFieldsLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionValuesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLength);
            Assert.AreEqual(exceptionDbTypesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionTypesNullOrZeroLength);
            Assert.AreEqual(exceptionFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
        }

        [TestMethod]
        public virtual void Insert_Arrays_Single_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsInsert";
            String sqlDelete = "delete from " + tableName + " where Id in (4000,5000,6000)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            String[] fields = new String[] { "Id", "ColumnVarChar", "ColumnDecimal", "ColumnDateTime", "ColumnByte", "ColumnChar" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2, OracleDbType.Decimal, OracleDbType.Date, OracleDbType.Int16, OracleDbType.Char };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { 4000, "Item 4000", 4000.1m, new DateTime(2023, 11, 09, 18, 00, 30), 2, '1' },
                new Object[] { 5000, "Item 5000", 5000.1m, new DateTime(2023, 11, 09, 18, 00, 30), 4, '0' },
                new Object[] { 6000, "Item 6000", 6000.1m, new DateTime(2023, 11, 09, 18, 00, 30), 8, '1' }
            };

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            rowsAffected += databaseOracle.Insert(tableName, valuesList[0], dbTypes, fields);
            rowsAffected += databaseOracle.Insert(tableName, valuesList[1], dbTypes, fields);
            rowsAffected += databaseOracle.Insert(tableName, valuesList[2], dbTypes, fields);

            // Assert
            Assert.AreEqual(rowsAffected, 3);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void Insert_Validations_DataRow_Exception()
        {
            base.Insert_Validations_DataRow_Exception();
        }

        [TestMethod]
        public override void Insert_Validations_LazyDbTypeArrays_Exception()
        {
            base.Insert_Validations_LazyDbTypeArrays_Exception();
        }

        [TestMethod]
        public override void Insert_DataRow_Added_Success()
        {
            base.Insert_DataRow_Added_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

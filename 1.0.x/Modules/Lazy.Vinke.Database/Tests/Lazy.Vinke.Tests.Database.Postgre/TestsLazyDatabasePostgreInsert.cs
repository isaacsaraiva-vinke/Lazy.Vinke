// TestsLazyDatabasePostgreInsert.cs
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
    public class TestsLazyDatabasePostgreInsert : TestsLazyDatabaseInsert
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Insert_Validations_DbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsInsert";
            String subQuery = "(select * from TestsInsert)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            NpgsqlDbType[] dbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Numeric };
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

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.Insert(tableName, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.Insert(null, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databasePostgre.Insert(subQuery, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databasePostgre.Insert(tableName, null, dbTypes, fields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { databasePostgre.Insert(tableName, values, null, fields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { databasePostgre.Insert(tableName, values, dbTypes, null); } catch (Exception exp) { exceptionFieldsNullButOthers = exp; }

            try { databasePostgre.Insert(tableName, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databasePostgre.Insert(tableName, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databasePostgre.Insert(tableName, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionFieldsLessButOthers = exp; }

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
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar, NpgsqlDbType.Numeric, NpgsqlDbType.Timestamp, NpgsqlDbType.Smallint, NpgsqlDbType.Char };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { 4000, "Item 4000", 4000.1m, new DateTime(2023, 11, 09, 18, 00, 30), 2, '1' },
                new Object[] { 5000, "Item 5000", 5000.1m, new DateTime(2023, 11, 09, 18, 00, 30), 4, '0' },
                new Object[] { 6000, "Item 6000", 6000.1m, new DateTime(2023, 11, 09, 18, 00, 30), 8, '1' }
            };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            rowsAffected += databasePostgre.Insert(tableName, valuesList[0], dbTypes, fields);
            rowsAffected += databasePostgre.Insert(tableName, valuesList[1], dbTypes, fields);
            rowsAffected += databasePostgre.Insert(tableName, valuesList[2], dbTypes, fields);

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

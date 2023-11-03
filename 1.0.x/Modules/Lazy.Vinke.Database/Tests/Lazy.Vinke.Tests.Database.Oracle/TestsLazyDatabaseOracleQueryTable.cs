﻿// TestsLazyDatabaseOracleQueryTable.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Oracle" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.Oracle;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.Oracle
{
    [TestClass]
    public class TestsLazyDatabaseOracleQueryTable : TestsLazyDatabaseQueryTable
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseOracle(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void QueryTable_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "insert into QueryTable_Validations_DbmsDbType (id, name) values (@id, @name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            OracleDbType[] dbTypesLess = new OracleDbType[] { OracleDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            databaseOracle.CloseConnection();

            try { databaseOracle.QueryTable(sql, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseOracle.OpenConnection();

            try { databaseOracle.QueryTable(null, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseOracle.QueryTable(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseOracle.QueryTable(sql, "tableName", valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public virtual void QueryTable_DataAdapterFill_DbmsDbType_Success()
        {
            // Arrange
            String tableName = "QueryTable_DataAdapterFill";
            String columnsName = "Code, Elements, Active";
            String columnsParameter = "@Code, @Elements, @Active";
            String sqlDelete = "delete from QueryTable_DataAdapterFill where Code in ('Array3','Array4')";
            String sqlInsert = "insert into QueryTable_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            databaseOracle.Execute(sqlInsert, new Object[] { "Array3", new Byte[] { 56, 64 }, '0' });
            databaseOracle.Execute(sqlInsert, new Object[] { "Array4", new Byte[] { 72, 86 }, '1' });

            Object[] values = new Object[] { "Array3", "Array4" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Varchar2, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "Code1", "Code2" };

            // Act
            DataTable dataTable = databaseOracle.QueryTable("select * from QueryTable_DataAdapterFill where (Code = @Code1 or Code = @Code2)", tableName, values, dbTypes, parameters);

            // Assert
            Assert.AreEqual(dataTable.Rows.Count, 2);
            Assert.AreEqual(dataTable.TableName, tableName);
            Assert.AreEqual(Convert.ToString(dataTable.Rows[0]["Code"]), "Array3");
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["Elements"])[0], (Byte)56);
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["Elements"])[1], (Byte)64);
            Assert.AreEqual(Convert.ToChar(dataTable.Rows[0]["Active"]), '0');
            Assert.AreEqual(Convert.ToString(dataTable.Rows[1]["Code"]), "Array4");
            Assert.AreEqual(((Byte[])dataTable.Rows[1]["Elements"])[0], (Byte)72);
            Assert.AreEqual(((Byte[])dataTable.Rows[1]["Elements"])[1], (Byte)86);
            Assert.AreEqual(Convert.ToChar(dataTable.Rows[1]["Active"]), '1');

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryTable_Validations_LazyDbType_Exception()
        {
            base.QueryTable_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void QueryTable_DataAdapterFill_LazyDbType_Success()
        {
            base.QueryTable_DataAdapterFill_LazyDbType_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

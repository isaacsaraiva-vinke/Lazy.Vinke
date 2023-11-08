// TestsLazyDatabaseSqlServerQueryTable.cs
//
// This file is integrated part of "Lazy Vinke Tests Database SqlServer" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

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
    public class TestsLazyDatabaseSqlServerQueryTable : TestsLazyDatabaseQueryTable
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void QueryTable_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String tableName = "TestsQueryTable";
            String sql = "select * from TestsQueryTable where Code = @Code";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.VarChar };
            String[] parameters = new String[] { "Code", "Active" };

            Object[] valuesLess = new Object[] { 1 };
            SqlDbType[] dbTypesLess = new SqlDbType[] { SqlDbType.Int };
            String[] parametersLess = new String[] { "Code" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.CloseConnection();

            try { databaseSqlServer.QueryTable(sql, tableName, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.QueryTable(null, tableName, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseSqlServer.QueryTable(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseSqlServer.QueryTable(sql, tableName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseSqlServer.QueryTable(sql, tableName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseSqlServer.QueryTable(sql, tableName, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseSqlServer.QueryTable(sql, tableName, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.QueryTable(sql, tableName, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.QueryTable(sql, tableName, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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
            String tableName = "TestsQueryTable";
            String columnsName = "Code, Elements, Active";
            String columnsParameter = "@Code, @Elements, @Active";
            String sqlDelete = "delete from " + tableName + " where Code in ('Array3','Array4')";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            databaseSqlServer.Execute(sqlInsert, new Object[] { "Array3", new Byte[] { 56, 64 }, '0' });
            databaseSqlServer.Execute(sqlInsert, new Object[] { "Array4", new Byte[] { 72, 86 }, '1' });

            Object[] values = new Object[] { "Array3", "Array4" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.VarChar, SqlDbType.VarChar };
            String[] parameters = new String[] { "Code1", "Code2" };

            // Act
            DataTable dataTable = databaseSqlServer.QueryTable("select * from " + tableName + " where (Code = @Code1 or Code = @Code2)", tableName, values, dbTypes, parameters);

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

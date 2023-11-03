// TestsLazyDatabaseSqlServerQueryFind.cs
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

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.SqlServer;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.SqlServer
{
    [TestClass]
    public class TestsLazyDatabaseSqlServerQueryFind : TestsLazyDatabaseQueryFind
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void QueryFind_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "insert into TestsQueryFind (Id, Code) values (@Id, @Code)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.VarChar };
            String[] parameters = new String[] { "Id", "Code" };

            Object[] valuesLess = new Object[] { 1 };
            SqlDbType[] dbTypesLess = new SqlDbType[] { SqlDbType.Int };
            String[] parametersLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.CloseConnection();

            try { databaseSqlServer.QueryFind(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.QueryFind(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseSqlServer.QueryFind(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseSqlServer.QueryFind(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseSqlServer.QueryFind(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseSqlServer.QueryFind(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.QueryFind(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.QueryFind(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public virtual void QueryFind_DataAdapterFill_DbmsDbType_Success()
        {
            // Arrange
            String tableName = "TestsQueryFind";
            String columnsName = "Id, Code, Description, Amount";
            String columnsParameter = "@Id, @Code, @Description, @Amount";
            String sqlDelete = "delete from " + tableName + " where Id in (500,600,700,800)";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            databaseSqlServer.Execute(sqlInsert, new Object[] { 500, "C500", "Test 500", 500.5m });
            databaseSqlServer.Execute(sqlInsert, new Object[] { 600, "C600", "Test 600", 600.6m });
            databaseSqlServer.Execute(sqlInsert, new Object[] { 700, "C700", null, 700.7m });
            databaseSqlServer.Execute(sqlInsert, new Object[] { 800, "C800", "Test 700", 800.8m });

            // Act
            Boolean test1Result = databaseSqlServer.QueryFind("select 1 from " + tableName + " where Id = @Id", new Object[] { 500 }, new SqlDbType[] { SqlDbType.Int }, new String[] { "Id" });
            Boolean test2Result = databaseSqlServer.QueryFind("select 1 from " + tableName + " where Code = @Code", new Object[] { "C650" }, new SqlDbType[] { SqlDbType.VarChar }, new String[] { "Code" });
            Boolean test3Result = databaseSqlServer.QueryFind("select 1 from " + tableName + " where Description is null", null);
            Boolean test4Result = databaseSqlServer.QueryFind("select 1 from " + tableName + " where Amount > @Amount", new Object[] { 800.8m }, new SqlDbType[] { SqlDbType.Decimal }, new String[] { "Amount" });

            // Assert
            Assert.IsTrue(test1Result);
            Assert.IsFalse(test2Result);
            Assert.IsTrue(test3Result);
            Assert.IsFalse(test4Result);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryFind_Validations_LazyDbType_Exception()
        {
            base.QueryFind_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void QueryFind_DataAdapterFill_LazyDbType_Success()
        {
            base.QueryFind_DataAdapterFill_LazyDbType_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

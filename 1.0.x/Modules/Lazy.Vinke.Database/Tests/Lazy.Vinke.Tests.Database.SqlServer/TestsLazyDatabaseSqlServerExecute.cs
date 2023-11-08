// TestsLazyDatabaseSqlServerExecute.cs
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
    public class TestsLazyDatabaseSqlServerExecute : TestsLazyDatabaseExecute
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void Execute_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "insert into TestsExecuteDbmsDbType (Id, Name) values (@Id, @Name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.SmallInt, SqlDbType.VarChar };
            String[] parameters = new String[] { "Id", "Name" };

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

            try { databaseSqlServer.Execute(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.Execute(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseSqlServer.Execute(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseSqlServer.Execute(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseSqlServer.Execute(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseSqlServer.Execute(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.Execute(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.Execute(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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
        public void Execute_ExecuteNonQuery_WithValuesDbmsType_Success()
        {
            // Arrange
            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar };
            String sqlCreate = "create table TestsExecuteDbmsType ( Id integer, Name varchar(256) )";
            String sqlInsert = "insert into TestsExecuteDbmsType (Id, Name) values (@Id, @Name)";
            String sqlDrop = "drop table TestsExecuteDbmsType";
            try { this.Database.Execute(sqlDrop, null); }
            catch { /* Just to be sure that the table will not exists */ }

            // Act
            this.Database.Execute(sqlCreate, null);
            Int32 affectedRecord = ((LazyDatabaseSqlServer)this.Database).Execute(sqlInsert, values, dbTypes);
            this.Database.Execute(sqlDrop, null);

            // Assert
            Assert.AreEqual(affectedRecord, 1);
        }

        [TestMethod]
        public override void Execute_Validations_LazyDbType_Exception()
        {
            base.Execute_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void Execute_ExecuteNonQuery_DropTable_Success()
        {
            base.Execute_ExecuteNonQuery_DropTable_Success();
        }

        [TestMethod]
        public override void Execute_ExecuteNonQuery_InsertValues_Success()
        {
            base.Execute_ExecuteNonQuery_InsertValues_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

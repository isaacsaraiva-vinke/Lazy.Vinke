// TestsLazyDatabaseSqlServerExecuteProcedure.cs
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
    public class TestsLazyDatabaseSqlServerExecuteProcedure : TestsLazyDatabaseExecuteProcedure
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void ExecuteProcedure_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String procName = "Sp_TestsExecuteProcedure";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar };
            String[] parameters = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            SqlDbType[] dbTypesLess = new SqlDbType[] { SqlDbType.Int };
            String[] parametersLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionProcNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.CloseConnection();

            try { databaseSqlServer.ExecuteProcedure(procName, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.ExecuteProcedure(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionProcNameNull = exp; }
            try { databaseSqlServer.ExecuteProcedure(procName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseSqlServer.ExecuteProcedure(procName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseSqlServer.ExecuteProcedure(procName, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseSqlServer.ExecuteProcedure(procName, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.ExecuteProcedure(procName, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.ExecuteProcedure(procName, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionProcNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public void ExecuteProcedure_ExecuteNonQuery_DbmsDbType_Success()
        {
            // Arrange
            String procedureName = "Sp_TestsExecuteProcedure";
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar };
            String[] parameters = new String[] { "Id", "Name", "Description" };
            String sqlSelect = "select count(*) from TestsExecuteProcedure where Id in (5,6,7,8)";
            String sqlDelete = "delete from TestsExecuteProcedure where Id in (5,6,7,8)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.ExecuteProcedure(procedureName, new Object[] { 5, "SqlServer Lazy", "Description SqlServer Lazy" }, dbTypes, parameters);
            databaseSqlServer.ExecuteProcedure(procedureName, new Object[] { 6, "SqlServer Vinke", "Description SqlServer Vinke" }, dbTypes, parameters);
            databaseSqlServer.ExecuteProcedure(procedureName, new Object[] { 7, "SqlServer Tests", "Description SqlServer Tests" }, dbTypes, parameters);
            databaseSqlServer.ExecuteProcedure(procedureName, new Object[] { 8, "SqlServer Database", "Description SqlServer Database" }, dbTypes, parameters);

            Int32 count = Convert.ToInt32(databaseSqlServer.QueryValue(sqlSelect, null));

            // Assert
            Assert.AreEqual(count, 4);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void ExecuteProcedure_Validations_LazyDbType_Exception()
        {
            base.ExecuteProcedure_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void ExecuteProcedure_ExecuteNonQuery_LazyDbType_Success()
        {
            base.ExecuteProcedure_ExecuteNonQuery_LazyDbType_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

// TestsLazyDatabaseMySqlExecuteProcedure.cs
//
// This file is integrated part of "Lazy Vinke Tests Database MySql" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.MySql;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.MySql
{
    [TestClass]
    public class TestsLazyDatabaseMySqlExecuteProcedure : TestsLazyDatabaseExecuteProcedure
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseMySql(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void ExecuteProcedure_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String procName = "Sp_TestsExecuteProcedure";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarChar };
            String[] parameters = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            MySqlDbType[] dbTypesLess = new MySqlDbType[] { MySqlDbType.Int32 };
            String[] parametersLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionProcNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.CloseConnection();

            try { databaseMySql.ExecuteProcedure(procName, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseMySql.OpenConnection();

            try { databaseMySql.ExecuteProcedure(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionProcNameNull = exp; }
            try { databaseMySql.ExecuteProcedure(procName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseMySql.ExecuteProcedure(procName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseMySql.ExecuteProcedure(procName, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseMySql.ExecuteProcedure(procName, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseMySql.ExecuteProcedure(procName, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseMySql.ExecuteProcedure(procName, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarString, MySqlDbType.VarString };
            String[] parameters = new String[] { "Id", "Name", "Description" };
            String sqlSelect = "select count(*) from TestsExecuteProcedure where Id in (5,6,7,8)";
            String sqlDelete = "delete from TestsExecuteProcedure where Id in (5,6,7,8)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.ExecuteProcedure(procedureName, new Object[] { 5, "MySql Lazy", "Description MySql Lazy" }, dbTypes, parameters);
            databaseMySql.ExecuteProcedure(procedureName, new Object[] { 6, "MySql Vinke", "Description MySql Vinke" }, dbTypes, parameters);
            databaseMySql.ExecuteProcedure(procedureName, new Object[] { 7, "MySql Tests", "Description MySql Tests" }, dbTypes, parameters);
            databaseMySql.ExecuteProcedure(procedureName, new Object[] { 8, "MySql Database", "Description MySql Database" }, dbTypes, parameters);

            Int32 count = Convert.ToInt32(databaseMySql.QueryValue(sqlSelect, null));

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

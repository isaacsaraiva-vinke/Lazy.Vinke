// TestsLazyDatabasePostgreExecuteProcedure.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Postgre" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Npgsql;
using NpgsqlTypes;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.Postgre;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.Postgre
{
    [TestClass]
    public class TestsLazyDatabasePostgreExecuteProcedure : TestsLazyDatabaseExecuteProcedure
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void ExecuteProcedure_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String procName = "ExecuteProcedure_Validations_DbmsDbType";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            NpgsqlDbType[] dbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Integer };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionProcNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.ExecuteProcedure(procName, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.ExecuteProcedure(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionProcNameNull = exp; }
            try { databasePostgre.ExecuteProcedure(procName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databasePostgre.ExecuteProcedure(procName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databasePostgre.ExecuteProcedure(procName, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databasePostgre.ExecuteProcedure(procName, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databasePostgre.ExecuteProcedure(procName, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databasePostgre.ExecuteProcedure(procName, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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
            String procedureName = "ExecuteProcedure_ExecuteNonQuery";
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar, NpgsqlDbType.Varchar };
            String[] parameters = new String[] { "Id", "Name", "Description" };
            String sqlSelect = "select count(*) from QueryProc_ExecuteNonQuery where Id in (5,6,7,8)";
            String sqlDelete = "delete from QueryProc_ExecuteNonQuery where Id in (5,6,7,8)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.ExecuteProcedure(procedureName, new Object[] { 5, "NpgsqlLazy", "Description Npgsql Lazy" }, dbTypes, parameters);
            databasePostgre.ExecuteProcedure(procedureName, new Object[] { 6, "NpgsqlVinke", "Description Npgsql Vinke" }, dbTypes, parameters);
            databasePostgre.ExecuteProcedure(procedureName, new Object[] { 7, "NpgsqlTests", "Description Npgsql Tests" }, dbTypes, parameters);
            databasePostgre.ExecuteProcedure(procedureName, new Object[] { 8, "NpgsqlDatabase", "Description Npgsql Database" }, dbTypes, parameters);

            Int32 count = Convert.ToInt32(databasePostgre.QueryValue(sqlSelect, null));

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

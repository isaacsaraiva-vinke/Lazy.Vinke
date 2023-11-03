// TestsLazyDatabaseExecuteProcedure.cs
//
// This file is integrated part of "Lazy Vinke Tests Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;

namespace Lazy.Vinke.Tests.Database
{
    public class TestsLazyDatabaseExecuteProcedure
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void ExecuteProcedure_Validations_LazyDbType_Exception()
        {
            // Arrange
            String procName = "ExecuteProcedure_Validations_LazyDbType";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionProcNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.ExecuteProcedure(procName, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.ExecuteProcedure(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionProcNameNull = exp; }
            try { this.Database.ExecuteProcedure(procName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.ExecuteProcedure(procName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.ExecuteProcedure(procName, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.ExecuteProcedure(procName, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.ExecuteProcedure(procName, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.ExecuteProcedure(procName, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void ExecuteProcedure_ExecuteNonQuery_LazyDbType_Success()
        {
            // Arrange
            String procedureName = "ExecuteProcedure_ExecuteNonQuery";
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar, LazyDbType.VarChar };
            String[] parameters = new String[] { "Id", "Name", "Description" };
            String sqlSelect = "select count(*) from QueryProc_ExecuteNonQuery where Id in (1,2,3,4)";
            String sqlDelete = "delete from QueryProc_ExecuteNonQuery where Id in (1,2,3,4)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            // Act
            this.Database.ExecuteProcedure(procedureName, new Object[] { 1, "Lazy", "Description Lazy" }, dbTypes, parameters);
            this.Database.ExecuteProcedure(procedureName, new Object[] { 2, "Vinke", "Description Vinke" }, dbTypes, parameters);
            this.Database.ExecuteProcedure(procedureName, new Object[] { 3, "Tests", "Description Tests" }, dbTypes, parameters);
            this.Database.ExecuteProcedure(procedureName, new Object[] { 4, "Database", "Description Database" }, dbTypes, parameters);

            Int32 count = Convert.ToInt32(this.Database.QueryValue(sqlSelect, null));

            // Assert
            Assert.AreEqual(count, 4);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

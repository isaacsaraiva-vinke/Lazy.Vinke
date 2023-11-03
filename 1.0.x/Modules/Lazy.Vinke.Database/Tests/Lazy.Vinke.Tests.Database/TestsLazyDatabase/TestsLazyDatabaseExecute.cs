// TestsLazyDatabaseExecute.cs
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
    public class TestsLazyDatabaseExecute
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void Execute_Validations_LazyDbType_Exception()
        {
            // Arrange
            String sql = "insert into Execute_Validations_LazyDbType (id, name) values (@id, @name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Execute(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Execute(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.Execute(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.Execute(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.Execute(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.Execute(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.Execute(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.Execute(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void Execute_ExecuteNonQuery_WithoutValues_Success()
        {
            // Arrange
            String sqlCreate = "create table NonQuery_WithoutValues ( id int, name varchar(256) )";
            String sqlDrop = "drop table NonQuery_WithoutValues";
            String sqlInsert = "insert into NonQuery_WithoutValues (id, name) values (1, 'Lazy.Vinke.Database')";
            try { this.Database.Execute(sqlDrop, null); }
            catch { /* Just to be sure that the table will not exists */ }

            // Act
            this.Database.Execute(sqlCreate, null);
            this.Database.Execute(sqlDrop, null);

            Exception exception = null;
            try { this.Database.Execute(sqlInsert, null); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.IsNotNull(exception);
        }

        public virtual void Execute_ExecuteNonQuery_WithValues_Success()
        {
            // Arrange
            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            String sqlCreate = "create table NonQuery_WithValues ( id int, name varchar(256) )";
            String sqlInsert = "insert into NonQuery_WithValues (id, name) values (@id, @name)";
            String sqlDrop = "drop table NonQuery_WithValues";
            try { this.Database.Execute(sqlDrop, null); }
            catch { /* Just to be sure that the table will not exists */ }

            // Act
            this.Database.Execute(sqlCreate, null);
            Int32 affectedRecord = this.Database.Execute(sqlInsert, values);
            this.Database.Execute(sqlDrop, null);

            // Assert
            Assert.AreEqual(affectedRecord, 1);
        }

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

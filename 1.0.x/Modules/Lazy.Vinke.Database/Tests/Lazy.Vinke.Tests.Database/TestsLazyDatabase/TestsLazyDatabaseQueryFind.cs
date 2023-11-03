// TestsLazyDatabaseQueryFind.cs
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
    public class TestsLazyDatabaseQueryFind
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void QueryFind_Validations_LazyDbType_Exception()
        {
            // Arrange
            String sql = "select * from TestsQueryFind where Id = @Id";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "Id", "Code" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] parametersLess = new String[] { "Id" };

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

            try { this.Database.QueryFind(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.QueryFind(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.QueryFind(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.QueryFind(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.QueryFind(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.QueryFind(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.QueryFind(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.QueryFind(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void QueryFind_DataAdapterFill_LazyDbType_Success()
        {
            // Arrange
            String tableName = "TestsQueryFind";
            String columnsName = "Id, Code, Description, Amount";
            String columnsParameter = "@Id, @Code, @Description, @Amount";
            String sqlDelete = "delete from " + tableName + " where Id in (100,200,300,400)";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 100, "C100", "Test 100", 100.1m });
            this.Database.Execute(sqlInsert, new Object[] { 200, "C200", "Test 200", 200.2m });
            this.Database.Execute(sqlInsert, new Object[] { 300, "C300", DBNull.Value, 300.3m });
            this.Database.Execute(sqlInsert, new Object[] { 400, "C400", "Test 400", 400.4m });

            // Act
            Boolean test1Result = this.Database.QueryFind("select 1 from " + tableName + " where Id = @Id", new Object[] { 100 });
            Boolean test2Result = this.Database.QueryFind("select 1 from " + tableName + " where Code = @Code", new Object[] { "C350" });
            Boolean test3Result = this.Database.QueryFind("select 1 from " + tableName + " where Description is null", null);
            Boolean test4Result = this.Database.QueryFind("select 1 from " + tableName + " where Amount > @Amount", new Object[] { 400.4m });

            // Assert
            Assert.IsTrue(test1Result);
            Assert.IsFalse(test2Result);
            Assert.IsTrue(test3Result);
            Assert.IsFalse(test4Result);

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

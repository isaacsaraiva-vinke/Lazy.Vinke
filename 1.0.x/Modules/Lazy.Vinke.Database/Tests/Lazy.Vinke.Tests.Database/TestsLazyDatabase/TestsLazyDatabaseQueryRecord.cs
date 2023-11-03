// TestsLazyDatabaseQueryRecord.cs
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
    public class TestsLazyDatabaseQueryRecord
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void QueryRecord_Validations_LazyDbType_Exception()
        {
            // Arrange
            String sql = "select * from QueryRecord_Validations_LazyDbType where id = @id";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
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

            // Act
            this.Database.CloseConnection();

            try { this.Database.QueryRecord(sql, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.QueryRecord(null, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.QueryRecord(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.QueryRecord(sql, "tableName", values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.QueryRecord(sql, "tableName", null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.QueryRecord(sql, "tableName", null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.QueryRecord(sql, "tableName", valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.QueryRecord(sql, "tableName", values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.QueryRecord(sql, "tableName", values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void QueryRecord_DataAdapterFill_LazyDbType_Success()
        {
            // Arrange
            String tableName = "QueryRecord_DataAdapterFill";
            String columnsName = "Id, Name, Birthdate";
            String columnsParameter = "@Id, @Name, @Birthdate";
            String sqlDelete = "delete from QueryRecord_DataAdapterFill where Id in (1,2,3,4)";
            String sqlInsert = "insert into QueryRecord_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 1, "Lazy", new DateTime(1986, 9, 14) });
            this.Database.Execute(sqlInsert, new Object[] { 2, "Vinke", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 3, "Tests", new DateTime(1988, 7, 24) });
            this.Database.Execute(sqlInsert, new Object[] { 4, DBNull.Value, new DateTime(1989, 6, 29) });

            // Act
            DataRow dataRecord1 = this.Database.QueryRecord("select * from QueryRecord_DataAdapterFill where Id = @Id", tableName, new Object[] { 1 });
            DataRow dataRecord2 = this.Database.QueryRecord("select Name, Birthdate from QueryRecord_DataAdapterFill where Name = @Name", String.Empty, new Object[] { "Vinke" });
            DataRow dataRecord3 = this.Database.QueryRecord("select Birthdate from QueryRecord_DataAdapterFill where Id = @Id", tableName, new Object[] { 5 });
            DataRow dataRecord4 = this.Database.QueryRecord("select Name, Birthdate from QueryRecord_DataAdapterFill where Name is null", String.Empty, null);

            // Assert
            Assert.AreEqual(dataRecord1.Table.TableName, tableName);
            Assert.AreEqual(Convert.ToInt16(dataRecord1["Id"]), (Int16)1);
            Assert.AreEqual(Convert.ToString(dataRecord1["Name"]), "Lazy");
            Assert.AreEqual(Convert.ToDateTime(dataRecord1["Birthdate"]), new DateTime(1986, 9, 14));
            Assert.AreEqual(dataRecord2.Table.TableName, String.Empty);
            Assert.AreEqual(Convert.ToString(dataRecord2["Name"]), "Vinke");
            Assert.AreEqual(dataRecord2["Birthdate"], DBNull.Value);
            Assert.IsNull(dataRecord3);
            Assert.AreEqual(dataRecord4.Table.TableName, String.Empty);
            Assert.AreEqual(dataRecord4["Name"], DBNull.Value);
            Assert.AreEqual(Convert.ToDateTime(dataRecord4["Birthdate"]), new DateTime(1989, 6, 29));

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

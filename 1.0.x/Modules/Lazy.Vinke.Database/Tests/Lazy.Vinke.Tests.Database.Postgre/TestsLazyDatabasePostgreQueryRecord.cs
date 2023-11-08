// TestsLazyDatabasePostgreQueryRecord.cs
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

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.Postgre;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.Postgre
{
    [TestClass]
    public class TestsLazyDatabasePostgreQueryRecord : TestsLazyDatabaseQueryRecord
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void QueryRecord_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String tableName = "TestsQueryRecord";
            String sql = "select * from TestsQueryRecord where Id = @Id";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Smallint, NpgsqlDbType.Varchar };
            String[] parameters = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            NpgsqlDbType[] dbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Integer };
            String[] parametersLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.QueryRecord(sql, tableName, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.QueryRecord(null, tableName, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databasePostgre.QueryRecord(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databasePostgre.QueryRecord(sql, tableName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databasePostgre.QueryRecord(sql, tableName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databasePostgre.QueryRecord(sql, tableName, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databasePostgre.QueryRecord(sql, tableName, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databasePostgre.QueryRecord(sql, tableName, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databasePostgre.QueryRecord(sql, tableName, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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
        public virtual void QueryRecord_DataAdapterFill_DbmsDbType_Success()
        {
            // Arrange
            String tableName = "TestsQueryRecord";
            String columnsName = "Id, Name, Birthdate";
            String columnsParameter = "@Id, @Name, @Birthdate";
            String sqlDelete = "delete from " + tableName + " where Id in (500,600,700,800)";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            databasePostgre.Execute(sqlInsert, new Object[] { 500, "Postgre Lazy", new DateTime(1986, 9, 14) });
            databasePostgre.Execute(sqlInsert, new Object[] { 600, "Postgre Vinke", DBNull.Value });
            databasePostgre.Execute(sqlInsert, new Object[] { 700, "Postgre Tests", new DateTime(1988, 7, 24) });
            databasePostgre.Execute(sqlInsert, new Object[] { 800, DBNull.Value, new DateTime(1989, 6, 29) });

            // Act
            DataRow dataRecord1 = databasePostgre.QueryRecord("select * from TestsQueryRecord where Id = @Id", tableName, new Object[] { 500 }, new NpgsqlDbType[] { NpgsqlDbType.Smallint }, new String[] { "Id" });
            DataRow dataRecord2 = databasePostgre.QueryRecord("select Name, Birthdate from TestsQueryRecord where Name = @Name", String.Empty, new Object[] { "Postgre Vinke" }, new NpgsqlDbType[] { NpgsqlDbType.Varchar }, new String[] { "Name" });
            DataRow dataRecord3 = databasePostgre.QueryRecord("select Birthdate from TestsQueryRecord where Id = @Id", tableName, new Object[] { 650 }, new NpgsqlDbType[] { NpgsqlDbType.Smallint }, new String[] { "Id" });
            DataRow dataRecord4 = databasePostgre.QueryRecord("select Name, Birthdate from TestsQueryRecord where Name is null and Id = @Id", String.Empty, new Object[] { 800 }, new NpgsqlDbType[] { NpgsqlDbType.Smallint }, new String[] { "Id" });

            // Assert
            Assert.AreEqual(dataRecord1.Table.TableName, tableName);
            Assert.AreEqual(Convert.ToInt16(dataRecord1["Id"]), (Int16)500);
            Assert.AreEqual(Convert.ToString(dataRecord1["Name"]), "Postgre Lazy");
            Assert.AreEqual(Convert.ToDateTime(dataRecord1["Birthdate"]), new DateTime(1986, 9, 14));
            Assert.AreEqual(dataRecord2.Table.TableName, String.Empty);
            Assert.AreEqual(Convert.ToString(dataRecord2["Name"]), "Postgre Vinke");
            Assert.AreEqual(dataRecord2["Birthdate"], DBNull.Value);
            Assert.IsNull(dataRecord3);
            Assert.AreEqual(dataRecord4.Table.TableName, String.Empty);
            Assert.AreEqual(dataRecord4["Name"], DBNull.Value);
            Assert.AreEqual(Convert.ToDateTime(dataRecord4["Birthdate"]), new DateTime(1989, 6, 29));

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryRecord_Validations_LazyDbType_Exception()
        {
            base.QueryRecord_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void QueryRecord_DataAdapterFill_LazyDbType_Success()
        {
            base.QueryRecord_DataAdapterFill_LazyDbType_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

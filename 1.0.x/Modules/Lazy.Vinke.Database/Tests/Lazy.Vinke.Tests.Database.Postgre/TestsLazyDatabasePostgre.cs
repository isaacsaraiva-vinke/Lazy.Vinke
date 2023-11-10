// TestsLazyDatabasePostgre.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Postgre" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

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
    public class TestsLazyDatabasePostgre : TestsLazyDatabase
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public override void Constructor_Parameter_Null_Success()
        {
            base.Constructor_Parameter_Null_Success();
        }

        [TestMethod]
        public override void Constructor_Parameter_Valued_Success()
        {
            base.Constructor_Parameter_Valued_Success();
        }

        [TestMethod]
        public override void OpenConnection_ConnectionString_StringNullOrEmpty_Exception()
        {
            base.OpenConnection_ConnectionString_StringNullOrEmpty_Exception();
        }

        [TestMethod]
        public override void OpenConnection_ConnectionState_AlreadyOpen_Exception()
        {
            base.OpenConnection_ConnectionState_AlreadyOpen_Exception();
        }

        [TestMethod]
        public override void OpenConnection_ConnectionState_Opened_Success()
        {
            base.OpenConnection_ConnectionState_Opened_Success();
        }

        [TestMethod]
        public override void CloseConnection_ConnectionState_AlreadyClose_Exception()
        {
            base.CloseConnection_ConnectionState_AlreadyClose_Exception();
        }

        [TestMethod]
        public override void CloseConnection_ConnectionState_Close_Success()
        {
            base.CloseConnection_ConnectionState_Close_Success();
        }

        [TestMethod]
        public override void BeginTransaction_Connection_NotOpen_Exception()
        {
            base.BeginTransaction_Connection_NotOpen_Exception();
        }

        [TestMethod]
        public override void BeginTransaction_Transaction_AlreadyOpen_Exception()
        {
            base.BeginTransaction_Transaction_AlreadyOpen_Exception();
        }

        [TestMethod]
        public override void BeginTransaction_Transaction_NotOpen_Success()
        {
            base.BeginTransaction_Transaction_NotOpen_Success();
        }

        [TestMethod]
        public override void CommitTransaction_Commit_Single_Success()
        {
            base.CommitTransaction_Commit_Single_Success();
        }

        [TestMethod]
        public override void RollbackTransaction_Rollback_Single_Success()
        {
            base.RollbackTransaction_Rollback_Single_Success();
        }

        [TestMethod]
        public override void CreateNew_Instance_ConnectionOwnerNull_Success()
        {
            base.CreateNew_Instance_ConnectionOwnerNull_Success();
        }

        [TestMethod]
        public override void CreateNew_Instance_ConnectionOwnerValued_Success()
        {
            base.CreateNew_Instance_ConnectionOwnerValued_Success();
        }

        [TestMethod]
        public void QueryLike_DataAdapterFill_DbmsDbType_Success()
        {
            // Arrange
            String tableName = "TestsQueryLike";
            String columnsName = "TestId, Content, Notes";
            String columnsParameter = "@TestId, @Content, @Notes";
            String sqlDelete = "delete from " + tableName + " where TestId in (20,21,22)";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            databasePostgre.Execute(sqlInsert, new Object[] { 20, "Content 20", "Notes 20 Notes 20 Notes 20 Notes 20" });
            databasePostgre.Execute(sqlInsert, new Object[] { 21, "21 Content", "21 Notes 21 Notes 21 Notes 21 Notes 21 Notes" });
            databasePostgre.Execute(sqlInsert, new Object[] { 22, "Content 22 Content", "Notes 22 22 Notes 22 22 Notes 22 22 Notes" });

            // Act
            DataRow dataRowTest1 = databasePostgre.QueryRecord("select * from " + tableName + " where cast(TestId as varchar(2048)) like @TestId", tableName, new Object[] { "%20" });
            DataRow dataRowTest2 = databasePostgre.QueryRecord("select * from " + tableName + " where cast(TestId as varchar(2048)) like @TestId", tableName, new Object[] { "21%" });
            DataRow dataRowTest3 = databasePostgre.QueryRecord("select * from " + tableName + " where cast(TestId as varchar(2048)) like @TestId", tableName, new Object[] { "%22%" });

            // Assert
            Assert.IsNotNull(dataRowTest1);
            Assert.IsNotNull(dataRowTest2);
            Assert.IsNotNull(dataRowTest3);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryLike_DataAdapterFill_LazyDbType_Success()
        {
            base.QueryLike_DataAdapterFill_LazyDbType_Success();
        }

        [TestMethod]
        public override void ConvertSystemTypeToLazyDbType_LazyDbTypeArray_Single_Success()
        {
            base.ConvertSystemTypeToLazyDbType_LazyDbTypeArray_Single_Success();
        }

        [TestMethod]
        public void ConvertLazyDbTypeToDbmsType_SqlDbType_Single_Success()
        {
            // Arrange
            MethodInfo methodInfo = this.Database.GetType().GetMethod("ConvertLazyDbTypeToDbmsType", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            // Act
            NpgsqlDbType dbTypeNull = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DBNull });
            NpgsqlDbType dbTypeChar = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Char });
            NpgsqlDbType dbTypeVarChar = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarChar });
            NpgsqlDbType dbTypeVarText = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarText });
            NpgsqlDbType dbTypeByte = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Byte });
            NpgsqlDbType dbTypeInt16 = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int16 });
            NpgsqlDbType dbTypeInt32 = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int32 });
            NpgsqlDbType dbTypeInt64 = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int64 });
            NpgsqlDbType dbTypeUByte = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.UByte });
            NpgsqlDbType dbTypeFloat = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Float });
            NpgsqlDbType dbTypeDouble = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Double });
            NpgsqlDbType dbTypeDecimal = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Decimal });
            NpgsqlDbType dbTypeDateTime = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DateTime });
            NpgsqlDbType dbTypeVarUByte = (NpgsqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarUByte });

            // Assert
            Assert.AreEqual(dbTypeNull, NpgsqlDbType.Unknown);
            Assert.AreEqual(dbTypeChar, NpgsqlDbType.Char);
            Assert.AreEqual(dbTypeVarChar, NpgsqlDbType.Varchar);
            Assert.AreEqual(dbTypeVarText, NpgsqlDbType.Text);
            Assert.AreEqual(dbTypeByte, NpgsqlDbType.Smallint);
            Assert.AreEqual(dbTypeInt16, NpgsqlDbType.Smallint);
            Assert.AreEqual(dbTypeInt32, NpgsqlDbType.Integer);
            Assert.AreEqual(dbTypeInt64, NpgsqlDbType.Bigint);
            Assert.AreEqual(dbTypeUByte, NpgsqlDbType.Smallint);
            Assert.AreEqual(dbTypeFloat, NpgsqlDbType.Real);
            Assert.AreEqual(dbTypeDouble, NpgsqlDbType.Double);
            Assert.AreEqual(dbTypeDecimal, NpgsqlDbType.Numeric);
            Assert.AreEqual(dbTypeDateTime, NpgsqlDbType.Timestamp);
            Assert.AreEqual(dbTypeVarUByte, NpgsqlDbType.Bytea);
        }

        [TestMethod]
        public void Quotes_All_Single_Success()
        {
            // Arrange
            String tableName = "TestsQuotes";
            String tableNameQuotes = "\"TestsQuotes\"";
            String columnsName = "\"Id\", \"Name\", \"Description\"";
            String columnsParameter = "@Id, @Name, @Description";
            String columnsParameterKey = "@Id";
            String columnsKey = "\"Id\"";
            String sqlInsert = "insert into " + tableNameQuotes + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlSelect = "select " + columnsName + " from " + tableNameQuotes + " where " + columnsKey + " = " + columnsParameterKey;
            String sqlDelete = "delete from " + tableNameQuotes + " where " + columnsKey + " in (100,200,300,400)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            DataTable dataTable = new DataTable(tableName);
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Columns.Add("Name", typeof(String));
            dataTable.Columns.Add("Description", typeof(String));
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"] };
            dataTable.AcceptChanges();

            this.Database.GetType().GetProperty("UseDoubleQuotes").SetValue(this.Database, true);
            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;
            databasePostgre.UseDoubleQuotes = true;

            // Act
            databasePostgre.Execute(sqlInsert, new Object[] { 100, "TestsQuotes 100", "Lazy Vinke Tests Database 100" });
            DataRow dataRow100 = databasePostgre.QueryRecord(sqlSelect, tableName, new Object[] { 100 });

            dataTable.Rows.Add(200, "TestsQuotes 200", "Lazy Vinke Tests Database 200");
            databasePostgre.Insert(tableName, dataTable.Rows[0]);

            dataTable.Rows.Add(300, "TestsQuotes 300", "Lazy Vinke Tests Database 300");
            databasePostgre.Indate(tableName, dataTable.Rows[0]);
            databasePostgre.Indate(tableName, dataTable.Rows[1]);

            dataTable.AcceptChanges();
            dataTable.Rows[0]["Name"] = "Update TestsQuotes 200";
            dataTable.Rows[0]["Description"] = "Update Lazy Vinke Tests Database 200";
            dataTable.Rows[1]["Name"] = "Update TestsQuotes 300";
            dataTable.Rows[1]["Description"] = "Update Lazy Vinke Tests Database 300";
            databasePostgre.Update(tableName, dataTable.Rows[0]);
            databasePostgre.Update(tableName, dataTable.Rows[1]);

            dataTable.AcceptChanges();
            dataTable.Rows[1]["Name"] = "Upsert TestsQuotes 300";
            dataTable.Rows[1]["Description"] = "Upsert Lazy Vinke Tests Database 300";
            dataTable.Rows.Add(400, "Upsert TestsQuotes 400", "Upsert Lazy Vinke Tests Database 400");
            dataTable.Rows[2].AcceptChanges();
            dataTable.Rows[2].SetModified();
            databasePostgre.Upsert(tableName, dataTable.Rows[1]);
            databasePostgre.Upsert(tableName, dataTable.Rows[2]);

            dataTable.AcceptChanges();

            DataRow dataRow200 = databasePostgre.Select(tableName, dataTable.Rows[0]).Rows[0];
            DataRow dataRow300 = databasePostgre.Select(tableName, dataTable.Rows[1]).Rows[0];
            DataRow dataRow400 = databasePostgre.Select(tableName, dataTable.Rows[2]).Rows[0];

            dataTable.AcceptChanges();
            dataTable.Rows[0].Delete();
            dataTable.Rows[1].Delete();
            dataTable.Rows[2].Delete();
            databasePostgre.Delete(tableName, dataTable.Rows[0]);
            databasePostgre.Delete(tableName, dataTable.Rows[1]);
            databasePostgre.Delete(tableName, dataTable.Rows[2]);

            // Assert
            Assert.AreEqual(Convert.ToInt32(dataRow200["Id"]), 200);
            Assert.AreEqual(Convert.ToString(dataRow200["Name"]), "Update TestsQuotes 200");
            Assert.AreEqual(Convert.ToString(dataRow200["Description"]), "Update Lazy Vinke Tests Database 200");
            Assert.AreEqual(Convert.ToInt32(dataRow300["Id"]), 300);
            Assert.AreEqual(Convert.ToString(dataRow300["Name"]), "Upsert TestsQuotes 300");
            Assert.AreEqual(Convert.ToString(dataRow300["Description"]), "Upsert Lazy Vinke Tests Database 300");
            Assert.AreEqual(Convert.ToInt32(dataRow400["Id"]), 400);
            Assert.AreEqual(Convert.ToString(dataRow400["Name"]), "Upsert TestsQuotes 400");
            Assert.AreEqual(Convert.ToString(dataRow400["Description"]), "Upsert Lazy Vinke Tests Database 400");

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

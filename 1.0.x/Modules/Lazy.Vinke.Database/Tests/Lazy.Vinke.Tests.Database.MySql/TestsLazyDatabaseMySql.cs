// TestsLazyDatabaseMySql.cs
//
// This file is integrated part of "Lazy Vinke Tests Database MySql" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

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
    public class TestsLazyDatabaseMySql : TestsLazyDatabase
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseMySql(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            databaseMySql.Execute(sqlInsert, new Object[] { 20, "Content 20", "Notes 20 Notes 20 Notes 20 Notes 20" });
            databaseMySql.Execute(sqlInsert, new Object[] { 21, "21 Content", "21 Notes 21 Notes 21 Notes 21 Notes 21 Notes" });
            databaseMySql.Execute(sqlInsert, new Object[] { 22, "Content 22 Content", "Notes 22 22 Notes 22 22 Notes 22 22 Notes" });

            // Act
            DataRow dataRowTest1 = databaseMySql.QueryRecord("select * from " + tableName + " where cast(TestId as char(2048)) like @TestId", tableName, new Object[] { "%20" });
            DataRow dataRowTest2 = databaseMySql.QueryRecord("select * from " + tableName + " where cast(TestId as char(2048)) like @TestId", tableName, new Object[] { "21%" });
            DataRow dataRowTest3 = databaseMySql.QueryRecord("select * from " + tableName + " where cast(TestId as char(2048)) like @TestId", tableName, new Object[] { "%22%" });

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
            MySqlDbType dbTypeNull = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DBNull });
            MySqlDbType dbTypeChar = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Char });
            MySqlDbType dbTypeVarChar = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarChar });
            MySqlDbType dbTypeVarText = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarText });
            MySqlDbType dbTypeByte = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Byte });
            MySqlDbType dbTypeInt16 = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int16 });
            MySqlDbType dbTypeInt32 = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int32 });
            MySqlDbType dbTypeInt64 = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int64 });
            MySqlDbType dbTypeUByte = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.UByte });
            MySqlDbType dbTypeFloat = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Float });
            MySqlDbType dbTypeDouble = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Double });
            MySqlDbType dbTypeDecimal = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Decimal });
            MySqlDbType dbTypeDateTime = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DateTime });
            MySqlDbType dbTypeVarUByte = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarUByte });

            // Assert
            Assert.AreEqual(dbTypeNull, MySqlDbType.VarString);
            Assert.AreEqual(dbTypeChar, MySqlDbType.VarChar);
            Assert.AreEqual(dbTypeVarChar, MySqlDbType.VarString);
            Assert.AreEqual(dbTypeVarText, MySqlDbType.LongText);
            Assert.AreEqual(dbTypeByte, MySqlDbType.Byte);
            Assert.AreEqual(dbTypeInt16, MySqlDbType.Int16);
            Assert.AreEqual(dbTypeInt32, MySqlDbType.Int32);
            Assert.AreEqual(dbTypeInt64, MySqlDbType.Int64);
            Assert.AreEqual(dbTypeUByte, MySqlDbType.UByte);
            Assert.AreEqual(dbTypeFloat, MySqlDbType.Float);
            Assert.AreEqual(dbTypeDouble, MySqlDbType.Double);
            Assert.AreEqual(dbTypeDecimal, MySqlDbType.Decimal);
            Assert.AreEqual(dbTypeDateTime, MySqlDbType.DateTime);
            Assert.AreEqual(dbTypeVarUByte, MySqlDbType.Blob);
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

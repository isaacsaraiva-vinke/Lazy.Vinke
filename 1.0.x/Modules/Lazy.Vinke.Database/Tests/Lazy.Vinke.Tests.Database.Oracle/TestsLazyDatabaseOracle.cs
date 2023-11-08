// TestsLazyDatabaseOracle.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Oracle" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.Oracle;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.Oracle
{
    [TestClass]
    public class TestsLazyDatabaseOracle : TestsLazyDatabase
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseOracle(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
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

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            databaseOracle.Execute(sqlInsert, new Object[] { 20, "Content 20", "Notes 20 Notes 20 Notes 20 Notes 20" });
            databaseOracle.Execute(sqlInsert, new Object[] { 21, "21 Content", "21 Notes 21 Notes 21 Notes 21 Notes 21 Notes" });
            databaseOracle.Execute(sqlInsert, new Object[] { 22, "Content 22 Content", "Notes 22 22 Notes 22 22 Notes 22 22 Notes" });

            // Act
            DataRow dataRowTest1 = databaseOracle.QueryRecord("select * from " + tableName + " where cast(TestId as varchar2(2048)) like @TestId", tableName, new Object[] { "%20" });
            DataRow dataRowTest2 = databaseOracle.QueryRecord("select * from " + tableName + " where cast(TestId as varchar2(2048)) like @TestId", tableName, new Object[] { "21%" });
            DataRow dataRowTest3 = databaseOracle.QueryRecord("select * from " + tableName + " where cast(TestId as varchar2(2048)) like @TestId", tableName, new Object[] { "%22%" });

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
            OracleDbType dbTypeNull = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DBNull });
            OracleDbType dbTypeChar = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Char });
            OracleDbType dbTypeVarChar = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarChar });
            OracleDbType dbTypeVarText = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarText });
            OracleDbType dbTypeByte = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Byte });
            OracleDbType dbTypeInt16 = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int16 });
            OracleDbType dbTypeInt32 = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int32 });
            OracleDbType dbTypeInt64 = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int64 });
            OracleDbType dbTypeUByte = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.UByte });
            OracleDbType dbTypeFloat = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Float });
            OracleDbType dbTypeDouble = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Double });
            OracleDbType dbTypeDecimal = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Decimal });
            OracleDbType dbTypeDateTime = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DateTime });
            OracleDbType dbTypeVarUByte = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarUByte });

            // Assert
            Assert.AreEqual(dbTypeNull, OracleDbType.Varchar2);
            Assert.AreEqual(dbTypeChar, OracleDbType.Char);
            Assert.AreEqual(dbTypeVarChar, OracleDbType.Varchar2);
            Assert.AreEqual(dbTypeVarText, OracleDbType.Clob);
            Assert.AreEqual(dbTypeByte, OracleDbType.Int16);
            Assert.AreEqual(dbTypeInt16, OracleDbType.Int16);
            Assert.AreEqual(dbTypeInt32, OracleDbType.Int32);
            Assert.AreEqual(dbTypeInt64, OracleDbType.Int64);
            Assert.AreEqual(dbTypeUByte, OracleDbType.Int16);
            Assert.AreEqual(dbTypeFloat, OracleDbType.Single);
            Assert.AreEqual(dbTypeDouble, OracleDbType.Double);
            Assert.AreEqual(dbTypeDecimal, OracleDbType.Decimal);
            Assert.AreEqual(dbTypeDateTime, OracleDbType.Date);
            Assert.AreEqual(dbTypeVarUByte, OracleDbType.Blob);
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

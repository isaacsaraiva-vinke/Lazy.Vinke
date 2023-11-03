// TestsLazyDatabaseSqlServer.cs
//
// This file is integrated part of "Lazy Vinke Tests Database SqlServer" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.SqlServer;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.SqlServer
{
    [TestClass]
    public class TestsLazyDatabaseSqlServer : TestsLazyDatabase
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
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
            String tableName = "QueryLike_DataAdapterFill";
            String columnsName = "TestId, Content, Notes";
            String columnsParameter = "@TestId, @Content, @Notes";
            String sqlDelete = "delete from QueryLike_DataAdapterFill where TestId in (20,21,22)";
            String sqlInsert = "insert into QueryLike_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            databaseSqlServer.Execute(sqlInsert, new Object[] { 20, "Content 20", "Notes 20 Notes 20 Notes 20 Notes 20" });
            databaseSqlServer.Execute(sqlInsert, new Object[] { 21, "21 Content", "21 Notes 21 Notes 21 Notes 21 Notes 21 Notes" });
            databaseSqlServer.Execute(sqlInsert, new Object[] { 22, "Content 22 Content", "Notes 22 22 Notes 22 22 Notes 22 22 Notes" });

            // Act
            DataRow dataRowTest1 = databaseSqlServer.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as varchar(2048)) like @TestId", tableName, new Object[] { "%20" });
            DataRow dataRowTest2 = databaseSqlServer.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as varchar(2048)) like @TestId", tableName, new Object[] { "21%" });
            DataRow dataRowTest3 = databaseSqlServer.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as varchar(2048)) like @TestId", tableName, new Object[] { "%22%" });

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
            SqlDbType dbTypeNull = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DBNull });
            SqlDbType dbTypeChar = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Char });
            SqlDbType dbTypeVarChar = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarChar });
            SqlDbType dbTypeVarText = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarText });
            SqlDbType dbTypeByte = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Byte });
            SqlDbType dbTypeInt16 = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int16 });
            SqlDbType dbTypeInt32 = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int32 });
            SqlDbType dbTypeInt64 = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int64 });
            SqlDbType dbTypeUByte = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.UByte });
            SqlDbType dbTypeFloat = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Float });
            SqlDbType dbTypeDouble = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Double });
            SqlDbType dbTypeDecimal = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Decimal });
            SqlDbType dbTypeDateTime = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DateTime });
            SqlDbType dbTypeVarUByte = (SqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarUByte });

            // Assert
            Assert.AreEqual(dbTypeNull, SqlDbType.VarChar);
            Assert.AreEqual(dbTypeChar, SqlDbType.Char);
            Assert.AreEqual(dbTypeVarChar, SqlDbType.VarChar);
            Assert.AreEqual(dbTypeVarText, SqlDbType.Text);
            Assert.AreEqual(dbTypeByte, SqlDbType.SmallInt);
            Assert.AreEqual(dbTypeInt16, SqlDbType.SmallInt);
            Assert.AreEqual(dbTypeInt32, SqlDbType.Int);
            Assert.AreEqual(dbTypeInt64, SqlDbType.BigInt);
            Assert.AreEqual(dbTypeUByte, SqlDbType.TinyInt);
            Assert.AreEqual(dbTypeFloat, SqlDbType.Real);
            Assert.AreEqual(dbTypeDouble, SqlDbType.Float);
            Assert.AreEqual(dbTypeDecimal, SqlDbType.Decimal);
            Assert.AreEqual(dbTypeDateTime, SqlDbType.DateTime);
            Assert.AreEqual(dbTypeVarUByte, SqlDbType.Image);
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

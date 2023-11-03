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

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            databasePostgre.Execute(sqlInsert, new Object[] { 20, "Content 20", "Notes 20 Notes 20 Notes 20 Notes 20" });
            databasePostgre.Execute(sqlInsert, new Object[] { 21, "21 Content", "21 Notes 21 Notes 21 Notes 21 Notes 21 Notes" });
            databasePostgre.Execute(sqlInsert, new Object[] { 22, "Content 22 Content", "Notes 22 22 Notes 22 22 Notes 22 22 Notes" });

            // Act
            DataRow dataRowTest1 = databasePostgre.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as varchar(2048)) like @TestId", tableName, new Object[] { "%20" });
            DataRow dataRowTest2 = databasePostgre.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as varchar(2048)) like @TestId", tableName, new Object[] { "21%" });
            DataRow dataRowTest3 = databasePostgre.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as varchar(2048)) like @TestId", tableName, new Object[] { "%22%" });

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

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

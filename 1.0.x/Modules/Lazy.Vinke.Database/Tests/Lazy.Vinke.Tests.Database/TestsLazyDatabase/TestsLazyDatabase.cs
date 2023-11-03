// TestsLazyDatabase.cs
//
// This file is integrated part of "Lazy Vinke Tests Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;

namespace Lazy.Vinke.Tests.Database
{
    public class TestsLazyDatabase
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void Constructor_Parameter_Null_Success()
        {
            // Arrange
            this.Database.CloseConnection();

            // Act
            this.Database = (LazyDatabase)Activator.CreateInstance(this.Database.GetType(), new Object[] { null, null });

            // Assert
            Assert.AreEqual(this.Database.ConnectionString, null);
            Assert.AreEqual(this.Database.ConnectionOwner, null);
        }

        public virtual void Constructor_Parameter_Valued_Success()
        {
            // Arrange
            this.Database.CloseConnection();

            // Act
            this.Database = (LazyDatabase)Activator.CreateInstance(this.Database.GetType(), new Object[] { "SomeConnectionString", "SomeConnectionOwner" });

            // Assert
            Assert.AreEqual(this.Database.ConnectionString, "SomeConnectionString");
            Assert.AreEqual(this.Database.ConnectionOwner, "SomeConnectionOwner");
        }

        public virtual void CreateNew_Instance_ConnectionOwnerNull_Success()
        {
            // Arrange
            this.Database.CloseConnection();
            this.Database = this.Database.CreateNew("SomeConnectionOwner");

            // Act
            LazyDatabase newDatabase = this.Database.CreateNew();

            // Assert
            Assert.AreEqual(newDatabase.GetType(), this.Database.GetType());
            Assert.AreEqual(newDatabase.ConnectionOwner, null);
        }

        public virtual void CreateNew_Instance_ConnectionOwnerValued_Success()
        {
            // Arrange

            // Act
            LazyDatabase newDatabase = this.Database.CreateNew("SomeNewConnectionOwner");

            // Assert
            Assert.AreEqual(newDatabase.GetType(), this.Database.GetType());
            Assert.AreNotEqual(newDatabase.ConnectionOwner, this.Database.ConnectionOwner);
            Assert.AreEqual(newDatabase.ConnectionOwner, "SomeNewConnectionOwner");
        }

        public virtual void QueryLike_DataAdapterFill_LazyDbType_Success()
        {
            // Arrange
            String tableName = "QueryLike_DataAdapterFill";
            String columnsName = "TestId, Content, Notes";
            String columnsParameter = "@TestId, @Content, @Notes";
            String sqlDelete = "delete from QueryLike_DataAdapterFill where TestId in (10,11,12)";
            String sqlInsert = "insert into QueryLike_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 10, "Content 10", "Notes 10 Notes 10 Notes 10 Notes 10" });
            this.Database.Execute(sqlInsert, new Object[] { 11, "11 Content", "11 Notes 11 Notes 11 Notes 11 Notes 11 Notes" });
            this.Database.Execute(sqlInsert, new Object[] { 12, "Content 12 Content", "Notes 12 12 Notes 12 12 Notes 12 12 Notes" });

            // Act
            DataRow dataRowTest1 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Content like @Content", tableName, new Object[] { "%10" });
            DataRow dataRowTest2 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Content like @Content", tableName, new Object[] { "11%" });
            DataRow dataRowTest3 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Content like @Content", tableName, new Object[] { "%12%" });
            DataRow dataRowTest4 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Notes like @Notes", tableName, new Object[] { "%10 Notes 10" });
            DataRow dataRowTest5 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Notes like @Notes", tableName, new Object[] { "11 Notes 11 %" });
            DataRow dataRowTest6 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Notes like @Notes", tableName, new Object[] { "%12 12 Notes 12 12%" });

            // Assert
            Assert.IsNotNull(dataRowTest1);
            Assert.IsNotNull(dataRowTest2);
            Assert.IsNotNull(dataRowTest3);
            Assert.IsNotNull(dataRowTest4);
            Assert.IsNotNull(dataRowTest5);
            Assert.IsNotNull(dataRowTest6);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void ConvertSystemTypeToLazyDbType_LazyDbTypeArray_Single_Success()
        {
            // Arrange
            Object[] values = new Object[] {
                null, DBNull.Value,
                'L', "Lazy.Vinke.Tests.Database",
                (SByte)(-8), (Int16)(-16), (Int32)(-32), (Int64)(-64),
                (Byte)8, 1.1f, 10.01d, 100.001m,
                new DateTime(2023, 10, 24, 09, 15, 30),
                new Byte[] { 8, 16, 34, 64 },

                new List<Int32>(),
                new DataSet()
            };

            // Act
            MethodInfo methodInfo = this.Database.GetType().GetMethod("ConvertSystemTypeToLazyDbType", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            LazyDbType[] dbTypes = (LazyDbType[])methodInfo.Invoke(this.Database, new Object[] { values });

            // Assert
            Assert.AreEqual(dbTypes[0], LazyDbType.DBNull);
            Assert.AreEqual(dbTypes[1], LazyDbType.DBNull);
            Assert.AreEqual(dbTypes[2], LazyDbType.Char);
            Assert.AreEqual(dbTypes[3], LazyDbType.VarChar);
            Assert.AreEqual(dbTypes[4], LazyDbType.Byte);
            Assert.AreEqual(dbTypes[5], LazyDbType.Int16);
            Assert.AreEqual(dbTypes[6], LazyDbType.Int32);
            Assert.AreEqual(dbTypes[7], LazyDbType.Int64);
            Assert.AreEqual(dbTypes[8], LazyDbType.UByte);
            Assert.AreEqual(dbTypes[9], LazyDbType.Float);
            Assert.AreEqual(dbTypes[10], LazyDbType.Double);
            Assert.AreEqual(dbTypes[11], LazyDbType.Decimal);
            Assert.AreEqual(dbTypes[12], LazyDbType.DateTime);
            Assert.AreEqual(dbTypes[13], LazyDbType.VarUByte);
            Assert.AreEqual(dbTypes[14], LazyDbType.DBNull);
            Assert.AreEqual(dbTypes[15], LazyDbType.DBNull);
        }

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

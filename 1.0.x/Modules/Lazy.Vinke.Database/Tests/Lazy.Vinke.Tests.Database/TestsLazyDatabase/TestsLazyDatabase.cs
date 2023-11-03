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

        public virtual void OpenConnection_ConnectionString_StringNullOrEmpty_Exception()
        {
            // Arrange
            this.Database.CloseConnection();

            // Act
            Exception exception = null;
            try { this.Database = (LazyDatabase)Activator.CreateInstance(this.Database.GetType()); this.Database.OpenConnection(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionStringNullOrEmpty);
        }

        public virtual void OpenConnection_ConnectionState_AlreadyOpen_Exception()
        {
            // Arrange

            // Act
            Exception exception = null;
            try { this.Database.OpenConnection(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionAlreadyOpen);
        }

        public virtual void OpenConnection_ConnectionState_Opened_Success()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(this.Database.ConnectionState, ConnectionState.Open);
        }

        public virtual void CloseConnection_ConnectionState_AlreadyClose_Exception()
        {
            // Arrange

            // Act
            Exception exception = null;
            try { this.Database.CloseConnection(); this.Database.CloseConnection(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionAlreadyClose);
        }

        public virtual void CloseConnection_ConnectionState_Close_Success()
        {
            // Arrange

            // Act
            this.Database.CloseConnection();

            // Assert
            Assert.AreEqual(this.Database.ConnectionState, ConnectionState.Closed);
        }

        public virtual void BeginTransaction_Connection_NotOpen_Exception()
        {
            // Arrange
            this.Database.CloseConnection();

            // Act
            Exception exception = null;
            try { this.Database = (LazyDatabase)Activator.CreateInstance(this.Database.GetType()); this.Database.BeginTransaction(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
        }

        public virtual void BeginTransaction_Transaction_AlreadyOpen_Exception()
        {
            // Arrange

            // Act
            Exception exception = null;
            try { this.Database.BeginTransaction(); this.Database.BeginTransaction(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionTransactionAlreadyOpen);
        }

        public virtual void BeginTransaction_Transaction_NotOpen_Success()
        {
            // Arrange

            // Act
            this.Database.BeginTransaction();

            // Assert
            Assert.IsTrue(this.Database.InTransaction);
        }

        public virtual void CommitTransaction_Commit_Single_Success()
        {
            // Arrange
            String sqlInsert = "insert into TestsTransaction values (@Id, @Content)";
            String sqlSelect = "select count(*) from TestsTransaction where Id in (1,2,3,4)";
            String sqlDelete = "delete from TestsTransaction where Id in (1,2,3,4)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            // Act
            this.Database.BeginTransaction();
            this.Database.Execute(sqlInsert, new Object[] { 1, "Lazy" });
            this.Database.Execute(sqlInsert, new Object[] { 2, "Vinke" });
            this.Database.Execute(sqlInsert, new Object[] { 3, "Tests" });
            this.Database.Execute(sqlInsert, new Object[] { 4, "Database" });
            this.Database.CommitTransaction();

            Int32 count = Convert.ToInt32(this.Database.QueryValue(sqlSelect, null));

            // Assert
            Assert.AreEqual(count, 4);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void RollbackTransaction_Rollback_Single_Success()
        {
            // Arrange
            String sqlInsert = "insert into TestsTransaction values (@Id, @Content)";
            String sqlSelect = "select count(*) from TestsTransaction where Id in (5,6,7,8)";
            String sqlDelete = "delete from TestsTransaction where Id in (5,6,7,8)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            // Act
            this.Database.BeginTransaction();
            this.Database.Execute(sqlInsert, new Object[] { 5, "Lazy" });
            this.Database.Execute(sqlInsert, new Object[] { 6, "Vinke" });
            this.Database.Execute(sqlInsert, new Object[] { 7, "Tests" });
            this.Database.Execute(sqlInsert, new Object[] { 8, "Database" });
            this.Database.RollbackTransaction();

            Int32 count = Convert.ToInt32(this.Database.QueryValue(sqlSelect, null));

            // Assert
            Assert.AreEqual(count, 0);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
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
            String tableName = "TestsQueryLike";
            String columnsName = "TestId, Content, Notes";
            String columnsParameter = "@TestId, @Content, @Notes";
            String sqlDelete = "delete from " + tableName + " where TestId in (10,11,12)";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 10, "Content 10", "Notes 10 Notes 10 Notes 10 Notes 10" });
            this.Database.Execute(sqlInsert, new Object[] { 11, "11 Content", "11 Notes 11 Notes 11 Notes 11 Notes 11 Notes" });
            this.Database.Execute(sqlInsert, new Object[] { 12, "Content 12 Content", "Notes 12 12 Notes 12 12 Notes 12 12 Notes" });

            // Act
            DataRow dataRowTest1 = this.Database.QueryRecord("select * from " + tableName + " where Content like @Content", tableName, new Object[] { "%10" });
            DataRow dataRowTest2 = this.Database.QueryRecord("select * from " + tableName + " where Content like @Content", tableName, new Object[] { "11%" });
            DataRow dataRowTest3 = this.Database.QueryRecord("select * from " + tableName + " where Content like @Content", tableName, new Object[] { "%12%" });
            DataRow dataRowTest4 = this.Database.QueryRecord("select * from " + tableName + " where Notes like @Notes", tableName, new Object[] { "%10 Notes 10" });
            DataRow dataRowTest5 = this.Database.QueryRecord("select * from " + tableName + " where Notes like @Notes", tableName, new Object[] { "11 Notes 11 %" });
            DataRow dataRowTest6 = this.Database.QueryRecord("select * from " + tableName + " where Notes like @Notes", tableName, new Object[] { "%12 12 Notes 12 12%" });

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

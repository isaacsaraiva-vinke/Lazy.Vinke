// TestsLazyDatabaseTransaction.cs
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
    public class TestsLazyDatabaseTransaction
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
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
            String sqlInsert = "insert into Transaction_CommitRollback values (@Id, @Content)";
            String sqlSelect = "select count(*) from Transaction_CommitRollback where Id in (1,2,3,4)";
            String sqlDelete = "delete from Transaction_CommitRollback where Id in (1,2,3,4)";
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
            String sqlInsert = "insert into Transaction_CommitRollback values (@Id, @Content)";
            String sqlSelect = "select count(*) from Transaction_CommitRollback where Id in (5,6,7,8)";
            String sqlDelete = "delete from Transaction_CommitRollback where Id in (5,6,7,8)";
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

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

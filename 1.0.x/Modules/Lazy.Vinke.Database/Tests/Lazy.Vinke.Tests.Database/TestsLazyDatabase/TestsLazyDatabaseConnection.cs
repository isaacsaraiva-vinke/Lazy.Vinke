// TestsLazyDatabaseConnection.cs
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
    public class TestsLazyDatabaseConnection
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
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

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

// TestsLazyDatabaseInsert.cs
//
// This file is integrated part of "Lazy Vinke Tests Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 04

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;

namespace Lazy.Vinke.Tests.Database
{
    public class TestsLazyDatabaseInsert
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void Insert_DataRow_Added_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsInsert";
            String sqlDelete = "delete from " + tableName + " where Id in (1000,2000,3000)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Columns.Add("Name", typeof(String));
            dataTable.Columns.Add("Amount", typeof(Decimal));
            dataTable.Rows.Add(1000, "Item 1000", 1000.1m);
            dataTable.Rows.Add(2000, "Item 2000", 2000.1m);
            dataTable.Rows.Add(3000, "Item 3000", 3000.1m);

            // Act
            rowsAffected += this.Database.Insert(tableName, dataTable.Rows[0]);
            rowsAffected += this.Database.Insert(tableName, dataTable.Rows[1]);
            rowsAffected += this.Database.Insert(tableName, dataTable.Rows[2]);

            // Assert
            Assert.AreEqual(rowsAffected, 3);

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

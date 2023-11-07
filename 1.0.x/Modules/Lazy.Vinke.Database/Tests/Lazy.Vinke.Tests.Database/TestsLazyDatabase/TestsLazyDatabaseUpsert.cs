// TestsLazyDatabaseUpsert.cs
//
// This file is integrated part of "Lazy Vinke Tests Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 07

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;

namespace Lazy.Vinke.Tests.Database
{
    public class TestsLazyDatabaseUpsert
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void Upsert_DataRow_Added_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsUpsert";
            String testCode = "Upsert_DataRow_Added_Success";
            String sqlDelete = "delete from " + tableName + " where TestCode = '" + testCode + "'";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("TestCode", typeof(String));
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Columns.Add("Item", typeof(String));
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["TestCode"], dataTable.Columns["Id"] };
            dataTable.Rows.Add(testCode, 1, "Lazy");
            dataTable.Rows.Add(testCode, 2, "Vinke");
            dataTable.Rows.Add(testCode, 3, "Tests");
            dataTable.Rows.Add(testCode, 4, "Database");

            this.Database.Insert(tableName, dataTable.Rows[0]);
            this.Database.Insert(tableName, dataTable.Rows[1]);
            this.Database.Insert(tableName, dataTable.Rows[2]);
            this.Database.Insert(tableName, dataTable.Rows[3]);

            dataTable.AcceptChanges();

            dataTable.Rows[2]["Id"] = 7;
            dataTable.Rows[2]["Item"] = "Isaac";
            dataTable.Rows[3]["Id"] = 8;
            dataTable.Rows[3]["Item"] = "Bezerra";
            dataTable.Rows.Add(testCode, 9, "Saraiva");
            dataTable.Rows[4].AcceptChanges();
            dataTable.Rows[4].SetModified();

            // Act
            rowsAffected += this.Database.Upsert(tableName, dataTable.Rows[2]);
            rowsAffected += this.Database.Upsert(tableName, dataTable.Rows[3]);
            rowsAffected += this.Database.Upsert(tableName, dataTable.Rows[4]);

            // Assert
            Assert.AreEqual(rowsAffected, 3);

            // Clean
            //try { this.Database.Execute(sqlDelete, null); }
            //catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

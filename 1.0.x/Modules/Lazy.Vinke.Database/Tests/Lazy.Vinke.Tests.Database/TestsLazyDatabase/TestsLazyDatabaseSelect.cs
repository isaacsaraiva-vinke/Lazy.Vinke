// TestsLazyDatabaseSelect.cs
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
    public class TestsLazyDatabaseSelect
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void Select_QueryTable_DataRowWithPrimaryKey_Success()
        {
            // Arrange
            String tableName = "TestsSelectQueryTable";
            String columnsName = "Id, Name, Amount";
            String columnsParameter = "@Id, @Name, @Amount";
            String sqlDelete = "delete from " + tableName + " where Id in (1000,2000,3000)";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 1000, "Item 1000", 1000.1m });
            this.Database.Execute(sqlInsert, new Object[] { 2000, "Item 2000", 2000.1m });
            this.Database.Execute(sqlInsert, new Object[] { 3000, "Item 3000", 3000.1m });

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Columns.Add("Name", typeof(String));
            dataTable.Rows.Add(2000, "Item 2000");
            dataTable.AcceptChanges();

            DataRow dataRow = dataTable.Rows[0];

            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"], dataTable.Columns["Name"] };

            // Act
            dataTable = this.Database.Select(tableName, dataRow, returnFields: new String[] { "Amount" });

            // Assert
            Assert.AreEqual(dataTable.TableName, tableName);
            Assert.IsTrue(dataTable.Rows.Count == 1);
            Assert.AreEqual(Convert.ToDecimal(dataTable.Rows[0]["Amount"]), 2000.1m);

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

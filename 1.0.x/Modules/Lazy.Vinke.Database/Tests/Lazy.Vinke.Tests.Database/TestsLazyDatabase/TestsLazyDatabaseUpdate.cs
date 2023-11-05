// TestsLazyDatabaseUpdate.cs
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
    public class TestsLazyDatabaseUpdate
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void Update_DataRow_Modified_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsUpdate";
            String sqlDelete = "delete from " + tableName + " where Id in (1000,2000)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Columns.Add("ColumnVarChar", typeof(String));
            dataTable.Columns.Add("ColumnDecimal", typeof(Decimal));
            dataTable.Columns.Add("ColumnDateTime", typeof(DateTime));
            dataTable.Columns.Add("ColumnByte", typeof(Byte));
            dataTable.Columns.Add("ColumnChar", typeof(Char));
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"] };
            dataTable.Rows.Add(1000, "Item 1000", 1000.1m, new DateTime(2023, 11, 04, 12, 05, 30), 2, '1');
            dataTable.Rows.Add(2000, "Item 2000", 2000.2m, new DateTime(2023, 11, 04, 12, 05, 30), 4, '0');

            this.Database.Insert(tableName, dataTable.Rows[0]);
            this.Database.Insert(tableName, dataTable.Rows[1]);

            dataTable.AcceptChanges();

            dataTable.Rows[0]["ColumnVarChar"] = "Modified 1000";
            dataTable.Rows[0]["ColumnDecimal"] = 1001.01m;
            dataTable.Rows[0]["ColumnDateTime"] = new DateTime(2023, 11, 04, 22, 55, 30);
            dataTable.Rows[0]["ColumnByte"] = 16;
            dataTable.Rows[0]["ColumnChar"] = '0';
            dataTable.Rows[1]["ColumnVarChar"] = "Modified 2000";
            dataTable.Rows[1]["ColumnDecimal"] = 2002.02m;
            dataTable.Rows[1]["ColumnDateTime"] = new DateTime(2023, 11, 04, 22, 55, 30);
            dataTable.Rows[1]["ColumnByte"] = 32;
            dataTable.Rows[1]["ColumnChar"] = '1';

            // Act
            rowsAffected += this.Database.Update(tableName, dataTable.Rows[0]);
            rowsAffected += this.Database.Update(tableName, dataTable.Rows[1]);

            // Assert
            Assert.AreEqual(rowsAffected, 2);

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

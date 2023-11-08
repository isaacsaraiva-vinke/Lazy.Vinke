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

using Lazy.Vinke.Data;
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

        public virtual void Insert_Validations_DataRow_Exception()
        {
            // Arrange
            String tableName = "TestsInsert";
            String subQuery = "(select * from TestsInsert)";

            DataTable dataTableNoColumns = new DataTable(tableName);
            dataTableNoColumns.Rows.Add(dataTableNoColumns.NewRow());

            DataTable dataTable = new DataTable(tableName);
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Rows.Add(1);

            DataRow dataRowNoColumns = dataTableNoColumns.Rows[0];
            DataRow dataRow = dataTable.Rows[0];

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionDataRowNull = null;
            Exception exceptionDataRowNoColumns = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Insert(tableName, dataRow); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Insert(null, dataRow); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Insert(subQuery, dataRow); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Insert(tableName, null); } catch (Exception exp) { exceptionDataRowNull = exp; }
            try { this.Database.Insert(tableName, dataRowNoColumns); } catch (Exception exp) { exceptionDataRowNoColumns = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionDataRowNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowNull);
            Assert.AreEqual(exceptionDataRowNoColumns.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowColumnsMissing);
        }

        public virtual void Insert_Validations_Arrays_Exception()
        {
            // Arrange
            String tableName = "TestsInsert";
            String subQuery = "(select * from TestsInsert)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Decimal };
            String[] fieldsLess = new String[] { "Amount" };

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionValuesNullButOthers = null;
            Exception exceptionDbTypesNullButOthers = null;
            Exception exceptionFieldsNullButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionFieldsLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Insert(tableName, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Insert(null, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Insert(subQuery, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Insert(tableName, null, dbTypes, fields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { this.Database.Insert(tableName, values, null, fields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { this.Database.Insert(tableName, values, dbTypes, null); } catch (Exception exp) { exceptionFieldsNullButOthers = exp; }

            try { this.Database.Insert(tableName, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.Insert(tableName, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.Insert(tableName, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionFieldsLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionValuesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLength);
            Assert.AreEqual(exceptionDbTypesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionTypesNullOrZeroLength);
            Assert.AreEqual(exceptionFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
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
            dataTable.Columns.Add("ColumnVarChar", typeof(String));
            dataTable.Columns.Add("ColumnDecimal", typeof(Decimal));
            dataTable.Columns.Add("ColumnDateTime", typeof(DateTime));
            dataTable.Columns.Add("ColumnByte", typeof(Byte));
            dataTable.Columns.Add("ColumnChar", typeof(Char));
            dataTable.Rows.Add(1000, "Item 1000", 1000.1m, new DateTime(2023, 11, 04, 12, 05, 30), 2, '1');
            dataTable.Rows.Add(2000, "Item 2000", 2000.1m, new DateTime(2023, 11, 04, 12, 05, 30), 4, '0');
            dataTable.Rows.Add(3000, "Item 3000", 3000.1m, new DateTime(2023, 11, 04, 12, 05, 30), 8, '1');

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

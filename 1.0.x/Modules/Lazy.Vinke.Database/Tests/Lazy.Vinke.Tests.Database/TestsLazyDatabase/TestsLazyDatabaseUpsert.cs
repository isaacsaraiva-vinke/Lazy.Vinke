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

using Lazy.Vinke.Data;
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

        public virtual void Upsert_Validations_DataRow_Exception()
        {
            // Arrange
            String tableName = "TestsUpsert";
            String subQuery = "(select * from TestsUpsert)";

            DataTable dataTableNoColumns = new DataTable(tableName);
            dataTableNoColumns.Rows.Add(dataTableNoColumns.NewRow());

            DataTable dataTableNoPrimaryKey = new DataTable(tableName);
            dataTableNoPrimaryKey.Columns.Add("Id", typeof(Int32));
            dataTableNoPrimaryKey.Rows.Add(1);

            DataTable dataTable = new DataTable(tableName);
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"] };
            dataTable.Rows.Add(1);

            DataRow dataRowNoColumns = dataTableNoColumns.Rows[0];
            DataRow dataRowNoPrimaryKey = dataTableNoPrimaryKey.Rows[0];
            DataRow dataRow = dataTable.Rows[0];

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionDataRowNull = null;
            Exception exceptionDataRowNoColumns = null;
            Exception exceptionDataRowNoPrimaryKey = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Upsert(tableName, dataRow); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Upsert(null, dataRow); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Upsert(subQuery, dataRow); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Upsert(tableName, null); } catch (Exception exp) { exceptionDataRowNull = exp; }
            try { this.Database.Upsert(tableName, dataRowNoColumns); } catch (Exception exp) { exceptionDataRowNoColumns = exp; }
            try { this.Database.Upsert(tableName, dataRowNoPrimaryKey); } catch (Exception exp) { exceptionDataRowNoPrimaryKey = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionDataRowNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowNull);
            Assert.AreEqual(exceptionDataRowNoColumns.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowColumnsMissing);
            Assert.AreEqual(exceptionDataRowNoPrimaryKey.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowPrimaryKeyColumnsMissing);
        }

        public virtual void Upsert_Validations_Arrays_Exception()
        {
            // Arrange
            String tableName = "TestsUpsert";
            String subQuery = "(select * from TestsUpsert)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] keyValues = new Object[] { 1, 1 };
            LazyDbType[] keyDbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.Int32 };
            String[] keyFields = new String[] { "IdMaster", "IdChild" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Decimal };
            String[] fieldsLess = new String[] { "Amount" };

            Object[] keyValuesLess = new Object[] { 1 };
            LazyDbType[] keyDbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] keyFieldsLess = new String[] { "Id" };

            String[] keyFieldsNotMatch1 = new String[] { "Code", "Date" };
            String[] keyFieldsNotMatch2 = new String[] { "Id", "Code" };
            String[] keyFieldsNotMatch3 = new String[] { "Code", "Id" };

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionValuesNullButOthers = null;
            Exception exceptionDbTypesNullButOthers = null;
            Exception exceptionDbFieldsNullButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbFieldsLessButOthers = null;
            Exception exceptionKeyValuesNullButOthers = null;
            Exception exceptionKeyDbTypesNullButOthers = null;
            Exception exceptionKeyFieldsNullButOthers = null;
            Exception exceptionKeyValuesLessButOthers = null;
            Exception exceptionKeyDbTypesLessButOthers = null;
            Exception exceptionKeyFieldsLessButOthers = null;
            Exception exceptionKeyFieldsNotMatch1 = null;
            Exception exceptionKeyFieldsNotMatch2 = null;
            Exception exceptionKeyFieldsNotMatch3 = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Upsert(null, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Upsert(subQuery, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Upsert(tableName, null, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { this.Database.Upsert(tableName, values, null, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, null, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsNullButOthers = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, fields, null, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesNullButOthers = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, fields, keyValues, null, keyFields); } catch (Exception exp) { exceptionKeyDbTypesNullButOthers = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, null); } catch (Exception exp) { exceptionKeyFieldsNullButOthers = exp; }

            try { this.Database.Upsert(tableName, valuesLess, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.Upsert(tableName, values, dbTypesLess, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, fieldsLess, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, fields, keyValuesLess, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesLessButOthers = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypesLess, keyFields); } catch (Exception exp) { exceptionKeyDbTypesLessButOthers = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsLess); } catch (Exception exp) { exceptionKeyFieldsLessButOthers = exp; }

            try { this.Database.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsNotMatch1); } catch (Exception exp) { exceptionKeyFieldsNotMatch1 = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsNotMatch2); } catch (Exception exp) { exceptionKeyFieldsNotMatch2 = exp; }
            try { this.Database.Upsert(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsNotMatch3); } catch (Exception exp) { exceptionKeyFieldsNotMatch3 = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionValuesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLength);
            Assert.AreEqual(exceptionDbTypesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionTypesNullOrZeroLength);
            Assert.AreEqual(exceptionDbFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyValuesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesNullOrZeroLength);
            Assert.AreEqual(exceptionKeyDbTypesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyTypesNullOrZeroLength);
            Assert.AreEqual(exceptionKeyFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionKeyValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyFieldsNotMatch1.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNotPresentInFields);
            Assert.AreEqual(exceptionKeyFieldsNotMatch2.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNotPresentInFields);
            Assert.AreEqual(exceptionKeyFieldsNotMatch3.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNotPresentInFields);
        }

        public virtual void Upsert_DataRow_Modified_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsUpsert";
            String testCode = "Upsert_DataRow_Modified_Success";
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

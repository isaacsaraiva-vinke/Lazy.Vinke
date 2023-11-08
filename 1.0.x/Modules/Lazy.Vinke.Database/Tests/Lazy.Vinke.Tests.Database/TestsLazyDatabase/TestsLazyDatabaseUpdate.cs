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

using Lazy.Vinke.Data;
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

        public virtual void Update_Validations_DataRow_Exception()
        {
            // Arrange
            String tableName = "TestsUpdate";
            String subQuery = "(select * from TestsUpdate)";

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

            try { this.Database.Update(tableName, dataRow); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Update(null, dataRow); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Update(subQuery, dataRow); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Update(tableName, null); } catch (Exception exp) { exceptionDataRowNull = exp; }
            try { this.Database.Update(tableName, dataRowNoColumns); } catch (Exception exp) { exceptionDataRowNoColumns = exp; }
            try { this.Database.Update(tableName, dataRowNoPrimaryKey); } catch (Exception exp) { exceptionDataRowNoPrimaryKey = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionDataRowNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowNull);
            Assert.AreEqual(exceptionDataRowNoColumns.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowColumnsMissing);
            Assert.AreEqual(exceptionDataRowNoPrimaryKey.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowPrimaryKeyColumnsMissing);
        }

        public virtual void Update_Validations_Arrays_Exception()
        {
            // Arrange
            String tableName = "TestsUpdate";
            String subQuery = "(select * from TestsUpdate)";

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

            // Act
            this.Database.CloseConnection();

            try { this.Database.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Update(null, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Update(subQuery, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Update(tableName, null, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { this.Database.Update(tableName, values, null, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { this.Database.Update(tableName, values, dbTypes, null, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsNullButOthers = exp; }
            try { this.Database.Update(tableName, values, dbTypes, fields, null, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesNullButOthers = exp; }
            try { this.Database.Update(tableName, values, dbTypes, fields, keyValues, null, keyFields); } catch (Exception exp) { exceptionKeyDbTypesNullButOthers = exp; }
            try { this.Database.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypes, null); } catch (Exception exp) { exceptionKeyFieldsNullButOthers = exp; }

            try { this.Database.Update(tableName, valuesLess, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.Update(tableName, values, dbTypesLess, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.Update(tableName, values, dbTypes, fieldsLess, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }
            try { this.Database.Update(tableName, values, dbTypes, fields, keyValuesLess, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesLessButOthers = exp; }
            try { this.Database.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypesLess, keyFields); } catch (Exception exp) { exceptionKeyDbTypesLessButOthers = exp; }
            try { this.Database.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsLess); } catch (Exception exp) { exceptionKeyFieldsLessButOthers = exp; }

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
        }

        public virtual void Update_DataRow_Modified_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsUpdate";
            String sqlDelete = "delete from " + tableName + " where Id between 1000 and 3000";
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

            dataTable.Rows[0]["Id"] = 1001;
            dataTable.Rows[0]["ColumnVarChar"] = "Modified 1000";
            dataTable.Rows[0]["ColumnDecimal"] = 1001.01m;
            dataTable.Rows[0]["ColumnDateTime"] = new DateTime(2023, 11, 04, 22, 55, 30);
            dataTable.Rows[0]["ColumnByte"] = 16;
            dataTable.Rows[0]["ColumnChar"] = '0';
            dataTable.Rows[1]["Id"] = 2002;
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

        public virtual void Update_DataRow_ModifiedOnlyKeys_Success()
        {
            // Arrange
            Int32 rowsAffected = 0;
            String tableName = "TestsUpdateOnlyKeys";
            String sqlDelete = "delete from " + tableName + " where IdMaster between 1000 and 3000";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("IdMaster", typeof(Int32));
            dataTable.Columns.Add("IdChild", typeof(Int32));
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["IdMaster"], dataTable.Columns["IdChild"] };
            dataTable.Rows.Add(1000, 100);
            dataTable.Rows.Add(2000, 200);

            this.Database.Insert(tableName, dataTable.Rows[0]);
            this.Database.Insert(tableName, dataTable.Rows[1]);

            dataTable.AcceptChanges();

            dataTable.Rows[0]["IdMaster"] = 1001;
            dataTable.Rows[0]["IdChild"] = 101;
            dataTable.Rows[1]["IdMaster"] = 2002;
            dataTable.Rows[1]["IdChild"] = 202;

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

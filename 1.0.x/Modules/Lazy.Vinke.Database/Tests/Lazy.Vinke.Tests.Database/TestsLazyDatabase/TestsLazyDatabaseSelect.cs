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

using Lazy.Vinke.Data;
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

        public virtual void Select_Validations_QueryTableDataRow_Exception()
        {
            // Arrange
            String tableName = "TestsSelectQueryTable";
            String subQuery = "(select * from TestsSelectQueryTable)";

            DataTable dataTable = new DataTable(tableName);
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Rows.Add(1);

            DataRow dataRow = dataTable.Rows[0];

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionDataRowNull = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Select(tableName, dataRow); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Select(null, dataRow); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Select(subQuery, dataRow); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Select(tableName, null); } catch (Exception exp) { exceptionDataRowNull = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionDataRowNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowNull);
        }

        public virtual void Select_Validations_QueryTableLazyDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsSelectQueryTable";
            String subQuery = "(select * from TestsSelectQueryTable)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Decimal };
            String[] fieldsLess = new String[] { "Amount" };

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbFieldsButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbFieldsLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Select(tableName, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Select(null, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Select(subQuery, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Select(tableName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.Select(tableName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.Select(tableName, null, null, fields); } catch (Exception exp) { exceptionDbFieldsButOthers = exp; }

            try { this.Database.Select(tableName, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.Select(tableName, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.Select(tableName, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbFieldsButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
        }

        public virtual void Select_Validations_QueryPageDataRow_Exception()
        {
            // Arrange
            String tableName = "TestsSelectQueryPage";
            String subQuery = "(select * from TestsSelectQueryPage)";

            DataTable dataTable = new DataTable(tableName);
            dataTable.Columns.Add("Id", typeof(Int32));
            dataTable.Rows.Add(1);

            DataRow dataRow = dataTable.Rows[0];

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionDataRowNull = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Select(tableName, dataRow); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Select(null, dataRow); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Select(subQuery, dataRow); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Select(tableName, null); } catch (Exception exp) { exceptionDataRowNull = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionDataRowNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionDataRowNull);
        }

        public virtual void Select_Validations_QueryPageLazyDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsSelectQueryPage";
            String subQuery = "(select * from TestsSelectQueryPage)";

            LazyPageData pageData = new LazyPageData();
            LazyPageData pageDataPageNumZero = new LazyPageData() { PageNum = 0 };
            LazyPageData pageDataPageSizeZero = new LazyPageData() { PageSize = 0 };
            LazyPageData pageDataOrderByEmpty = new LazyPageData() { OrderBy = "" };

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] fieldsLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionPageDataNull = null;
            Exception exceptionPageDataPageNumZero = null;
            Exception exceptionPageDataPageSizeZero = null;
            Exception exceptionPageDataOrderByEmpty = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbFieldsButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbFieldsLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Select(tableName, pageData, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Select(null, pageData, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Select(subQuery, pageData, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Select(tableName, null, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataNull = exp; }
            try { this.Database.Select(tableName, pageDataPageNumZero, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataPageNumZero = exp; }
            try { this.Database.Select(tableName, pageDataPageSizeZero, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataPageSizeZero = exp; }
            try { this.Database.Select(tableName, pageDataOrderByEmpty, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataOrderByEmpty = exp; }
            try { this.Database.Select(tableName, pageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.Select(tableName, pageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.Select(tableName, pageData, null, null, fields); } catch (Exception exp) { exceptionDbFieldsButOthers = exp; }

            try { this.Database.Select(tableName, pageData, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.Select(tableName, pageData, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.Select(tableName, pageData, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionPageDataNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionPageDataNull);
            Assert.AreEqual(exceptionPageDataPageNumZero.Message, LazyResourcesDatabase.LazyDatabaseExceptionPageDataPageNumLowerThanOne);
            Assert.AreEqual(exceptionPageDataPageSizeZero.Message, LazyResourcesDatabase.LazyDatabaseExceptionPageDataPageSizeLowerThanOne);
            Assert.AreEqual(exceptionPageDataOrderByEmpty.Message, LazyResourcesDatabase.LazyDatabaseExceptionPageDataOrderByNullOrEmpty);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbFieldsButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
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
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Id"], dataTable.Columns["Name"] };
            dataTable.Rows.Add(2000, "Item 2000");
            dataTable.AcceptChanges();

            DataRow dataRow = dataTable.Rows[0];

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

        public virtual void Select_QueryPage_DataRowWithPrimaryKey_Success()
        {
            // Arrange
            String tableName = "TestsSelectQueryPage";
            String columnsName = "IdMaster, IdChild, Name, Amount";
            String columnsParameter = "@IdMaster, @IdChild, @Name, @Amount";
            String sqlDelete = "delete from " + tableName + " where IdMaster in (100,125,175)";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 100, 1000, "Item 1000", 1000.1m });
            this.Database.Execute(sqlInsert, new Object[] { 100, 2000, "Item 2000", 2000.2m });
            this.Database.Execute(sqlInsert, new Object[] { 100, 3000, "Item 3000", 3000.3m });
            this.Database.Execute(sqlInsert, new Object[] { 125, 1000, "Item 1000", 1000.1m });
            this.Database.Execute(sqlInsert, new Object[] { 175, 2000, "Item 2000", 2000.2m });

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("IdMaster", typeof(Int32));
            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["IdMaster"] };
            dataTable.Rows.Add(100);
            dataTable.AcceptChanges();

            DataRow dataRow = dataTable.Rows[0];

            LazyPageData pageData = new LazyPageData() { PageSize = 2, OrderBy = "IdMaster,IdChild" };

            // Act
            pageData.PageNum = 1;
            LazyPageResult pageResult1 = this.Database.Select(tableName, pageData, dataRow, returnFields: new String[] { "IdMaster", "IdChild", "Amount" });
            pageData.PageNum = 2;
            LazyPageResult pageResult2 = this.Database.Select(tableName, pageData, dataRow, returnFields: new String[] { "IdMaster", "IdChild", "Amount" });

            // Assert
            Assert.AreEqual(pageResult1.PageNum, 1);
            Assert.AreEqual(pageResult1.PageSize, 2);
            Assert.AreEqual(pageResult1.PageItems, 2);
            Assert.AreEqual(pageResult1.PageCount, 2);
            Assert.AreEqual(pageResult1.CurrentCount, 2);
            Assert.AreEqual(pageResult1.TotalCount, 3);
            Assert.AreEqual(pageResult1.HasNextPage, true);
            Assert.AreEqual(pageResult1.DataTable.TableName, tableName);
            Assert.IsTrue(pageResult1.DataTable.Rows.Count == 2);
            Assert.AreEqual(Convert.ToDecimal(pageResult1.DataTable.Rows[0]["Amount"]), 1000.1m);
            Assert.AreEqual(Convert.ToDecimal(pageResult1.DataTable.Rows[1]["Amount"]), 2000.2m);
            Assert.AreEqual(pageResult2.PageNum, 2);
            Assert.AreEqual(pageResult2.PageSize, 2);
            Assert.AreEqual(pageResult2.PageItems, 1);
            Assert.AreEqual(pageResult2.PageCount, 2);
            Assert.AreEqual(pageResult2.CurrentCount, 3);
            Assert.AreEqual(pageResult2.TotalCount, 3);
            Assert.AreEqual(pageResult2.HasNextPage, false);
            Assert.AreEqual(pageResult2.DataTable.TableName, tableName);
            Assert.IsTrue(pageResult2.DataTable.Rows.Count == 1);
            Assert.AreEqual(Convert.ToDecimal(pageResult2.DataTable.Rows[0]["Amount"]), 3000.3m);

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

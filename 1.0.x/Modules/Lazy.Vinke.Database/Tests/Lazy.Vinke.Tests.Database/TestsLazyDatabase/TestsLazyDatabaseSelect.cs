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

        public virtual void Select_Validations_QueryTable_Exception()
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

        public virtual void Select_Validations_QueryPage_Exception()
        {
            // Arrange
            String tableName = "TestsSelectQueryPage";
            String subQuery = "(select * from TestsSelectQueryPage)";

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            LazyQueryPageData queryPageDataPageNumZero = new LazyQueryPageData() { PageNum = 0 };
            LazyQueryPageData queryPageDataPageSizeZero = new LazyQueryPageData() { PageSize = 0 };
            LazyQueryPageData queryPageDataOrderByEmpty = new LazyQueryPageData() { OrderBy = "" };

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] fieldsLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionQueryPageDataNull = null;
            Exception exceptionQueryPageDataPageNumZero = null;
            Exception exceptionQueryPageDataPageSizeZero = null;
            Exception exceptionQueryPageDataOrderByEmpty = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbFieldsButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbFieldsLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Select(tableName, queryPageData, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Select(null, queryPageData, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.Select(subQuery, queryPageData, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { this.Database.Select(tableName, null, values, dbTypes, fields); } catch (Exception exp) { exceptionQueryPageDataNull = exp; }
            try { this.Database.Select(tableName, queryPageDataPageNumZero, values, dbTypes, fields); } catch (Exception exp) { exceptionQueryPageDataPageNumZero = exp; }
            try { this.Database.Select(tableName, queryPageDataPageSizeZero, values, dbTypes, fields); } catch (Exception exp) { exceptionQueryPageDataPageSizeZero = exp; }
            try { this.Database.Select(tableName, queryPageDataOrderByEmpty, values, dbTypes, fields); } catch (Exception exp) { exceptionQueryPageDataOrderByEmpty = exp; }
            try { this.Database.Select(tableName, queryPageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.Select(tableName, queryPageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.Select(tableName, queryPageData, null, null, fields); } catch (Exception exp) { exceptionDbFieldsButOthers = exp; }

            try { this.Database.Select(tableName, queryPageData, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.Select(tableName, queryPageData, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.Select(tableName, queryPageData, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionQueryPageDataNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataNull);
            Assert.AreEqual(exceptionQueryPageDataPageNumZero.Message, LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataPageNumLowerThanOne);
            Assert.AreEqual(exceptionQueryPageDataPageSizeZero.Message, LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataPageSizeLowerThanOne);
            Assert.AreEqual(exceptionQueryPageDataOrderByEmpty.Message, LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataOrderByNullOrEmpty);
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

            LazyQueryPageData queryPageData = new LazyQueryPageData() { PageSize = 2, OrderBy = "IdMaster,IdChild" };

            // Act
            queryPageData.PageNum = 1;
            LazyQueryPageResult queryPageResult1 = this.Database.Select(tableName, queryPageData, dataRow, returnFields: new String[] { "IdMaster", "IdChild", "Amount" });
            queryPageData.PageNum = 2;
            LazyQueryPageResult queryPageResult2 = this.Database.Select(tableName, queryPageData, dataRow, returnFields: new String[] { "IdMaster", "IdChild", "Amount" });

            // Assert
            Assert.AreEqual(queryPageResult1.PageNum, 1);
            Assert.AreEqual(queryPageResult1.PageSize, 2);
            Assert.AreEqual(queryPageResult1.PageItems, 2);
            Assert.AreEqual(queryPageResult1.PageCount, 2);
            Assert.AreEqual(queryPageResult1.CurrentCount, 2);
            Assert.AreEqual(queryPageResult1.TotalCount, 3);
            Assert.AreEqual(queryPageResult1.HasNextPage, true);
            Assert.AreEqual(queryPageResult1.DataTable.TableName, tableName);
            Assert.IsTrue(queryPageResult1.DataTable.Rows.Count == 2);
            Assert.AreEqual(Convert.ToDecimal(queryPageResult1.DataTable.Rows[0]["Amount"]), 1000.1m);
            Assert.AreEqual(Convert.ToDecimal(queryPageResult1.DataTable.Rows[1]["Amount"]), 2000.2m);
            Assert.AreEqual(queryPageResult2.PageNum, 2);
            Assert.AreEqual(queryPageResult2.PageSize, 2);
            Assert.AreEqual(queryPageResult2.PageItems, 1);
            Assert.AreEqual(queryPageResult2.PageCount, 2);
            Assert.AreEqual(queryPageResult2.CurrentCount, 3);
            Assert.AreEqual(queryPageResult2.TotalCount, 3);
            Assert.AreEqual(queryPageResult2.HasNextPage, false);
            Assert.AreEqual(queryPageResult2.DataTable.TableName, tableName);
            Assert.IsTrue(queryPageResult2.DataTable.Rows.Count == 1);
            Assert.AreEqual(Convert.ToDecimal(queryPageResult2.DataTable.Rows[0]["Amount"]), 3000.3m);

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

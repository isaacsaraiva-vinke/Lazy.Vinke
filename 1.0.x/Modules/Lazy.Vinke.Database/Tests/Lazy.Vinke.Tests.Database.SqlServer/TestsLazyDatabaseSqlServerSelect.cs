// TestsLazyDatabaseSqlServerSelect.cs
//
// This file is integrated part of "Lazy Vinke Tests Database SqlServer" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.SqlServer;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.SqlServer
{
    [TestClass]
    public class TestsLazyDatabaseSqlServerSelect : TestsLazyDatabaseSelect
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Select_Validations_QueryTableDbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsSelectQueryTable";
            String subQuery = "(select * from TestsSelectQueryTable)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            SqlDbType[] dbTypesLess = new SqlDbType[] { SqlDbType.Decimal };
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

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.CloseConnection();

            try { databaseSqlServer.Select(tableName, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.Select(null, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseSqlServer.Select(subQuery, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databaseSqlServer.Select(tableName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseSqlServer.Select(tableName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseSqlServer.Select(tableName, null, null, fields); } catch (Exception exp) { exceptionDbFieldsButOthers = exp; }

            try { databaseSqlServer.Select(tableName, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.Select(tableName, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.Select(tableName, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }

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

        [TestMethod]
        public virtual void Select_Validations_QueryPageDbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsSelectQueryPage";
            String subQuery = "(select * from TestsSelectQueryPage)";

            LazyPageData pageData = new LazyPageData();
            LazyPageData pageDataPageNumZero = new LazyPageData() { PageNum = 0 };
            LazyPageData pageDataPageSizeZero = new LazyPageData() { PageSize = 0 };
            LazyPageData pageDataOrderByEmpty = new LazyPageData() { OrderBy = "" };

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            SqlDbType[] dbTypesLess = new SqlDbType[] { SqlDbType.Int };
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

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.CloseConnection();

            try { databaseSqlServer.Select(tableName, pageData, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.Select(null, pageData, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseSqlServer.Select(subQuery, pageData, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databaseSqlServer.Select(tableName, null, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataNull = exp; }
            try { databaseSqlServer.Select(tableName, pageDataPageNumZero, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataPageNumZero = exp; }
            try { databaseSqlServer.Select(tableName, pageDataPageSizeZero, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataPageSizeZero = exp; }
            try { databaseSqlServer.Select(tableName, pageDataOrderByEmpty, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataOrderByEmpty = exp; }
            try { databaseSqlServer.Select(tableName, pageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseSqlServer.Select(tableName, pageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseSqlServer.Select(tableName, pageData, null, null, fields); } catch (Exception exp) { exceptionDbFieldsButOthers = exp; }

            try { databaseSqlServer.Select(tableName, pageData, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.Select(tableName, pageData, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.Select(tableName, pageData, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }

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

        [TestMethod]
        public virtual void Select_QueryTable_Arrays_Success()
        {
            // Arrange
            String tableName = "TestsSelectQueryTable";
            String columnsName = "Id, Name, Amount";
            String columnsParameter = "@Id, @Name, @Amount";
            String sqlDelete = "delete from " + tableName + " where Id in (4000,5000,6000)";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            String[] fields = new String[] { "Id", "Name", "Amount" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar, SqlDbType.Decimal };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { 4000, "Item 4000", 4000.1m },
                new Object[] { 5000, "Item 5000", 5000.1m },
                new Object[] { 6000, "Item 6000", 6000.1m }
            };

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            databaseSqlServer.Execute(sqlInsert, valuesList[0], dbTypes, fields);
            databaseSqlServer.Execute(sqlInsert, valuesList[1], dbTypes, fields);
            databaseSqlServer.Execute(sqlInsert, valuesList[2], dbTypes, fields);

            // Act
            DataTable dataTable = databaseSqlServer.Select(tableName, valuesList[1], dbTypes, fields, returnFields: new String[] { "Amount" });

            // Assert
            Assert.AreEqual(dataTable.TableName, tableName);
            Assert.IsTrue(dataTable.Rows.Count == 1);
            Assert.AreEqual(Convert.ToDecimal(dataTable.Rows[0]["Amount"]), 5000.1m);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public virtual void Select_QueryPage_Arrays_Success()
        {
            // Arrange
            String tableName = "TestsSelectQueryPage";
            String columnsName = "IdMaster, IdChild, Name, Amount";
            String columnsParameter = "@IdMaster, @IdChild, @Name, @Amount";
            String sqlDelete = "delete from " + tableName + " where IdMaster in (300,325,375)";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            String[] fields = new String[] { "IdMaster", "IdChild", "Name", "Amount" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.Int, SqlDbType.VarChar, SqlDbType.Decimal };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { 300, 1000, "Item 1000", 1000.1m },
                new Object[] { 300, 2000, "Item 2000", 2000.2m },
                new Object[] { 300, 3000, "Item 3000", 3000.3m },
                new Object[] { 325, 1000, "Item 1000", 1000.1m },
                new Object[] { 375, 2000, "Item 2000", 2000.2m }
            };

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            databaseSqlServer.Execute(sqlInsert, valuesList[0], dbTypes, fields);
            databaseSqlServer.Execute(sqlInsert, valuesList[1], dbTypes, fields);
            databaseSqlServer.Execute(sqlInsert, valuesList[2], dbTypes, fields);
            databaseSqlServer.Execute(sqlInsert, valuesList[3], dbTypes, fields);
            databaseSqlServer.Execute(sqlInsert, valuesList[4], dbTypes, fields);

            fields = new String[] { "IdMaster" };
            dbTypes = new SqlDbType[] { SqlDbType.Int };
            valuesList = new List<Object[]>() { new Object[] { 300 } };

            LazyPageData pageData = new LazyPageData() { PageSize = 2, OrderBy = "IdMaster,IdChild" };

            // Act
            pageData.PageNum = 1;
            LazyPageResult pageResult1 = databaseSqlServer.Select(tableName, pageData, valuesList[0], dbTypes, fields, returnFields: new String[] { "IdMaster", "IdChild", "Amount" });
            pageData.PageNum = 2;
            LazyPageResult pageResult2 = databaseSqlServer.Select(tableName, pageData, valuesList[0], dbTypes, fields, returnFields: new String[] { "IdMaster", "IdChild", "Amount" });

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

        [TestMethod]
        public override void Select_Validations_QueryTableDataRow_Exception()
        {
            base.Select_Validations_QueryTableDataRow_Exception();
        }

        [TestMethod]
        public override void Select_Validations_QueryTableLazyDbTypeArrays_Exception()
        {
            base.Select_Validations_QueryTableLazyDbTypeArrays_Exception();
        }

        [TestMethod]
        public override void Select_Validations_QueryPageDataRow_Exception()
        {
            base.Select_Validations_QueryPageDataRow_Exception();
        }

        [TestMethod]
        public override void Select_Validations_QueryPageLazyDbTypeArrays_Exception()
        {
            base.Select_Validations_QueryPageLazyDbTypeArrays_Exception();
        }

        [TestMethod]
        public override void Select_QueryTable_DataRowWithPrimaryKey_Success()
        {
            base.Select_QueryTable_DataRowWithPrimaryKey_Success();
        }

        [TestMethod]
        public override void Select_QueryPage_DataRowWithPrimaryKey_Success()
        {
            base.Select_QueryPage_DataRowWithPrimaryKey_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

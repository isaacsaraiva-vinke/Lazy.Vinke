// TestsLazyDatabasePostgreSelect.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Postgre" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Npgsql;
using NpgsqlTypes;

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.Postgre;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.Postgre
{
    [TestClass]
    public class TestsLazyDatabasePostgreSelect : TestsLazyDatabaseSelect
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Select_Validations_QueryTableDbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsSelectQueryTable";
            String subQuery = "(select * from TestsSelectQueryTable)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            NpgsqlDbType[] dbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Numeric };
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

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.Select(tableName, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.Select(null, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databasePostgre.Select(subQuery, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databasePostgre.Select(tableName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databasePostgre.Select(tableName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databasePostgre.Select(tableName, null, null, fields); } catch (Exception exp) { exceptionDbFieldsButOthers = exp; }

            try { databasePostgre.Select(tableName, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databasePostgre.Select(tableName, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databasePostgre.Select(tableName, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }

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
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            NpgsqlDbType[] dbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Integer };
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

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.Select(tableName, pageData, values, dbTypes, fields); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.Select(null, pageData, values, dbTypes, fields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databasePostgre.Select(subQuery, pageData, values, dbTypes, fields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databasePostgre.Select(tableName, null, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataNull = exp; }
            try { databasePostgre.Select(tableName, pageDataPageNumZero, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataPageNumZero = exp; }
            try { databasePostgre.Select(tableName, pageDataPageSizeZero, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataPageSizeZero = exp; }
            try { databasePostgre.Select(tableName, pageDataOrderByEmpty, values, dbTypes, fields); } catch (Exception exp) { exceptionPageDataOrderByEmpty = exp; }
            try { databasePostgre.Select(tableName, pageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databasePostgre.Select(tableName, pageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databasePostgre.Select(tableName, pageData, null, null, fields); } catch (Exception exp) { exceptionDbFieldsButOthers = exp; }

            try { databasePostgre.Select(tableName, pageData, valuesLess, dbTypes, fields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databasePostgre.Select(tableName, pageData, values, dbTypesLess, fields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databasePostgre.Select(tableName, pageData, values, dbTypes, fieldsLess); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }

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
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar, NpgsqlDbType.Numeric };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { 4000, "Item 4000", 4000.1m },
                new Object[] { 5000, "Item 5000", 5000.1m },
                new Object[] { 6000, "Item 6000", 6000.1m }
            };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            databasePostgre.Execute(sqlInsert, valuesList[0], dbTypes, fields);
            databasePostgre.Execute(sqlInsert, valuesList[1], dbTypes, fields);
            databasePostgre.Execute(sqlInsert, valuesList[2], dbTypes, fields);

            // Act
            DataTable dataTable = databasePostgre.Select(tableName, valuesList[1], dbTypes, fields, returnFields: new String[] { "Amount" });

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
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer, NpgsqlDbType.Varchar, NpgsqlDbType.Numeric };
            List<Object[]> valuesList = new List<Object[]>() {
                new Object[] { 300, 1000, "Item 1000", 1000.1m },
                new Object[] { 300, 2000, "Item 2000", 2000.2m },
                new Object[] { 300, 3000, "Item 3000", 3000.3m },
                new Object[] { 325, 1000, "Item 1000", 1000.1m },
                new Object[] { 375, 2000, "Item 2000", 2000.2m }
            };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            databasePostgre.Execute(sqlInsert, valuesList[0], dbTypes, fields);
            databasePostgre.Execute(sqlInsert, valuesList[1], dbTypes, fields);
            databasePostgre.Execute(sqlInsert, valuesList[2], dbTypes, fields);
            databasePostgre.Execute(sqlInsert, valuesList[3], dbTypes, fields);
            databasePostgre.Execute(sqlInsert, valuesList[4], dbTypes, fields);

            fields = new String[] { "IdMaster" };
            dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer };
            valuesList = new List<Object[]>() { new Object[] { 300 } };

            LazyPageData pageData = new LazyPageData() { PageSize = 2, OrderBy = "IdMaster,IdChild" };

            // Act
            pageData.PageNum = 1;
            LazyPageResult pageResult1 = databasePostgre.Select(tableName, pageData, valuesList[0], dbTypes, fields, returnFields: new String[] { "IdMaster", "IdChild", "Amount" });
            pageData.PageNum = 2;
            LazyPageResult pageResult2 = databasePostgre.Select(tableName, pageData, valuesList[0], dbTypes, fields, returnFields: new String[] { "IdMaster", "IdChild", "Amount" });

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

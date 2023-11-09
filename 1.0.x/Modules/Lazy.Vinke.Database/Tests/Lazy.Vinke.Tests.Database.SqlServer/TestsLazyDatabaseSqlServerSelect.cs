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

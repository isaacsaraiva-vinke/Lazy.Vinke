// TestsLazyDatabaseSqlServerQueryPage.cs
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

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.SqlServer;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.SqlServer
{
    [TestClass]
    public class TestsLazyDatabaseSqlServerQueryPage : TestsLazyDatabaseQueryPage
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void QueryPage_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "insert into QueryPage_Validations_DbmsDbType (id, name) values (@id, @name)";

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            LazyQueryPageData queryPageDataPageNumZero = new LazyQueryPageData() { PageNum = 0 };
            LazyQueryPageData queryPageDataPageSizeZero = new LazyQueryPageData() { PageSize = 0 };
            LazyQueryPageData queryPageDataOrderByEmpty = new LazyQueryPageData() { OrderBy = "" };

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            SqlDbType[] dbTypes = new SqlDbType[] { SqlDbType.Int, SqlDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            SqlDbType[] dbTypesLess = new SqlDbType[] { SqlDbType.Int };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionQueryPageDataNull = null;
            Exception exceptionQueryPageDataPageNumZero = null;
            Exception exceptionQueryPageDataPageSizeZero = null;
            Exception exceptionQueryPageDataOrderByEmpty = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            databaseSqlServer.CloseConnection();

            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseSqlServer.OpenConnection();

            try { databaseSqlServer.QueryPage(null, "tableName", queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseSqlServer.QueryPage(sql, null, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseSqlServer.QueryPage(sql, "tableName", null, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataNull = exp; }
            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageDataPageNumZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageNumZero = exp; }
            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageDataPageSizeZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageSizeZero = exp; }
            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageDataOrderByEmpty, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataOrderByEmpty = exp; }
            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageData, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageData, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageData, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseSqlServer.QueryPage(sql, "tableName", queryPageData, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);
            Assert.AreEqual(exceptionQueryPageDataNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataNull);
            Assert.AreEqual(exceptionQueryPageDataPageNumZero.Message, LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataPageNumLowerThanOne);
            Assert.AreEqual(exceptionQueryPageDataPageSizeZero.Message, LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataPageSizeLowerThanOne);
            Assert.AreEqual(exceptionQueryPageDataOrderByEmpty.Message, LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataOrderByNullOrEmpty);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public virtual void QueryPage_DataAdapterFill_DbmsDbTypeLowerPage_Success()
        {
            // Arrange
            String tableName = "QueryPage_DataAdapterFill";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from QueryPage_DataAdapterFill where Id between 33 and 43";
            String sqlInsert = "insert into QueryPage_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from QueryPage_DataAdapterFill where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 33, "Name 33", "Description 33" });
            this.Database.Execute(sqlInsert, new Object[] { 34, "Name 34", "Description 34" });
            this.Database.Execute(sqlInsert, new Object[] { 35, "Name 35", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 36, "Name 36", "Description 36" });
            this.Database.Execute(sqlInsert, new Object[] { 37, "Name 37", "Description 37" });
            this.Database.Execute(sqlInsert, new Object[] { 38, "Name 38", "Description 38" });
            this.Database.Execute(sqlInsert, new Object[] { 39, DBNull.Value, "Description 39" });
            this.Database.Execute(sqlInsert, new Object[] { 40, "Name 40", "Description 40" });
            this.Database.Execute(sqlInsert, new Object[] { 41, "Name 41", "Description 41" });
            this.Database.Execute(sqlInsert, new Object[] { 42, "Name 42", "Description 42" });
            this.Database.Execute(sqlInsert, new Object[] { 43, "Name 43", "Description 43" });

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            queryPageData.OrderBy = "Id";
            queryPageData.PageNum = 1;
            queryPageData.PageSize = 3;

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            LazyQueryPageResult queryPageResult1 = databaseSqlServer.QueryPage(sql, tableName, queryPageData, new Object[] { 33, 43 }, new SqlDbType[] { SqlDbType.Int, SqlDbType.Int }, new String[] { "LowId", "HighId" });
            queryPageData.PageNum = 2;
            LazyQueryPageResult queryPageResult2 = databaseSqlServer.QueryPage(sql, tableName, queryPageData, new Object[] { 33, 43 }, new SqlDbType[] { SqlDbType.Int, SqlDbType.Int }, new String[] { "LowId", "HighId" });
            queryPageData.PageNum = 3;
            LazyQueryPageResult queryPageResult3 = databaseSqlServer.QueryPage(sql, tableName, queryPageData, new Object[] { 33, 43 }, new SqlDbType[] { SqlDbType.Int, SqlDbType.Int }, new String[] { "LowId", "HighId" });

            // Assert
            Assert.AreEqual(queryPageResult1.PageNum, 1);
            Assert.AreEqual(queryPageResult1.PageSize, 3);
            Assert.AreEqual(queryPageResult1.PageItems, 3);
            Assert.AreEqual(queryPageResult1.PageCount, 3);
            Assert.AreEqual(queryPageResult1.CurrentCount, 3);
            Assert.AreEqual(queryPageResult1.TotalCount, 9);
            Assert.AreEqual(queryPageResult1.HasNextPage, true);
            Assert.AreEqual(queryPageResult2.PageNum, 2);
            Assert.AreEqual(queryPageResult2.PageSize, 3);
            Assert.AreEqual(queryPageResult2.PageItems, 3);
            Assert.AreEqual(queryPageResult2.PageCount, 3);
            Assert.AreEqual(queryPageResult2.CurrentCount, 6);
            Assert.AreEqual(queryPageResult2.TotalCount, 9);
            Assert.AreEqual(queryPageResult2.HasNextPage, true);
            Assert.AreEqual(queryPageResult3.PageNum, 3);
            Assert.AreEqual(queryPageResult3.PageSize, 3);
            Assert.AreEqual(queryPageResult3.PageItems, 3);
            Assert.AreEqual(queryPageResult3.PageCount, 3);
            Assert.AreEqual(queryPageResult3.CurrentCount, 9);
            Assert.AreEqual(queryPageResult3.TotalCount, 9);
            Assert.AreEqual(queryPageResult3.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public virtual void QueryPage_DataAdapterFill_DbmsDbTypeHigherPage_Success()
        {
            // Arrange
            String tableName = "QueryPage_DataAdapterFill";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from QueryPage_DataAdapterFill where Id between 44 and 54";
            String sqlInsert = "insert into QueryPage_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from QueryPage_DataAdapterFill where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 44, "Name 44", "Description 44" });
            this.Database.Execute(sqlInsert, new Object[] { 45, "Name 45", "Description 45" });
            this.Database.Execute(sqlInsert, new Object[] { 46, "Name 46", "Description 46" });
            this.Database.Execute(sqlInsert, new Object[] { 47, "Name 47", "Description 47" });
            this.Database.Execute(sqlInsert, new Object[] { 48, "Name 48", "Description 48" });
            this.Database.Execute(sqlInsert, new Object[] { 49, "Name 49", "Description 49" });
            this.Database.Execute(sqlInsert, new Object[] { 50, "Name 50", "Description 50" });
            this.Database.Execute(sqlInsert, new Object[] { 51, "Name 51", "Description 51" });
            this.Database.Execute(sqlInsert, new Object[] { 52, "Name 52", "Description 52" });
            this.Database.Execute(sqlInsert, new Object[] { 53, "Name 53", "Description 53" });
            this.Database.Execute(sqlInsert, new Object[] { 54, "Name 54", "Description 54" });

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            queryPageData.OrderBy = "Id";
            queryPageData.PageNum = 1;
            queryPageData.PageSize = 50;

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            LazyQueryPageResult queryPageResult1 = databaseSqlServer.QueryPage(sql, tableName, queryPageData, new Object[] { 44, 54 }, new SqlDbType[] { SqlDbType.Int, SqlDbType.Int }, new String[] { "LowId", "HighId" });

            // Assert
            Assert.AreEqual(queryPageResult1.PageNum, 1);
            Assert.AreEqual(queryPageResult1.PageSize, 50);
            Assert.AreEqual(queryPageResult1.PageItems, 11);
            Assert.AreEqual(queryPageResult1.PageCount, 1);
            Assert.AreEqual(queryPageResult1.CurrentCount, 11);
            Assert.AreEqual(queryPageResult1.TotalCount, 11);
            Assert.AreEqual(queryPageResult1.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public virtual void QueryPage_DataAdapterFill_DbmsDbTypeOutOfRange_Success()
        {
            // Arrange
            String tableName = "QueryPage_DataAdapterFill";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from QueryPage_DataAdapterFill where Id in (3,4)";
            String sqlInsert = "insert into QueryPage_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from QueryPage_DataAdapterFill where Id in (@Id3,@Id4)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 3, "Name 3", "Description 3" });
            this.Database.Execute(sqlInsert, new Object[] { 4, "Name 4", "Description 4" });

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            queryPageData.OrderBy = "Id";
            queryPageData.PageNum = 2;
            queryPageData.PageSize = 2;

            LazyDatabaseSqlServer databaseSqlServer = (LazyDatabaseSqlServer)this.Database;

            // Act
            LazyQueryPageResult queryPageResult = databaseSqlServer.QueryPage(sql, tableName, queryPageData, new Object[] { 3, 4 }, new SqlDbType[] { SqlDbType.Int, SqlDbType.Int }, new String[] { "Id3", "Id4" });

            // Assert
            Assert.AreEqual(queryPageResult.PageNum, 2);
            Assert.AreEqual(queryPageResult.PageSize, 2);
            Assert.AreEqual(queryPageResult.PageItems, 0);
            Assert.AreEqual(queryPageResult.PageCount, 0);
            Assert.AreEqual(queryPageResult.CurrentCount, 0);
            Assert.AreEqual(queryPageResult.TotalCount, 0);
            Assert.AreEqual(queryPageResult.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryPage_Validations_LazyDbType_Exception()
        {
            base.QueryPage_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void QueryPage_DataAdapterFill_LazyDbTypeLowerPage_Success()
        {
            base.QueryPage_DataAdapterFill_LazyDbTypeLowerPage_Success();
        }

        [TestMethod]
        public override void QueryPage_DataAdapterFill_LazyDbTypeHigherPage_Success()
        {
            base.QueryPage_DataAdapterFill_LazyDbTypeHigherPage_Success();
        }

        [TestMethod]
        public override void QueryPage_DataAdapterFill_LazyDbTypeOutOfRange_Success()
        {
            base.QueryPage_DataAdapterFill_LazyDbTypeOutOfRange_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

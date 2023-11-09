// TestsLazyDatabasePostgreQueryPage.cs
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
    public class TestsLazyDatabasePostgreQueryPage : TestsLazyDatabaseQueryPage
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void QueryPage_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String sql = "select * from TestsQueryPage where Id = @Id";

            LazyPageData pageData = new LazyPageData();
            LazyPageData pageDataPageNumZero = new LazyPageData() { PageNum = 0 };
            LazyPageData pageDataPageSizeZero = new LazyPageData() { PageSize = 0 };
            LazyPageData pageDataOrderByEmpty = new LazyPageData() { OrderBy = "" };

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar };
            String[] parameters = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            NpgsqlDbType[] dbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Integer };
            String[] parametersLess = new String[] { "Id" };

            LazyPageData pageDataNull = null;

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionPageDataNull = null;
            Exception exceptionPageDataPageNumZero = null;
            Exception exceptionPageDataPageSizeZero = null;
            Exception exceptionPageDataOrderByEmpty = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.QueryPage(sql, tableName, pageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.QueryPage(null, tableName, pageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databasePostgre.QueryPage(sql, null, pageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databasePostgre.QueryPage(sql, tableName, pageDataNull, values, dbTypes, parameters); } catch (Exception exp) { exceptionPageDataNull = exp; }
            try { databasePostgre.QueryPage(sql, tableName, pageDataPageNumZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionPageDataPageNumZero = exp; }
            try { databasePostgre.QueryPage(sql, tableName, pageDataPageSizeZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionPageDataPageSizeZero = exp; }
            try { databasePostgre.QueryPage(sql, tableName, pageDataOrderByEmpty, values, dbTypes, parameters); } catch (Exception exp) { exceptionPageDataOrderByEmpty = exp; }
            try { databasePostgre.QueryPage(sql, tableName, pageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databasePostgre.QueryPage(sql, tableName, pageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databasePostgre.QueryPage(sql, tableName, pageData, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databasePostgre.QueryPage(sql, tableName, pageData, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databasePostgre.QueryPage(sql, tableName, pageData, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databasePostgre.QueryPage(sql, tableName, pageData, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);
            Assert.AreEqual(exceptionPageDataNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionPageDataNull);
            Assert.AreEqual(exceptionPageDataPageNumZero.Message, LazyResourcesDatabase.LazyDatabaseExceptionPageDataPageNumLowerThanOne);
            Assert.AreEqual(exceptionPageDataPageSizeZero.Message, LazyResourcesDatabase.LazyDatabaseExceptionPageDataPageSizeLowerThanOne);
            Assert.AreEqual(exceptionPageDataOrderByEmpty.Message, LazyResourcesDatabase.LazyDatabaseExceptionPageDataOrderByNullOrEmpty);
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
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 500, 600 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 500, "Name 500", "Description 500" });
            this.Database.Execute(sqlInsert, new Object[] { 510, "Name 510", "Description 510" });
            this.Database.Execute(sqlInsert, new Object[] { 520, "Name 520", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 530, "Name 530", "Description 530" });
            this.Database.Execute(sqlInsert, new Object[] { 540, "Name 540", "Description 540" });
            this.Database.Execute(sqlInsert, new Object[] { 550, "Name 550", "Description 550" });
            this.Database.Execute(sqlInsert, new Object[] { 560, DBNull.Value, "Description 560" });
            this.Database.Execute(sqlInsert, new Object[] { 570, "Name 570", "Description 570" });
            this.Database.Execute(sqlInsert, new Object[] { 580, "Name 580", "Description 580" });
            this.Database.Execute(sqlInsert, new Object[] { 590, "Name 590", "Description 590" });
            this.Database.Execute(sqlInsert, new Object[] { 600, "Name 600", "Description 600" });

            LazyPageData pageData = new LazyPageData() { PageSize = 3, OrderBy = "Id" };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            pageData.PageNum = 1;
            LazyPageResult pageResult1 = databasePostgre.QueryPage(sql, tableName, pageData, new Object[] { 500, 600 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });
            pageData.PageNum = 2;
            LazyPageResult pageResult2 = databasePostgre.QueryPage(sql, tableName, pageData, new Object[] { 500, 600 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });
            pageData.PageNum = 3;
            LazyPageResult pageResult3 = databasePostgre.QueryPage(sql, tableName, pageData, new Object[] { 500, 600 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });

            // Assert
            Assert.AreEqual(pageResult1.PageNum, 1);
            Assert.AreEqual(pageResult1.PageSize, 3);
            Assert.AreEqual(pageResult1.PageItems, 3);
            Assert.AreEqual(pageResult1.PageCount, 3);
            Assert.AreEqual(pageResult1.CurrentCount, 3);
            Assert.AreEqual(pageResult1.TotalCount, 9);
            Assert.AreEqual(pageResult1.HasNextPage, true);
            Assert.AreEqual(pageResult2.PageNum, 2);
            Assert.AreEqual(pageResult2.PageSize, 3);
            Assert.AreEqual(pageResult2.PageItems, 3);
            Assert.AreEqual(pageResult2.PageCount, 3);
            Assert.AreEqual(pageResult2.CurrentCount, 6);
            Assert.AreEqual(pageResult2.TotalCount, 9);
            Assert.AreEqual(pageResult2.HasNextPage, true);
            Assert.AreEqual(pageResult3.PageNum, 3);
            Assert.AreEqual(pageResult3.PageSize, 3);
            Assert.AreEqual(pageResult3.PageItems, 3);
            Assert.AreEqual(pageResult3.PageCount, 3);
            Assert.AreEqual(pageResult3.CurrentCount, 9);
            Assert.AreEqual(pageResult3.TotalCount, 9);
            Assert.AreEqual(pageResult3.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 500, 600 }); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public virtual void QueryPage_DataAdapterFill_DbmsDbTypeHigherPage_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 700, 800 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 700, "Name 700", "Description 700" });
            this.Database.Execute(sqlInsert, new Object[] { 710, "Name 710", "Description 710" });
            this.Database.Execute(sqlInsert, new Object[] { 720, "Name 720", "Description 720" });
            this.Database.Execute(sqlInsert, new Object[] { 730, "Name 730", "Description 730" });
            this.Database.Execute(sqlInsert, new Object[] { 740, "Name 740", "Description 740" });
            this.Database.Execute(sqlInsert, new Object[] { 750, "Name 750", "Description 750" });
            this.Database.Execute(sqlInsert, new Object[] { 760, "Name 760", "Description 760" });
            this.Database.Execute(sqlInsert, new Object[] { 770, "Name 770", "Description 770" });
            this.Database.Execute(sqlInsert, new Object[] { 780, "Name 780", "Description 780" });
            this.Database.Execute(sqlInsert, new Object[] { 790, "Name 790", "Description 790" });
            this.Database.Execute(sqlInsert, new Object[] { 800, "Name 800", "Description 800" });

            LazyPageData pageData = new LazyPageData() { PageSize = 50, OrderBy = "Id" };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            pageData.PageNum = 1;
            LazyPageResult pageResult1 = databasePostgre.QueryPage(sql, tableName, pageData, new Object[] { 700, 800 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });

            // Assert
            Assert.AreEqual(pageResult1.PageNum, 1);
            Assert.AreEqual(pageResult1.PageSize, 50);
            Assert.AreEqual(pageResult1.PageItems, 11);
            Assert.AreEqual(pageResult1.PageCount, 1);
            Assert.AreEqual(pageResult1.CurrentCount, 11);
            Assert.AreEqual(pageResult1.TotalCount, 11);
            Assert.AreEqual(pageResult1.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 700, 800 }); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public virtual void QueryPage_DataAdapterFill_DbmsDbTypeOutOfRange_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 3, 4 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 3, "Name 3", "Description 3" });
            this.Database.Execute(sqlInsert, new Object[] { 4, "Name 4", "Description 4" });

            LazyPageData pageData = new LazyPageData() { PageSize = 2, OrderBy = "Id" };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            pageData.PageNum = 2;
            LazyPageResult pageResult = databasePostgre.QueryPage(sql, tableName, pageData, new Object[] { 3, 4 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });

            // Assert
            Assert.AreEqual(pageResult.PageNum, 2);
            Assert.AreEqual(pageResult.PageSize, 2);
            Assert.AreEqual(pageResult.PageItems, 0);
            Assert.AreEqual(pageResult.PageCount, 0);
            Assert.AreEqual(pageResult.CurrentCount, 0);
            Assert.AreEqual(pageResult.TotalCount, 0);
            Assert.AreEqual(pageResult.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 3, 4 }); }
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

    [TestClass]
    [Obsolete("QueryPage with LazyQueryPageResult and LazyQueryPageData was deprecated! Use QueryPage with LazyPageResult and LazyPageData instead!", false)]
    public class TestsLazyDatabasePostgreQueryPageObsolete : TestsLazyDatabaseQueryPageObsolete
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void QueryPage_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String sql = "select * from TestsQueryPage where Id = @Id";

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            LazyQueryPageData queryPageDataPageNumZero = new LazyQueryPageData() { PageNum = 0 };
            LazyQueryPageData queryPageDataPageSizeZero = new LazyQueryPageData() { PageSize = 0 };
            LazyQueryPageData queryPageDataOrderByEmpty = new LazyQueryPageData() { OrderBy = "" };

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar };
            String[] parameters = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            NpgsqlDbType[] dbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Integer };
            String[] parametersLess = new String[] { "Id" };

            LazyQueryPageData queryPageDataNull = null;

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

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.QueryPage(sql, tableName, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.QueryPage(null, tableName, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databasePostgre.QueryPage(sql, null, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databasePostgre.QueryPage(sql, tableName, queryPageDataNull, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataNull = exp; }
            try { databasePostgre.QueryPage(sql, tableName, queryPageDataPageNumZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageNumZero = exp; }
            try { databasePostgre.QueryPage(sql, tableName, queryPageDataPageSizeZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageSizeZero = exp; }
            try { databasePostgre.QueryPage(sql, tableName, queryPageDataOrderByEmpty, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataOrderByEmpty = exp; }
            try { databasePostgre.QueryPage(sql, tableName, queryPageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databasePostgre.QueryPage(sql, tableName, queryPageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databasePostgre.QueryPage(sql, tableName, queryPageData, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databasePostgre.QueryPage(sql, tableName, queryPageData, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databasePostgre.QueryPage(sql, tableName, queryPageData, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databasePostgre.QueryPage(sql, tableName, queryPageData, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 500, 600 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 500, "Name 500", "Description 500" });
            this.Database.Execute(sqlInsert, new Object[] { 510, "Name 510", "Description 510" });
            this.Database.Execute(sqlInsert, new Object[] { 520, "Name 520", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 530, "Name 530", "Description 530" });
            this.Database.Execute(sqlInsert, new Object[] { 540, "Name 540", "Description 540" });
            this.Database.Execute(sqlInsert, new Object[] { 550, "Name 550", "Description 550" });
            this.Database.Execute(sqlInsert, new Object[] { 560, DBNull.Value, "Description 560" });
            this.Database.Execute(sqlInsert, new Object[] { 570, "Name 570", "Description 570" });
            this.Database.Execute(sqlInsert, new Object[] { 580, "Name 580", "Description 580" });
            this.Database.Execute(sqlInsert, new Object[] { 590, "Name 590", "Description 590" });
            this.Database.Execute(sqlInsert, new Object[] { 600, "Name 600", "Description 600" });

            LazyQueryPageData queryPageData = new LazyQueryPageData() { PageSize = 3, OrderBy = "Id" };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            queryPageData.PageNum = 1;
            LazyQueryPageResult queryPageResult1 = databasePostgre.QueryPage(sql, tableName, queryPageData, new Object[] { 500, 600 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });
            queryPageData.PageNum = 2;
            LazyQueryPageResult queryPageResult2 = databasePostgre.QueryPage(sql, tableName, queryPageData, new Object[] { 500, 600 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });
            queryPageData.PageNum = 3;
            LazyQueryPageResult queryPageResult3 = databasePostgre.QueryPage(sql, tableName, queryPageData, new Object[] { 500, 600 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });

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
            try { this.Database.Execute(sqlDelete, new Object[] { 500, 600 }); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public virtual void QueryPage_DataAdapterFill_DbmsDbTypeHigherPage_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 700, 800 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 700, "Name 700", "Description 700" });
            this.Database.Execute(sqlInsert, new Object[] { 710, "Name 710", "Description 710" });
            this.Database.Execute(sqlInsert, new Object[] { 720, "Name 720", "Description 720" });
            this.Database.Execute(sqlInsert, new Object[] { 730, "Name 730", "Description 730" });
            this.Database.Execute(sqlInsert, new Object[] { 740, "Name 740", "Description 740" });
            this.Database.Execute(sqlInsert, new Object[] { 750, "Name 750", "Description 750" });
            this.Database.Execute(sqlInsert, new Object[] { 760, "Name 760", "Description 760" });
            this.Database.Execute(sqlInsert, new Object[] { 770, "Name 770", "Description 770" });
            this.Database.Execute(sqlInsert, new Object[] { 780, "Name 780", "Description 780" });
            this.Database.Execute(sqlInsert, new Object[] { 790, "Name 790", "Description 790" });
            this.Database.Execute(sqlInsert, new Object[] { 800, "Name 800", "Description 800" });

            LazyQueryPageData queryPageData = new LazyQueryPageData() { PageSize = 50, OrderBy = "Id" };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            queryPageData.PageNum = 1;
            LazyQueryPageResult queryPageResult1 = databasePostgre.QueryPage(sql, tableName, queryPageData, new Object[] { 700, 800 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });

            // Assert
            Assert.AreEqual(queryPageResult1.PageNum, 1);
            Assert.AreEqual(queryPageResult1.PageSize, 50);
            Assert.AreEqual(queryPageResult1.PageItems, 11);
            Assert.AreEqual(queryPageResult1.PageCount, 1);
            Assert.AreEqual(queryPageResult1.CurrentCount, 11);
            Assert.AreEqual(queryPageResult1.TotalCount, 11);
            Assert.AreEqual(queryPageResult1.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 700, 800 }); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public virtual void QueryPage_DataAdapterFill_DbmsDbTypeOutOfRange_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 3, 4 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 3, "Name 3", "Description 3" });
            this.Database.Execute(sqlInsert, new Object[] { 4, "Name 4", "Description 4" });

            LazyQueryPageData queryPageData = new LazyQueryPageData() { PageSize = 2, OrderBy = "Id" };

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            queryPageData.PageNum = 2;
            LazyQueryPageResult queryPageResult = databasePostgre.QueryPage(sql, tableName, queryPageData, new Object[] { 3, 4 }, new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer }, new String[] { "LowId", "HighId" });

            // Assert
            Assert.AreEqual(queryPageResult.PageNum, 2);
            Assert.AreEqual(queryPageResult.PageSize, 2);
            Assert.AreEqual(queryPageResult.PageItems, 0);
            Assert.AreEqual(queryPageResult.PageCount, 0);
            Assert.AreEqual(queryPageResult.CurrentCount, 0);
            Assert.AreEqual(queryPageResult.TotalCount, 0);
            Assert.AreEqual(queryPageResult.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 3, 4 }); }
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

// TestsLazyDatabaseQueryPage.cs
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
    public class TestsLazyDatabaseQueryPage
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void QueryPage_Validations_LazyDbType_Exception()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String sql = "select * from TestsQueryPage where Id = @Id";

            LazyPageData pageData = new LazyPageData();
            LazyPageData pageDataPageNumZero = new LazyPageData() { PageNum = 0 };
            LazyPageData pageDataPageSizeZero = new LazyPageData() { PageSize = 0 };
            LazyPageData pageDataOrderByEmpty = new LazyPageData() { OrderBy = "" };

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] parametersLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionTableNameNull = null;
            //Exception exceptionPageDataNull = null;
            Exception exceptionPageDataPageNumZero = null;
            Exception exceptionPageDataPageSizeZero = null;
            Exception exceptionPageDataOrderByEmpty = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.QueryPage(sql, tableName, pageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.QueryPage(null, tableName, pageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.QueryPage(sql, null, pageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            //try { this.Database.QueryPage(sql, tableName, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionPageDataNull = exp; }
            try { this.Database.QueryPage(sql, tableName, pageDataPageNumZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionPageDataPageNumZero = exp; }
            try { this.Database.QueryPage(sql, tableName, pageDataPageSizeZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionPageDataPageSizeZero = exp; }
            try { this.Database.QueryPage(sql, tableName, pageDataOrderByEmpty, values, dbTypes, parameters); } catch (Exception exp) { exceptionPageDataOrderByEmpty = exp; }
            try { this.Database.QueryPage(sql, tableName, pageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.QueryPage(sql, tableName, pageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.QueryPage(sql, tableName, pageData, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.QueryPage(sql, tableName, pageData, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.QueryPage(sql, tableName, pageData, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.QueryPage(sql, tableName, pageData, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);
            //Assert.AreEqual(exceptionPageDataNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionPageDataNull);
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

        public virtual void QueryPage_DataAdapterFill_LazyDbTypeLowerPage_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 100, 200 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 100, "Name 100", "Description 100" });
            this.Database.Execute(sqlInsert, new Object[] { 110, "Name 110", "Description 110" });
            this.Database.Execute(sqlInsert, new Object[] { 120, "Name 120", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 130, "Name 130", "Description 130" });
            this.Database.Execute(sqlInsert, new Object[] { 140, "Name 140", "Description 140" });
            this.Database.Execute(sqlInsert, new Object[] { 150, "Name 150", "Description 150" });
            this.Database.Execute(sqlInsert, new Object[] { 160, DBNull.Value, "Description 160" });
            this.Database.Execute(sqlInsert, new Object[] { 170, "Name 170", "Description 170" });
            this.Database.Execute(sqlInsert, new Object[] { 180, "Name 180", "Description 180" });
            this.Database.Execute(sqlInsert, new Object[] { 190, "Name 190", "Description 190" });
            this.Database.Execute(sqlInsert, new Object[] { 200, "Name 200", "Description 200" });

            LazyPageData pageData = new LazyPageData() { PageSize = 2, OrderBy = "Id" };

            // Act
            pageData.PageNum = 1;
            LazyPageResult pageResult1 = this.Database.QueryPage(sql, tableName, pageData, new Object[] { 100, 200 });
            pageData.PageNum = 2;
            LazyPageResult pageResult2 = this.Database.QueryPage(sql, tableName, pageData, new Object[] { 100, 200 });
            pageData.PageNum = 3;
            LazyPageResult pageResult3 = this.Database.QueryPage(sql, tableName, pageData, new Object[] { 100, 200 });
            pageData.PageNum = 4;
            LazyPageResult pageResult4 = this.Database.QueryPage(sql, tableName, pageData, new Object[] { 100, 200 });
            pageData.PageNum = 5;
            LazyPageResult pageResult5 = this.Database.QueryPage(sql, tableName, pageData, new Object[] { 100, 200 });

            // Assert
            Assert.AreEqual(pageResult1.PageNum, 1);
            Assert.AreEqual(pageResult1.PageSize, 2);
            Assert.AreEqual(pageResult1.PageItems, 2);
            Assert.AreEqual(pageResult1.PageCount, 5);
            Assert.AreEqual(pageResult1.CurrentCount, 2);
            Assert.AreEqual(pageResult1.TotalCount, 9);
            Assert.AreEqual(pageResult1.HasNextPage, true);
            Assert.AreEqual(pageResult2.PageNum, 2);
            Assert.AreEqual(pageResult2.PageSize, 2);
            Assert.AreEqual(pageResult2.PageItems, 2);
            Assert.AreEqual(pageResult2.PageCount, 5);
            Assert.AreEqual(pageResult2.CurrentCount, 4);
            Assert.AreEqual(pageResult2.TotalCount, 9);
            Assert.AreEqual(pageResult2.HasNextPage, true);
            Assert.AreEqual(pageResult3.PageNum, 3);
            Assert.AreEqual(pageResult3.PageSize, 2);
            Assert.AreEqual(pageResult3.PageItems, 2);
            Assert.AreEqual(pageResult3.PageCount, 5);
            Assert.AreEqual(pageResult3.CurrentCount, 6);
            Assert.AreEqual(pageResult3.TotalCount, 9);
            Assert.AreEqual(pageResult3.HasNextPage, true);
            Assert.AreEqual(pageResult4.PageNum, 4);
            Assert.AreEqual(pageResult4.PageSize, 2);
            Assert.AreEqual(pageResult4.PageItems, 2);
            Assert.AreEqual(pageResult4.PageCount, 5);
            Assert.AreEqual(pageResult4.CurrentCount, 8);
            Assert.AreEqual(pageResult4.TotalCount, 9);
            Assert.AreEqual(pageResult4.HasNextPage, true);
            Assert.AreEqual(pageResult5.PageNum, 5);
            Assert.AreEqual(pageResult5.PageSize, 2);
            Assert.AreEqual(pageResult5.PageItems, 1);
            Assert.AreEqual(pageResult5.PageCount, 5);
            Assert.AreEqual(pageResult5.CurrentCount, 9);
            Assert.AreEqual(pageResult5.TotalCount, 9);
            Assert.AreEqual(pageResult5.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 100, 200 }); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryPage_DataAdapterFill_LazyDbTypeHigherPage_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 300, 400 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 300, "Name 300", "Description 300" });
            this.Database.Execute(sqlInsert, new Object[] { 310, "Name 310", "Description 310" });
            this.Database.Execute(sqlInsert, new Object[] { 320, "Name 320", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 330, "Name 330", "Description 330" });
            this.Database.Execute(sqlInsert, new Object[] { 340, "Name 340", "Description 340" });
            this.Database.Execute(sqlInsert, new Object[] { 350, "Name 350", "Description 350" });
            this.Database.Execute(sqlInsert, new Object[] { 360, DBNull.Value, "Description 360" });
            this.Database.Execute(sqlInsert, new Object[] { 370, "Name 370", "Description 370" });
            this.Database.Execute(sqlInsert, new Object[] { 380, "Name 380", "Description 380" });
            this.Database.Execute(sqlInsert, new Object[] { 390, "Name 390", "Description 390" });
            this.Database.Execute(sqlInsert, new Object[] { 400, "Name 400", DBNull.Value });

            LazyPageData pageData = new LazyPageData() { PageSize = 50, OrderBy = "Id" };

            // Act
            pageData.PageNum = 1;
            LazyPageResult pageResult1 = this.Database.QueryPage(sql, tableName, pageData, new Object[] { 300, 400 });

            // Assert
            Assert.AreEqual(pageResult1.PageNum, 1);
            Assert.AreEqual(pageResult1.PageSize, 50);
            Assert.AreEqual(pageResult1.PageItems, 8);
            Assert.AreEqual(pageResult1.PageCount, 1);
            Assert.AreEqual(pageResult1.CurrentCount, 8);
            Assert.AreEqual(pageResult1.TotalCount, 8);
            Assert.AreEqual(pageResult1.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 300, 400 }); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryPage_DataAdapterFill_LazyDbTypeOutOfRange_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId";
            try { this.Database.Execute(sqlDelete, new Object[] { 1, 2 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 1, "Name 1", "Description 1" });
            this.Database.Execute(sqlInsert, new Object[] { 2, "Name 2", "Description 2" });

            LazyPageData pageData = new LazyPageData() { PageSize = 2, OrderBy = "Id" };

            // Act
            pageData.PageNum = 2;
            LazyPageResult pageResult = this.Database.QueryPage(sql, tableName, pageData, new Object[] { 1, 2 });

            // Assert
            Assert.AreEqual(pageResult.PageNum, 2);
            Assert.AreEqual(pageResult.PageSize, 2);
            Assert.AreEqual(pageResult.PageItems, 0);
            Assert.AreEqual(pageResult.PageCount, 0);
            Assert.AreEqual(pageResult.CurrentCount, 0);
            Assert.AreEqual(pageResult.TotalCount, 0);
            Assert.AreEqual(pageResult.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 1, 2 }); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }

    [Obsolete("QueryPage with LazyQueryPageResult and LazyQueryPageData was deprecated! Use QueryPage with LazyPageResult and LazyPageData instead!", false)]
    public class TestsLazyDatabaseQueryPageObsolete
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void QueryPage_Validations_LazyDbType_Exception()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String sql = "select * from TestsQueryPage where Id = @Id";

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            LazyQueryPageData queryPageDataPageNumZero = new LazyQueryPageData() { PageNum = 0 };
            LazyQueryPageData queryPageDataPageSizeZero = new LazyQueryPageData() { PageSize = 0 };
            LazyQueryPageData queryPageDataOrderByEmpty = new LazyQueryPageData() { OrderBy = "" };

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "Id", "Name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] parametersLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionTableNameNull = null;
            //Exception exceptionQueryPageDataNull = null;
            Exception exceptionQueryPageDataPageNumZero = null;
            Exception exceptionQueryPageDataPageSizeZero = null;
            Exception exceptionQueryPageDataOrderByEmpty = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.QueryPage(sql, tableName, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.QueryPage(null, tableName, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.QueryPage(sql, null, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            //try { this.Database.QueryPage(sql, tableName, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataNull = exp; }
            try { this.Database.QueryPage(sql, tableName, queryPageDataPageNumZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageNumZero = exp; }
            try { this.Database.QueryPage(sql, tableName, queryPageDataPageSizeZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageSizeZero = exp; }
            try { this.Database.QueryPage(sql, tableName, queryPageDataOrderByEmpty, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataOrderByEmpty = exp; }
            try { this.Database.QueryPage(sql, tableName, queryPageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.QueryPage(sql, tableName, queryPageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.QueryPage(sql, tableName, queryPageData, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.QueryPage(sql, tableName, queryPageData, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.QueryPage(sql, tableName, queryPageData, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.QueryPage(sql, tableName, queryPageData, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);
            //Assert.AreEqual(exceptionQueryPageDataNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionQueryPageDataNull);
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

        public virtual void QueryPage_DataAdapterFill_LazyDbTypeLowerPage_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 100, 200 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 100, "Name 100", "Description 100" });
            this.Database.Execute(sqlInsert, new Object[] { 110, "Name 110", "Description 110" });
            this.Database.Execute(sqlInsert, new Object[] { 120, "Name 120", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 130, "Name 130", "Description 130" });
            this.Database.Execute(sqlInsert, new Object[] { 140, "Name 140", "Description 140" });
            this.Database.Execute(sqlInsert, new Object[] { 150, "Name 150", "Description 150" });
            this.Database.Execute(sqlInsert, new Object[] { 160, DBNull.Value, "Description 160" });
            this.Database.Execute(sqlInsert, new Object[] { 170, "Name 170", "Description 170" });
            this.Database.Execute(sqlInsert, new Object[] { 180, "Name 180", "Description 180" });
            this.Database.Execute(sqlInsert, new Object[] { 190, "Name 190", "Description 190" });
            this.Database.Execute(sqlInsert, new Object[] { 200, "Name 200", "Description 200" });

            LazyQueryPageData queryPageData = new LazyQueryPageData() { PageSize = 2, OrderBy = "Id" };

            // Act
            queryPageData.PageNum = 1;
            LazyQueryPageResult queryPageResult1 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 100, 200 });
            queryPageData.PageNum = 2;
            LazyQueryPageResult queryPageResult2 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 100, 200 });
            queryPageData.PageNum = 3;
            LazyQueryPageResult queryPageResult3 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 100, 200 });
            queryPageData.PageNum = 4;
            LazyQueryPageResult queryPageResult4 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 100, 200 });
            queryPageData.PageNum = 5;
            LazyQueryPageResult queryPageResult5 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 100, 200 });

            // Assert
            Assert.AreEqual(queryPageResult1.PageNum, 1);
            Assert.AreEqual(queryPageResult1.PageSize, 2);
            Assert.AreEqual(queryPageResult1.PageItems, 2);
            Assert.AreEqual(queryPageResult1.PageCount, 5);
            Assert.AreEqual(queryPageResult1.CurrentCount, 2);
            Assert.AreEqual(queryPageResult1.TotalCount, 9);
            Assert.AreEqual(queryPageResult1.HasNextPage, true);
            Assert.AreEqual(queryPageResult2.PageNum, 2);
            Assert.AreEqual(queryPageResult2.PageSize, 2);
            Assert.AreEqual(queryPageResult2.PageItems, 2);
            Assert.AreEqual(queryPageResult2.PageCount, 5);
            Assert.AreEqual(queryPageResult2.CurrentCount, 4);
            Assert.AreEqual(queryPageResult2.TotalCount, 9);
            Assert.AreEqual(queryPageResult2.HasNextPage, true);
            Assert.AreEqual(queryPageResult3.PageNum, 3);
            Assert.AreEqual(queryPageResult3.PageSize, 2);
            Assert.AreEqual(queryPageResult3.PageItems, 2);
            Assert.AreEqual(queryPageResult3.PageCount, 5);
            Assert.AreEqual(queryPageResult3.CurrentCount, 6);
            Assert.AreEqual(queryPageResult3.TotalCount, 9);
            Assert.AreEqual(queryPageResult3.HasNextPage, true);
            Assert.AreEqual(queryPageResult4.PageNum, 4);
            Assert.AreEqual(queryPageResult4.PageSize, 2);
            Assert.AreEqual(queryPageResult4.PageItems, 2);
            Assert.AreEqual(queryPageResult4.PageCount, 5);
            Assert.AreEqual(queryPageResult4.CurrentCount, 8);
            Assert.AreEqual(queryPageResult4.TotalCount, 9);
            Assert.AreEqual(queryPageResult4.HasNextPage, true);
            Assert.AreEqual(queryPageResult5.PageNum, 5);
            Assert.AreEqual(queryPageResult5.PageSize, 2);
            Assert.AreEqual(queryPageResult5.PageItems, 1);
            Assert.AreEqual(queryPageResult5.PageCount, 5);
            Assert.AreEqual(queryPageResult5.CurrentCount, 9);
            Assert.AreEqual(queryPageResult5.TotalCount, 9);
            Assert.AreEqual(queryPageResult5.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 100, 200 }); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryPage_DataAdapterFill_LazyDbTypeHigherPage_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, new Object[] { 300, 400 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 300, "Name 300", "Description 300" });
            this.Database.Execute(sqlInsert, new Object[] { 310, "Name 310", "Description 310" });
            this.Database.Execute(sqlInsert, new Object[] { 320, "Name 320", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 330, "Name 330", "Description 330" });
            this.Database.Execute(sqlInsert, new Object[] { 340, "Name 340", "Description 340" });
            this.Database.Execute(sqlInsert, new Object[] { 350, "Name 350", "Description 350" });
            this.Database.Execute(sqlInsert, new Object[] { 360, DBNull.Value, "Description 360" });
            this.Database.Execute(sqlInsert, new Object[] { 370, "Name 370", "Description 370" });
            this.Database.Execute(sqlInsert, new Object[] { 380, "Name 380", "Description 380" });
            this.Database.Execute(sqlInsert, new Object[] { 390, "Name 390", "Description 390" });
            this.Database.Execute(sqlInsert, new Object[] { 400, "Name 400", DBNull.Value });

            LazyQueryPageData queryPageData = new LazyQueryPageData() { PageSize = 50, OrderBy = "Id" };

            // Act
            queryPageData.PageNum = 1;
            LazyQueryPageResult queryPageResult1 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 300, 400 });

            // Assert
            Assert.AreEqual(queryPageResult1.PageNum, 1);
            Assert.AreEqual(queryPageResult1.PageSize, 50);
            Assert.AreEqual(queryPageResult1.PageItems, 8);
            Assert.AreEqual(queryPageResult1.PageCount, 1);
            Assert.AreEqual(queryPageResult1.CurrentCount, 8);
            Assert.AreEqual(queryPageResult1.TotalCount, 8);
            Assert.AreEqual(queryPageResult1.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 300, 400 }); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryPage_DataAdapterFill_LazyDbTypeOutOfRange_Success()
        {
            // Arrange
            String tableName = "TestsQueryPage";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from " + tableName + " where Id between @LowId and @HighId";
            String sqlInsert = "insert into " + tableName + " (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from TestsQueryPage where Id between @LowId and @HighId";
            try { this.Database.Execute(sqlDelete, new Object[] { 1, 2 }); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 1, "Name 1", "Description 1" });
            this.Database.Execute(sqlInsert, new Object[] { 2, "Name 2", "Description 2" });

            LazyQueryPageData queryPageData = new LazyQueryPageData() { PageSize = 2, OrderBy = "Id" };

            // Act
            queryPageData.PageNum = 2;
            LazyQueryPageResult queryPageResult = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 1, 2 });

            // Assert
            Assert.AreEqual(queryPageResult.PageNum, 2);
            Assert.AreEqual(queryPageResult.PageSize, 2);
            Assert.AreEqual(queryPageResult.PageItems, 0);
            Assert.AreEqual(queryPageResult.PageCount, 0);
            Assert.AreEqual(queryPageResult.CurrentCount, 0);
            Assert.AreEqual(queryPageResult.TotalCount, 0);
            Assert.AreEqual(queryPageResult.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, new Object[] { 1, 2 }); }
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

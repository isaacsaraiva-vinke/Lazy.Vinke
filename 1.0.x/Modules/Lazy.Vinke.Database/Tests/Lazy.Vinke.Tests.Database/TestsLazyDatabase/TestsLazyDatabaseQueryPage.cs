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
            String sql = "select * from QueryPage_Validations_LazyDbType where id = @id";

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            LazyQueryPageData queryPageDataPageNumZero = new LazyQueryPageData() { PageNum = 0 };
            LazyQueryPageData queryPageDataPageSizeZero = new LazyQueryPageData() { PageSize = 0 };
            LazyQueryPageData queryPageDataOrderByEmpty = new LazyQueryPageData() { OrderBy = "" };

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
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

            // Act
            this.Database.CloseConnection();

            try { this.Database.QueryPage(sql, "tableName", queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.QueryPage(null, "tableName", queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.QueryPage(sql, null, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.QueryPage(sql, "tableName", null, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataNull = exp; }
            try { this.Database.QueryPage(sql, "tableName", queryPageDataPageNumZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageNumZero = exp; }
            try { this.Database.QueryPage(sql, "tableName", queryPageDataPageSizeZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageSizeZero = exp; }
            try { this.Database.QueryPage(sql, "tableName", queryPageDataOrderByEmpty, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataOrderByEmpty = exp; }
            try { this.Database.QueryPage(sql, "tableName", queryPageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.QueryPage(sql, "tableName", queryPageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.QueryPage(sql, "tableName", queryPageData, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.QueryPage(sql, "tableName", queryPageData, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.QueryPage(sql, "tableName", queryPageData, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.QueryPage(sql, "tableName", queryPageData, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void QueryPage_DataAdapterFill_LazyDbTypeLowerPage_Success()
        {
            // Arrange
            String tableName = "QueryPage_DataAdapterFill";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from QueryPage_DataAdapterFill where Id between 11 and 21";
            String sqlInsert = "insert into QueryPage_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from QueryPage_DataAdapterFill where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 11, "Name 11", "Description 11" });
            this.Database.Execute(sqlInsert, new Object[] { 12, "Name 12", "Description 12" });
            this.Database.Execute(sqlInsert, new Object[] { 13, "Name 13", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 14, "Name 14", "Description 14" });
            this.Database.Execute(sqlInsert, new Object[] { 15, "Name 15", "Description 15" });
            this.Database.Execute(sqlInsert, new Object[] { 16, "Name 16", "Description 16" });
            this.Database.Execute(sqlInsert, new Object[] { 17, DBNull.Value, "Description 17" });
            this.Database.Execute(sqlInsert, new Object[] { 18, "Name 18", "Description 18" });
            this.Database.Execute(sqlInsert, new Object[] { 19, "Name 19", "Description 19" });
            this.Database.Execute(sqlInsert, new Object[] { 20, "Name 20", "Description 20" });
            this.Database.Execute(sqlInsert, new Object[] { 21, "Name 21", "Description 21" });

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            queryPageData.OrderBy = "Id";
            queryPageData.PageNum = 1;
            queryPageData.PageSize = 2;

            // Act
            LazyQueryPageResult queryPageResult1 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 11, 21 });
            queryPageData.PageNum = 2;
            LazyQueryPageResult queryPageResult2 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 11, 21 });
            queryPageData.PageNum = 3;
            LazyQueryPageResult queryPageResult3 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 11, 21 });
            queryPageData.PageNum = 4;
            LazyQueryPageResult queryPageResult4 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 11, 21 });
            queryPageData.PageNum = 5;
            LazyQueryPageResult queryPageResult5 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 11, 21 });

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
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryPage_DataAdapterFill_LazyDbTypeHigherPage_Success()
        {
            // Arrange
            String tableName = "QueryPage_DataAdapterFill";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from QueryPage_DataAdapterFill where Id between 22 and 32";
            String sqlInsert = "insert into QueryPage_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from QueryPage_DataAdapterFill where Id between @LowId and @HighId and Name is not null and Description is not null";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 22, "Name 22", "Description 22" });
            this.Database.Execute(sqlInsert, new Object[] { 23, "Name 23", "Description 23" });
            this.Database.Execute(sqlInsert, new Object[] { 24, "Name 24", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 25, "Name 25", "Description 25" });
            this.Database.Execute(sqlInsert, new Object[] { 26, "Name 26", "Description 26" });
            this.Database.Execute(sqlInsert, new Object[] { 27, "Name 27", "Description 27" });
            this.Database.Execute(sqlInsert, new Object[] { 28, DBNull.Value, "Description 28" });
            this.Database.Execute(sqlInsert, new Object[] { 29, "Name 29", "Description 29" });
            this.Database.Execute(sqlInsert, new Object[] { 30, "Name 30", "Description 30" });
            this.Database.Execute(sqlInsert, new Object[] { 31, "Name 31", "Description 31" });
            this.Database.Execute(sqlInsert, new Object[] { 32, "Name 32", DBNull.Value });

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            queryPageData.OrderBy = "Id";
            queryPageData.PageNum = 1;
            queryPageData.PageSize = 50;

            // Act
            LazyQueryPageResult queryPageResult1 = this.Database.QueryPage(sql, tableName, queryPageData, new Object[] { 22, 32 });

            // Assert
            Assert.AreEqual(queryPageResult1.PageNum, 1);
            Assert.AreEqual(queryPageResult1.PageSize, 50);
            Assert.AreEqual(queryPageResult1.PageItems, 8);
            Assert.AreEqual(queryPageResult1.PageCount, 1);
            Assert.AreEqual(queryPageResult1.CurrentCount, 8);
            Assert.AreEqual(queryPageResult1.TotalCount, 8);
            Assert.AreEqual(queryPageResult1.HasNextPage, false);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryPage_DataAdapterFill_LazyDbTypeOutOfRange_Success()
        {
            // Arrange
            String tableName = "QueryPage_DataAdapterFill";
            String columnsName = "Id, Name, Description";
            String columnsParameter = "@Id, @Name, @Description";
            String sqlDelete = "delete from QueryPage_DataAdapterFill where Id in (1,2)";
            String sqlInsert = "insert into QueryPage_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sql = "select * from QueryPage_DataAdapterFill where Id in (@Id1,@Id2)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 1, "Name 1", "Description 1" });
            this.Database.Execute(sqlInsert, new Object[] { 2, "Name 2", "Description 2" });

            LazyQueryPageData queryPageData = new LazyQueryPageData();
            queryPageData.OrderBy = "Id";
            queryPageData.PageNum = 2;
            queryPageData.PageSize = 2;

            // Act
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

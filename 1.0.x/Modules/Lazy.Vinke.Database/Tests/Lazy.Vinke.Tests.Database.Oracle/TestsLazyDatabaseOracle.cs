﻿// TestsLazyDatabaseOracle.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Oracle" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.Oracle;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.Oracle
{
    [TestClass]
    public class TestsLazyDatabaseOracle : TestsLazyDatabase
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseOracle(File.ReadAllText(".\\Properties\\Miscellaneous\\ConnectionString.txt"));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public override void Constructor_Parameter_Null_Success()
        {
            base.Constructor_Parameter_Null_Success();
        }

        [TestMethod]
        public override void Constructor_Parameter_Valued_Success()
        {
            base.Constructor_Parameter_Valued_Success();
        }

        [TestMethod]
        public override void OpenConnection_ConnectionString_StringNullOrEmpty_Exception()
        {
            base.OpenConnection_ConnectionString_StringNullOrEmpty_Exception();
        }

        [TestMethod]
        public override void OpenConnection_ConnectionState_AlreadyOpen_Exception()
        {
            base.OpenConnection_ConnectionState_AlreadyOpen_Exception();
        }

        [TestMethod]
        public override void OpenConnection_ConnectionState_Opened_Success()
        {
            base.OpenConnection_ConnectionState_Opened_Success();
        }

        [TestMethod]
        public override void CloseConnection_ConnectionState_AlreadyClose_Exception()
        {
            base.CloseConnection_ConnectionState_AlreadyClose_Exception();
        }

        [TestMethod]
        public override void CloseConnection_ConnectionState_Close_Success()
        {
            base.CloseConnection_ConnectionState_Close_Success();
        }

        [TestMethod]
        public override void BeginTransaction_Connection_NotOpen_Exception()
        {
            base.BeginTransaction_Connection_NotOpen_Exception();
        }

        [TestMethod]
        public override void BeginTransaction_Transaction_AlreadyOpen_Exception()
        {
            base.BeginTransaction_Transaction_AlreadyOpen_Exception();
        }

        [TestMethod]
        public override void BeginTransaction_Transaction_NotOpen_Success()
        {
            base.BeginTransaction_Transaction_NotOpen_Success();
        }

        [TestMethod]
        public override void CommitTransaction_Commit_Single_Success()
        {
            base.CommitTransaction_Commit_Single_Success();
        }

        [TestMethod]
        public override void RollbackTransaction_Rollback_Single_Success()
        {
            base.RollbackTransaction_Rollback_Single_Success();
        }

        [TestMethod]
        public override void CreateNew_Instance_ConnectionOwnerNull_Success()
        {
            base.CreateNew_Instance_ConnectionOwnerNull_Success();
        }

        [TestMethod]
        public override void CreateNew_Instance_ConnectionOwnerValued_Success()
        {
            base.CreateNew_Instance_ConnectionOwnerValued_Success();
        }

        [TestMethod]
        public void Execute_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "insert into Execute_Validations_DbmsDbType (id, name) values (@id, @name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            OracleDbType[] dbTypesLess = new OracleDbType[] { OracleDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            databaseOracle.CloseConnection();

            try { databaseOracle.Execute(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseOracle.OpenConnection();

            try { databaseOracle.Execute(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseOracle.Execute(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseOracle.Execute(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseOracle.Execute(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseOracle.Execute(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseOracle.Execute(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseOracle.Execute(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public void Execute_ExecuteNonQuery_WithValuesDbmsType_Success()
        {
            // Arrange
            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String sqlCreate = "create table NonQuery_WithValuesDbmsType ( id int, name varchar(256) )";
            String sqlInsert = "insert into NonQuery_WithValuesDbmsType (id, name) values (@id, @name)";
            String sqlDrop = "drop table NonQuery_WithValuesDbmsType";
            try { this.Database.Execute(sqlDrop, null); }
            catch { /* Just to be sure that the table will not exists */ }

            // Act
            this.Database.Execute(sqlCreate, null);
            Int32 affectedRecord = ((LazyDatabaseOracle)this.Database).Execute(sqlInsert, values, dbTypes);
            this.Database.Execute(sqlDrop, null);

            // Assert
            Assert.AreEqual(affectedRecord, 1);
        }

        [TestMethod]
        public override void Execute_Validations_LazyDbType_Exception()
        {
            base.Execute_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void Execute_ExecuteNonQuery_WithoutValues_Success()
        {
            base.Execute_ExecuteNonQuery_WithoutValues_Success();
        }

        [TestMethod]
        public override void Execute_ExecuteNonQuery_WithValues_Success()
        {
            base.Execute_ExecuteNonQuery_WithValues_Success();
        }

        [TestMethod]
        public void ExecuteProcedure_ExecuteNonQuery_DbmsDbType_Success()
        {
            // Arrange
            String procedureName = "ExecuteProcedure_ExecuteNonQuery";
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "Id", "Name", "Description" };
            String sqlSelect = "select count(*) from QueryProc_ExecuteNonQuery where Id in (5,6,7,8)";
            String sqlDelete = "delete from QueryProc_ExecuteNonQuery where Id in (5,6,7,8)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            databaseOracle.ExecuteProcedure(procedureName, new Object[] { 5, "OracleLazy", "Description Oracle Lazy" }, dbTypes, parameters);
            databaseOracle.ExecuteProcedure(procedureName, new Object[] { 6, "OracleVinke", "Description Oracle Vinke" }, dbTypes, parameters);
            databaseOracle.ExecuteProcedure(procedureName, new Object[] { 7, "OracleTests", "Description Oracle Tests" }, dbTypes, parameters);
            databaseOracle.ExecuteProcedure(procedureName, new Object[] { 8, "OracleDatabase", "Description Oracle Database" }, dbTypes, parameters);

            Int32 count = Convert.ToInt32(databaseOracle.QueryValue(sqlSelect, null));

            // Assert
            Assert.AreEqual(count, 4);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void ExecuteProcedure_ExecuteNonQuery_LazyDbType_Success()
        {
            base.ExecuteProcedure_ExecuteNonQuery_LazyDbType_Success();
        }

        [TestMethod]
        public void QueryValue_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "insert into QueryValue_Validations_DbmsDbType (id, name) values (@id, @name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            OracleDbType[] dbTypesLess = new OracleDbType[] { OracleDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            databaseOracle.CloseConnection();

            try { databaseOracle.QueryValue(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseOracle.OpenConnection();

            try { databaseOracle.QueryValue(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseOracle.QueryValue(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseOracle.QueryValue(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseOracle.QueryValue(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseOracle.QueryValue(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseOracle.QueryValue(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseOracle.QueryValue(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public void QueryValue_DataAdapterFill_ColumnDecimalDbms_Success()
        {
            // Arrange
            Decimal minValue = Decimal.MinValue;
            Decimal maxValue = Decimal.MaxValue;
            String testCode = "QueryValue_DataAdapterFill_ColumnDecimalDbms";
            String columnsName = "TestCode, ColumnDecimalN, ColumnDecimalP, ColumnDecimalNull";
            String columnsParameter = "@TestCode, @ColumnDecimalN, @ColumnDecimalP, @ColumnDecimalNull";
            Object[] values = new Object[] { testCode, minValue, maxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            OracleDbType[] dbKeyTypes = new OracleDbType[] { OracleDbType.Varchar2 };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;
            databaseOracle.Execute(sqlInsert, values);

            // Act
            Object columnDecimalN = databaseOracle.QueryValue(String.Format(sqlselect, "ColumnDecimalN"), tableKeyArray, dbKeyTypes);
            Object columnDecimalP = databaseOracle.QueryValue(String.Format(sqlselect, "ColumnDecimalP"), tableKeyArray, dbKeyTypes);
            Object columnDecimalNull = databaseOracle.QueryValue(String.Format(sqlselect, "ColumnDecimalNull"), tableKeyArray, dbKeyTypes);

            // Assert
            Assert.AreEqual(Convert.ToDecimal(columnDecimalN), minValue);
            Assert.AreEqual(Convert.ToDecimal(columnDecimalP), maxValue);
            Assert.AreEqual(columnDecimalNull, DBNull.Value);

            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryValue_Validations_LazyDbType_Exception()
        {
            base.QueryValue_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnChar_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnChar_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnVarChar_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnVarChar_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnVarText_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnVarText_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnByte_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnByte_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnInt16_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnInt16_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnInt32_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnInt32_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnInt64_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnInt64_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnUByte_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnUByte_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnFloat_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnFloat_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnDouble_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnDouble_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnDecimal_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnDecimal_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnDateTime_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnDateTime_Success();
        }

        [TestMethod]
        public override void QueryValue_DataAdapterFill_ColumnVarUByte_Success()
        {
            base.QueryValue_DataAdapterFill_ColumnVarUByte_Success();
        }

        [TestMethod]
        public void QueryFind_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "insert into QueryFind_Validations_DbmsDbType (id, name) values (@id, @name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            OracleDbType[] dbTypesLess = new OracleDbType[] { OracleDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            databaseOracle.CloseConnection();

            try { databaseOracle.QueryFind(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseOracle.OpenConnection();

            try { databaseOracle.QueryFind(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseOracle.QueryFind(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseOracle.QueryFind(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseOracle.QueryFind(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseOracle.QueryFind(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseOracle.QueryFind(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseOracle.QueryFind(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public virtual void QueryFind_DataAdapterFill_DbmsDbType_Success()
        {
            // Arrange
            String columnsName = "Id, Code, Description, Amount";
            String columnsParameter = "@Id, @Code, @Description, @Amount";
            String sqlDelete = "delete from QueryFind_DataAdapterFill where Id in (5,6,7,8)";
            String sqlInsert = "insert into QueryFind_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            databaseOracle.Execute(sqlInsert, new Object[] { 5, "500", "Test 500", 502.12 });
            databaseOracle.Execute(sqlInsert, new Object[] { 6, "600", "Test 600", 603.13 });
            databaseOracle.Execute(sqlInsert, new Object[] { 7, "700", null, 704.14 });
            databaseOracle.Execute(sqlInsert, new Object[] { 8, "800", "Test 700", 805.15 });

            // Act
            Boolean test1Result = databaseOracle.QueryFind("select 1 from QueryFind_DataAdapterFill where Id = @Id", new Object[] { 5 }, new OracleDbType[] { OracleDbType.Int32 }, new String[] { "Id" });
            Boolean test2Result = databaseOracle.QueryFind("select 1 from QueryFind_DataAdapterFill where Code = @Code", new Object[] { "900" }, new OracleDbType[] { OracleDbType.Varchar2 }, new String[] { "Code" });
            Boolean test3Result = databaseOracle.QueryFind("select 1 from QueryFind_DataAdapterFill where Description is null", null);
            Boolean test4Result = databaseOracle.QueryFind("select 1 from QueryFind_DataAdapterFill where Amount > @Amount", new Object[] { 805.15 }, new OracleDbType[] { OracleDbType.Decimal }, new String[] { "Amount" });

            // Assert
            Assert.IsTrue(test1Result);
            Assert.IsFalse(test2Result);
            Assert.IsTrue(test3Result);
            Assert.IsFalse(test4Result);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryFind_Validations_LazyDbType_Exception()
        {
            base.QueryFind_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void QueryFind_DataAdapterFill_LazyDbType_Success()
        {
            base.QueryFind_DataAdapterFill_LazyDbType_Success();
        }

        [TestMethod]
        public void QueryRecord_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "insert into QueryRecord_Validations_DbmsDbType (id, name) values (@id, @name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            OracleDbType[] dbTypesLess = new OracleDbType[] { OracleDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            databaseOracle.CloseConnection();

            try { databaseOracle.QueryRecord(sql, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseOracle.OpenConnection();

            try { databaseOracle.QueryRecord(null, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseOracle.QueryRecord(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseOracle.QueryRecord(sql, "tableName", values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseOracle.QueryRecord(sql, "tableName", null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseOracle.QueryRecord(sql, "tableName", null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseOracle.QueryRecord(sql, "tableName", valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseOracle.QueryRecord(sql, "tableName", values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseOracle.QueryRecord(sql, "tableName", values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public virtual void QueryRecord_DataAdapterFill_DbmsDbType_Success()
        {
            // Arrange
            String tableName = "QueryRecord_DataAdapterFill";
            String columnsName = "Id, Name, Birthdate";
            String columnsParameter = "@Id, @Name, @Birthdate";
            String sqlDelete = "delete from QueryRecord_DataAdapterFill where Id in (10,20,30,40)";
            String sqlInsert = "insert into QueryRecord_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            databaseOracle.Execute(sqlInsert, new Object[] { 10, "OracleLazy", new DateTime(1986, 9, 14) });
            databaseOracle.Execute(sqlInsert, new Object[] { 20, "OracleVinke", DBNull.Value });
            databaseOracle.Execute(sqlInsert, new Object[] { 30, "OracleTests", new DateTime(1988, 7, 24) });
            databaseOracle.Execute(sqlInsert, new Object[] { 40, DBNull.Value, new DateTime(1989, 6, 29) });

            // Act
            DataRow dataRecord1 = databaseOracle.QueryRecord("select * from QueryRecord_DataAdapterFill where Id = @Id", tableName, new Object[] { 10 }, new OracleDbType[] { OracleDbType.Int16 }, new String[] { "Id" });
            DataRow dataRecord2 = databaseOracle.QueryRecord("select Name, Birthdate from QueryRecord_DataAdapterFill where Name = @Name", String.Empty, new Object[] { "OracleVinke" }, new OracleDbType[] { OracleDbType.Varchar2 }, new String[] { "Name" });
            DataRow dataRecord3 = databaseOracle.QueryRecord("select Birthdate from QueryRecord_DataAdapterFill where Id = @Id", tableName, new Object[] { 50 }, new OracleDbType[] { OracleDbType.Int16 }, new String[] { "Id" });
            DataRow dataRecord4 = databaseOracle.QueryRecord("select Name, Birthdate from QueryRecord_DataAdapterFill where Name is null and Id = @Id", String.Empty, new Object[] { 40 }, new OracleDbType[] { OracleDbType.Int16 }, new String[] { "Id" });

            // Assert
            Assert.AreEqual(dataRecord1.Table.TableName, tableName);
            Assert.AreEqual(Convert.ToInt16(dataRecord1["Id"]), (Int16)10);
            Assert.AreEqual(Convert.ToString(dataRecord1["Name"]), "OracleLazy");
            Assert.AreEqual(Convert.ToDateTime(dataRecord1["Birthdate"]), new DateTime(1986, 9, 14));
            Assert.AreEqual(dataRecord2.Table.TableName, String.Empty);
            Assert.AreEqual(Convert.ToString(dataRecord2["Name"]), "OracleVinke");
            Assert.AreEqual(dataRecord2["Birthdate"], DBNull.Value);
            Assert.IsNull(dataRecord3);
            Assert.AreEqual(dataRecord4.Table.TableName, String.Empty);
            Assert.AreEqual(dataRecord4["Name"], DBNull.Value);
            Assert.AreEqual(Convert.ToDateTime(dataRecord4["Birthdate"]), new DateTime(1989, 6, 29));

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryRecord_Validations_LazyDbType_Exception()
        {
            base.QueryRecord_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void QueryRecord_DataAdapterFill_LazyDbType_Success()
        {
            base.QueryRecord_DataAdapterFill_LazyDbType_Success();
        }

        [TestMethod]
        public void QueryTable_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "insert into QueryTable_Validations_DbmsDbType (id, name) values (@id, @name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            OracleDbType[] dbTypesLess = new OracleDbType[] { OracleDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            databaseOracle.CloseConnection();

            try { databaseOracle.QueryTable(sql, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseOracle.OpenConnection();

            try { databaseOracle.QueryTable(null, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseOracle.QueryTable(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseOracle.QueryTable(sql, "tableName", valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseOracle.QueryTable(sql, "tableName", values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionSqlNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNull);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public virtual void QueryTable_DataAdapterFill_DbmsDbType_Success()
        {
            // Arrange
            String tableName = "QueryTable_DataAdapterFill";
            String columnsName = "Code, Elements, Active";
            String columnsParameter = "@Code, @Elements, @Active";
            String sqlDelete = "delete from QueryTable_DataAdapterFill where Code in ('Array3','Array4')";
            String sqlInsert = "insert into QueryTable_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            databaseOracle.Execute(sqlInsert, new Object[] { "Array3", new Byte[] { 56, 64 }, '0' });
            databaseOracle.Execute(sqlInsert, new Object[] { "Array4", new Byte[] { 72, 86 }, '1' });

            Object[] values = new Object[] { "Array3", "Array4" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Varchar2, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "Code1", "Code2" };

            // Act
            DataTable dataTable = databaseOracle.QueryTable("select * from QueryTable_DataAdapterFill where (Code = @Code1 or Code = @Code2)", tableName, values, dbTypes, parameters);

            // Assert
            Assert.AreEqual(dataTable.Rows.Count, 2);
            Assert.AreEqual(dataTable.TableName, tableName);
            Assert.AreEqual(Convert.ToString(dataTable.Rows[0]["Code"]), "Array3");
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["Elements"])[0], (Byte)56);
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["Elements"])[1], (Byte)64);
            Assert.AreEqual(Convert.ToChar(dataTable.Rows[0]["Active"]), '0');
            Assert.AreEqual(Convert.ToString(dataTable.Rows[1]["Code"]), "Array4");
            Assert.AreEqual(((Byte[])dataTable.Rows[1]["Elements"])[0], (Byte)72);
            Assert.AreEqual(((Byte[])dataTable.Rows[1]["Elements"])[1], (Byte)86);
            Assert.AreEqual(Convert.ToChar(dataTable.Rows[1]["Active"]), '1');

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryTable_Validations_LazyDbType_Exception()
        {
            base.QueryTable_Validations_LazyDbType_Exception();
        }

        [TestMethod]
        public override void QueryTable_DataAdapterFill_LazyDbType_Success()
        {
            base.QueryTable_DataAdapterFill_LazyDbType_Success();
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
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            OracleDbType[] dbTypesLess = new OracleDbType[] { OracleDbType.Int32 };
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

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            databaseOracle.CloseConnection();

            try { databaseOracle.QueryPage(sql, "tableName", queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseOracle.OpenConnection();

            try { databaseOracle.QueryPage(null, "tableName", queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseOracle.QueryPage(sql, null, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseOracle.QueryPage(sql, "tableName", null, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataNull = exp; }
            try { databaseOracle.QueryPage(sql, "tableName", queryPageDataPageNumZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageNumZero = exp; }
            try { databaseOracle.QueryPage(sql, "tableName", queryPageDataPageSizeZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageSizeZero = exp; }
            try { databaseOracle.QueryPage(sql, "tableName", queryPageDataOrderByEmpty, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataOrderByEmpty = exp; }
            try { databaseOracle.QueryPage(sql, "tableName", queryPageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseOracle.QueryPage(sql, "tableName", queryPageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseOracle.QueryPage(sql, "tableName", queryPageData, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseOracle.QueryPage(sql, "tableName", queryPageData, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseOracle.QueryPage(sql, "tableName", queryPageData, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseOracle.QueryPage(sql, "tableName", queryPageData, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            LazyQueryPageResult queryPageResult1 = databaseOracle.QueryPage(sql, tableName, queryPageData, new Object[] { 33, 43 }, new OracleDbType[] { OracleDbType.Int32, OracleDbType.Int32 }, new String[] { "LowId", "HighId" });
            queryPageData.PageNum = 2;
            LazyQueryPageResult queryPageResult2 = databaseOracle.QueryPage(sql, tableName, queryPageData, new Object[] { 33, 43 }, new OracleDbType[] { OracleDbType.Int32, OracleDbType.Int32 }, new String[] { "LowId", "HighId" });
            queryPageData.PageNum = 3;
            LazyQueryPageResult queryPageResult3 = databaseOracle.QueryPage(sql, tableName, queryPageData, new Object[] { 33, 43 }, new OracleDbType[] { OracleDbType.Int32, OracleDbType.Int32 }, new String[] { "LowId", "HighId" });

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

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            // Act
            LazyQueryPageResult queryPageResult1 = databaseOracle.QueryPage(sql, tableName, queryPageData, new Object[] { 44, 54 }, new OracleDbType[] { OracleDbType.Int32, OracleDbType.Int32 }, new String[] { "LowId", "HighId" });

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
        public void QueryLike_DataAdapterFill_DbmsDbType_Success()
        {
            // Arrange
            String tableName = "QueryLike_DataAdapterFill";
            String columnsName = "TestId, Content, Notes";
            String columnsParameter = "@TestId, @Content, @Notes";
            String sqlDelete = "delete from QueryLike_DataAdapterFill where TestId in (20,21,22)";
            String sqlInsert = "insert into QueryLike_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseOracle databaseOracle = (LazyDatabaseOracle)this.Database;

            databaseOracle.Execute(sqlInsert, new Object[] { 20, "Content 20", "Notes 20 Notes 20 Notes 20 Notes 20" });
            databaseOracle.Execute(sqlInsert, new Object[] { 21, "21 Content", "21 Notes 21 Notes 21 Notes 21 Notes 21 Notes" });
            databaseOracle.Execute(sqlInsert, new Object[] { 22, "Content 22 Content", "Notes 22 22 Notes 22 22 Notes 22 22 Notes" });

            // Act
            DataRow dataRowTest1 = databaseOracle.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as varchar2(2048)) like @TestId", tableName, new Object[] { "%20" });
            DataRow dataRowTest2 = databaseOracle.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as varchar2(2048)) like @TestId", tableName, new Object[] { "21%" });
            DataRow dataRowTest3 = databaseOracle.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as varchar2(2048)) like @TestId", tableName, new Object[] { "%22%" });

            // Assert
            Assert.IsNotNull(dataRowTest1);
            Assert.IsNotNull(dataRowTest2);
            Assert.IsNotNull(dataRowTest3);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void QueryLike_DataAdapterFill_LazyDbType_Success()
        {
            base.QueryLike_DataAdapterFill_LazyDbType_Success();
        }

        [TestMethod]
        public override void ConvertSystemTypeToLazyDbType_LazyDbTypeArray_Single_Success()
        {
            base.ConvertSystemTypeToLazyDbType_LazyDbTypeArray_Single_Success();
        }

        [TestMethod]
        public void ConvertLazyDbTypeToDbmsType_SqlDbType_Single_Success()
        {
            // Arrange
            MethodInfo methodInfo = this.Database.GetType().GetMethod("ConvertLazyDbTypeToDbmsType", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

            // Act
            OracleDbType dbTypeNull = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DBNull });
            OracleDbType dbTypeChar = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Char });
            OracleDbType dbTypeVarChar = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarChar });
            OracleDbType dbTypeVarText = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarText });
            OracleDbType dbTypeByte = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Byte });
            OracleDbType dbTypeInt16 = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int16 });
            OracleDbType dbTypeInt32 = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int32 });
            OracleDbType dbTypeInt64 = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int64 });
            OracleDbType dbTypeUByte = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.UByte });
            OracleDbType dbTypeFloat = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Float });
            OracleDbType dbTypeDouble = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Double });
            OracleDbType dbTypeDecimal = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Decimal });
            OracleDbType dbTypeDateTime = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DateTime });
            OracleDbType dbTypeVarUByte = (OracleDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarUByte });

            // Assert
            Assert.AreEqual(dbTypeNull, OracleDbType.Varchar2);
            Assert.AreEqual(dbTypeChar, OracleDbType.Char);
            Assert.AreEqual(dbTypeVarChar, OracleDbType.Varchar2);
            Assert.AreEqual(dbTypeVarText, OracleDbType.Clob);
            Assert.AreEqual(dbTypeByte, OracleDbType.Int16);
            Assert.AreEqual(dbTypeInt16, OracleDbType.Int16);
            Assert.AreEqual(dbTypeInt32, OracleDbType.Int32);
            Assert.AreEqual(dbTypeInt64, OracleDbType.Int64);
            Assert.AreEqual(dbTypeUByte, OracleDbType.Int16);
            Assert.AreEqual(dbTypeFloat, OracleDbType.Single);
            Assert.AreEqual(dbTypeDouble, OracleDbType.Double);
            Assert.AreEqual(dbTypeDecimal, OracleDbType.Decimal);
            Assert.AreEqual(dbTypeDateTime, OracleDbType.Date);
            Assert.AreEqual(dbTypeVarUByte, OracleDbType.Blob);
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}
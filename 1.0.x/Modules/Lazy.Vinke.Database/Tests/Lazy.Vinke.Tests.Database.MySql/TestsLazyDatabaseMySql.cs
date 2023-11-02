// TestsLazyDatabaseMySql.cs
//
// This file is integrated part of "Lazy Vinke Tests Database MySql" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using MySql.Data.Types;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.MySql;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.MySql
{
    [TestClass]
    public class TestsLazyDatabaseMySql : TestsLazyDatabase
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseMySql(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
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
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            MySqlDbType[] dbTypesLess = new MySqlDbType[] { MySqlDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.CloseConnection();

            try { databaseMySql.Execute(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseMySql.OpenConnection();

            try { databaseMySql.Execute(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseMySql.Execute(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseMySql.Execute(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseMySql.Execute(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseMySql.Execute(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseMySql.Execute(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseMySql.Execute(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarString };
            String sqlCreate = "create table NonQuery_WithValuesDbmsType ( id int, name varchar(256) )";
            String sqlInsert = "insert into NonQuery_WithValuesDbmsType (id, name) values (@id, @name)";
            String sqlDrop = "drop table NonQuery_WithValuesDbmsType";
            try { this.Database.Execute(sqlDrop, null); }
            catch { /* Just to be sure that the table will not exists */ }

            // Act
            this.Database.Execute(sqlCreate, null);
            Int32 affectedRecord = ((LazyDatabaseMySql)this.Database).Execute(sqlInsert, values, dbTypes);
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
        public void ExecuteProcedure_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String procName = "ExecuteProcedure_Validations_DbmsDbType";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            MySqlDbType[] dbTypesLess = new MySqlDbType[] { MySqlDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionProcNameNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.CloseConnection();

            try { databaseMySql.ExecuteProcedure(procName, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseMySql.OpenConnection();

            try { databaseMySql.ExecuteProcedure(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionProcNameNull = exp; }
            try { databaseMySql.ExecuteProcedure(procName, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseMySql.ExecuteProcedure(procName, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseMySql.ExecuteProcedure(procName, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseMySql.ExecuteProcedure(procName, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseMySql.ExecuteProcedure(procName, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseMySql.ExecuteProcedure(procName, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionProcNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionStatementNullOrEmpty);
            Assert.AreEqual(exceptionValuesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
            Assert.AreEqual(exceptionDbParametersLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesParametersNotMatch);
        }

        [TestMethod]
        public void ExecuteProcedure_ExecuteNonQuery_DbmsDbType_Success()
        {
            // Arrange
            String procedureName = "ExecuteProcedure_ExecuteNonQuery";
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarString, MySqlDbType.VarString };
            String[] parameters = new String[] { "Id", "Name", "Description" };
            String sqlSelect = "select count(*) from QueryProc_ExecuteNonQuery where Id in (5,6,7,8)";
            String sqlDelete = "delete from QueryProc_ExecuteNonQuery where Id in (5,6,7,8)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.ExecuteProcedure(procedureName, new Object[] { 5, "MySqlLazy", "Description MySql Lazy" }, dbTypes, parameters);
            databaseMySql.ExecuteProcedure(procedureName, new Object[] { 6, "MySqlVinke", "Description MySql Vinke" }, dbTypes, parameters);
            databaseMySql.ExecuteProcedure(procedureName, new Object[] { 7, "MySqlTests", "Description MySql Tests" }, dbTypes, parameters);
            databaseMySql.ExecuteProcedure(procedureName, new Object[] { 8, "MySqlDatabase", "Description MySql Database" }, dbTypes, parameters);

            Int32 count = Convert.ToInt32(databaseMySql.QueryValue(sqlSelect, null));

            // Assert
            Assert.AreEqual(count, 4);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        [TestMethod]
        public override void ExecuteProcedure_Validations_LazyDbType_Exception()
        {
            base.ExecuteProcedure_Validations_LazyDbType_Exception();
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
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            MySqlDbType[] dbTypesLess = new MySqlDbType[] { MySqlDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.CloseConnection();

            try { databaseMySql.QueryValue(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseMySql.OpenConnection();

            try { databaseMySql.QueryValue(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseMySql.QueryValue(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseMySql.QueryValue(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseMySql.QueryValue(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseMySql.QueryValue(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseMySql.QueryValue(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseMySql.QueryValue(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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
            MySqlDbType[] dbKeyTypes = new MySqlDbType[] { MySqlDbType.VarChar };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;
            databaseMySql.Execute(sqlInsert, values);

            // Act
            Object columnDecimalN = databaseMySql.QueryValue(String.Format(sqlselect, "ColumnDecimalN"), tableKeyArray, dbKeyTypes);
            Object columnDecimalP = databaseMySql.QueryValue(String.Format(sqlselect, "ColumnDecimalP"), tableKeyArray, dbKeyTypes);
            Object columnDecimalNull = databaseMySql.QueryValue(String.Format(sqlselect, "ColumnDecimalNull"), tableKeyArray, dbKeyTypes);

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
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            MySqlDbType[] dbTypesLess = new MySqlDbType[] { MySqlDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.CloseConnection();

            try { databaseMySql.QueryFind(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseMySql.OpenConnection();

            try { databaseMySql.QueryFind(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseMySql.QueryFind(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseMySql.QueryFind(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseMySql.QueryFind(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseMySql.QueryFind(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseMySql.QueryFind(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseMySql.QueryFind(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            databaseMySql.Execute(sqlInsert, new Object[] { 5, "500", "Test 500", 502.12 });
            databaseMySql.Execute(sqlInsert, new Object[] { 6, "600", "Test 600", 603.13 });
            databaseMySql.Execute(sqlInsert, new Object[] { 7, "700", null, 704.14 });
            databaseMySql.Execute(sqlInsert, new Object[] { 8, "800", "Test 700", 805.15 });

            // Act
            Boolean test1Result = databaseMySql.QueryFind("select 1 from QueryFind_DataAdapterFill where Id = @Id", new Object[] { 5 }, new MySqlDbType[] { MySqlDbType.Int32 }, new String[] { "Id" });
            Boolean test2Result = databaseMySql.QueryFind("select 1 from QueryFind_DataAdapterFill where Code = @Code", new Object[] { "900" }, new MySqlDbType[] { MySqlDbType.VarString }, new String[] { "Code" });
            Boolean test3Result = databaseMySql.QueryFind("select 1 from QueryFind_DataAdapterFill where Description is null", null);
            Boolean test4Result = databaseMySql.QueryFind("select 1 from QueryFind_DataAdapterFill where Amount > @Amount", new Object[] { 805.15 }, new MySqlDbType[] { MySqlDbType.Decimal }, new String[] { "Amount" });

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
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            MySqlDbType[] dbTypesLess = new MySqlDbType[] { MySqlDbType.Int32 };
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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.CloseConnection();

            try { databaseMySql.QueryRecord(sql, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseMySql.OpenConnection();

            try { databaseMySql.QueryRecord(null, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseMySql.QueryRecord(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseMySql.QueryRecord(sql, "tableName", values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseMySql.QueryRecord(sql, "tableName", null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseMySql.QueryRecord(sql, "tableName", null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseMySql.QueryRecord(sql, "tableName", valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseMySql.QueryRecord(sql, "tableName", values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseMySql.QueryRecord(sql, "tableName", values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            databaseMySql.Execute(sqlInsert, new Object[] { 10, "MySqlLazy", new DateTime(1986, 9, 14) });
            databaseMySql.Execute(sqlInsert, new Object[] { 20, "MySqlVinke", DBNull.Value });
            databaseMySql.Execute(sqlInsert, new Object[] { 30, "MySqlTests", new DateTime(1988, 7, 24) });
            databaseMySql.Execute(sqlInsert, new Object[] { 40, DBNull.Value, new DateTime(1989, 6, 29) });

            // Act
            DataRow dataRecord1 = databaseMySql.QueryRecord("select * from QueryRecord_DataAdapterFill where Id = @Id", tableName, new Object[] { 10 }, new MySqlDbType[] { MySqlDbType.Int16 }, new String[] { "Id" });
            DataRow dataRecord2 = databaseMySql.QueryRecord("select Name, Birthdate from QueryRecord_DataAdapterFill where Name = @Name", String.Empty, new Object[] { "MySqlVinke" }, new MySqlDbType[] { MySqlDbType.VarString }, new String[] { "Name" });
            DataRow dataRecord3 = databaseMySql.QueryRecord("select Birthdate from QueryRecord_DataAdapterFill where Id = @Id", tableName, new Object[] { 50 }, new MySqlDbType[] { MySqlDbType.Int16 }, new String[] { "Id" });
            DataRow dataRecord4 = databaseMySql.QueryRecord("select Name, Birthdate from QueryRecord_DataAdapterFill where Name is null and Id = @Id", String.Empty, new Object[] { 40 }, new MySqlDbType[] { MySqlDbType.Int16 }, new String[] { "Id" });

            // Assert
            Assert.AreEqual(dataRecord1.Table.TableName, tableName);
            Assert.AreEqual(Convert.ToInt16(dataRecord1["Id"]), (Int16)10);
            Assert.AreEqual(Convert.ToString(dataRecord1["Name"]), "MySqlLazy");
            Assert.AreEqual(Convert.ToDateTime(dataRecord1["Birthdate"]), new DateTime(1986, 9, 14));
            Assert.AreEqual(dataRecord2.Table.TableName, String.Empty);
            Assert.AreEqual(Convert.ToString(dataRecord2["Name"]), "MySqlVinke");
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
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            MySqlDbType[] dbTypesLess = new MySqlDbType[] { MySqlDbType.Int32 };
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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.CloseConnection();

            try { databaseMySql.QueryTable(sql, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseMySql.OpenConnection();

            try { databaseMySql.QueryTable(null, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseMySql.QueryTable(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseMySql.QueryTable(sql, "tableName", values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseMySql.QueryTable(sql, "tableName", null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseMySql.QueryTable(sql, "tableName", null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseMySql.QueryTable(sql, "tableName", valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseMySql.QueryTable(sql, "tableName", values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseMySql.QueryTable(sql, "tableName", values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            databaseMySql.Execute(sqlInsert, new Object[] { "Array3", new Byte[] { 56, 64 }, '0' });
            databaseMySql.Execute(sqlInsert, new Object[] { "Array4", new Byte[] { 72, 86 }, '1' });

            Object[] values = new Object[] { "Array3", "Array4" };
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.VarString, MySqlDbType.VarString };
            String[] parameters = new String[] { "Code1", "Code2" };

            // Act
            DataTable dataTable = databaseMySql.QueryTable("select * from QueryTable_DataAdapterFill where (Code = @Code1 or Code = @Code2)", tableName, values, dbTypes, parameters);

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
            MySqlDbType[] dbTypes = new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            MySqlDbType[] dbTypesLess = new MySqlDbType[] { MySqlDbType.Int32 };
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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            databaseMySql.CloseConnection();

            try { databaseMySql.QueryPage(sql, "tableName", queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            databaseMySql.OpenConnection();

            try { databaseMySql.QueryPage(null, "tableName", queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { databaseMySql.QueryPage(sql, null, queryPageData, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databaseMySql.QueryPage(sql, "tableName", null, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataNull = exp; }
            try { databaseMySql.QueryPage(sql, "tableName", queryPageDataPageNumZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageNumZero = exp; }
            try { databaseMySql.QueryPage(sql, "tableName", queryPageDataPageSizeZero, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataPageSizeZero = exp; }
            try { databaseMySql.QueryPage(sql, "tableName", queryPageDataOrderByEmpty, values, dbTypes, parameters); } catch (Exception exp) { exceptionQueryPageDataOrderByEmpty = exp; }
            try { databaseMySql.QueryPage(sql, "tableName", queryPageData, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { databaseMySql.QueryPage(sql, "tableName", queryPageData, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { databaseMySql.QueryPage(sql, "tableName", queryPageData, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { databaseMySql.QueryPage(sql, "tableName", queryPageData, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databaseMySql.QueryPage(sql, "tableName", queryPageData, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databaseMySql.QueryPage(sql, "tableName", queryPageData, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            LazyQueryPageResult queryPageResult1 = databaseMySql.QueryPage(sql, tableName, queryPageData, new Object[] { 33, 43 }, new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.Int32 }, new String[] { "LowId", "HighId" });
            queryPageData.PageNum = 2;
            LazyQueryPageResult queryPageResult2 = databaseMySql.QueryPage(sql, tableName, queryPageData, new Object[] { 33, 43 }, new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.Int32 }, new String[] { "LowId", "HighId" });
            queryPageData.PageNum = 3;
            LazyQueryPageResult queryPageResult3 = databaseMySql.QueryPage(sql, tableName, queryPageData, new Object[] { 33, 43 }, new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.Int32 }, new String[] { "LowId", "HighId" });

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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            LazyQueryPageResult queryPageResult1 = databaseMySql.QueryPage(sql, tableName, queryPageData, new Object[] { 44, 54 }, new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.Int32 }, new String[] { "LowId", "HighId" });

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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            // Act
            LazyQueryPageResult queryPageResult = databaseMySql.QueryPage(sql, tableName, queryPageData, new Object[] { 3, 4 }, new MySqlDbType[] { MySqlDbType.Int32, MySqlDbType.Int32 }, new String[] { "Id3", "Id4" });

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

            LazyDatabaseMySql databaseMySql = (LazyDatabaseMySql)this.Database;

            databaseMySql.Execute(sqlInsert, new Object[] { 20, "Content 20", "Notes 20 Notes 20 Notes 20 Notes 20" });
            databaseMySql.Execute(sqlInsert, new Object[] { 21, "21 Content", "21 Notes 21 Notes 21 Notes 21 Notes 21 Notes" });
            databaseMySql.Execute(sqlInsert, new Object[] { 22, "Content 22 Content", "Notes 22 22 Notes 22 22 Notes 22 22 Notes" });

            // Act
            DataRow dataRowTest1 = databaseMySql.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as char(2048)) like @TestId", tableName, new Object[] { "%20" });
            DataRow dataRowTest2 = databaseMySql.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as char(2048)) like @TestId", tableName, new Object[] { "21%" });
            DataRow dataRowTest3 = databaseMySql.QueryRecord("select * from QueryLike_DataAdapterFill where cast(TestId as char(2048)) like @TestId", tableName, new Object[] { "%22%" });

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
            MySqlDbType dbTypeNull = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DBNull });
            MySqlDbType dbTypeChar = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Char });
            MySqlDbType dbTypeVarChar = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarChar });
            MySqlDbType dbTypeVarText = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarText });
            MySqlDbType dbTypeByte = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Byte });
            MySqlDbType dbTypeInt16 = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int16 });
            MySqlDbType dbTypeInt32 = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int32 });
            MySqlDbType dbTypeInt64 = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Int64 });
            MySqlDbType dbTypeUByte = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.UByte });
            MySqlDbType dbTypeFloat = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Float });
            MySqlDbType dbTypeDouble = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Double });
            MySqlDbType dbTypeDecimal = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.Decimal });
            MySqlDbType dbTypeDateTime = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.DateTime });
            MySqlDbType dbTypeVarUByte = (MySqlDbType)methodInfo.Invoke(this.Database, new Object[] { LazyDbType.VarUByte });

            // Assert
            Assert.AreEqual(dbTypeNull, MySqlDbType.VarString);
            Assert.AreEqual(dbTypeChar, MySqlDbType.VarChar);
            Assert.AreEqual(dbTypeVarChar, MySqlDbType.VarString);
            Assert.AreEqual(dbTypeVarText, MySqlDbType.Text);
            Assert.AreEqual(dbTypeByte, MySqlDbType.Byte);
            Assert.AreEqual(dbTypeInt16, MySqlDbType.Int16);
            Assert.AreEqual(dbTypeInt32, MySqlDbType.Int32);
            Assert.AreEqual(dbTypeInt64, MySqlDbType.Int64);
            Assert.AreEqual(dbTypeUByte, MySqlDbType.UByte);
            Assert.AreEqual(dbTypeFloat, MySqlDbType.Float);
            Assert.AreEqual(dbTypeDouble, MySqlDbType.Double);
            Assert.AreEqual(dbTypeDecimal, MySqlDbType.Decimal);
            Assert.AreEqual(dbTypeDateTime, MySqlDbType.DateTime);
            Assert.AreEqual(dbTypeVarUByte, MySqlDbType.Blob);
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

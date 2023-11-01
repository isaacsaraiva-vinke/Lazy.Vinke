// TestsLazyDatabase.cs
//
// This file is integrated part of "Lazy Vinke Tests Database" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 21

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;

namespace Lazy.Vinke.Tests.Database
{
    public class TestsLazyDatabase
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
        }

        public virtual void Constructor_Parameter_Null_Success()
        {
            // Arrange
            this.Database.CloseConnection();

            // Act
            this.Database = (LazyDatabase)Activator.CreateInstance(this.Database.GetType(), new Object[] { null, null });

            // Assert
            Assert.AreEqual(this.Database.ConnectionString, null);
            Assert.AreEqual(this.Database.ConnectionOwner, null);
        }

        public virtual void Constructor_Parameter_Valued_Success()
        {
            // Arrange
            this.Database.CloseConnection();

            // Act
            this.Database = (LazyDatabase)Activator.CreateInstance(this.Database.GetType(), new Object[] { "SomeConnectionString", "SomeConnectionOwner" });

            // Assert
            Assert.AreEqual(this.Database.ConnectionString, "SomeConnectionString");
            Assert.AreEqual(this.Database.ConnectionOwner, "SomeConnectionOwner");
        }

        public virtual void OpenConnection_ConnectionString_StringNullOrEmpty_Exception()
        {
            // Arrange
            this.Database.CloseConnection();

            // Act
            Exception exception = null;
            try { this.Database = (LazyDatabase)Activator.CreateInstance(this.Database.GetType()); this.Database.OpenConnection(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionStringNullOrEmpty);
        }

        public virtual void OpenConnection_ConnectionState_AlreadyOpen_Exception()
        {
            // Arrange

            // Act
            Exception exception = null;
            try { this.Database.OpenConnection(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionAlreadyOpen);
        }

        public virtual void OpenConnection_ConnectionState_Opened_Success()
        {
            // Arrange

            // Act

            // Assert
            Assert.AreEqual(this.Database.ConnectionState, ConnectionState.Open);
        }

        public virtual void CloseConnection_ConnectionState_AlreadyClose_Exception()
        {
            // Arrange

            // Act
            Exception exception = null;
            try { this.Database.CloseConnection(); this.Database.CloseConnection(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionAlreadyClose);
        }

        public virtual void CloseConnection_ConnectionState_Close_Success()
        {
            // Arrange

            // Act
            this.Database.CloseConnection();

            // Assert
            Assert.AreEqual(this.Database.ConnectionState, ConnectionState.Closed);
        }

        public virtual void BeginTransaction_Connection_NotOpen_Exception()
        {
            // Arrange
            this.Database.CloseConnection();

            // Act
            Exception exception = null;
            try { this.Database = (LazyDatabase)Activator.CreateInstance(this.Database.GetType()); this.Database.BeginTransaction(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
        }

        public virtual void BeginTransaction_Transaction_AlreadyOpen_Exception()
        {
            // Arrange

            // Act
            Exception exception = null;
            try { this.Database.BeginTransaction(); this.Database.BeginTransaction(); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.AreEqual(exception.Message, LazyResourcesDatabase.LazyDatabaseExceptionTransactionAlreadyOpen);
        }

        public virtual void BeginTransaction_Transaction_NotOpen_Success()
        {
            // Arrange

            // Act
            this.Database.BeginTransaction();

            // Assert
            Assert.IsTrue(this.Database.InTransaction);
        }

        public virtual void CommitTransaction_Commit_Single_Success()
        {
            // Arrange
            String sqlInsert = "insert into Transaction_CommitRollback values (@Id, @Content)";
            String sqlSelect = "select count(*) from Transaction_CommitRollback where Id in (1,2,3,4)";
            String sqlDelete = "delete from Transaction_CommitRollback where Id in (1,2,3,4)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            // Act
            this.Database.BeginTransaction();
            this.Database.Execute(sqlInsert, new Object[] { 1, "Lazy" });
            this.Database.Execute(sqlInsert, new Object[] { 2, "Vinke" });
            this.Database.Execute(sqlInsert, new Object[] { 3, "Tests" });
            this.Database.Execute(sqlInsert, new Object[] { 4, "Database" });
            this.Database.CommitTransaction();

            Int32 count = Convert.ToInt32(this.Database.QueryValue(sqlSelect, null));

            // Assert
            Assert.AreEqual(count, 4);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void RollbackTransaction_Rollback_Single_Success()
        {
            // Arrange
            String sqlInsert = "insert into Transaction_CommitRollback values (@Id, @Content)";
            String sqlSelect = "select count(*) from Transaction_CommitRollback where Id in (5,6,7,8)";
            String sqlDelete = "delete from Transaction_CommitRollback where Id in (5,6,7,8)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            // Act
            this.Database.BeginTransaction();
            this.Database.Execute(sqlInsert, new Object[] { 5, "Lazy" });
            this.Database.Execute(sqlInsert, new Object[] { 6, "Vinke" });
            this.Database.Execute(sqlInsert, new Object[] { 7, "Tests" });
            this.Database.Execute(sqlInsert, new Object[] { 8, "Database" });
            this.Database.RollbackTransaction();

            Int32 count = Convert.ToInt32(this.Database.QueryValue(sqlSelect, null));

            // Assert
            Assert.AreEqual(count, 0);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void CreateNew_Instance_ConnectionOwnerNull_Success()
        {
            // Arrange
            this.Database.CloseConnection();
            this.Database = this.Database.CreateNew("SomeConnectionOwner");

            // Act
            LazyDatabase newDatabase = this.Database.CreateNew();

            // Assert
            Assert.AreEqual(newDatabase.GetType(), this.Database.GetType());
            Assert.AreEqual(newDatabase.ConnectionOwner, null);
        }

        public virtual void CreateNew_Instance_ConnectionOwnerValued_Success()
        {
            // Arrange

            // Act
            LazyDatabase newDatabase = this.Database.CreateNew("SomeNewConnectionOwner");

            // Assert
            Assert.AreEqual(newDatabase.GetType(), this.Database.GetType());
            Assert.AreNotEqual(newDatabase.ConnectionOwner, this.Database.ConnectionOwner);
            Assert.AreEqual(newDatabase.ConnectionOwner, "SomeNewConnectionOwner");
        }

        public virtual void Execute_Validations_LazyDbType_Exception()
        {
            // Arrange
            String sql = "insert into Execute_Validations_LazyDbType (id, name) values (@id, @name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.Execute(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.Execute(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.Execute(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.Execute(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.Execute(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.Execute(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.Execute(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.Execute(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void Execute_ExecuteNonQuery_WithoutValues_Success()
        {
            // Arrange
            String sqlCreate = "create table NonQuery_WithoutValues ( id int, name varchar(256) )";
            String sqlDrop = "drop table NonQuery_WithoutValues";
            String sqlInsert = "insert into NonQuery_WithoutValues (id, name) values (1, 'Lazy.Vinke.Database')";
            try { this.Database.Execute(sqlDrop, null); }
            catch { /* Just to be sure that the table will not exists */ }

            // Act
            this.Database.Execute(sqlCreate, null);
            this.Database.Execute(sqlDrop, null);

            Exception exception = null;
            try { this.Database.Execute(sqlInsert, null); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.IsNotNull(exception);
        }

        public virtual void Execute_ExecuteNonQuery_WithValues_Success()
        {
            // Arrange
            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            String sqlCreate = "create table NonQuery_WithValues ( id int, name varchar(256) )";
            String sqlInsert = "insert into NonQuery_WithValues (id, name) values (@id, @name)";
            String sqlDrop = "drop table NonQuery_WithValues";
            try { this.Database.Execute(sqlDrop, null); }
            catch { /* Just to be sure that the table will not exists */ }

            // Act
            this.Database.Execute(sqlCreate, null);
            Int32 affectedRecord = this.Database.Execute(sqlInsert, values);
            this.Database.Execute(sqlDrop, null);

            // Assert
            Assert.AreEqual(affectedRecord, 1);
        }

        public virtual void ExecuteProcedure_ExecuteNonQuery_LazyDbType_Success()
        {
            // Arrange
            String procedureName = "ExecuteProcedure_ExecuteNonQuery";
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar, LazyDbType.VarChar };
            String[] parameters = new String[] { "Id", "Name", "Description" };
            String sqlSelect = "select count(*) from QueryProc_ExecuteNonQuery where Id in (1,2,3,4)";
            String sqlDelete = "delete from QueryProc_ExecuteNonQuery where Id in (1,2,3,4)";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            // Act
            this.Database.ExecuteProcedure(procedureName, new Object[] { 1, "Lazy", "Description Lazy" }, dbTypes, parameters);
            this.Database.ExecuteProcedure(procedureName, new Object[] { 2, "Vinke", "Description Vinke" }, dbTypes, parameters);
            this.Database.ExecuteProcedure(procedureName, new Object[] { 3, "Tests", "Description Tests" }, dbTypes, parameters);
            this.Database.ExecuteProcedure(procedureName, new Object[] { 4, "Database", "Description Database" }, dbTypes, parameters);

            Int32 count = Convert.ToInt32(this.Database.QueryValue(sqlSelect, null));

            // Assert
            Assert.AreEqual(count, 4);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_Validations_LazyDbType_Exception()
        {
            // Arrange
            String sql = "insert into QueryValue_Validations_LazyDbType (id, name) values (@id, @name)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.QueryValue(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.QueryValue(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.QueryValue(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.QueryValue(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.QueryValue(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.QueryValue(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.QueryValue(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.QueryValue(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void QueryValue_DataAdapterFill_ColumnChar_Success()
        {
            // Arrange
            String testCode = "QueryValue_DataAdapterFill_ColumnChar";
            String columnsName = "TestCode, ColumnCharD, ColumnCharB, ColumnCharNull";
            String columnsParameter = "@TestCode, @ColumnCharD, @ColumnCharB, @ColumnCharNull";
            Object[] values = new Object[] { testCode, 'D', 'B', DBNull.Value };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object columnCharD = this.Database.QueryValue(String.Format(sqlselect, "ColumnCharD"), tableKeyArray);
            Object columnCharB = this.Database.QueryValue(String.Format(sqlselect, "ColumnCharB"), tableKeyArray);
            Object columnCharNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnCharNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToChar(columnCharD), 'D');
            Assert.AreEqual(Convert.ToChar(columnCharB), 'B');
            Assert.AreEqual(columnCharNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnVarChar_Success()
        {
            // Arrange
            String testCode = "QueryValue_DataAdapterFill_ColumnVarChar";
            String columnsName = "TestCode, ColumnVarChar1, ColumnVarChar2, ColumnVarCharNull";
            String columnsParameter = "@TestCode, @ColumnVarChar1, @ColumnVarChar2, @ColumnVarCharNull";
            Object[] values = new Object[] { testCode, "Lazy.Vinke", "Tests.Database", null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object columnVarChar1 = this.Database.QueryValue(String.Format(sqlselect, "ColumnVarChar1"), tableKeyArray);
            Object columnVarChar2 = this.Database.QueryValue(String.Format(sqlselect, "ColumnVarChar2"), tableKeyArray);
            Object columnVarCharNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnVarCharNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToString(columnVarChar1), "Lazy.Vinke");
            Assert.AreEqual(Convert.ToString(columnVarChar2), "Tests.Database");
            Assert.AreEqual(columnVarCharNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnVarText_Success()
        {
            // Arrange
            String text1 = "Lazy.Vinke.Tests.Database";
            String text2 = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "../../../", this.GetType().Name) + ".cs");
            String testCode = "QueryValue_DataAdapterFill_ColumnVarText";
            String columnsName = "TestCode, ColumnVarText1, ColumnVarText2, ColumnVarTextNull";
            String columnsParameter = "@TestCode, @ColumnVarText1, @ColumnVarText2, @ColumnVarTextNull";
            Object[] values = new Object[] { testCode, text1, text2, DBNull.Value };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.VarChar, LazyDbType.VarText, LazyDbType.VarText, LazyDbType.VarText };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values, dbTypes);

            // Act
            Object ColumnVarText1 = this.Database.QueryValue(String.Format(sqlselect, "ColumnVarText1"), tableKeyArray);
            Object ColumnVarText2 = this.Database.QueryValue(String.Format(sqlselect, "ColumnVarText2"), tableKeyArray);
            Object columnVarTextNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnVarTextNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToString(ColumnVarText1), text1);
            Assert.AreEqual(Convert.ToString(ColumnVarText2), text2);
            Assert.AreEqual(columnVarTextNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnByte_Success()
        {
            // Arrange
            String testCode = "QueryValue_DataAdapterFill_ColumnByte";
            String columnsName = "TestCode, ColumnByteN, ColumnByteP, ColumnByteNull";
            String columnsParameter = "@TestCode, @ColumnByteN, @ColumnByteP, @ColumnByteNull";
            Object[] values = new Object[] { testCode, SByte.MinValue, SByte.MaxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object columnByteN = this.Database.QueryValue(String.Format(sqlselect, "ColumnByteN"), tableKeyArray);
            Object columnByteP = this.Database.QueryValue(String.Format(sqlselect, "ColumnByteP"), tableKeyArray);
            Object columnByteNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnByteNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToSByte(columnByteN), SByte.MinValue);
            Assert.AreEqual(Convert.ToSByte(columnByteP), SByte.MaxValue);
            Assert.AreEqual(columnByteNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnInt16_Success()
        {
            // Arrange
            String testCode = "QueryValue_DataAdapterFill_ColumnInt16";
            String columnsName = "TestCode, ColumnInt16N, ColumnInt16P, ColumnInt16Null";
            String columnsParameter = "@TestCode, @ColumnInt16N, @ColumnInt16P, @ColumnInt16Null";
            Object[] values = new Object[] { testCode, Int16.MinValue, Int16.MaxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object columnInt16N = this.Database.QueryValue(String.Format(sqlselect, "ColumnInt16N"), tableKeyArray);
            Object columnInt16P = this.Database.QueryValue(String.Format(sqlselect, "ColumnInt16P"), tableKeyArray);
            Object columnInt16Null = this.Database.QueryValue(String.Format(sqlselect, "ColumnInt16Null"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToInt16(columnInt16N), Int16.MinValue);
            Assert.AreEqual(Convert.ToInt16(columnInt16P), Int16.MaxValue);
            Assert.AreEqual(columnInt16Null, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnInt32_Success()
        {
            // Arrange
            String testCode = "QueryValue_DataAdapterFill_ColumnInt32";
            String columnsName = "TestCode, ColumnInt32N, ColumnInt32P, ColumnInt32Null";
            String columnsParameter = "@TestCode, @ColumnInt32N, @ColumnInt32P, @ColumnInt32Null";
            Object[] values = new Object[] { testCode, Int32.MinValue, Int32.MaxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object columnInt32N = this.Database.QueryValue(String.Format(sqlselect, "ColumnInt32N"), tableKeyArray);
            Object columnInt32P = this.Database.QueryValue(String.Format(sqlselect, "ColumnInt32P"), tableKeyArray);
            Object columnInt32Null = this.Database.QueryValue(String.Format(sqlselect, "ColumnInt32Null"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToInt32(columnInt32N), Int32.MinValue);
            Assert.AreEqual(Convert.ToInt32(columnInt32P), Int32.MaxValue);
            Assert.AreEqual(columnInt32Null, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnInt64_Success()
        {
            // Arrange
            String testCode = "QueryValue_DataAdapterFill_ColumnInt64";
            String columnsName = "TestCode, ColumnInt64N, ColumnInt64P, ColumnInt64Null";
            String columnsParameter = "@TestCode, @ColumnInt64N, @ColumnInt64P, @ColumnInt64Null";
            Object[] values = new Object[] { testCode, Int64.MinValue, Int64.MaxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object columnInt64N = this.Database.QueryValue(String.Format(sqlselect, "ColumnInt64N"), tableKeyArray);
            Object columnInt64P = this.Database.QueryValue(String.Format(sqlselect, "ColumnInt64P"), tableKeyArray);
            Object columnInt64Null = this.Database.QueryValue(String.Format(sqlselect, "ColumnInt64Null"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToInt64(columnInt64N), Int64.MinValue);
            Assert.AreEqual(Convert.ToInt64(columnInt64P), Int64.MaxValue);
            Assert.AreEqual(columnInt64Null, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnUByte_Success()
        {
            // Arrange
            String testCode = "QueryValue_DataAdapterFill_ColumnUByte";
            String columnsName = "TestCode, ColumnUByte1, ColumnUByte2, ColumnUByteNull";
            String columnsParameter = "@TestCode, @ColumnUByte1, @ColumnUByte2, @ColumnUByteNull";
            Object[] values = new Object[] { testCode, Byte.MinValue, Byte.MaxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object ColumnUByte1 = this.Database.QueryValue(String.Format(sqlselect, "ColumnUByte1"), tableKeyArray);
            Object ColumnUByte2 = this.Database.QueryValue(String.Format(sqlselect, "ColumnUByte2"), tableKeyArray);
            Object columnUByteNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnUByteNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToByte(ColumnUByte1), Byte.MinValue);
            Assert.AreEqual(Convert.ToByte(ColumnUByte2), Byte.MaxValue);
            Assert.AreEqual(columnUByteNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnFloat_Success()
        {
            // Arrange
            Single minValue = Convert.ToSingle(-3.40282E+38);
            Single maxValue = Convert.ToSingle(3.40282E+38);
            String testCode = "QueryValue_DataAdapterFill_ColumnFloat";
            String columnsName = "TestCode, ColumnFloatN, ColumnFloatP, ColumnFloatNull";
            String columnsParameter = "@TestCode, @ColumnFloatN, @ColumnFloatP, @ColumnFloatNull";
            Object[] values = new Object[] { testCode, minValue, maxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object columnFloatN = this.Database.QueryValue(String.Format(sqlselect, "ColumnFloatN"), tableKeyArray);
            Object columnFloatP = this.Database.QueryValue(String.Format(sqlselect, "ColumnFloatP"), tableKeyArray);
            Object columnFloatNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnFloatNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToSingle(columnFloatN), minValue);
            Assert.AreEqual(Convert.ToSingle(columnFloatP), maxValue);
            Assert.AreEqual(columnFloatNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnDouble_Success()
        {
            // Arrange
            Double minValue = -1.1d; // Double.MinValue;
            Double maxValue = 1.1d; // Double.MaxValue;
            String testCode = "QueryValue_DataAdapterFill_ColumnDouble";
            String columnsName = "TestCode, ColumnDoubleN, ColumnDoubleP, ColumnDoubleNull";
            String columnsParameter = "@TestCode, @ColumnDoubleN, @ColumnDoubleP, @ColumnDoubleNull";
            Object[] values = new Object[] { testCode, minValue, maxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object columnDoubleN = this.Database.QueryValue(String.Format(sqlselect, "ColumnDoubleN"), tableKeyArray);
            Object columnDoubleP = this.Database.QueryValue(String.Format(sqlselect, "ColumnDoubleP"), tableKeyArray);
            Object columnDoubleNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnDoubleNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToDouble(columnDoubleN), minValue);
            Assert.AreEqual(Convert.ToDouble(columnDoubleP), maxValue);
            Assert.AreEqual(columnDoubleNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnDecimal_Success()
        {
            // Arrange
            Decimal minValue = Decimal.MinValue;
            Decimal maxValue = Decimal.MaxValue;
            String testCode = "QueryValue_DataAdapterFill_ColumnDecimal";
            String columnsName = "TestCode, ColumnDecimalN, ColumnDecimalP, ColumnDecimalNull";
            String columnsParameter = "@TestCode, @ColumnDecimalN, @ColumnDecimalP, @ColumnDecimalNull";
            Object[] values = new Object[] { testCode, minValue, maxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object columnDecimalN = this.Database.QueryValue(String.Format(sqlselect, "ColumnDecimalN"), tableKeyArray);
            Object columnDecimalP = this.Database.QueryValue(String.Format(sqlselect, "ColumnDecimalP"), tableKeyArray);
            Object columnDecimalNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnDecimalNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToDecimal(columnDecimalN), minValue);
            Assert.AreEqual(Convert.ToDecimal(columnDecimalP), maxValue);
            Assert.AreEqual(columnDecimalNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnDateTime_Success()
        {
            // Arrange
            DateTime minValue = new DateTime(1753, 01, 01, 12, 00, 00);
            DateTime maxValue = new DateTime(9999, 12, 31, 23, 59, 59);
            String testCode = "QueryValue_DataAdapterFill_ColumnDateTime";
            String columnsName = "TestCode, ColumnDateTime1, ColumnDateTime2, ColumnDateTimeNull";
            String columnsParameter = "@TestCode, @ColumnDateTime1, @ColumnDateTime2, @ColumnDateTimeNull";
            Object[] values = new Object[] { testCode, minValue, maxValue, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object ColumnDateTime1 = this.Database.QueryValue(String.Format(sqlselect, "ColumnDateTime1"), tableKeyArray);
            Object ColumnDateTime2 = this.Database.QueryValue(String.Format(sqlselect, "ColumnDateTime2"), tableKeyArray);
            Object columnDateTimeNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnDateTimeNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(Convert.ToDateTime(ColumnDateTime1), minValue);
            Assert.AreEqual(Convert.ToDateTime(ColumnDateTime2), maxValue);
            Assert.AreEqual(columnDateTimeNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryValue_DataAdapterFill_ColumnVarUByte_Success()
        {
            // Arrange
            Byte[] values1 = new Byte[] { 8, 12, 16, 24, 32, 48, 56, 64 };
            Byte[] values2 = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, Assembly.GetExecutingAssembly().GetName().Name) + ".dll");
            String testCode = "QueryValue_DataAdapterFill_ColumnVarUByte";
            String columnsName = "TestCode, ColumnVarUByte1, ColumnVarUByte2, ColumnVarUByteNull";
            String columnsParameter = "@TestCode, @ColumnVarUByte1, @ColumnVarUByte2, @ColumnVarUByteNull";
            Object[] values = new Object[] { testCode, values1, values2, null };
            String sqlDelete = "delete from QueryValue_DataAdapterFill where TestCode = @TestCode";
            String sqlInsert = "insert into QueryValue_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from QueryValue_DataAdapterFill where TestCode = @TestCode";
            Object[] tableKeyArray = new Object[] { testCode };
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, values);

            // Act
            Object ColumnVarUByte1 = this.Database.QueryValue(String.Format(sqlselect, "ColumnVarUByte1"), tableKeyArray);
            Object ColumnVarUByte2 = this.Database.QueryValue(String.Format(sqlselect, "ColumnVarUByte2"), tableKeyArray);
            Object columnVarUByteNull = this.Database.QueryValue(String.Format(sqlselect, "ColumnVarUByteNull"), tableKeyArray);

            // Assert
            Assert.AreEqual(((Byte[])ColumnVarUByte1)[0], 8);
            Assert.AreEqual(((Byte[])ColumnVarUByte1)[1], 12);
            Assert.AreEqual(((Byte[])ColumnVarUByte1)[2], 16);
            Assert.AreEqual(((Byte[])ColumnVarUByte1)[3], 24);
            Assert.AreEqual(((Byte[])ColumnVarUByte1)[4], 32);
            Assert.AreEqual(((Byte[])ColumnVarUByte1)[5], 48);
            Assert.AreEqual(((Byte[])ColumnVarUByte1)[6], 56);
            Assert.AreEqual(((Byte[])ColumnVarUByte1)[7], 64);

            Byte[] queryDataArray = (Byte[])ColumnVarUByte2;

            Assert.AreEqual(values2.Length, queryDataArray.Length);
            for (int index = 0; index < values2.Length; index++)
                Assert.AreEqual(values2[index], queryDataArray[index]);

            Assert.AreEqual(columnVarUByteNull, DBNull.Value);

            // Clean
            try { this.Database.Execute(sqlDelete, tableKeyArray); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryFind_Validations_LazyDbType_Exception()
        {
            // Arrange
            String sql = "select * from QueryFind_Validations_LazyDbType where id = @id";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
            String[] parametersLess = new String[] { "id" };

            Exception exceptionConnection = null;
            Exception exceptionSqlNull = null;
            Exception exceptionValuesButOthers = null;
            Exception exceptionDbTypesButOthers = null;
            Exception exceptionDbParametersButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbParametersLessButOthers = null;

            // Act
            this.Database.CloseConnection();

            try { this.Database.QueryFind(sql, values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.QueryFind(null, values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.QueryFind(sql, values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.QueryFind(sql, null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.QueryFind(sql, null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.QueryFind(sql, valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.QueryFind(sql, values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.QueryFind(sql, values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void QueryFind_DataAdapterFill_LazyDbType_Success()
        {
            // Arrange
            String columnsName = "Id, Code, Description, Amount";
            String columnsParameter = "@Id, @Code, @Description, @Amount";
            String sqlDelete = "delete from QueryFind_DataAdapterFill where Id in (1,2,3,4)";
            String sqlInsert = "insert into QueryFind_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 1, "001", "Test 001", 102.12 });
            this.Database.Execute(sqlInsert, new Object[] { 2, "002", "Test 002", 103.13 });
            this.Database.Execute(sqlInsert, new Object[] { 3, "003", DBNull.Value, 104.14 });
            this.Database.Execute(sqlInsert, new Object[] { 4, "004", "Test 004", 105.15 });

            // Act
            Boolean test1Result = this.Database.QueryFind("select 1 from QueryFind_DataAdapterFill where Id = @Id", new Object[] { 1 });
            Boolean test2Result = this.Database.QueryFind("select 1 from QueryFind_DataAdapterFill where Code = @Code", new Object[] { "005" });
            Boolean test3Result = this.Database.QueryFind("select 1 from QueryFind_DataAdapterFill where Description is null", null);
            Boolean test4Result = this.Database.QueryFind("select 1 from QueryFind_DataAdapterFill where Amount > @Amount", new Object[] { 105.15 });

            // Assert
            Assert.IsTrue(test1Result);
            Assert.IsFalse(test2Result);
            Assert.IsTrue(test3Result);
            Assert.IsFalse(test4Result);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryRecord_Validations_LazyDbType_Exception()
        {
            // Arrange
            String sql = "select * from QueryRecord_Validations_LazyDbType where id = @id";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
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

            // Act
            this.Database.CloseConnection();

            try { this.Database.QueryRecord(sql, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.QueryRecord(null, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.QueryRecord(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.QueryRecord(sql, "tableName", values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.QueryRecord(sql, "tableName", null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.QueryRecord(sql, "tableName", null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.QueryRecord(sql, "tableName", valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.QueryRecord(sql, "tableName", values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.QueryRecord(sql, "tableName", values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void QueryRecord_DataAdapterFill_LazyDbType_Success()
        {
            // Arrange
            String tableName = "QueryRecord_DataAdapterFill";
            String columnsName = "Id, Name, Birthdate";
            String columnsParameter = "@Id, @Name, @Birthdate";
            String sqlDelete = "delete from QueryRecord_DataAdapterFill where Id in (1,2,3,4)";
            String sqlInsert = "insert into QueryRecord_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 1, "Lazy", new DateTime(1986, 9, 14) });
            this.Database.Execute(sqlInsert, new Object[] { 2, "Vinke", DBNull.Value });
            this.Database.Execute(sqlInsert, new Object[] { 3, "Tests", new DateTime(1988, 7, 24) });
            this.Database.Execute(sqlInsert, new Object[] { 4, DBNull.Value, new DateTime(1989, 6, 29) });

            // Act
            DataRow dataRecord1 = this.Database.QueryRecord("select * from QueryRecord_DataAdapterFill where Id = @Id", tableName, new Object[] { 1 });
            DataRow dataRecord2 = this.Database.QueryRecord("select Name, Birthdate from QueryRecord_DataAdapterFill where Name = @Name", String.Empty, new Object[] { "Vinke" });
            DataRow dataRecord3 = this.Database.QueryRecord("select Birthdate from QueryRecord_DataAdapterFill where Id = @Id", tableName, new Object[] { 5 });
            DataRow dataRecord4 = this.Database.QueryRecord("select Name, Birthdate from QueryRecord_DataAdapterFill where Name is null", String.Empty, null);

            // Assert
            Assert.AreEqual(dataRecord1.Table.TableName, tableName);
            Assert.AreEqual(Convert.ToInt16(dataRecord1["Id"]), (Int16)1);
            Assert.AreEqual(Convert.ToString(dataRecord1["Name"]), "Lazy");
            Assert.AreEqual(Convert.ToDateTime(dataRecord1["Birthdate"]), new DateTime(1986, 9, 14));
            Assert.AreEqual(dataRecord2.Table.TableName, String.Empty);
            Assert.AreEqual(Convert.ToString(dataRecord2["Name"]), "Vinke");
            Assert.AreEqual(dataRecord2["Birthdate"], DBNull.Value);
            Assert.IsNull(dataRecord3);
            Assert.AreEqual(dataRecord4.Table.TableName, String.Empty);
            Assert.AreEqual(dataRecord4["Name"], DBNull.Value);
            Assert.AreEqual(Convert.ToDateTime(dataRecord4["Birthdate"]), new DateTime(1989, 6, 29));

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void QueryTable_Validations_LazyDbType_Exception()
        {
            // Arrange
            String sql = "select * from QueryTable_Validations_LazyDbType where id = @id";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            LazyDbType[] dbTypes = new LazyDbType[] { LazyDbType.Int32, LazyDbType.VarChar };
            String[] parameters = new String[] { "id", "name" };

            Object[] valuesLess = new Object[] { 1 };
            LazyDbType[] dbTypesLess = new LazyDbType[] { LazyDbType.Int32 };
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

            // Act
            this.Database.CloseConnection();

            try { this.Database.QueryTable(sql, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionConnection = exp; }

            this.Database.OpenConnection();

            try { this.Database.QueryTable(null, "tableName", values, dbTypes, parameters); } catch (Exception exp) { exceptionSqlNull = exp; }
            try { this.Database.QueryTable(sql, null, values, dbTypes, parameters); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { this.Database.QueryTable(sql, "tableName", values, null, null); } catch (Exception exp) { exceptionValuesButOthers = exp; }
            try { this.Database.QueryTable(sql, "tableName", null, dbTypes, null); } catch (Exception exp) { exceptionDbTypesButOthers = exp; }
            try { this.Database.QueryTable(sql, "tableName", null, null, parameters); } catch (Exception exp) { exceptionDbParametersButOthers = exp; }

            try { this.Database.QueryTable(sql, "tableName", valuesLess, dbTypes, parameters); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { this.Database.QueryTable(sql, "tableName", values, dbTypesLess, parameters); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { this.Database.QueryTable(sql, "tableName", values, dbTypes, parametersLess); } catch (Exception exp) { exceptionDbParametersLessButOthers = exp; }

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

        public virtual void QueryTable_DataAdapterFill_LazyDbType_Success()
        {
            // Arrange
            String tableName = "QueryTable_DataAdapterFill";
            String columnsName = "Code, Elements, Active";
            String columnsParameter = "@Code, @Elements, @Active";
            String sqlDelete = "delete from QueryTable_DataAdapterFill where Code in ('Array1','Array2')";
            String sqlInsert = "insert into QueryTable_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { "Array1", new Byte[] { 16, 24 }, '1' });
            this.Database.Execute(sqlInsert, new Object[] { "Array2", new Byte[] { 32, 48 }, '0' });

            // Act
            DataTable dataTable = this.Database.QueryTable("select * from QueryTable_DataAdapterFill", tableName, null);

            // Assert
            Assert.AreEqual(dataTable.Rows.Count, 2);
            Assert.AreEqual(Convert.ToString(dataTable.Rows[0]["Code"]), "Array1");
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["Elements"])[0], (Byte)16);
            Assert.AreEqual(((Byte[])dataTable.Rows[0]["Elements"])[1], (Byte)24);
            Assert.AreEqual(Convert.ToChar(dataTable.Rows[0]["Active"]), '1');
            Assert.AreEqual(Convert.ToString(dataTable.Rows[1]["Code"]), "Array2");
            Assert.AreEqual(((Byte[])dataTable.Rows[1]["Elements"])[0], (Byte)32);
            Assert.AreEqual(((Byte[])dataTable.Rows[1]["Elements"])[1], (Byte)48);
            Assert.AreEqual(Convert.ToChar(dataTable.Rows[1]["Active"]), '0');

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
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

        public virtual void QueryLike_DataAdapterFill_LazyDbType_Success()
        {
            // Arrange
            String tableName = "QueryLike_DataAdapterFill";
            String columnsName = "TestId, Content, Notes";
            String columnsParameter = "@TestId, @Content, @Notes";
            String sqlDelete = "delete from QueryLike_DataAdapterFill where TestId in (10,11,12)";
            String sqlInsert = "insert into QueryLike_DataAdapterFill (" + columnsName + ") values (" + columnsParameter + ")";
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }

            this.Database.Execute(sqlInsert, new Object[] { 10, "Content 10", "Notes 10 Notes 10 Notes 10 Notes 10" });
            this.Database.Execute(sqlInsert, new Object[] { 11, "11 Content", "11 Notes 11 Notes 11 Notes 11 Notes 11 Notes" });
            this.Database.Execute(sqlInsert, new Object[] { 12, "Content 12 Content", "Notes 12 12 Notes 12 12 Notes 12 12 Notes" });

            // Act
            DataRow dataRowTest1 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Content like @Content", tableName, new Object[] { "%10" });
            DataRow dataRowTest2 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Content like @Content", tableName, new Object[] { "11%" });
            DataRow dataRowTest3 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Content like @Content", tableName, new Object[] { "%12%" });
            DataRow dataRowTest4 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Notes like @Notes", tableName, new Object[] { "%10 Notes 10" });
            DataRow dataRowTest5 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Notes like @Notes", tableName, new Object[] { "11 Notes 11 %" });
            DataRow dataRowTest6 = this.Database.QueryRecord("select * from QueryLike_DataAdapterFill where Notes like @Notes", tableName, new Object[] { "%12 12 Notes 12 12%" });

            // Assert
            Assert.IsNotNull(dataRowTest1);
            Assert.IsNotNull(dataRowTest2);
            Assert.IsNotNull(dataRowTest3);
            Assert.IsNotNull(dataRowTest4);
            Assert.IsNotNull(dataRowTest5);
            Assert.IsNotNull(dataRowTest6);

            // Clean
            try { this.Database.Execute(sqlDelete, null); }
            catch { /* Just to be sure that the table will be empty */ }
        }

        public virtual void ConvertSystemTypeToLazyDbType_LazyDbTypeArray_Single_Success()
        {
            // Arrange
            Object[] values = new Object[] {
                null, DBNull.Value,
                'L', "Lazy.Vinke.Tests.Database",
                (SByte)(-8), (Int16)(-16), (Int32)(-32), (Int64)(-64),
                (Byte)8, 1.1f, 10.01d, 100.001m,
                new DateTime(2023, 10, 24, 09, 15, 30),
                new Byte[] { 8, 16, 34, 64 },

                new List<Int32>(),
                new DataSet()
            };

            // Act
            MethodInfo methodInfo = this.Database.GetType().GetMethod("ConvertSystemTypeToLazyDbType", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            LazyDbType[] dbTypes = (LazyDbType[])methodInfo.Invoke(this.Database, new Object[] { values });

            // Assert
            Assert.AreEqual(dbTypes[0], LazyDbType.DBNull);
            Assert.AreEqual(dbTypes[1], LazyDbType.DBNull);
            Assert.AreEqual(dbTypes[2], LazyDbType.Char);
            Assert.AreEqual(dbTypes[3], LazyDbType.VarChar);
            Assert.AreEqual(dbTypes[4], LazyDbType.Byte);
            Assert.AreEqual(dbTypes[5], LazyDbType.Int16);
            Assert.AreEqual(dbTypes[6], LazyDbType.Int32);
            Assert.AreEqual(dbTypes[7], LazyDbType.Int64);
            Assert.AreEqual(dbTypes[8], LazyDbType.UByte);
            Assert.AreEqual(dbTypes[9], LazyDbType.Float);
            Assert.AreEqual(dbTypes[10], LazyDbType.Double);
            Assert.AreEqual(dbTypes[11], LazyDbType.Decimal);
            Assert.AreEqual(dbTypes[12], LazyDbType.DateTime);
            Assert.AreEqual(dbTypes[13], LazyDbType.VarUByte);
            Assert.AreEqual(dbTypes[14], LazyDbType.DBNull);
            Assert.AreEqual(dbTypes[15], LazyDbType.DBNull);
        }

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

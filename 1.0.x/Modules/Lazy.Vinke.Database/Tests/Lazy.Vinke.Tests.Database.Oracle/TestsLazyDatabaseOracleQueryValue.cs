// TestsLazyDatabaseOracleQueryValue.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Oracle" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

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
    public class TestsLazyDatabaseOracleQueryValue : TestsLazyDatabaseQueryValue
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseOracle(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public void QueryValue_Validations_DbmsDbType_Exception()
        {
            // Arrange
            String sql = "select ColumnVarChar1 from TestsQueryValue where TestCode = @TestCode";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            OracleDbType[] dbTypes = new OracleDbType[] { OracleDbType.Int32, OracleDbType.Varchar2 };
            String[] parameters = new String[] { "ColumnInt16P", "ColumnInt32P" };

            Object[] valuesLess = new Object[] { 1 };
            OracleDbType[] dbTypesLess = new OracleDbType[] { OracleDbType.Int32 };
            String[] parametersLess = new String[] { "ColumnCharD" };

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
            String sqlDelete = "delete from TestsQueryValue where TestCode = @TestCode";
            String sqlInsert = "insert into TestsQueryValue (" + columnsName + ") values (" + columnsParameter + ")";
            String sqlselect = "select {0} from TestsQueryValue where TestCode = @TestCode";
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

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

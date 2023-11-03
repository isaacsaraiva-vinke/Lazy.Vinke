// TestsLazyDatabaseQueryValue.cs
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
    public class TestsLazyDatabaseQueryValue
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
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

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

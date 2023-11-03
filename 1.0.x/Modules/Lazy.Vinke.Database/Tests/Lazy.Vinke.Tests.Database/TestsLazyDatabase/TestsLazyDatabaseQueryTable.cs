// TestsLazyDatabaseQueryTable.cs
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
    public class TestsLazyDatabaseQueryTable
    {
        protected LazyDatabase Database { get; set; }

        public virtual void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database.OpenConnection();
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

        public virtual void TestCleanup_CloseConnection_Single_Success()
        {
            /* Some tests may crash because connection state will be already closed */
            if (this.Database.ConnectionState == ConnectionState.Open)
                this.Database.CloseConnection();
        }
    }
}

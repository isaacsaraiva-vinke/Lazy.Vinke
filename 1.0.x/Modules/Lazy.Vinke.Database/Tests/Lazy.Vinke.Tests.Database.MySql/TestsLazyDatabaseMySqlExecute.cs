// TestsLazyDatabaseMySqlExecute.cs
//
// This file is integrated part of "Lazy Vinke Tests Database MySql" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

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
    public class TestsLazyDatabaseMySqlExecute : TestsLazyDatabaseExecute
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseMySql(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
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

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

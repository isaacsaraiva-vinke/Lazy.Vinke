// TestsLazyDatabasePostgreUpdate.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Postgre" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 04

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
    public class TestsLazyDatabasePostgreUpdate : TestsLazyDatabaseUpdate
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public virtual void Update_Validations_DbmsDbTypeArrays_Exception()
        {
            // Arrange
            String tableName = "TestsUpdate";
            String subQuery = "(select * from TestsUpdate)";

            Object[] values = new Object[] { 1, "Lazy.Vinke.Database" };
            NpgsqlDbType[] dbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Varchar };
            String[] fields = new String[] { "Id", "Name" };

            Object[] keyValues = new Object[] { 1, 1 };
            NpgsqlDbType[] keyDbTypes = new NpgsqlDbType[] { NpgsqlDbType.Integer, NpgsqlDbType.Integer };
            String[] keyFields = new String[] { "IdMaster", "IdChild" };

            Object[] valuesLess = new Object[] { 1 };
            NpgsqlDbType[] dbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Numeric };
            String[] fieldsLess = new String[] { "Amount" };

            Object[] keyValuesLess = new Object[] { 1 };
            NpgsqlDbType[] keyDbTypesLess = new NpgsqlDbType[] { NpgsqlDbType.Integer };
            String[] keyFieldsLess = new String[] { "Id" };

            Exception exceptionConnection = null;
            Exception exceptionTableNameNull = null;
            Exception exceptionSubQueryAsTableName = null;
            Exception exceptionValuesNullButOthers = null;
            Exception exceptionDbTypesNullButOthers = null;
            Exception exceptionDbFieldsNullButOthers = null;
            Exception exceptionValuesLessButOthers = null;
            Exception exceptionDbTypesLessButOthers = null;
            Exception exceptionDbFieldsLessButOthers = null;
            Exception exceptionKeyValuesNullButOthers = null;
            Exception exceptionKeyDbTypesNullButOthers = null;
            Exception exceptionKeyFieldsNullButOthers = null;
            Exception exceptionKeyValuesLessButOthers = null;
            Exception exceptionKeyDbTypesLessButOthers = null;
            Exception exceptionKeyFieldsLessButOthers = null;

            LazyDatabasePostgre databasePostgre = (LazyDatabasePostgre)this.Database;

            // Act
            databasePostgre.CloseConnection();

            try { databasePostgre.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionConnection = exp; }

            databasePostgre.OpenConnection();

            try { databasePostgre.Update(null, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionTableNameNull = exp; }
            try { databasePostgre.Update(subQuery, values, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionSubQueryAsTableName = exp; }
            try { databasePostgre.Update(tableName, null, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesNullButOthers = exp; }
            try { databasePostgre.Update(tableName, values, null, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesNullButOthers = exp; }
            try { databasePostgre.Update(tableName, values, dbTypes, null, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsNullButOthers = exp; }
            try { databasePostgre.Update(tableName, values, dbTypes, fields, null, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesNullButOthers = exp; }
            try { databasePostgre.Update(tableName, values, dbTypes, fields, keyValues, null, keyFields); } catch (Exception exp) { exceptionKeyDbTypesNullButOthers = exp; }
            try { databasePostgre.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypes, null); } catch (Exception exp) { exceptionKeyFieldsNullButOthers = exp; }

            try { databasePostgre.Update(tableName, valuesLess, dbTypes, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionValuesLessButOthers = exp; }
            try { databasePostgre.Update(tableName, values, dbTypesLess, fields, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbTypesLessButOthers = exp; }
            try { databasePostgre.Update(tableName, values, dbTypes, fieldsLess, keyValues, keyDbTypes, keyFields); } catch (Exception exp) { exceptionDbFieldsLessButOthers = exp; }
            try { databasePostgre.Update(tableName, values, dbTypes, fields, keyValuesLess, keyDbTypes, keyFields); } catch (Exception exp) { exceptionKeyValuesLessButOthers = exp; }
            try { databasePostgre.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypesLess, keyFields); } catch (Exception exp) { exceptionKeyDbTypesLessButOthers = exp; }
            try { databasePostgre.Update(tableName, values, dbTypes, fields, keyValues, keyDbTypes, keyFieldsLess); } catch (Exception exp) { exceptionKeyFieldsLessButOthers = exp; }

            // Assert
            Assert.AreEqual(exceptionConnection.Message, LazyResourcesDatabase.LazyDatabaseExceptionConnectionNotOpen);
            Assert.AreEqual(exceptionTableNameNull.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameNullOrEmpty);
            Assert.AreEqual(exceptionSubQueryAsTableName.Message, LazyResourcesDatabase.LazyDatabaseExceptionTableNameContainsWhiteSpace);
            Assert.AreEqual(exceptionValuesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesNullOrZeroLength);
            Assert.AreEqual(exceptionDbTypesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionTypesNullOrZeroLength);
            Assert.AreEqual(exceptionDbFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionDbFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyValuesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesNullOrZeroLength);
            Assert.AreEqual(exceptionKeyDbTypesNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyTypesNullOrZeroLength);
            Assert.AreEqual(exceptionKeyFieldsNullButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyFieldsNullOrZeroLength);
            Assert.AreEqual(exceptionKeyValuesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyDbTypesLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
            Assert.AreEqual(exceptionKeyFieldsLessButOthers.Message, LazyResourcesDatabase.LazyDatabaseExceptionKeyValuesTypesFieldsNotMatch);
        }

        [TestMethod]
        public override void Update_Validations_DataRow_Exception()
        {
            base.Update_Validations_DataRow_Exception();
        }

        [TestMethod]
        public override void Update_Validations_LazyDbTypeArrays_Exception()
        {
            base.Update_Validations_LazyDbTypeArrays_Exception();
        }

        [TestMethod]
        public override void Update_DataRow_Modified_Success()
        {
            base.Update_DataRow_Modified_Success();
        }

        [TestMethod]
        public override void Update_DataRow_ModifiedOnlyKeys_Success()
        {
            base.Update_DataRow_ModifiedOnlyKeys_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

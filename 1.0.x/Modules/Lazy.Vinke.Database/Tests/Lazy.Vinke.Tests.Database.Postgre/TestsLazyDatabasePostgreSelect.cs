// TestsLazyDatabasePostgreSelect.cs
//
// This file is integrated part of "Lazy Vinke Tests Database Postgre" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 03

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
    public class TestsLazyDatabasePostgreSelect : TestsLazyDatabaseSelect
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabasePostgre(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public override void Select_Validations_QueryTableDataRow_Exception()
        {
            base.Select_Validations_QueryTableDataRow_Exception();
        }

        [TestMethod]
        public override void Select_Validations_QueryTableArray_Exception()
        {
            base.Select_Validations_QueryTableArray_Exception();
        }

        [TestMethod]
        public override void Select_Validations_QueryPageDataRow_Exception()
        {
            base.Select_Validations_QueryPageDataRow_Exception();
        }

        [TestMethod]
        public override void Select_Validations_QueryPageArray_Exception()
        {
            base.Select_Validations_QueryPageArray_Exception();
        }

        [TestMethod]
        public override void Select_QueryTable_DataRowWithPrimaryKey_Success()
        {
            base.Select_QueryTable_DataRowWithPrimaryKey_Success();
        }

        [TestMethod]
        public override void Select_QueryPage_DataRowWithPrimaryKey_Success()
        {
            base.Select_QueryPage_DataRowWithPrimaryKey_Success();
        }

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

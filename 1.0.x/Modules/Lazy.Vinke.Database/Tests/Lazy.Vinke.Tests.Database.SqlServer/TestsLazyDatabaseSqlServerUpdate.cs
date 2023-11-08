// TestsLazyDatabaseSqlServerUpdate.cs
//
// This file is integrated part of "Lazy Vinke Tests Database SqlServer" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, November 04

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlTypes;

using Lazy.Vinke.Data;
using Lazy.Vinke.Database;
using Lazy.Vinke.Database.Properties;
using Lazy.Vinke.Database.SqlServer;

using Lazy.Vinke.Tests.Database;

namespace Lazy.Vinke.Tests.Database.SqlServer
{
    [TestClass]
    public class TestsLazyDatabaseSqlServerUpdate : TestsLazyDatabaseUpdate
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseSqlServer(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
        }

        [TestMethod]
        public override void Update_Validations_DataRow_Exception()
        {
            base.Update_Validations_DataRow_Exception();
        }

        [TestMethod]
        public override void Update_Validations_Arrays_Exception()
        {
            base.Update_Validations_Arrays_Exception();
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

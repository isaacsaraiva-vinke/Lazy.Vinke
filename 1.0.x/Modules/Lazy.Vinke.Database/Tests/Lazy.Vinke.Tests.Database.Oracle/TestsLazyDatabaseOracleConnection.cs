// TestsLazyDatabaseOracleConnection.cs
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
    public class TestsLazyDatabaseOracleConnection : TestsLazyDatabaseConnection
    {
        [TestInitialize]
        public override void TestInitialize_OpenConnection_Single_Success()
        {
            this.Database = new LazyDatabaseOracle(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Properties", "Miscellaneous", "ConnectionString.txt")));
            base.TestInitialize_OpenConnection_Single_Success();
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

        [TestCleanup]
        public override void TestCleanup_CloseConnection_Single_Success()
        {
            base.TestCleanup_CloseConnection_Single_Success();
        }
    }
}

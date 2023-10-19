// TestsSamplesLazyJsonSerializerDataSet.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 17

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

using Lazy.Vinke.Json;
using Lazy.Vinke.Json.Properties;
using Lazy.Vinke.Tests.Json.Properties;

namespace Lazy.Vinke.Tests.Json
{
    public class TestsSamplesLazyJsonSerializerDataSet
    {
        /* Avoid Visual Studio recognize file as a DataSet */
    }

    public class TestsSamplesLazyJsonSerializerDataSetSimple : DataSet
    {
        public TestsSamplesLazyJsonSerializerDataSetSimple() { }
        public TestsSamplesLazyJsonSerializerDataSetSimple(String name) : base(name) { }
    }

    public class TestsSamplesLazyJsonSerializerDataTableSimple : DataTable
    {
        public TestsSamplesLazyJsonSerializerDataTableSimple() { }
        public TestsSamplesLazyJsonSerializerDataTableSimple(String name) : base(name) { }
    }
}

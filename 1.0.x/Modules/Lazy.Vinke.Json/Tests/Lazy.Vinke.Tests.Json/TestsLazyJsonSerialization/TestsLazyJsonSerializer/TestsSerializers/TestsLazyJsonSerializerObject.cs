// TestsLazyJsonSerializerObject.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 20

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
    [TestClass]
    public class TestsLazyJsonSerializerObject
    {
        [TestMethod]
        public void Serialize_Object_Int32_Success()
        {
            // Arrange
            Object obj = 0;

            // Act
            LazyJsonObject jsonObject = (LazyJsonObject)new LazyJsonSerializerObject().Serialize(obj);

            // Assert
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Class"].Token).Value, "Int32");
            Assert.AreEqual(((LazyJsonInteger)jsonObject["Value"].Token).Value, 0);
        }

        [TestMethod]
        public void Serialize_Object_String_Success()
        {
            // Arrange
            Object obj = "Lazy.Vinke.Tests.Json";

            // Act
            LazyJsonObject jsonObject = (LazyJsonObject)new LazyJsonSerializerObject().Serialize(obj);

            // Assert
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Class"].Token).Value, "String");
            Assert.AreEqual(((LazyJsonString)jsonObject["Value"].Token).Value, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void Serialize_Object_Decimal_Success()
        {
            // Arrange
            Object obj = 101.101m;

            // Act
            LazyJsonObject jsonObject = (LazyJsonObject)new LazyJsonSerializerObject().Serialize(obj);

            // Assert
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Class"].Token).Value, "Decimal");
            Assert.AreEqual(((LazyJsonDecimal)jsonObject["Value"].Token).Value, 101.101m);
        }

        [TestMethod]
        public void Serialize_Object_Array_Success()
        {
            // Arrange
            Object obj = new Object[] { true, new DateTime(2023, 10, 20, 15, 52, 30) };

            // Act
            LazyJsonObject jsonObject = (LazyJsonObject)new LazyJsonSerializerObject().Serialize(obj);

            // Assert
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonObject["Type"].Token)["Class"].Token).Value, "Object[]");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObject["Value"].Token)[0])["Type"].Token)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObject["Value"].Token)[0])["Type"].Token)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObject["Value"].Token)[0])["Type"].Token)["Class"].Token).Value, "Boolean");
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonObject)((LazyJsonArray)jsonObject["Value"].Token)[0])["Value"].Token).Value, true);
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObject["Value"].Token)[1])["Type"].Token)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObject["Value"].Token)[1])["Type"].Token)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonObject)((LazyJsonArray)jsonObject["Value"].Token)[1])["Type"].Token)["Class"].Token).Value, "DateTime");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)((LazyJsonArray)jsonObject["Value"].Token)[1])["Value"].Token).Value, "2023-10-20T15:52:30:000Z");
        }
    }
}

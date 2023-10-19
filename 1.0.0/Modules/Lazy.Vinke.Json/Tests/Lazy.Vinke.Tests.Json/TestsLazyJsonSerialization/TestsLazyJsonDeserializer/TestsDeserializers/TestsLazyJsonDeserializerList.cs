// TestsLazyJsonDeserializerList.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 11

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
    public class TestsLazyJsonDeserializerList
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerList().Deserialize(jsonToken, typeof(List<Int32>));
            Object data2 = new LazyJsonDeserializerList().Deserialize(jsonArray, null);

            // Assert
            Assert.IsNull(data1);
            Assert.IsNull(data2);
        }

        [TestMethod]
        public void Deserialize_Type_NotList_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger();
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerList().Deserialize(jsonArray, typeof(String));
            Object data2 = new LazyJsonDeserializerList().Deserialize(jsonInteger, typeof(List<Decimal>));

            // Assert
            Assert.IsNull(data1);
            Assert.IsNull(data2);
        }

        [TestMethod]
        public void Deserialize_Type_Empty_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object list = new LazyJsonDeserializerList().Deserialize(jsonArray, typeof(List<Char>));

            // Assert
            Assert.AreEqual(list.GetType(), typeof(List<Char>));
            Assert.AreEqual(((List<Char>)list).Count, 0);
        }

        [TestMethod]
        public void Deserialize_Type_Integer_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(1));
            jsonArray.Add(new LazyJsonInteger(0));
            jsonArray.Add(new LazyJsonInteger(1));

            // Act
            Object list = new LazyJsonDeserializerList().Deserialize(jsonArray, typeof(List<Int16>));

            // Assert
            Assert.AreEqual(list.GetType(), typeof(List<Int16>));
            Assert.AreEqual(((List<Int16>)list).Count, 3);
            Assert.AreEqual(((List<Int16>)list)[0], (Int16)1);
            Assert.AreEqual(((List<Int16>)list)[1], (Int16)0);
            Assert.AreEqual(((List<Int16>)list)[2], (Int16)1);
        }

        [TestMethod]
        public void Deserialize_Type_Decimal_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(1.1m));
            jsonArray.Add(new LazyJsonDecimal(-101.101m));
            jsonArray.Add(new LazyJsonDecimal(101.101m));
            jsonArray.Add(new LazyJsonDecimal(-1.1m));

            // Act
            Object list = new LazyJsonDeserializerList().Deserialize(jsonArray, typeof(List<Decimal>));

            // Assert
            Assert.AreEqual(list.GetType(), typeof(List<Decimal>));
            Assert.AreEqual(((List<Decimal>)list).Count, 4);
            Assert.AreEqual(((List<Decimal>)list)[0], 1.1m);
            Assert.AreEqual(((List<Decimal>)list)[1], -101.101m);
            Assert.AreEqual(((List<Decimal>)list)[2], 101.101m);
            Assert.AreEqual(((List<Decimal>)list)[3], -1.1m);
        }

        [TestMethod]
        public void Deserialize_Type_ObjectKnown_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(1.1m));
            jsonArray.Add(new LazyJsonBoolean(false));
            jsonArray.Add(new LazyJsonString("Lazy.Vinke.Tests.Json"));
            jsonArray.Add(new LazyJsonArray());
            ((LazyJsonArray)jsonArray[3]).Add(new LazyJsonBoolean(true));
            ((LazyJsonArray)jsonArray[3]).Add(new LazyJsonNull());
            jsonArray.Add(new LazyJsonNull());
            jsonArray.Add(new LazyJsonInteger(-101));
            jsonArray.Add(new LazyJsonString("2023-10-11T14:14:30:000Z"));

            // Act
            Object list = new LazyJsonDeserializerList().Deserialize(jsonArray, typeof(List<Object>));

            // Assert
            Assert.AreEqual(list.GetType(), typeof(List<Object>));
            Assert.AreEqual(((List<Object>)list).Count, 7);
            Assert.AreEqual(((List<Object>)list)[0], 1.1m);
            Assert.AreEqual(((List<Object>)list)[1], false);
            Assert.AreEqual(((List<Object>)list)[2], "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((List<Object>)list)[3].GetType(), typeof(Object[]));
            Assert.AreEqual(((Object[])((List<Object>)list)[3])[0], true);
            Assert.AreEqual(((Object[])((List<Object>)list)[3])[1], null);
            Assert.AreEqual(((List<Object>)list)[4], null);
            Assert.AreEqual(((List<Object>)list)[5], (Int64)(-101));
            Assert.AreEqual(((List<Object>)list)[6], new DateTime(2023, 10, 11, 14, 14, 30));
        }
    }
}

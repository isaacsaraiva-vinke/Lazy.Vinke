// TestsLazyJsonDeserializerArray.cs
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
    public class TestsLazyJsonDeserializerArray
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerArray().Deserialize(jsonToken, typeof(Int32[]));
            Object data2 = new LazyJsonDeserializerArray().Deserialize(jsonArray, null);

            // Assert
            Assert.IsNull(data1);
            Assert.IsNull(data2);
        }

        [TestMethod]
        public void Deserialize_Type_NotArray_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger();
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerArray().Deserialize(jsonArray, typeof(String));
            Object data2 = new LazyJsonDeserializerArray().Deserialize(jsonInteger, typeof(Decimal[]));

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
            Object array = new LazyJsonDeserializerArray().Deserialize(jsonArray, typeof(String[]));

            // Assert
            Assert.AreEqual(array.GetType(), typeof(String[]));
            Assert.AreEqual(((String[])array).Length, 0);
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
            Object array = new LazyJsonDeserializerArray().Deserialize(jsonArray, typeof(Int16[]));

            // Assert
            Assert.AreEqual(array.GetType(), typeof(Int16[]));
            Assert.AreEqual(((Int16[])array).Length, 3);
            Assert.AreEqual(((Int16[])array)[0], (Int16)1);
            Assert.AreEqual(((Int16[])array)[1], (Int16)0);
            Assert.AreEqual(((Int16[])array)[2], (Int16)1);
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
            Object array = new LazyJsonDeserializerArray().Deserialize(jsonArray, typeof(Decimal[]));

            // Assert
            Assert.AreEqual(array.GetType(), typeof(Decimal[]));
            Assert.AreEqual(((Decimal[])array).Length, 4);
            Assert.AreEqual(((Decimal[])array)[0], 1.1m);
            Assert.AreEqual(((Decimal[])array)[1], -101.101m);
            Assert.AreEqual(((Decimal[])array)[2], 101.101m);
            Assert.AreEqual(((Decimal[])array)[3], -1.1m);
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
            jsonArray.Add(new LazyJsonString("2023-10-11T08:40:00:000Z"));

            // Act
            Object array = new LazyJsonDeserializerArray().Deserialize(jsonArray, typeof(Object[]));

            // Assert
            Assert.AreEqual(array.GetType(), typeof(Object[]));
            Assert.AreEqual(((Object[])array).Length, 7);
            Assert.AreEqual(((Object[])array)[0], 1.1m);
            Assert.AreEqual(((Object[])array)[1], false);
            Assert.AreEqual(((Object[])array)[2], "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((Object[])array)[3].GetType(), typeof(Object[]));
            Assert.AreEqual(((Object[])((Object[])array)[3])[0], true);
            Assert.AreEqual(((Object[])((Object[])array)[3])[1], null);
            Assert.AreEqual(((Object[])array)[4], null);
            Assert.AreEqual(((Object[])array)[5], (Int64)(-101));
            Assert.AreEqual(((Object[])array)[6], new DateTime(2023, 10, 11, 08, 40, 00));
        }
    }
}

// TestsLazyJsonDeserializerDictionary.cs
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
    public class TestsLazyJsonDeserializerDictionary
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerDictionary().Deserialize(jsonToken, typeof(Dictionary<Int32, String>));
            Object data2 = new LazyJsonDeserializerDictionary().Deserialize(jsonArray, null);

            // Assert
            Assert.IsNull(data1);
            Assert.IsNull(data2);
        }

        [TestMethod]
        public void Deserialize_Type_NotDictionary_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger();
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerDictionary().Deserialize(jsonArray, typeof(String));
            Object data2 = new LazyJsonDeserializerDictionary().Deserialize(jsonInteger, typeof(Dictionary<String, Decimal>));

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
            Object dictionary = new LazyJsonDeserializerDictionary().Deserialize(jsonArray, typeof(Dictionary<Char, Decimal>));

            // Assert
            Assert.AreEqual(dictionary.GetType(), typeof(Dictionary<Char, Decimal>));
            Assert.AreEqual(((Dictionary<Char, Decimal>)dictionary).Count, 0);
        }

        [TestMethod]
        public void Deserialize_Type_StringInteger_Success()
        {
            // Arrange
            LazyJsonArray jsonArrayKeyPair1 = new LazyJsonArray();
            jsonArrayKeyPair1.Add(new LazyJsonString("X"));
            jsonArrayKeyPair1.Add(new LazyJsonInteger(1));

            LazyJsonArray jsonArrayKeyPair2 = new LazyJsonArray();
            jsonArrayKeyPair2.Add(new LazyJsonString("Y"));
            jsonArrayKeyPair2.Add(new LazyJsonInteger(101));

            LazyJsonArray jsonArrayKeyPair3 = new LazyJsonArray();
            jsonArrayKeyPair3.Add(new LazyJsonString("Z"));
            jsonArrayKeyPair3.Add(new LazyJsonInteger(-1));

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonArrayKeyPair1);
            jsonArray.Add(jsonArrayKeyPair2);
            jsonArray.Add(jsonArrayKeyPair3);

            // Act
            Object dictionary = new LazyJsonDeserializerDictionary().Deserialize(jsonArray, typeof(Dictionary<String, Int64>));

            // Assert
            Assert.AreEqual(dictionary.GetType(), typeof(Dictionary<String, Int64>));
            Assert.AreEqual(((Dictionary<String, Int64>)dictionary).Count, 3);
            Assert.AreEqual(((Dictionary<String, Int64>)dictionary)["X"], (Int64)1);
            Assert.AreEqual(((Dictionary<String, Int64>)dictionary)["Y"], (Int64)101);
            Assert.AreEqual(((Dictionary<String, Int64>)dictionary)["Z"], (Int64)(-1));
        }

        [TestMethod]
        public void Deserialize_Type_IntegerDecimal_Success()
        {
            // Arrange
            LazyJsonArray jsonArrayKeyPair1 = new LazyJsonArray();
            jsonArrayKeyPair1.Add(new LazyJsonInteger(101));
            jsonArrayKeyPair1.Add(new LazyJsonDecimal(-1.1m));

            LazyJsonArray jsonArrayKeyPair2 = new LazyJsonArray();
            jsonArrayKeyPair2.Add(new LazyJsonInteger(1));
            jsonArrayKeyPair2.Add(new LazyJsonDecimal(101.101m));

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonArrayKeyPair1);
            jsonArray.Add(jsonArrayKeyPair2);

            // Act
            Object dictionary = new LazyJsonDeserializerDictionary().Deserialize(jsonArray, typeof(Dictionary<Int16, Decimal>));

            // Assert
            Assert.AreEqual(dictionary.GetType(), typeof(Dictionary<Int16, Decimal>));
            Assert.AreEqual(((Dictionary<Int16, Decimal>)dictionary).Count, 2);
            Assert.AreEqual(((Dictionary<Int16, Decimal>)dictionary)[101], -1.1m);
            Assert.AreEqual(((Dictionary<Int16, Decimal>)dictionary)[1], 101.101m);
        }

        [TestMethod]
        public void Deserialize_Type_ObjectObjectKnown_Success()
        {
            // Arrange
            LazyJsonArray jsonArrayKeyPair1 = new LazyJsonArray();
            jsonArrayKeyPair1.Add(new LazyJsonInteger(101));
            jsonArrayKeyPair1.Add(new LazyJsonDecimal(-1.1m));

            LazyJsonArray jsonArrayKeyPair2 = new LazyJsonArray();
            jsonArrayKeyPair2.Add(new LazyJsonString("Lazy.Vinke.Tests.Json"));
            jsonArrayKeyPair2.Add(new LazyJsonInteger(1));

            LazyJsonArray jsonArrayKeyPair3 = new LazyJsonArray();
            jsonArrayKeyPair3.Add(new LazyJsonDecimal(101.101m));
            jsonArrayKeyPair3.Add(new LazyJsonString("Deserialize_Type_ObjectObjectKnown_Success"));

            LazyJsonArray jsonArrayKeyPair4 = new LazyJsonArray();
            jsonArrayKeyPair4.Add(new LazyJsonString("2023-10-11T21:15:30:000Z"));
            jsonArrayKeyPair4.Add(new LazyJsonBoolean(true));

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonArrayKeyPair1);
            jsonArray.Add(jsonArrayKeyPair2);
            jsonArray.Add(jsonArrayKeyPair3);
            jsonArray.Add(jsonArrayKeyPair4);

            // Act
            Object dictionary = new LazyJsonDeserializerDictionary().Deserialize(jsonArray, typeof(Dictionary<Object, Object>));

            // Assert
            Assert.AreEqual(dictionary.GetType(), typeof(Dictionary<Object, Object>));
            Assert.AreEqual(((Dictionary<Object, Object>)dictionary).Count, 4);
            Assert.AreEqual(((Dictionary<Object, Object>)dictionary)[(Int64)101], -1.1m);
            Assert.AreEqual(((Dictionary<Object, Object>)dictionary)["Lazy.Vinke.Tests.Json"], (Int64)1);
            Assert.AreEqual(((Dictionary<Object, Object>)dictionary)[101.101m], "Deserialize_Type_ObjectObjectKnown_Success");
            Assert.AreEqual(((Dictionary<Object, Object>)dictionary)[new DateTime(2023, 10, 11, 21, 15, 30)], true);
        }
    }
}

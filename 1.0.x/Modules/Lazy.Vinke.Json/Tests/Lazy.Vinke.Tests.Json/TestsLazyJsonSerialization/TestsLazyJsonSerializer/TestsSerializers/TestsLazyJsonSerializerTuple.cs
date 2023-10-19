// TestsLazyJsonSerializerTuple.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 19

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
    public class TestsLazyJsonSerializerTuple
    {
        [TestMethod]
        public void Serialize_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerTuple().Serialize(null);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Type_NotTuple_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerTuple().Serialize(new List<Int32>());

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Type_Empty_Success()
        {
            // Arrange
            ValueTuple valueTuple = new ValueTuple();

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerTuple().Serialize(valueTuple);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Type_OneItem_Success()
        {
            // Arrange
            Tuple<Int32> tuple = new Tuple<Int32>(101);
            ValueTuple<Int32> valueTuple = new ValueTuple<Int32>();

            // Act
            LazyJsonArray jsonTokenTuple = (LazyJsonArray)new LazyJsonSerializerTuple().Serialize(tuple);
            LazyJsonArray jsonTokenValueTuple = (LazyJsonArray)new LazyJsonSerializerTuple().Serialize(valueTuple);

            // Assert
            Assert.AreEqual(jsonTokenTuple.Length, 1);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenTuple[0]).Value, 101);
            Assert.AreEqual(jsonTokenValueTuple.Length, 1);
            Assert.AreEqual(((LazyJsonInteger)jsonTokenValueTuple[0]).Value, 0);
        }

        [TestMethod]
        public void Serialize_Type_ThreeItens_Success()
        {
            // Arrange
            Tuple<String, DateTime, Boolean> tuple = new Tuple<String, DateTime, Boolean>("Lazy.Vinke.Tests.Json", new DateTime(2023, 10, 19, 15, 45, 30), true);
            ValueTuple<String, DateTime, Boolean> valueTuple = new ValueTuple<String, DateTime, Boolean>("Lazy.Vinke.Tests.Json", new DateTime(2023, 10, 19, 15, 45, 30), true);

            // Act
            LazyJsonArray jsonTokenTuple = (LazyJsonArray)new LazyJsonSerializerTuple().Serialize(tuple);
            LazyJsonArray jsonTokenValueTuple = (LazyJsonArray)new LazyJsonSerializerTuple().Serialize(valueTuple);

            // Assert
            Assert.AreEqual(jsonTokenTuple.Length, 3);
            Assert.AreEqual(((LazyJsonString)jsonTokenTuple[0]).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonString)jsonTokenTuple[1]).Value, "2023-10-19T15:45:30:000Z");
            Assert.AreEqual(((LazyJsonBoolean)jsonTokenTuple[2]).Value, true);
            Assert.AreEqual(jsonTokenValueTuple.Length, 3);
            Assert.AreEqual(((LazyJsonString)jsonTokenValueTuple[0]).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonString)jsonTokenValueTuple[1]).Value, "2023-10-19T15:45:30:000Z");
            Assert.AreEqual(((LazyJsonBoolean)jsonTokenValueTuple[2]).Value, true);
        }


        [TestMethod]
        public void Serialize_Type_Nested_Success()
        {
            // Arrange
            Tuple<String, ValueTuple<Byte, Char>> tuple = new Tuple<String, ValueTuple<Byte, Char>>("Lazy.Vinke.Tests.Json", new ValueTuple<Byte, Char>(1, '0'));
            ValueTuple<Decimal, Tuple<String, Boolean>> valueTuple = new ValueTuple<Decimal, Tuple<String, Boolean>>(101.101m, new Tuple<String, Boolean>("Lazy.Vinke.Tests.Json", false));

            // Act
            LazyJsonArray jsonTokenTuple = (LazyJsonArray)new LazyJsonSerializerTuple().Serialize(tuple);
            LazyJsonArray jsonTokenValueTuple = (LazyJsonArray)new LazyJsonSerializerTuple().Serialize(valueTuple);

            // Assert
            Assert.AreEqual(jsonTokenTuple.Length, 2);
            Assert.AreEqual(((LazyJsonString)jsonTokenTuple[0]).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonTokenTuple[1])[0]).Value, 1);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonTokenTuple[1])[1]).Value, "0");
            Assert.AreEqual(jsonTokenValueTuple.Length, 2);
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenValueTuple[0]).Value, 101.101m);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonTokenValueTuple[1])[0]).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)jsonTokenValueTuple[1])[1]).Value, false);
        }
    }
}

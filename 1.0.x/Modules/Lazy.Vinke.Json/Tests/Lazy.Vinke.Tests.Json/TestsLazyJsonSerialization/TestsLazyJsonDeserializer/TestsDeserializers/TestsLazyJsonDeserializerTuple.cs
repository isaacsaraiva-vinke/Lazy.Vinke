// TestsLazyJsonDeserializerTuple.cs
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
    public class TestsLazyJsonDeserializerTuple
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerTuple().Deserialize(jsonToken, typeof(Tuple<Int32>));
            Object data2 = new LazyJsonDeserializerTuple().Deserialize(jsonArray, null);

            // Assert
            Assert.IsNull(data1);
            Assert.IsNull(data2);
        }

        [TestMethod]
        public void Deserialize_Type_NotTuple_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger();
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(String));
            Object data2 = new LazyJsonDeserializerTuple().Deserialize(jsonInteger, typeof(Tuple<Decimal>));

            // Assert
            Assert.IsNull(data1);
            Assert.IsNull(data2);
        }

        [TestMethod]
        public void Deserialize_Type_ParameterCountMismatch_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object tuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(Tuple<Char>));
            Object valueTuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(ValueTuple<Boolean>));

            // Assert
            Assert.IsNull(tuple);
            Assert.IsNull(valueTuple);
        }

        [TestMethod]
        public void Deserialize_Type_Empty_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object tuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(Tuple<>));
            Object valueTuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(ValueTuple<>));

            // Assert
            Assert.IsNull(tuple);
            Assert.IsNull(valueTuple);
        }

        [TestMethod]
        public void Deserialize_Type_OneItem_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Lazy.Vinke.Tests.Json"));

            // Act
            Object tuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(Tuple<String>));
            Object valueTuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(ValueTuple<String>));

            // Assert
            Assert.AreEqual(((Tuple<String>)tuple).Item1, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((ValueTuple<String>)valueTuple).Item1, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void Deserialize_Type_FourItens_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Lazy.Vinke.Tests.Json"));
            jsonArray.Add(new LazyJsonString("J"));
            jsonArray.Add(new LazyJsonBoolean(false));
            jsonArray.Add(new LazyJsonDecimal(1.1m));

            // Act
            Object tuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(Tuple<String, Char, Boolean, Decimal>));
            Object valueTuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(ValueTuple<String, Char, Boolean, Decimal>));

            // Assert
            Assert.AreEqual(((Tuple<String, Char, Boolean, Decimal>)tuple).Item1, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((Tuple<String, Char, Boolean, Decimal>)tuple).Item2, 'J');
            Assert.AreEqual(((Tuple<String, Char, Boolean, Decimal>)tuple).Item3, false);
            Assert.AreEqual(((Tuple<String, Char, Boolean, Decimal>)tuple).Item4, 1.1m);
            Assert.AreEqual(((ValueTuple<String, Char, Boolean, Decimal>)valueTuple).Item1, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((ValueTuple<String, Char, Boolean, Decimal>)valueTuple).Item2, 'J');
            Assert.AreEqual(((ValueTuple<String, Char, Boolean, Decimal>)valueTuple).Item3, false);
            Assert.AreEqual(((ValueTuple<String, Char, Boolean, Decimal>)valueTuple).Item4, 1.1m);
        }

        [TestMethod]
        public void Deserialize_Type_Nested_Success()
        {
            // Arrange
            LazyJsonArray jsonArrayNested = new LazyJsonArray();
            jsonArrayNested.Add(new LazyJsonBoolean(true));
            jsonArrayNested.Add(new LazyJsonDecimal(-101.101m));

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Lazy.Vinke.Tests.Json"));
            jsonArray.Add(jsonArrayNested);

            // Act
            Object tuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(Tuple<String, Tuple<Boolean, Decimal>>));
            Object valueTuple = new LazyJsonDeserializerTuple().Deserialize(jsonArray, typeof(ValueTuple<String, ValueTuple<Boolean, Decimal>>));

            // Assert
            Assert.AreEqual(((Tuple<String, Tuple<Boolean, Decimal>>)tuple).Item1, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((Tuple<Boolean, Decimal>)((Tuple<String, Tuple<Boolean, Decimal>>)tuple).Item2).Item1, true);
            Assert.AreEqual(((Tuple<Boolean, Decimal>)((Tuple<String, Tuple<Boolean, Decimal>>)tuple).Item2).Item2, -101.101m);
            Assert.AreEqual(((ValueTuple<String, ValueTuple<Boolean, Decimal>>)valueTuple).Item1, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((ValueTuple<Boolean, Decimal>)((ValueTuple<String, ValueTuple<Boolean, Decimal>>)valueTuple).Item2).Item1, true);
            Assert.AreEqual(((ValueTuple<Boolean, Decimal>)((ValueTuple<String, ValueTuple<Boolean, Decimal>>)valueTuple).Item2).Item2, -101.101m);
        }
    }
}

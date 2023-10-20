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
            LazyJsonObject jsonWrappedInt32Type = new LazyJsonObject();
            jsonWrappedInt32Type.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedInt32Type.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedInt32Type.Add(new LazyJsonProperty("Class", new LazyJsonString("Int32")));
            LazyJsonInteger jsonWrappedInt32Value = new LazyJsonInteger(101);
            LazyJsonObject jsonWrappedInt32 = new LazyJsonObject();
            jsonWrappedInt32.Add(new LazyJsonProperty("Type", jsonWrappedInt32Type));
            jsonWrappedInt32.Add(new LazyJsonProperty("Value", jsonWrappedInt32Value));

            LazyJsonObject jsonWrappedDecimalType = new LazyJsonObject();
            jsonWrappedDecimalType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedDecimalType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedDecimalType.Add(new LazyJsonProperty("Class", new LazyJsonString("Decimal")));
            LazyJsonDecimal jsonWrappedDecimalValue = new LazyJsonDecimal(-1.1m);
            LazyJsonObject jsonWrappedDecimal = new LazyJsonObject();
            jsonWrappedDecimal.Add(new LazyJsonProperty("Type", jsonWrappedDecimalType));
            jsonWrappedDecimal.Add(new LazyJsonProperty("Value", jsonWrappedDecimalValue));

            LazyJsonObject jsonWrappedStringType = new LazyJsonObject();
            jsonWrappedStringType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedStringType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedStringType.Add(new LazyJsonProperty("Class", new LazyJsonString("String")));
            LazyJsonString jsonWrappedStringValue = new LazyJsonString("Lazy.Vinke.Tests.Json");
            LazyJsonObject jsonWrappedString = new LazyJsonObject();
            jsonWrappedString.Add(new LazyJsonProperty("Type", jsonWrappedStringType));
            jsonWrappedString.Add(new LazyJsonProperty("Value", jsonWrappedStringValue));

            LazyJsonObject jsonWrappedCharType = new LazyJsonObject();
            jsonWrappedCharType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedCharType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedCharType.Add(new LazyJsonProperty("Class", new LazyJsonString("Char")));
            LazyJsonString jsonWrappedCharValue = new LazyJsonString("J");
            LazyJsonObject jsonWrappedChar = new LazyJsonObject();
            jsonWrappedChar.Add(new LazyJsonProperty("Type", jsonWrappedCharType));
            jsonWrappedChar.Add(new LazyJsonProperty("Value", jsonWrappedCharValue));

            LazyJsonObject jsonWrappedByteType = new LazyJsonObject();
            jsonWrappedByteType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedByteType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedByteType.Add(new LazyJsonProperty("Class", new LazyJsonString("Byte")));
            LazyJsonInteger jsonWrappedByteValue = new LazyJsonInteger(8);
            LazyJsonObject jsonWrappedByte = new LazyJsonObject();
            jsonWrappedByte.Add(new LazyJsonProperty("Type", jsonWrappedByteType));
            jsonWrappedByte.Add(new LazyJsonProperty("Value", jsonWrappedByteValue));

            LazyJsonObject jsonWrappedDateTimeType = new LazyJsonObject();
            jsonWrappedDateTimeType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedDateTimeType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedDateTimeType.Add(new LazyJsonProperty("Class", new LazyJsonString("DateTime")));
            LazyJsonString jsonWrappedDateTimeValue = new LazyJsonString("2023-10-11T21:15:30:000Z");
            LazyJsonObject jsonWrappedDateTime = new LazyJsonObject();
            jsonWrappedDateTime.Add(new LazyJsonProperty("Type", jsonWrappedDateTimeType));
            jsonWrappedDateTime.Add(new LazyJsonProperty("Value", jsonWrappedDateTimeValue));

            LazyJsonArray jsonArrayKeyPair1 = new LazyJsonArray();
            jsonArrayKeyPair1.Add(jsonWrappedInt32);
            jsonArrayKeyPair1.Add(jsonWrappedDecimal);

            LazyJsonArray jsonArrayKeyPair2 = new LazyJsonArray();
            jsonArrayKeyPair2.Add(jsonWrappedString);
            jsonArrayKeyPair2.Add(jsonWrappedChar);

            LazyJsonArray jsonArrayKeyPair3 = new LazyJsonArray();
            jsonArrayKeyPair3.Add(jsonWrappedByte);
            jsonArrayKeyPair3.Add(jsonWrappedDateTime);

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonArrayKeyPair1);
            jsonArray.Add(jsonArrayKeyPair2);
            jsonArray.Add(jsonArrayKeyPair3);

            // Act
            Object dictionary = new LazyJsonDeserializerDictionary().Deserialize(jsonArray, typeof(Dictionary<Object, Object>));

            // Assert
            Assert.AreEqual(dictionary.GetType(), typeof(Dictionary<Object, Object>));
            Assert.AreEqual(((Dictionary<Object, Object>)dictionary).Count, 3);
            Assert.AreEqual(((Dictionary<Object, Object>)dictionary)[101], -1.1m);
            Assert.AreEqual(((Dictionary<Object, Object>)dictionary)["Lazy.Vinke.Tests.Json"], 'J');
            Assert.AreEqual(((Dictionary<Object, Object>)dictionary)[(Byte)8], new DateTime(2023, 10, 11, 21, 15, 30));
        }
    }
}

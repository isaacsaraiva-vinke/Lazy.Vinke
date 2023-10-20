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

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(jsonWrappedInt32);
            jsonArray.Add(jsonWrappedDecimal);
            jsonArray.Add(jsonWrappedString);
            jsonArray.Add(jsonWrappedChar);
            jsonArray.Add(jsonWrappedByte);
            jsonArray.Add(jsonWrappedDateTime);

            // Act
            Object list = new LazyJsonDeserializerList().Deserialize(jsonArray, typeof(List<Object>));

            // Assert
            Assert.AreEqual(list.GetType(), typeof(List<Object>));
            Assert.AreEqual(((List<Object>)list).Count, 6);
            Assert.AreEqual(((List<Object>)list)[0], 101);
            Assert.AreEqual(((List<Object>)list)[1], -1.1m);
            Assert.AreEqual(((List<Object>)list)[2], "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((List<Object>)list)[3], 'J');
            Assert.AreEqual(((List<Object>)list)[4], (Byte)8);
            Assert.AreEqual(((List<Object>)list)[5], new DateTime(2023, 10, 11, 21, 15, 30));
        }
    }
}

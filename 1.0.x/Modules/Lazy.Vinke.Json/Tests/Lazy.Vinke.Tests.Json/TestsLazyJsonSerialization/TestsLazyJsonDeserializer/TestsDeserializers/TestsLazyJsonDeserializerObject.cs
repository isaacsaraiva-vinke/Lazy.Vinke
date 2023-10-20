// TestsLazyJsonDeserializerObject.cs
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
    public class TestsLazyJsonDeserializerObject
    {
        [TestMethod]
        public void Deserialize_Object_Int16_Success()
        {
            // Arrange
            LazyJsonObject jsonWrappedInt16Type = new LazyJsonObject();
            jsonWrappedInt16Type.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedInt16Type.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedInt16Type.Add(new LazyJsonProperty("Class", new LazyJsonString("Int16")));
            LazyJsonInteger jsonWrappedInt16Value = new LazyJsonInteger(101);
            LazyJsonObject jsonWrappedInt16 = new LazyJsonObject();
            jsonWrappedInt16.Add(new LazyJsonProperty("Type", jsonWrappedInt16Type));
            jsonWrappedInt16.Add(new LazyJsonProperty("Value", jsonWrappedInt16Value));

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonWrappedInt16, typeof(Object));

            // Assert
            Assert.AreEqual(data.GetType(), typeof(Int16));
            Assert.AreEqual(data, (Int16)101);
        }

        [TestMethod]
        public void Deserialize_Object_String_Success()
        {
            // Arrange
            LazyJsonObject jsonWrappedStringType = new LazyJsonObject();
            jsonWrappedStringType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedStringType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedStringType.Add(new LazyJsonProperty("Class", new LazyJsonString("String")));
            LazyJsonString jsonWrappedStringValue = new LazyJsonString("Lazy.Vinke.Tests.Json");
            LazyJsonObject jsonWrappedString = new LazyJsonObject();
            jsonWrappedString.Add(new LazyJsonProperty("Type", jsonWrappedStringType));
            jsonWrappedString.Add(new LazyJsonProperty("Value", jsonWrappedStringValue));

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonWrappedString, typeof(Object));

            // Assert
            Assert.AreEqual(data.GetType(), typeof(String));
            Assert.AreEqual(data, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void Deserialize_Object_DateTime_Success()
        {
            // Arrange
            LazyJsonObject jsonWrappedStringType = new LazyJsonObject();
            jsonWrappedStringType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedStringType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedStringType.Add(new LazyJsonProperty("Class", new LazyJsonString("DateTime")));
            LazyJsonString jsonWrappedStringValue = new LazyJsonString("2023-10-20T15:52:30:000Z");
            LazyJsonObject jsonWrappedString = new LazyJsonObject();
            jsonWrappedString.Add(new LazyJsonProperty("Type", jsonWrappedStringType));
            jsonWrappedString.Add(new LazyJsonProperty("Value", jsonWrappedStringValue));

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonWrappedString, typeof(Object));

            // Assert
            Assert.AreEqual(data.GetType(), typeof(DateTime));
            Assert.AreEqual(data, new DateTime(2023, 10, 20, 15, 52, 30));
        }

        [TestMethod]
        public void Deserialize_Object_Array_Success()
        {
            // Arrange
            LazyJsonObject jsonWrappedInt64Type = new LazyJsonObject();
            jsonWrappedInt64Type.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedInt64Type.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedInt64Type.Add(new LazyJsonProperty("Class", new LazyJsonString("Int64")));
            LazyJsonInteger jsonWrappedInt64Value = new LazyJsonInteger(2048);
            LazyJsonObject jsonWrappedInt64 = new LazyJsonObject();
            jsonWrappedInt64.Add(new LazyJsonProperty("Type", jsonWrappedInt64Type));
            jsonWrappedInt64.Add(new LazyJsonProperty("Value", jsonWrappedInt64Value));

            LazyJsonObject jsonWrappedSByteType = new LazyJsonObject();
            jsonWrappedSByteType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedSByteType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedSByteType.Add(new LazyJsonProperty("Class", new LazyJsonString("SByte")));
            LazyJsonInteger jsonWrappedSByteValue = new LazyJsonInteger(-32);
            LazyJsonObject jsonWrappedSByte = new LazyJsonObject();
            jsonWrappedSByte.Add(new LazyJsonProperty("Type", jsonWrappedSByteType));
            jsonWrappedSByte.Add(new LazyJsonProperty("Value", jsonWrappedSByteValue));

            LazyJsonObject jsonWrappedArrayType = new LazyJsonObject();
            jsonWrappedArrayType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonWrappedArrayType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonWrappedArrayType.Add(new LazyJsonProperty("Class", new LazyJsonString("Object[]")));
            LazyJsonArray jsonWrappedArrayValue = new LazyJsonArray();
            jsonWrappedArrayValue.Add(jsonWrappedInt64);
            jsonWrappedArrayValue.Add(jsonWrappedSByte);
            LazyJsonObject jsonWrappedArray = new LazyJsonObject();
            jsonWrappedArray.Add(new LazyJsonProperty("Type", jsonWrappedArrayType));
            jsonWrappedArray.Add(new LazyJsonProperty("Value", jsonWrappedArrayValue));

            // Act
            Object data = new LazyJsonDeserializerObject().Deserialize(jsonWrappedArray, typeof(Object));

            // Assert
            Assert.AreEqual(data.GetType(), typeof(Object[]));
            Assert.AreEqual(((Object[])data)[0].GetType(), typeof(Int64));
            Assert.AreEqual(((Object[])data)[1].GetType(), typeof(SByte));
            Assert.AreEqual((Int64)((Object[])data)[0], (Int64)2048);
            Assert.AreEqual((SByte)((Object[])data)[1], (SByte)(-32));
        }
    }
}

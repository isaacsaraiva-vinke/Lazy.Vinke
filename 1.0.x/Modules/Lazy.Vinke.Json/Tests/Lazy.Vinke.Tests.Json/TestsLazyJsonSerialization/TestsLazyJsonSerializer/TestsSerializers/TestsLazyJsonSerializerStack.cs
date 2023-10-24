// TestsLazyJsonSerializerStack.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 24

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
    public class TestsLazyJsonSerializerStack
    {
        [TestMethod]
        public void Serialize_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerStack().Serialize(null);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Type_NotStack_Success()
        {
            // Arrange
            String test = "Lazy.Vinke.Tests.Json";

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerStack().Serialize(test);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Type_Empty_Success()
        {
            // Arrange
            Stack<Decimal> decimalStack = new Stack<Decimal>();

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerStack().Serialize(decimalStack);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 0);
        }

        [TestMethod]
        public void Serialize_Reverse_Integer_Success()
        {
            // Arrange
            Stack<Int32> integerStack = new Stack<Int32>();
            integerStack.Push(1);
            integerStack.Push(2);
            integerStack.Push(3);
            integerStack.Push(4);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerStack().Serialize(integerStack);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 4);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[0]).Value, 1);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[1]).Value, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[2]).Value, 3);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[3]).Value, 4);
        }

        [TestMethod]
        public void Serialize_Normal_Integer_Success()
        {
            // Arrange
            Stack<Int32> integerStack = new Stack<Int32>();
            integerStack.Push(1);
            integerStack.Push(2);
            integerStack.Push(3);
            integerStack.Push(4);

            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsStack>().WriteReverse = false;

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerStack().Serialize(integerStack, jsonSerializerOptions);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 4);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[0]).Value, 4);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[1]).Value, 3);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[2]).Value, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonToken)[3]).Value, 1);
        }

        [TestMethod]
        public void Serialize_Reverse_String_Success()
        {
            // Arrange
            Stack<String> integerStack = new Stack<String>();
            integerStack.Push("Lazy");
            integerStack.Push("Vinke");
            integerStack.Push("Tests");
            integerStack.Push("Json");

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerStack().Serialize(integerStack);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 4);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[0]).Value, "Lazy");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[1]).Value, "Vinke");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[2]).Value, "Tests");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[3]).Value, "Json");
        }

        [TestMethod]
        public void Serialize_Normal_String_Success()
        {
            // Arrange
            Stack<String> integerStack = new Stack<String>();
            integerStack.Push("Lazy");
            integerStack.Push("Vinke");
            integerStack.Push("Tests");
            integerStack.Push("Json");

            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsStack>().WriteReverse = false;

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerStack().Serialize(integerStack, jsonSerializerOptions);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 4);
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[0]).Value, "Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[1]).Value, "Tests");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[2]).Value, "Vinke");
            Assert.AreEqual(((LazyJsonString)((LazyJsonArray)jsonToken)[3]).Value, "Lazy");
        }

        [TestMethod]
        public void Serialize_Reverse_ByteNested_Success()
        {
            // Arrange
            Stack<Stack<Byte>> integerStack = new Stack<Stack<Byte>>();
            integerStack.Push(new Stack<Byte>());
            integerStack.Peek().Push(1);
            integerStack.Peek().Push(2);
            integerStack.Push(new Stack<Byte>());
            integerStack.Peek().Push(3);
            integerStack.Peek().Push(4);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerStack().Serialize(integerStack);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 2);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[0]).Length, 2);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[1]).Length, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[0]).Value, 1);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[1]).Value, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[0]).Value, 3);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[1]).Value, 4);
        }

        [TestMethod]
        public void Serialize_Normal_ByteNested_Success()
        {
            // Arrange
            Stack<Stack<Byte>> integerStack = new Stack<Stack<Byte>>();
            integerStack.Push(new Stack<Byte>());
            integerStack.Peek().Push(1);
            integerStack.Peek().Push(2);
            integerStack.Push(new Stack<Byte>());
            integerStack.Peek().Push(3);
            integerStack.Peek().Push(4);

            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsStack>().WriteReverse = false;

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerStack().Serialize(integerStack, jsonSerializerOptions);

            // Assert
            Assert.AreEqual(((LazyJsonArray)jsonToken).Length, 2);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[0]).Length, 2);
            Assert.AreEqual(((LazyJsonArray)((LazyJsonArray)jsonToken)[1]).Length, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[0]).Value, 4);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[0])[1]).Value, 3);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[0]).Value, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)((LazyJsonArray)jsonToken)[1])[1]).Value, 1);
        }
    }
}

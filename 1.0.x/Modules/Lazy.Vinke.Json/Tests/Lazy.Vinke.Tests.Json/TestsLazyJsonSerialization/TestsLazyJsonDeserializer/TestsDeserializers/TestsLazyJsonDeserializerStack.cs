// TestsLazyJsonDeserializerStack.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 07

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
    public class TestsLazyJsonDeserializerStack
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonToken jsonToken = null;
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerStack().Deserialize(jsonToken, typeof(Stack<Int32>));
            Object data2 = new LazyJsonDeserializerStack().Deserialize(jsonArray, null);

            // Assert
            Assert.IsNull(data1);
            Assert.IsNull(data2);
        }

        [TestMethod]
        public void Deserialize_Type_NotStack_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger();
            LazyJsonArray jsonArray = new LazyJsonArray();

            // Act
            Object data1 = new LazyJsonDeserializerStack().Deserialize(jsonArray, typeof(String));
            Object data2 = new LazyJsonDeserializerStack().Deserialize(jsonInteger, typeof(Stack<Decimal>));

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
            Object stack = new LazyJsonDeserializerStack().Deserialize(jsonArray, typeof(Stack<Char>));

            // Assert
            Assert.AreEqual(stack.GetType(), typeof(Stack<Char>));
            Assert.AreEqual(((Stack<Char>)stack).Count, 0);
        }

        [TestMethod]
        public void Deserialize_Normal_Integer_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(1));
            jsonArray.Add(new LazyJsonInteger(2));
            jsonArray.Add(new LazyJsonInteger(3));
            jsonArray.Add(new LazyJsonInteger(4));

            // Act
            Object stack = new LazyJsonDeserializerStack().Deserialize(jsonArray, typeof(Stack<Int16>));

            // Assert
            Assert.AreEqual(stack.GetType(), typeof(Stack<Int16>));
            Assert.AreEqual(((Stack<Int16>)stack).Count, 4);
            Assert.AreEqual(((Stack<Int16>)stack).Pop(), (Int16)4);
            Assert.AreEqual(((Stack<Int16>)stack).Pop(), (Int16)3);
            Assert.AreEqual(((Stack<Int16>)stack).Pop(), (Int16)2);
            Assert.AreEqual(((Stack<Int16>)stack).Pop(), (Int16)1);
        }

        [TestMethod]
        public void Deserialize_Reverse_Integer_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(4));
            jsonArray.Add(new LazyJsonInteger(3));
            jsonArray.Add(new LazyJsonInteger(2));
            jsonArray.Add(new LazyJsonInteger(1));

            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsStack>().ReadReverse = true;

            // Act
            Object stack = new LazyJsonDeserializerStack().Deserialize(jsonArray, typeof(Stack<Int16>), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual(stack.GetType(), typeof(Stack<Int16>));
            Assert.AreEqual(((Stack<Int16>)stack).Count, 4);
            Assert.AreEqual(((Stack<Int16>)stack).Pop(), (Int16)4);
            Assert.AreEqual(((Stack<Int16>)stack).Pop(), (Int16)3);
            Assert.AreEqual(((Stack<Int16>)stack).Pop(), (Int16)2);
            Assert.AreEqual(((Stack<Int16>)stack).Pop(), (Int16)1);
        }

        [TestMethod]
        public void Deserialize_Normal_String_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Lazy"));
            jsonArray.Add(new LazyJsonString("Vinke"));
            jsonArray.Add(new LazyJsonString("Test"));
            jsonArray.Add(new LazyJsonString("Json"));

            // Act
            Object stack = new LazyJsonDeserializerStack().Deserialize(jsonArray, typeof(Stack<String>));

            // Assert
            Assert.AreEqual(stack.GetType(), typeof(Stack<String>));
            Assert.AreEqual(((Stack<String>)stack).Count, 4);
            Assert.AreEqual(((Stack<String>)stack).Pop(), "Json");
            Assert.AreEqual(((Stack<String>)stack).Pop(), "Test");
            Assert.AreEqual(((Stack<String>)stack).Pop(), "Vinke");
            Assert.AreEqual(((Stack<String>)stack).Pop(), "Lazy");
        }

        [TestMethod]
        public void Deserialize_Reverse_String_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Json"));
            jsonArray.Add(new LazyJsonString("Test"));
            jsonArray.Add(new LazyJsonString("Vinke"));
            jsonArray.Add(new LazyJsonString("Lazy"));

            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsStack>().ReadReverse = true;

            // Act
            Object stack = new LazyJsonDeserializerStack().Deserialize(jsonArray, typeof(Stack<String>), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual(stack.GetType(), typeof(Stack<String>));
            Assert.AreEqual(((Stack<String>)stack).Count, 4);
            Assert.AreEqual(((Stack<String>)stack).Pop(), "Json");
            Assert.AreEqual(((Stack<String>)stack).Pop(), "Test");
            Assert.AreEqual(((Stack<String>)stack).Pop(), "Vinke");
            Assert.AreEqual(((Stack<String>)stack).Pop(), "Lazy");
        }

        [TestMethod]
        public void Deserialize_Normal_ByteNested_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonArray());
            ((LazyJsonArray)jsonArray[0]).Add(new LazyJsonInteger(1));
            ((LazyJsonArray)jsonArray[0]).Add(new LazyJsonInteger(2));
            jsonArray.Add(new LazyJsonArray());
            ((LazyJsonArray)jsonArray[1]).Add(new LazyJsonInteger(3));
            ((LazyJsonArray)jsonArray[1]).Add(new LazyJsonInteger(4));

            // Act
            Stack<Stack<Byte>> stack = (Stack<Stack<Byte>>)new LazyJsonDeserializerStack().Deserialize(jsonArray, typeof(Stack<Stack<Byte>>));

            // Assert
            Assert.AreEqual(stack.GetType(), typeof(Stack<Stack<Byte>>));
            Assert.AreEqual(stack.Count, 2);
            Assert.AreEqual(stack.Peek().Count, 2);
            Assert.AreEqual(stack.Peek().Pop(), (Byte)4);
            Assert.AreEqual(stack.Pop().Pop(), (Int16)3);
            Assert.AreEqual(stack.Peek().Count, 2);
            Assert.AreEqual(stack.Peek().Pop(), (Int16)2);
            Assert.AreEqual(stack.Pop().Pop(), (Int16)1);
        }

        [TestMethod]
        public void Deserialize_Reverse_ByteNested_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonArray());
            ((LazyJsonArray)jsonArray[0]).Add(new LazyJsonInteger(4));
            ((LazyJsonArray)jsonArray[0]).Add(new LazyJsonInteger(3));
            jsonArray.Add(new LazyJsonArray());
            ((LazyJsonArray)jsonArray[1]).Add(new LazyJsonInteger(2));
            ((LazyJsonArray)jsonArray[1]).Add(new LazyJsonInteger(1));

            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsStack>().ReadReverse = true;

            // Act
            Stack<Stack<Byte>> stack = (Stack<Stack<Byte>>)new LazyJsonDeserializerStack().Deserialize(jsonArray, typeof(Stack<Stack<Byte>>), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual(stack.GetType(), typeof(Stack<Stack<Byte>>));
            Assert.AreEqual(stack.Count, 2);
            Assert.AreEqual(stack.Peek().Count, 2);
            Assert.AreEqual(stack.Peek().Pop(), (Byte)4);
            Assert.AreEqual(stack.Pop().Pop(), (Int16)3);
            Assert.AreEqual(stack.Peek().Count, 2);
            Assert.AreEqual(stack.Peek().Pop(), (Int16)2);
            Assert.AreEqual(stack.Pop().Pop(), (Int16)1);
        }
    }
}

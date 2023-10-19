// TestsLazyJsonReader.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, September 30

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
    public class TestsLazyJsonReader
    {
        [TestMethod]
        public void Read_Complete_JsonSample01_Success()
        {
            // Arrange
            String json = Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample01Success);

            // Act
            LazyJson lazyJson = LazyJsonReader.Read(json);

            // Assert
            Assert.AreNotEqual(lazyJson.Root.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Read_Complete_JsonSample02_Success()
        {
            // Arrange
            String json = Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample02Success);

            // Act
            LazyJson lazyJson = LazyJsonReader.Read(json);

            // Assert
            Assert.AreNotEqual(lazyJson.Root.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Read_Complete_JsonSample03_Success()
        {
            // Arrange
            String json = Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample03Success);

            // Act
            LazyJson lazyJson = LazyJsonReader.Read(json);

            // Assert
            Assert.AreNotEqual(lazyJson.Root.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Read_Complete_JsonSample04_Success()
        {
            // Arrange
            String json = Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample04Success);

            // Act
            LazyJson lazyJson = LazyJsonReader.Read(json);

            // Assert
            Assert.AreNotEqual(lazyJson.Root.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Read_Complete_JsonSample05_Success()
        {
            // Arrange
            String json = Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample05Success);

            // Act
            LazyJson lazyJson = LazyJsonReader.Read(json);

            // Assert
            Assert.AreNotEqual(lazyJson.Root.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Read_Complete_JsonSample50_Exception()
        {
            // Arrange
            String json = Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample50Exception);

            // Act
            Exception exception = null;
            try { LazyJsonReader.Read(json); }
            catch (Exception exp) { exception = exp; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains("(line 12 column 21)"));
        }

        [TestMethod]
        public void ReadRoot_Array_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[]";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 3);
            Assert.AreEqual(index, 2);
            Assert.AreEqual(lazyJson.Root.Type, LazyJsonType.Array);
        }

        [TestMethod]
        public void ReadRoot_Array_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**///\n [] //\n/**/ ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 19);
            Assert.AreEqual(lazyJson.Root.Type, LazyJsonType.Array);
        }

        [TestMethod]
        public void ReadRoot_Object_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{}";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 3);
            Assert.AreEqual(index, 2);
            Assert.AreEqual(lazyJson.Root.Type, LazyJsonType.Object);
        }

        [TestMethod]
        public void ReadRoot_Object_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**/ //\n {\t} //\n/**/ ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 21);
            Assert.AreEqual(lazyJson.Root.Type, LazyJsonType.Object);
        }

        [TestMethod]
        public void ReadRoot_String_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Lazy.Vinke.Tests.Json\"";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 24);
            Assert.AreEqual(index, 23);
            Assert.AreEqual(((LazyJsonString)lazyJson.Root).Value, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void ReadRoot_String_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**/ //\n \"Lazy.Vinke.Tests.Json\" //\n/**/ ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 41);
            Assert.AreEqual(((LazyJsonString)lazyJson.Root).Value, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void ReadRoot_IntegerNegative_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "-101";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
            Assert.AreEqual(((LazyJsonInteger)lazyJson.Root).Value, -101);
        }

        [TestMethod]
        public void ReadRoot_IntegerNegative_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**/ //\n -1 //\n/**/ ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 20);
            Assert.AreEqual(((LazyJsonInteger)lazyJson.Root).Value, -1);
        }

        [TestMethod]
        public void ReadRoot_DecimalNegative_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "-1.1";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
            Assert.AreEqual(((LazyJsonDecimal)lazyJson.Root).Value, -1.1m);
        }

        [TestMethod]
        public void ReadRoot_DecimalNegative_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**/ //\n -101.101 //\n/**/ ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 26);
            Assert.AreEqual(((LazyJsonDecimal)lazyJson.Root).Value, -101.101m);
        }

        [TestMethod]
        public void ReadRoot_IntegerPositive_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "101";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 4);
            Assert.AreEqual(index, 3);
            Assert.AreEqual(((LazyJsonInteger)lazyJson.Root).Value, 101);
        }

        [TestMethod]
        public void ReadRoot_IntegerPositive_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**/ //\n 1 //\n/**/ ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 19);
            Assert.AreEqual(((LazyJsonInteger)lazyJson.Root).Value, 1);
        }

        [TestMethod]
        public void ReadRoot_DecimalPositive_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "1.1";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 4);
            Assert.AreEqual(index, 3);
            Assert.AreEqual(((LazyJsonDecimal)lazyJson.Root).Value, 1.1m);
        }

        [TestMethod]
        public void ReadRoot_DecimalPositive_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**/ //\n 101.101 //\n/**/ ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 25);
            Assert.AreEqual(((LazyJsonDecimal)lazyJson.Root).Value, 101.101m);
        }

        [TestMethod]
        public void ReadRoot_Null_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "null";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
            Assert.AreEqual(lazyJson.Root.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadRoot_Null_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**/ //\n null //\n/**/ ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 22);
            Assert.AreEqual(lazyJson.Root.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadRoot_Boolean_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "false";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 5);
            Assert.AreEqual(((LazyJsonBoolean)lazyJson.Root).Value, false);
        }

        [TestMethod]
        public void ReadRoot_Boolean_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**/ //\n true //\n/**/ ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 22);
            Assert.AreEqual(((LazyJsonBoolean)lazyJson.Root).Value, true);
        }

        [TestMethod]
        public void ReadRoot_UnexpectedCharacter_MissingRootToken_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "//\n ( ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenType));
        }

        [TestMethod]
        public void ReadRoot_UnexpectedCharacter_ContentOverflow_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "//\n {\n} //\n x";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
        }

        [TestMethod]
        public void ReadRoot_UnexpectedEnd_MissingRootToken_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "//\n//\n ";

            LazyJson lazyJson = new LazyJson();
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), lazyJson, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJson), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenType));
        }

        [TestMethod]
        public void ReadArray_Empty_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 3);
            Assert.AreEqual(index, 2);
            Assert.AreEqual(jsonArray.Length, 0);
        }

        [TestMethod]
        public void ReadArray_Empty_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[ //\n\r\t /**/ \t\n  ]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 4);
            Assert.AreEqual(index, 18);
            Assert.AreEqual(jsonArray.Length, 0);
        }

        [TestMethod]
        public void ReadArray_TokenArray_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[[1,true]]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 11);
            Assert.AreEqual(index, 10);
            Assert.AreEqual(jsonArray.Length, 1);
            Assert.AreEqual(((LazyJsonArray)jsonArray[0]).Length, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonArray[0])[0]).Value, 1);
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)jsonArray[0])[1]).Value, true);
        }

        [TestMethod]
        public void ReadArray_TokenArray_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[ \n [ \n\t 1.1 ],\r\n[ \n\tfalse\n] \n]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 7);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 31);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonArray)jsonArray[0]).Length, 1);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonArray[0])[0]).Value, 1.1m);
            Assert.AreEqual(((LazyJsonArray)jsonArray[1]).Length, 1);
            Assert.AreEqual(((LazyJsonBoolean)((LazyJsonArray)jsonArray[1])[0]).Value, false);
        }

        [TestMethod]
        public void ReadArray_TokenObject_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[{\"Solution\":\"Lazy.Vinke.Tests.Json\"}]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 39);
            Assert.AreEqual(index, 38);
            Assert.AreEqual(jsonArray.Length, 1);
            Assert.AreEqual(((LazyJsonObject)jsonArray[0]).Count, 1);
            Assert.AreEqual(((LazyJsonObject)jsonArray[0])[0].Name, "Solution");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonArray[0])[0].Token).Value, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void ReadArray_TokenObject_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[\n{\n\"Solution\" : \r\n\t\"Lazy.Vinke.Tests.Json\"\r\n}\r\n,\n{\n\"Items\" : \r\n\t[]\r\n}\r\n]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 11);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 73);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonObject)jsonArray[0]).Count, 1);
            Assert.AreEqual(((LazyJsonObject)jsonArray[0])[0].Name, "Solution");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonArray[0])[0].Token).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(((LazyJsonObject)jsonArray[1]).Count, 1);
            Assert.AreEqual(((LazyJsonObject)jsonArray[1])[0].Name, "Items");
            Assert.AreEqual(((LazyJsonArray)((LazyJsonObject)jsonArray[1])[0].Token).Length, 0);
        }

        [TestMethod]
        public void ReadArray_TokenString_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[\"x\",\"y\",\"z\"]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 14);
            Assert.AreEqual(index, 13);
            Assert.AreEqual(jsonArray.Length, 3);
            Assert.AreEqual(((LazyJsonString)jsonArray[0]).Value, "x");
            Assert.AreEqual(((LazyJsonString)jsonArray[1]).Value, "y");
            Assert.AreEqual(((LazyJsonString)jsonArray[2]).Value, "z");
        }

        [TestMethod]
        public void ReadArray_TokenString_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[ \n\t\"x\", \n\t\"y\", \n\t\"z\"\n]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 5);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 23);
            Assert.AreEqual(jsonArray.Length, 3);
            Assert.AreEqual(((LazyJsonString)jsonArray[0]).Value, "x");
            Assert.AreEqual(((LazyJsonString)jsonArray[1]).Value, "y");
            Assert.AreEqual(((LazyJsonString)jsonArray[2]).Value, "z");
        }

        [TestMethod]
        public void ReadArray_TokenIntegerNegative_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[-1,-101]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 10);
            Assert.AreEqual(index, 9);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[0]).Value, -1);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[1]).Value, -101);
        }

        [TestMethod]
        public void ReadArray_TokenIntegerNegative_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[\n-101, \n-1\r\n]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 4);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 14);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[0]).Value, -101);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[1]).Value, -1);
        }

        [TestMethod]
        public void ReadArray_TokenDecimalNegative_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[-1.1,-101.101]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 16);
            Assert.AreEqual(index, 15);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonDecimal)jsonArray[0]).Value, -1.1m);
            Assert.AreEqual(((LazyJsonDecimal)jsonArray[1]).Value, -101.101m);
        }

        [TestMethod]
        public void ReadArray_TokenDecimalNegative_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[\n\t-101.101, \n\t-1.1]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 7);
            Assert.AreEqual(index, 20);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonDecimal)jsonArray[0]).Value, -101.101m);
            Assert.AreEqual(((LazyJsonDecimal)jsonArray[1]).Value, -1.1m);
        }

        [TestMethod]
        public void ReadArray_TokenIntegerPositive_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[1,101]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 8);
            Assert.AreEqual(index, 7);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[0]).Value, 1);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[1]).Value, 101);
        }

        [TestMethod]
        public void ReadArray_TokenIntegerPositive_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[\n101, \n1\r\n]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 4);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 12);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[0]).Value, 101);
            Assert.AreEqual(((LazyJsonInteger)jsonArray[1]).Value, 1);
        }

        [TestMethod]
        public void ReadArray_TokenDecimalPositive_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[1.1,101.101]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 14);
            Assert.AreEqual(index, 13);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonDecimal)jsonArray[0]).Value, 1.1m);
            Assert.AreEqual(((LazyJsonDecimal)jsonArray[1]).Value, 101.101m);
        }

        [TestMethod]
        public void ReadArray_TokenDecimalPositive_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[\n\t101.101, \n\t1.1]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 18);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonDecimal)jsonArray[0]).Value, 101.101m);
            Assert.AreEqual(((LazyJsonDecimal)jsonArray[1]).Value, 1.1m);
        }

        [TestMethod]
        public void ReadArray_TokenNull_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[null,null]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 12);
            Assert.AreEqual(index, 11);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(jsonArray[0].Type, LazyJsonType.Null);
            Assert.AreEqual(jsonArray[1].Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadArray_TokenNull_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[ \n\tnull \n]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 11);
            Assert.AreEqual(jsonArray.Length, 1);
            Assert.AreEqual(jsonArray[0].Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadArray_TokenBoolean_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[true,false]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 13);
            Assert.AreEqual(index, 12);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonBoolean)jsonArray[0]).Value, true);
            Assert.AreEqual(((LazyJsonBoolean)jsonArray[1]).Value, false);
        }

        [TestMethod]
        public void ReadArray_TokenBoolean_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[\n\tfalse, \n\ttrue]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonArray = (LazyJsonArray)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 7);
            Assert.AreEqual(index, 17);
            Assert.AreEqual(jsonArray.Length, 2);
            Assert.AreEqual(((LazyJsonBoolean)jsonArray[0]).Value, false);
            Assert.AreEqual(((LazyJsonBoolean)jsonArray[1]).Value, true);
        }

        [TestMethod]
        public void ReadArray_UnexpectedCharacter_MissingArrayToken_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[*]";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenType));
        }

        [TestMethod]
        public void ReadArray_UnexpectedCharacter_MissingArrayNextOrEnd_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[ null }";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeArrayNextOrEnd));
        }

        [TestMethod]
        public void ReadArray_UnexpectedEnd_MissingArrayNextOrEnd_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[ null";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeArrayNextOrEnd));
        }

        [TestMethod]
        public void ReadArray_UnexpectedEnd_MissingArrayTokenWithDisposableContent_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[ ";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenType));
        }

        [TestMethod]
        public void ReadArray_UnexpectedEnd_MissingArrayTokenWithoutDisposableContent_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "[";

            index++; column++;
            LazyJsonArray jsonArray = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonArray, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonArray).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenType));
        }

        [TestMethod]
        public void ReadObject_Empty_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{}";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonObject = (LazyJsonObject)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 3);
            Assert.AreEqual(index, 2);
            Assert.AreEqual(jsonObject.Count, 0);
        }

        [TestMethod]
        public void ReadObject_Empty_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{ \r/**/\t }";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonObject = (LazyJsonObject)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 11);
            Assert.AreEqual(index, 10);
            Assert.AreEqual(jsonObject.Count, 0);
        }

        [TestMethod]
        public void ReadObject_OneProperty_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{\"Solution\":\"Lazy.Vinke.Tests.Json\"}";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonObject = (LazyJsonObject)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 37);
            Assert.AreEqual(index, 36);
            Assert.AreEqual(jsonObject.Count, 1);
            Assert.AreEqual(jsonObject[0].Name, "Solution");
            Assert.AreEqual(((LazyJsonString)jsonObject[0].Token).Value, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void ReadObject_TwoProperties_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{\t\"Solution\" : \"Lazy.Vinke.Tests.Json\",\r\n\t\"Testing\" : true}";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonObject = (LazyJsonObject)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 19);
            Assert.AreEqual(index, 59);
            Assert.AreEqual(jsonObject.Count, 2);
            Assert.AreEqual(jsonObject[0].Name, "Solution");
            Assert.AreEqual(((LazyJsonString)jsonObject[0].Token).Value, "Lazy.Vinke.Tests.Json");
            Assert.AreEqual(jsonObject[1].Name, "Testing");
            Assert.AreEqual(((LazyJsonBoolean)jsonObject[1].Token).Value, true);
        }

        [TestMethod]
        public void ReadObject_PropertyAlreadyExists_TwoProperties_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{\t\"Testing\" : \"Lazy.Vinke.Tests.Json\",\r\n\t\"Testing\" : true}";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains("Testing"));
        }

        [TestMethod]
        public void ReadObject_UnexpectedCharacter_MissingObjectProperty_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{ Testing }";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeObjectProperty));
        }

        [TestMethod]
        public void ReadObject_UnexpectedCharacter_MissingObjectNextOrEnd_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{ \"Testing\":true ]";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeObjectNextOrEnd));
        }

        [TestMethod]
        public void ReadObject_UnexpectedEnd_MissingObjectPropertyWithoutDisposableContent_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeObjectProperty));
        }

        [TestMethod]
        public void ReadObject_UnexpectedEnd_MissingObjectPropertyWithDisposableContent_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{\n";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeObjectProperty));
        }

        [TestMethod]
        public void ReadObject_UnexpectedEnd_MissingObjectNextOrEnd_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{ \"Testing\":true";

            index++; column++;
            LazyJsonObject jsonObject = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonObject, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonObject).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeObjectNextOrEnd));
        }

        [TestMethod]
        public void ReadProperty_TokenArray_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Items\":[0,1.1]";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 16);
            Assert.AreEqual(index, 15);
            Assert.AreEqual(jsonProperty.Name, "Items");
            Assert.AreEqual(((LazyJsonArray)jsonProperty.Token).Length, 2);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonProperty.Token)[0]).Value, 0);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonProperty.Token)[1]).Value, 1.1m);
        }

        [TestMethod]
        public void ReadProperty_TokenArray_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Values\" //\n : /**/ [-1.1,-1]";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 18);
            Assert.AreEqual(index, 29);
            Assert.AreEqual(jsonProperty.Name, "Values");
            Assert.AreEqual(((LazyJsonArray)jsonProperty.Token).Length, 2);
            Assert.AreEqual(((LazyJsonDecimal)((LazyJsonArray)jsonProperty.Token)[0]).Value, -1.1m);
            Assert.AreEqual(((LazyJsonInteger)((LazyJsonArray)jsonProperty.Token)[1]).Value, -1);
        }

        [TestMethod]
        public void ReadProperty_TokenObject_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Solution\":{\"Test\":null}";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 25);
            Assert.AreEqual(index, 24);
            Assert.AreEqual(jsonProperty.Name, "Solution");
            Assert.AreEqual(((LazyJsonObject)jsonProperty.Token).Count, 1);
            Assert.AreEqual(((LazyJsonObject)jsonProperty.Token)[0].Name, "Test");
            Assert.AreEqual(((LazyJsonObject)jsonProperty.Token)[0].Token.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadProperty_TokenObject_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Solution\" /**/ ://\n//\n{/**/\"Test\" : \tnull\r }";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 23);
            Assert.AreEqual(index, 45);
            Assert.AreEqual(jsonProperty.Name, "Solution");
            Assert.AreEqual(((LazyJsonObject)jsonProperty.Token).Count, 1);
            Assert.AreEqual(((LazyJsonObject)jsonProperty.Token)[0].Name, "Test");
            Assert.AreEqual(((LazyJsonObject)jsonProperty.Token)[0].Token.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadProperty_TokenString_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Solution\":\"Test\"";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 18);
            Assert.AreEqual(index, 17);
            Assert.AreEqual(jsonProperty.Name, "Solution");
            Assert.AreEqual(((LazyJsonString)jsonProperty.Token).Value, "Test");
        }

        [TestMethod]
        public void ReadProperty_TokenString_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Solution\" \r\n\t: /**/ \"Test\"";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 15);
            Assert.AreEqual(index, 27);
            Assert.AreEqual(jsonProperty.Name, "Solution");
            Assert.AreEqual(((LazyJsonString)jsonProperty.Token).Value, "Test");
        }

        [TestMethod]
        public void ReadProperty_TokenIntegerNegative_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Status\":-1";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 12);
            Assert.AreEqual(index, 11);
            Assert.AreEqual(jsonProperty.Name, "Status");
            Assert.AreEqual(((LazyJsonInteger)jsonProperty.Token).Value, -1);
        }

        [TestMethod]
        public void ReadProperty_TokenIntegerNegative_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Status\"//\n://\n\t-101";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 20);
            Assert.AreEqual(jsonProperty.Name, "Status");
            Assert.AreEqual(((LazyJsonInteger)jsonProperty.Token).Value, -101);
        }

        [TestMethod]
        public void ReadProperty_TokenDecimalNegative_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Value\":-101.101";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 17);
            Assert.AreEqual(index, 16);
            Assert.AreEqual(jsonProperty.Name, "Value");
            Assert.AreEqual(((LazyJsonDecimal)jsonProperty.Token).Value, -101.101m);
        }

        [TestMethod]
        public void ReadProperty_TokenDecimalNegative_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Value\"//\n: \r\n\t/**/-1.1";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 10);
            Assert.AreEqual(index, 23);
            Assert.AreEqual(jsonProperty.Name, "Value");
            Assert.AreEqual(((LazyJsonDecimal)jsonProperty.Token).Value, -1.1m);
        }

        [TestMethod]
        public void ReadProperty_TokenIntegerPositive_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Status\":1";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 11);
            Assert.AreEqual(index, 10);
            Assert.AreEqual(jsonProperty.Name, "Status");
            Assert.AreEqual(((LazyJsonInteger)jsonProperty.Token).Value, 1);
        }

        [TestMethod]
        public void ReadProperty_TokenIntegerPositive_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Status\"//\n://\n\t101";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 19);
            Assert.AreEqual(jsonProperty.Name, "Status");
            Assert.AreEqual(((LazyJsonInteger)jsonProperty.Token).Value, 101);
        }

        [TestMethod]
        public void ReadProperty_TokenDecimalPositive_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Value\":101.101";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 16);
            Assert.AreEqual(index, 15);
            Assert.AreEqual(jsonProperty.Name, "Value");
            Assert.AreEqual(((LazyJsonDecimal)jsonProperty.Token).Value, 101.101m);
        }

        [TestMethod]
        public void ReadProperty_TokenDecimalPositive_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Value\"//\n: \r\n\t/**/1.1";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 9);
            Assert.AreEqual(index, 22);
            Assert.AreEqual(jsonProperty.Name, "Value");
            Assert.AreEqual(((LazyJsonDecimal)jsonProperty.Token).Value, 1.1m);
        }

        [TestMethod]
        public void ReadProperty_TokenNull_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Value\":null";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 13);
            Assert.AreEqual(index, 12);
            Assert.AreEqual(jsonProperty.Name, "Value");
            Assert.AreEqual(jsonProperty.Token.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadProperty_TokenNull_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Value\"//\n: \r\n\t/**/null";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 10);
            Assert.AreEqual(index, 23);
            Assert.AreEqual(jsonProperty.Name, "Value");
            Assert.AreEqual(jsonProperty.Token.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadProperty_TokenBoolean_WithoutDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Value\":true";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 13);
            Assert.AreEqual(index, 12);
            Assert.AreEqual(jsonProperty.Name, "Value");
            Assert.AreEqual(((LazyJsonBoolean)jsonProperty.Token).Value, true);
        }

        [TestMethod]
        public void ReadProperty_TokenBoolean_WithDisposableContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Value\"//\n: \r\n\t/**/false";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonProperty = (LazyJsonProperty)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 11);
            Assert.AreEqual(index, 24);
            Assert.AreEqual(jsonProperty.Name, "Value");
            Assert.AreEqual(((LazyJsonBoolean)jsonProperty.Token).Value, false);
        }

        [TestMethod]
        public void ReadProperty_UnexpectedCharacter_MissingPropertyValue_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Solution\"//\n & ";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeObjectPropertyValue));
        }

        [TestMethod]
        public void ReadProperty_UnexpectedCharacter_MissingPropertyToken_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Solution\"//\n: \r\n\t/**/ % ";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenType));
        }

        [TestMethod]
        public void ReadProperty_UnexpectedEnd_MissingPropertyValue_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Solution\"\n\n";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeObjectPropertyValue));
        }

        [TestMethod]
        public void ReadProperty_UnexpectedEnd_MissingPropertyToken_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Solution\"\n:\n";

            index++; column++;
            LazyJsonProperty jsonProperty = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonProperty, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonProperty).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenType));
        }

        [TestMethod]
        public void ReadString_Empty_Single_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"\"";

            index++; column++;
            LazyJsonString jsonString = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonString, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonString).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonString = (LazyJsonString)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 3);
            Assert.AreEqual(index, 2);
            Assert.AreEqual(jsonString.Value, String.Empty);
        }

        [TestMethod]
        public void ReadString_Whitespace_Single_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\" \"";

            index++; column++;
            LazyJsonString jsonString = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonString, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonString).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonString = (LazyJsonString)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 4);
            Assert.AreEqual(index, 3);
            Assert.AreEqual(jsonString.Value, " ");
        }

        [TestMethod]
        public void ReadString_NewLine_Single_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"\n\"";

            index++; column++;
            LazyJsonString jsonString = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonString, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonString).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonString = (LazyJsonString)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 3);
            Assert.AreEqual(jsonString.Value, "\n");
        }

        [TestMethod]
        public void ReadString_Text_OneLine_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Lazy.Vinke.Tests.Json\"";

            index++; column++;
            LazyJsonString jsonString = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonString, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonString).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonString = (LazyJsonString)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 24);
            Assert.AreEqual(index, 23);
            Assert.AreEqual(jsonString.Value, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void ReadString_Text_MultipleLines_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"\nLazy\nVinke\nTests\nJson\n\"";

            index++; column++;
            LazyJsonString jsonString = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonString, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonString).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonString = (LazyJsonString)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 6);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 25);
            Assert.AreEqual(jsonString.Value, "\nLazy\nVinke\nTests\nJson\n");
        }

        [TestMethod]
        public void ReadString_Text_ScapeSequence_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"\\\rLazy\\\tVinke\\\nTests\\\\Json\n\"";

            index++; column++;
            LazyJsonString jsonString = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonString, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonString).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonString = (LazyJsonString)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 29);
            Assert.AreEqual(jsonString.Value, "\\\rLazy\\\tVinke\\\nTests\\\\Json\n");
        }

        [TestMethod]
        public void ReadString_UnexpectedEnd_Single_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\"Lazy.Vinke.Tests.Json ";

            index++; column++;
            LazyJsonString jsonString = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonString, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonString).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeStringEnd));
        }

        [TestMethod]
        public void ReadIntegerOrDecimalNegative_Integer_OneDigit_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "-1";

            index++; column++;
            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimalNegative", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 3);
            Assert.AreEqual(index, 2);
            Assert.AreEqual(((LazyJsonInteger)jsonToken).Value, -1);
        }

        [TestMethod]
        public void ReadIntegerOrDecimalNegative_Integer_ThreeDigits_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "-101";

            index++; column++;
            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimalNegative", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
            Assert.AreEqual(((LazyJsonInteger)jsonToken).Value, -101);
        }

        [TestMethod]
        public void ReadIntegerOrDecimalNegative_Decimal_OneDecimalPlace_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "-1.1";

            index++; column++;
            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimalNegative", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
            Assert.AreEqual(((LazyJsonDecimal)jsonToken).Value, -1.1m);
        }

        [TestMethod]
        public void ReadIntegerOrDecimalNegative_Decimal_ThreeDecimalPlaces_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "-101.101";

            index++; column++;
            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimalNegative", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 9);
            Assert.AreEqual(index, 8);
            Assert.AreEqual(((LazyJsonDecimal)jsonToken).Value, -101.101m);
        }

        [TestMethod]
        public void ReadIntegerOrDecimalNegative_Decimal_MissingDecimalPlaces_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "-101.";

            index++; column++;
            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimalNegative", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains("Decimal token is missing decimal places"));
        }

        [TestMethod]
        public void ReadIntegerOrDecimal_Integer_OneDigit_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "1";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 1);
            Assert.AreEqual(((LazyJsonInteger)jsonToken).Value, 1);
        }

        [TestMethod]
        public void ReadIntegerOrDecimal_Integer_ThreeDigits_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "101";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 4);
            Assert.AreEqual(index, 3);
            Assert.AreEqual(((LazyJsonInteger)jsonToken).Value, 101);
        }

        [TestMethod]
        public void ReadIntegerOrDecimal_Decimal_OneDecimalPlace_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "1.1";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 4);
            Assert.AreEqual(index, 3);
            Assert.AreEqual(((LazyJsonDecimal)jsonToken).Value, 1.1m);
        }

        [TestMethod]
        public void ReadIntegerOrDecimal_Decimal_ThreeDecimalPlaces_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "101.101";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 8);
            Assert.AreEqual(index, 7);
            Assert.AreEqual(((LazyJsonDecimal)jsonToken).Value, 101.101m);
        }

        [TestMethod]
        public void ReadIntegerOrDecimal_Decimal_MissingDecimalPlaces_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "101.";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadIntegerOrDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains("Decimal token is missing decimal places"));
        }

        [TestMethod]
        public void ReadNullOrBoolean_Null_LowerCase_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "null";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadNullOrBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadNullOrBoolean_Null_PascalCase_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "Null";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadNullOrBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void ReadNullOrBoolean_True_LowerCase_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "true";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadNullOrBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
            Assert.AreEqual(((LazyJsonBoolean)jsonToken).Value, true);
        }

        [TestMethod]
        public void ReadNullOrBoolean_True_PascalCase_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "True";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadNullOrBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
            Assert.AreEqual(((LazyJsonBoolean)jsonToken).Value, true);
        }

        [TestMethod]
        public void ReadNullOrBoolean_False_LowerCase_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "false";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadNullOrBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 5);
            Assert.AreEqual(((LazyJsonBoolean)jsonToken).Value, false);
        }

        [TestMethod]
        public void ReadNullOrBoolean_False_PascalCase_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "False";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadNullOrBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            jsonToken = (LazyJsonToken)parameters[2];
            line = (Int32)parameters[3];
            column = (Int32)parameters[4];
            index = (Int32)parameters[5];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 6);
            Assert.AreEqual(index, 5);
            Assert.AreEqual(((LazyJsonBoolean)jsonToken).Value, false);
        }

        [TestMethod]
        public void ReadNullOrBoolean_UnexpectedToken_Single_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "Lazy";

            LazyJsonToken jsonToken = null;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), jsonToken, line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadNullOrBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(LazyJsonToken).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionTokenTypeNullOrBoolean));
        }

        [TestMethod]
        public void ReadDisposableContent_WithoutComments_OneCharacter_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\n";

            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadDisposableContent", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 1);
            Assert.AreEqual(index, 1);
        }

        [TestMethod]
        public void ReadDisposableContent_WithoutComments_ManyCharacter_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\r\n \t \t \n \r ";

            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadDisposableContent", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 4);
            Assert.AreEqual(index, 11);
        }

        [TestMethod]
        public void ReadDisposableContent_WithCommentsInLine_OneCharacter_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\n//\n";

            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadDisposableContent", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 3);
            Assert.AreEqual(column, 1);
            Assert.AreEqual(index, 4);
        }

        [TestMethod]
        public void ReadDisposableContent_WithCommentsInLine_ManyCharacter_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\n//\n \t\r\n //\n \t\n ";

            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadDisposableContent", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 6);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 16);
        }

        [TestMethod]
        public void ReadDisposableContent_WithCommentsInBlock_OneCharacter_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "\n/**/";

            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadDisposableContent", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 5);
        }

        [TestMethod]
        public void ReadDisposableContent_WithCommentsInBlock_ManyCharacter_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = " /*\nLazy\nVinke*/ \t\r \r\n /*\r\nTests\r\nJson*/\n ";

            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadDisposableContent", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 7);
            Assert.AreEqual(column, 2);
            Assert.AreEqual(index, 42);
        }

        [TestMethod]
        public void ReadDisposableContent_WithoutDisposableContent_Single_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "{\"Solution\":\"Lazy.Vinke.Tests.Json\"}";

            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadDisposableContent", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 1);
            Assert.AreEqual(index, 0);
        }

        [TestMethod]
        public void ReadComments_CommentInLine_WithoutContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "//\n";

            index++; column++;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadComments", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 1);
            Assert.AreEqual(index, 3);
        }

        [TestMethod]
        public void ReadComments_CommentInLine_WithContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "// Lazy.Vinke.Tests.Json \n";

            index++; column++;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadComments", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 1);
            Assert.AreEqual(index, 26);
        }

        [TestMethod]
        public void ReadComments_CommentInBlock_WithoutContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/**/";

            index++; column++;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadComments", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 1);
            Assert.AreEqual(column, 5);
            Assert.AreEqual(index, 4);
        }

        [TestMethod]
        public void ReadComments_CommentInBlock_WithContent_Success()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/* Lazy.Vinke \n Tests.Json */";

            index++; column++;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadComments", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            line = (Int32)parameters[2];
            column = (Int32)parameters[3];
            index = (Int32)parameters[4];

            // Assert
            Assert.AreEqual(line, 2);
            Assert.AreEqual(column, 15);
            Assert.AreEqual(index, 29);
        }

        [TestMethod]
        public void ReadComments_CommentInBlock_Incomplete_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/* Lazy.Vinke \n Tests.Json *";

            index++; column++;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadComments", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionCommentsInBlockEnd));
        }

        [TestMethod]
        public void ReadComments_UnexpectedCharacter_Single_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/ Lazy.Vinke \n Tests.Json */";

            index++; column++;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadComments", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionCommentsInLineOrInBlock));
        }

        [TestMethod]
        public void ReadComments_UnexpectedEnd_Single_Exception()
        {
            // Arrange
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            String json = "/";

            index++; column++;
            Object[] parameters = new Object[] { json, new LazyJsonReaderOptions(), line, column, index };
            MethodInfo methodInfo = typeof(LazyJsonReader).GetMethod("ReadComments", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(String), typeof(LazyJsonReaderOptions), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType(), typeof(Int32).MakeByRefType() });

            // Act
            Exception exception = null;
            try { methodInfo.Invoke(null, parameters); }
            catch (Exception exp) { exception = exp.InnerException; }

            // Assert
            Assert.IsTrue(exception.Message.Contains("LazyJsonReaderException"));
            Assert.IsTrue(exception.Message.Contains(LazyResourcesJson.LazyJsonCaptionCommentsInLineOrInBlock));
        }
    }
}

// TestsLazyJsonWriter.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 06

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
    public class TestsLazyJsonWriter
    {
        [TestMethod]
        public void Write_Complete_JsonSample01_Success()
        {
            // Arrange
            LazyJson lazyJson = LazyJsonReader.Read(Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample01Success));

            // Act
            String json = LazyJsonWriter.Write(lazyJson, new LazyJsonWriterOptions() { Indent = true, IndentSize = 2 });

            // Assert
            Assert.AreEqual(json, Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample01Success));
        }

        [TestMethod]
        public void Write_Complete_JsonSample02_Success()
        {
            // Arrange
            LazyJson lazyJson = LazyJsonReader.Read(Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample02Success));

            // Act
            String json = LazyJsonWriter.Write(lazyJson, new LazyJsonWriterOptions() { Indent = true, IndentSize = 2 });

            // Assert
            Assert.AreEqual(json, Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample02Success));
        }

        [TestMethod]
        public void Write_Complete_JsonSample03_Success()
        {
            // Arrange
            LazyJson lazyJson = LazyJsonReader.Read(Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample03Success));

            // Act
            String json = LazyJsonWriter.Write(lazyJson, new LazyJsonWriterOptions() { Indent = true, IndentSize = 2 });

            // Assert
            Assert.AreEqual(json, Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample03Success));
        }

        [TestMethod]
        public void Write_Complete_JsonSample04_Success()
        {
            // Arrange
            LazyJson lazyJson = LazyJsonReader.Read(Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample04Success));

            // Act
            String json = LazyJsonWriter.Write(lazyJson, new LazyJsonWriterOptions() { Indent = true, IndentSize = 2 });

            // Assert
            Assert.AreEqual(json, Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample04Success));
        }

        [TestMethod]
        public void Write_Complete_JsonSample05_Success()
        {
            // Arrange
            LazyJson lazyJson = LazyJsonReader.Read(Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample05Success));

            // Act
            String json = LazyJsonWriter.Write(lazyJson, new LazyJsonWriterOptions() { Indent = true, IndentSize = 2 });

            // Assert
            Assert.AreEqual(json, Encoding.UTF8.GetString(TestsLazyResourcesJson.JsonSample05Success));
        }

        [TestMethod]
        public void WriteRoot_Null_WithoutIndent_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonNull };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "null");
        }

        [TestMethod]
        public void WriteRoot_Null_WithIndent_Success()
        {
            // Arrange
            LazyJsonNull jsonNull = new LazyJsonNull();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonNull };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "null");
        }

        [TestMethod]
        public void WriteRoot_Boolean_WithoutIndent_Success()
        {
            // Arrange
            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(false);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonBoolean };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "false");
        }

        [TestMethod]
        public void WriteRoot_Boolean_WithIndent_Success()
        {
            // Arrange
            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(true);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonBoolean };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "true");
        }

        [TestMethod]
        public void WriteRoot_IntegerNegative_WithoutIndent_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger(-1);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "-1");
        }

        [TestMethod]
        public void WriteRoot_IntegerNegative_WithIndent_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger(-101);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "-101");
        }

        [TestMethod]
        public void WriteRoot_IntegerPositive_WithoutIndent_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger(101);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "101");
        }

        [TestMethod]
        public void WriteRoot_IntegerPositive_WithIndent_Success()
        {
            // Arrange
            LazyJsonInteger jsonInteger = new LazyJsonInteger(1);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "1");
        }

        [TestMethod]
        public void WriteRoot_DecimalNegative_WithoutIndent_Success()
        {
            // Arrange
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(-101.101m);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "-101.101");
        }

        [TestMethod]
        public void WriteRoot_DecimalNegative_WithIndent_Success()
        {
            // Arrange
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(-1.1m);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "-1.1");
        }

        [TestMethod]
        public void WriteRoot_DecimalPositive_WithoutIndent_Success()
        {
            // Arrange
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(1.1m);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "1.1");
        }

        [TestMethod]
        public void WriteRoot_DecimalPositive_WithIndent_Success()
        {
            // Arrange
            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(101.101m);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "101.101");
        }

        [TestMethod]
        public void WriteRoot_String_WithoutIndent_Success()
        {
            // Arrange
            LazyJsonString jsonString = new LazyJsonString("Lazy.Vinke.Tests.Json");

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonString };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Lazy.Vinke.Tests.Json\"");
        }

        [TestMethod]
        public void WriteRoot_String_WithIndent_Success()
        {
            // Arrange
            LazyJsonString jsonString = new LazyJsonString("Lazy.Vinke.Tests.Json");

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonString };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Lazy.Vinke.Tests.Json\"");
        }

        [TestMethod]
        public void WriteRoot_Object_WithoutIndent_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{}");
        }

        [TestMethod]
        public void WriteRoot_Object_WithIndent_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{}");
        }

        [TestMethod]
        public void WriteRoot_Object_WithIndentEmpty_Success()
        {
            // Arrange
            String newLine = Environment.NewLine;

            LazyJsonObject jsonObject = new LazyJsonObject();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true, IndentEmptyObject = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{" + newLine + "}");
        }

        [TestMethod]
        public void WriteRoot_Array_WithoutIndent_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[]");
        }

        [TestMethod]
        public void WriteRoot_Array_WithIndent_Success()
        {
            // Arrange
            LazyJsonArray jsonArray = new LazyJsonArray();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[]");
        }

        [TestMethod]
        public void WriteRoot_Array_WithIndentEmpty_Success()
        {
            // Arrange
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true, IndentEmptyArray = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteRoot", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonToken) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "]");
        }

        [TestMethod]
        public void WriteBoolean_Null_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonBoolean, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonBoolean), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "null");
        }

        [TestMethod]
        public void WriteBoolean_Null_WithIndentFormat_Success()
        {
            // Arrange
            String format = "        {0}";

            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonBoolean, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonBoolean), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "        null");
        }

        [TestMethod]
        public void WriteBoolean_Valued_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(false);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonBoolean, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonBoolean), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "false");
        }

        [TestMethod]
        public void WriteBoolean_Valued_WithIndentFormat_Success()
        {
            // Arrange
            String format = "    {0}";

            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(true);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonBoolean, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteBoolean", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonBoolean), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "    true");
        }

        [TestMethod]
        public void WriteInteger_Null_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonInteger jsonInteger = new LazyJsonInteger();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteInteger", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonInteger), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "null");
        }

        [TestMethod]
        public void WriteInteger_Null_WithIndentFormat_Success()
        {
            // Arrange
            String format = "  {0}";

            LazyJsonInteger jsonInteger = new LazyJsonInteger();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteInteger", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonInteger), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "  null");
        }

        [TestMethod]
        public void WriteInteger_ValuedNegative_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonInteger jsonInteger = new LazyJsonInteger(-1);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteInteger", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonInteger), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "-1");
        }

        [TestMethod]
        public void WriteInteger_ValuedNegative_WithIndentFormat_Success()
        {
            // Arrange
            String format = "        {0}";

            LazyJsonInteger jsonInteger = new LazyJsonInteger(-101);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteInteger", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonInteger), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "        -101");
        }

        [TestMethod]
        public void WriteInteger_ValuedPositive_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonInteger jsonInteger = new LazyJsonInteger(101);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteInteger", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonInteger), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "101");
        }

        [TestMethod]
        public void WriteInteger_ValuedPositive_WithIndentFormat_Success()
        {
            // Arrange
            String format = "        {0}";

            LazyJsonInteger jsonInteger = new LazyJsonInteger(1);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonInteger, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteInteger", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonInteger), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "        1");
        }

        [TestMethod]
        public void WriteDecimal_Null_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonDecimal), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "null");
        }

        [TestMethod]
        public void WriteDecimal_Null_WithIndentFormat_Success()
        {
            // Arrange
            String format = "  {0}";

            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonDecimal), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "  null");
        }

        [TestMethod]
        public void WriteDecimal_ValuedNegative_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(-101.101m);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonDecimal), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "-101.101");
        }

        [TestMethod]
        public void WriteDecimal_ValuedNegative_WithIndentFormat_Success()
        {
            // Arrange
            String format = "        {0}";

            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(-1.1m);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonDecimal), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "        -1.1");
        }

        [TestMethod]
        public void WriteDecimal_ValuedPositive_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(1.1m);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonDecimal), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "1.1");
        }

        [TestMethod]
        public void WriteDecimal_ValuedPositive_WithIndentFormat_Success()
        {
            // Arrange
            String format = "        {0}";

            LazyJsonDecimal jsonDecimal = new LazyJsonDecimal(101.101m);

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonDecimal, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteDecimal", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonDecimal), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "        101.101");
        }

        [TestMethod]
        public void WriteString_Null_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonString jsonString = new LazyJsonString();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonString, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonString), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "null");
        }

        [TestMethod]
        public void WriteString_Null_WithIndentFormat_Success()
        {
            // Arrange
            String format = "  {0}";

            LazyJsonString jsonString = new LazyJsonString();

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonString, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonString), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "  null");
        }

        [TestMethod]
        public void WriteString_Valued_WithoutIndentFormat_Success()
        {
            // Arrange
            String format = "{0}";

            LazyJsonString jsonString = new LazyJsonString("Lazy.Vinke.Tests.Json");

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonString, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonString), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Lazy.Vinke.Tests.Json\"");
        }

        [TestMethod]
        public void WriteString_Valued_WithIndentFormat_Success()
        {
            // Arrange
            String format = "        {0}";

            LazyJsonString jsonString = new LazyJsonString("Lazy.Vinke.Tests.Json");

            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions();
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonString, format };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteString", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonString), typeof(String) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "        \"Lazy.Vinke.Tests.Json\"");
        }

        [TestMethod]
        public void WriteObject_Empty_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonObject jsonObject = new LazyJsonObject();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{}");
        }

        [TestMethod]
        public void WriteObject_Empty_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonObject jsonObject = new LazyJsonObject();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    {}");
        }

        [TestMethod]
        public void WriteObject_Empty_ArrayIndexWithIndentEmpty_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonObject jsonObject = new LazyJsonObject();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true, IndentEmptyObject = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    {" + newLine + "    }");
        }

        [TestMethod]
        public void WriteObject_Empty_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonObject jsonObject = new LazyJsonObject();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{}");
        }

        [TestMethod]
        public void WriteObject_Empty_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonObject jsonObject = new LazyJsonObject();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{}");
        }

        [TestMethod]
        public void WriteObject_Empty_PropertyWithIndentEmpty_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonObject jsonObject = new LazyJsonObject();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true, IndentEmptyObject = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{" + newLine + "    }");
        }

        [TestMethod]
        public void WriteObject_OneProperty_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Count", new LazyJsonInteger(1)));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{\"Count\":1}");
        }

        [TestMethod]
        public void WriteObject_OneProperty_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Status", new LazyJsonBoolean(false)));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    {" + newLine + "        \"Status\": false" + newLine + "    }");
        }

        [TestMethod]
        public void WriteObject_OneProperty_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Value", new LazyJsonDecimal(1.1m)));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{\"Value\":1.1}");
        }

        [TestMethod]
        public void WriteObject_OneProperty_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Solution", new LazyJsonNull()));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{" + newLine + "        \"Solution\": null" + newLine + "    }");
        }

        [TestMethod]
        public void WriteObject_TwoProperties_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Count", new LazyJsonInteger(1)));
            jsonObject.Add(new LazyJsonProperty("Status", new LazyJsonBoolean(true)));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{\"Count\":1,\"Status\":true}");
        }

        [TestMethod]
        public void WriteObject_TwoProperties_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Status", new LazyJsonBoolean(false)));
            jsonObject.Add(new LazyJsonProperty("Value", new LazyJsonDecimal(1.1m)));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    {" + newLine + "        \"Status\": false," + newLine + "        \"Value\": 1.1" + newLine + "    }");
        }

        [TestMethod]
        public void WriteObject_TwoProperties_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Items", new LazyJsonArray()));
            jsonObject.Add(new LazyJsonProperty("Solution", new LazyJsonString("Lazy.Vinke.Tests.Json")));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{\"Items\":[],\"Solution\":\"Lazy.Vinke.Tests.Json\"}");
        }

        [TestMethod]
        public void WriteObject_TwoProperties_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Data", new LazyJsonObject()));
            jsonObject.Add(new LazyJsonProperty("Items", new LazyJsonArray()));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonObject, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteObject", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonObject), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "{" + newLine + "        \"Data\": {}," + newLine + "        \"Items\": []" + newLine + "    }");
        }

        [TestMethod]
        public void WriteArray_Empty_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[]");
        }

        [TestMethod]
        public void WriteArray_Empty_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    []");
        }

        [TestMethod]
        public void WriteArray_Empty_ArrayIndexWithIndentEmpty_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true, IndentEmptyArray = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_Empty_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[]");
        }

        [TestMethod]
        public void WriteArray_Empty_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[]");
        }

        [TestMethod]
        public void WriteArray_Empty_PropertyWithIndentEmpty_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true, IndentEmptyArray = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenNull_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonNull());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[null]");
        }

        [TestMethod]
        public void WriteArray_TokenNull_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonNull());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "        null" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenNull_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonNull());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[null]");
        }

        [TestMethod]
        public void WriteArray_TokenNull_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonNull());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "        null" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenBoolean_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonBoolean(false));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[false]");
        }

        [TestMethod]
        public void WriteArray_TokenBoolean_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonBoolean(true));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "        true" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenBoolean_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonBoolean(true));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[true]");
        }

        [TestMethod]
        public void WriteArray_TokenBoolean_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonBoolean(false));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "        false" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenIntegerNegative_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(-101));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[-101]");
        }

        [TestMethod]
        public void WriteArray_TokenIntegerNegative_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(-1));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "        -1" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenIntegerNegative_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(-1));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[-1]");
        }

        [TestMethod]
        public void WriteArray_TokenIntegerNegative_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(-101));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "        -101" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenIntegerPositive_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(1));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[1]");
        }

        [TestMethod]
        public void WriteArray_TokenIntegerPositive_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(101));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "        101" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenIntegerPositive_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(101));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[101]");
        }

        [TestMethod]
        public void WriteArray_TokenIntegerPositive_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonInteger(1));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "        1" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenDecimalNegative_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(-101.101m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[-101.101]");
        }

        [TestMethod]
        public void WriteArray_TokenDecimalNegative_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(-1.1m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "        -1.1" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenDecimalNegative_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(-1.1m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[-1.1]");
        }

        [TestMethod]
        public void WriteArray_TokenDecimalNegative_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(-101.101m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "        -101.101" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenDecimalPositive_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(1.1m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[1.1]");
        }

        [TestMethod]
        public void WriteArray_TokenDecimalPositive_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(101.101m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "        101.101" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenDecimalPositive_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(101.101m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[101.101]");
        }

        [TestMethod]
        public void WriteArray_TokenDecimalPositive_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonDecimal(1.1m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "        1.1" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenString_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Lazy.Vinke.Tests.Json"));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[\"Lazy.Vinke.Tests.Json\"]");
        }

        [TestMethod]
        public void WriteArray_TokenString_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Lazy.Vinke.Tests.Json"));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "        \"Lazy.Vinke.Tests.Json\"" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenString_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Lazy.Vinke.Tests.Json"));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[\"Lazy.Vinke.Tests.Json\"]");
        }

        [TestMethod]
        public void WriteArray_TokenString_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonString("Lazy.Vinke.Tests.Json"));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "        \"Lazy.Vinke.Tests.Json\"" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenObject_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonObject());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[{}]");
        }

        [TestMethod]
        public void WriteArray_TokenObject_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonObject());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "        {}" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenObject_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonObject());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[{}]");
        }

        [TestMethod]
        public void WriteArray_TokenObject_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonObject());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "        {}" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenArray_ArrayIndexWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonArray());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[[]]");
        }

        [TestMethod]
        public void WriteArray_TokenArray_ArrayIndexWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonArray());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, true };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    [" + newLine + "        []" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteArray_TokenArray_PropertyWithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonArray());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[[]]");
        }

        [TestMethod]
        public void WriteArray_TokenArray_PropertyWithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonArray jsonArray = new LazyJsonArray();
            jsonArray.Add(new LazyJsonArray());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonArray, indentLevel, false };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteArray", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonArray), typeof(Int32).MakeByRefType(), typeof(Boolean) });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "[" + newLine + "        []" + newLine + "    ]");
        }

        [TestMethod]
        public void WriteProperty_TokenNull_WithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Solution", new LazyJsonNull());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Solution\":null");
        }

        [TestMethod]
        public void WriteProperty_TokenNull_WithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Solution", new LazyJsonNull());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    \"Solution\": null");
        }

        [TestMethod]
        public void WriteProperty_TokenBoolean_WithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Testing", new LazyJsonBoolean(true));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Testing\":true");
        }

        [TestMethod]
        public void WriteProperty_TokenBoolean_WithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Testing", new LazyJsonBoolean(false));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    \"Testing\": false");
        }

        [TestMethod]
        public void WriteProperty_TokenIntegerNegative_WithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Status", new LazyJsonInteger(-101));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Status\":-101");
        }

        [TestMethod]
        public void WriteProperty_TokenIntegerNegative_WithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Status", new LazyJsonInteger(-1));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    \"Status\": -1");
        }

        [TestMethod]
        public void WriteProperty_TokenIntegerPositive_WithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Status", new LazyJsonInteger(1));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Status\":1");
        }

        [TestMethod]
        public void WriteProperty_TokenIntegerPositive_WithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Status", new LazyJsonInteger(101));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    \"Status\": 101");
        }

        [TestMethod]
        public void WriteProperty_TokenDecimalNegative_WithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Value", new LazyJsonDecimal(-1.1m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Value\":-1.1");
        }

        [TestMethod]
        public void WriteProperty_TokenDecimalNegative_WithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Value", new LazyJsonDecimal(-101.101m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    \"Value\": -101.101");
        }

        [TestMethod]
        public void WriteProperty_TokenDecimalPositive_WithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Status", new LazyJsonDecimal(101.101m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Status\":101.101");
        }

        [TestMethod]
        public void WriteProperty_TokenDecimalPositive_WithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Status", new LazyJsonDecimal(1.1m));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    \"Status\": 1.1");
        }

        [TestMethod]
        public void WriteProperty_TokenString_WithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Solution", new LazyJsonString("Lazy.Vinke.Tests.Json"));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Solution\":\"Lazy.Vinke.Tests.Json\"");
        }

        [TestMethod]
        public void WriteProperty_TokenString_WithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Solution", new LazyJsonString("Lazy.Vinke.Tests.Json"));

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    \"Solution\": \"Lazy.Vinke.Tests.Json\"");
        }

        [TestMethod]
        public void WriteProperty_TokenObject_WithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Location", new LazyJsonObject());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Location\":{}");
        }

        [TestMethod]
        public void WriteProperty_TokenObject_WithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Location", new LazyJsonObject());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    \"Location\": {}");
        }

        [TestMethod]
        public void WriteProperty_TokenArray_WithoutIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Items", new LazyJsonArray());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = false };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, "\"Items\":[]");
        }

        [TestMethod]
        public void WriteProperty_TokenArray_WithIndent_Success()
        {
            // Arrange
            Int32 indentLevel = 0;
            String newLine = Environment.NewLine;

            LazyJsonProperty jsonProperty = new LazyJsonProperty("Items", new LazyJsonArray());

            indentLevel++;
            String json = null;
            StringBuilder stringBuilder = new StringBuilder();
            LazyJsonWriterOptions jsonWriterOptions = new LazyJsonWriterOptions() { Indent = true };
            Object[] parameters = new Object[] { stringBuilder, jsonWriterOptions, jsonProperty, indentLevel };
            MethodInfo methodInfo = typeof(LazyJsonWriter).GetMethod("WriteProperty", BindingFlags.NonPublic | BindingFlags.Static,
                new Type[] { typeof(StringBuilder), typeof(LazyJsonWriterOptions), typeof(LazyJsonProperty), typeof(Int32).MakeByRefType() });

            // Act
            methodInfo.Invoke(null, parameters);
            stringBuilder = (StringBuilder)parameters[0];
            json = stringBuilder.ToString();

            // Assert
            Assert.AreEqual(json, newLine + "    \"Items\": []");
        }
    }
}

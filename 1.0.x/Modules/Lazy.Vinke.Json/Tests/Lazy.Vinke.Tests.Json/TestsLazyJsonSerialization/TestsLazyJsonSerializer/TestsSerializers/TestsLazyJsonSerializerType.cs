// TestsLazyJsonSerializerType.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 15

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
    public class TestsLazyJsonSerializerType
    {
        [TestMethod]
        public void Serialize_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerType().Serialize(null);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Valued_Invalid_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonTokenDataSetInvalid = new LazyJsonSerializerType().Serialize(new DataSet());
            LazyJsonToken jsonTokenStringInvalid = new LazyJsonSerializerType().Serialize("Lazy.Vinke.Tests.Json");

            // Assert
            Assert.AreEqual(jsonTokenDataSetInvalid.Type, LazyJsonType.Null);
            Assert.AreEqual(jsonTokenStringInvalid.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void Serialize_Valued_System_Success()
        {
            // Arrange
            Int32 index = 1;
            String solution = "Lazy.Vinke.Tests.Json";

            // Act
            LazyJsonToken jsonTokenIn32 = new LazyJsonSerializerType().Serialize(index.GetType());
            LazyJsonToken jsonTokenString = new LazyJsonSerializerType().Serialize(solution.GetType());
            LazyJsonToken jsonTokenDecimal = new LazyJsonSerializerType().Serialize(typeof(Decimal));
            LazyJsonToken jsonTokenDataTable = new LazyJsonSerializerType().Serialize(typeof(DataTable));
            LazyJsonToken jsonTokenType = new LazyJsonSerializerType().Serialize(typeof(Type));

            // Assert
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenIn32)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenIn32)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenIn32)["Class"].Token).Value, "Int32");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenString)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenString)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenString)["Class"].Token).Value, "String");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenDecimal)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenDecimal)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenDecimal)["Class"].Token).Value, "Decimal");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenDataTable)["Assembly"].Token).Value, "System.Data.Common");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenDataTable)["Namespace"].Token).Value, "System.Data");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenDataTable)["Class"].Token).Value, "DataTable");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenType)["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenType)["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonTokenType)["Class"].Token).Value, "Type");
        }

        [TestMethod]
        public void Serialize_Valued_LazyVinkeJson_Success()
        {
            // Arrange
            Type type = typeof(LazyJsonString);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerType().Serialize(type);

            // Assert
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonToken)["Assembly"].Token).Value, "Lazy.Vinke.Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonToken)["Namespace"].Token).Value, "Lazy.Vinke.Json");
            Assert.AreEqual(((LazyJsonString)((LazyJsonObject)jsonToken)["Class"].Token).Value, "LazyJsonString");
        }

        [TestMethod]
        public void Serialize_Generic_List_Success()
        {
            // Arrange
            Type type = typeof(List<Int32>);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerType().Serialize(type);

            // Assert
            LazyJsonObject jsonTokenList = (LazyJsonObject)jsonToken;
            LazyJsonArray jsonTokenListArgumentTypes = (LazyJsonArray)jsonTokenList["Arguments"].Token;
            LazyJsonObject jsonTokenListArgument0 = (LazyJsonObject)jsonTokenListArgumentTypes[0];

            Assert.AreEqual(((LazyJsonString)jsonTokenList["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenList["Namespace"].Token).Value, "System.Collections.Generic");
            Assert.AreEqual(((LazyJsonString)jsonTokenList["Class"].Token).Value, "List`1");

            Assert.AreEqual(jsonTokenListArgumentTypes.Length, 1);
            Assert.AreEqual(((LazyJsonString)jsonTokenListArgument0["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenListArgument0["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonTokenListArgument0["Class"].Token).Value, "Int32");
        }

        [TestMethod]
        public void Serialize_Generic_ListNested_Success()
        {
            // Arrange
            Type type = typeof(List<List<Int32>>);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerType().Serialize(type);

            // Assert
            LazyJsonObject jsonTokenList = (LazyJsonObject)jsonToken;
            LazyJsonArray jsonTokenListArgumentTypes = (LazyJsonArray)jsonTokenList["Arguments"].Token;
            LazyJsonObject jsonTokenListArgument0 = (LazyJsonObject)jsonTokenListArgumentTypes[0];
            LazyJsonArray jsonTokenListArgument0ArgumentTypes = (LazyJsonArray)jsonTokenListArgument0["Arguments"].Token;
            LazyJsonObject jsonTokenListArgument0Argument0 = (LazyJsonObject)jsonTokenListArgument0ArgumentTypes[0];

            Assert.AreEqual(((LazyJsonString)jsonTokenList["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenList["Namespace"].Token).Value, "System.Collections.Generic");
            Assert.AreEqual(((LazyJsonString)jsonTokenList["Class"].Token).Value, "List`1");

            Assert.AreEqual(jsonTokenListArgumentTypes.Length, 1);
            Assert.AreEqual(((LazyJsonString)jsonTokenListArgument0["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenListArgument0["Namespace"].Token).Value, "System.Collections.Generic");
            Assert.AreEqual(((LazyJsonString)jsonTokenListArgument0["Class"].Token).Value, "List`1");

            Assert.AreEqual(jsonTokenListArgument0ArgumentTypes.Length, 1);
            Assert.AreEqual(((LazyJsonString)jsonTokenListArgument0Argument0["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenListArgument0Argument0["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonTokenListArgument0Argument0["Class"].Token).Value, "Int32");
        }

        [TestMethod]
        public void Serialize_Generic_Dictionary_Success()
        {
            // Arrange
            Type type = typeof(Dictionary<String, DateTime>);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerType().Serialize(type);

            // Assert
            LazyJsonObject jsonTokenDictionary = (LazyJsonObject)jsonToken;
            LazyJsonArray jsonTokenDictionaryArgumentTypes = (LazyJsonArray)jsonTokenDictionary["Arguments"].Token;
            LazyJsonObject jsonTokenDictionaryArgument0 = (LazyJsonObject)jsonTokenDictionaryArgumentTypes[0];
            LazyJsonObject jsonTokenDictionaryArgument1 = (LazyJsonObject)jsonTokenDictionaryArgumentTypes[1];

            Assert.AreEqual(((LazyJsonString)jsonTokenDictionary["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionary["Namespace"].Token).Value, "System.Collections.Generic");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionary["Class"].Token).Value, "Dictionary`2");

            Assert.AreEqual(jsonTokenDictionaryArgumentTypes.Length, 2);
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0["Class"].Token).Value, "String");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1["Class"].Token).Value, "DateTime");
        }

        [TestMethod]
        public void Serialize_Generic_DictionaryNested_Success()
        {
            // Arrange
            Type type = typeof(Dictionary<Dictionary<String, DataTable>, Dictionary<Decimal, Byte>>);

            // Act
            LazyJsonToken jsonToken = new LazyJsonSerializerType().Serialize(type);

            // Assert
            LazyJsonObject jsonTokenDictionary = (LazyJsonObject)jsonToken;
            LazyJsonArray jsonTokenDictionaryArgumentTypes = (LazyJsonArray)jsonTokenDictionary["Arguments"].Token;
            LazyJsonObject jsonTokenDictionaryArgument0 = (LazyJsonObject)jsonTokenDictionaryArgumentTypes[0];
            LazyJsonArray jsonTokenDictionaryArgument0ArgumentTypes = (LazyJsonArray)jsonTokenDictionaryArgument0["Arguments"].Token;
            LazyJsonObject jsonTokenDictionaryArgument0Argument0 = (LazyJsonObject)jsonTokenDictionaryArgument0ArgumentTypes[0];
            LazyJsonObject jsonTokenDictionaryArgument0Argument1 = (LazyJsonObject)jsonTokenDictionaryArgument0ArgumentTypes[1];
            LazyJsonObject jsonTokenDictionaryArgument1 = (LazyJsonObject)jsonTokenDictionaryArgumentTypes[1];
            LazyJsonArray jsonTokenDictionaryArgument1ArgumentTypes = (LazyJsonArray)jsonTokenDictionaryArgument1["Arguments"].Token;
            LazyJsonObject jsonTokenDictionaryArgument1Argument0 = (LazyJsonObject)jsonTokenDictionaryArgument1ArgumentTypes[0];
            LazyJsonObject jsonTokenDictionaryArgument1Argument1 = (LazyJsonObject)jsonTokenDictionaryArgument1ArgumentTypes[1];

            Assert.AreEqual(((LazyJsonString)jsonTokenDictionary["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionary["Namespace"].Token).Value, "System.Collections.Generic");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionary["Class"].Token).Value, "Dictionary`2");

            Assert.AreEqual(jsonTokenDictionaryArgumentTypes.Length, 2);
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0["Namespace"].Token).Value, "System.Collections.Generic");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0["Class"].Token).Value, "Dictionary`2");

            Assert.AreEqual(jsonTokenDictionaryArgument0ArgumentTypes.Length, 2);
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0Argument0["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0Argument0["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0Argument0["Class"].Token).Value, "String");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0Argument1["Assembly"].Token).Value, "System.Data.Common");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0Argument1["Namespace"].Token).Value, "System.Data");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument0Argument1["Class"].Token).Value, "DataTable");

            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1["Namespace"].Token).Value, "System.Collections.Generic");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1["Class"].Token).Value, "Dictionary`2");

            Assert.AreEqual(jsonTokenDictionaryArgument1ArgumentTypes.Length, 2);
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1Argument0["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1Argument0["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1Argument0["Class"].Token).Value, "Decimal");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1Argument1["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1Argument1["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonTokenDictionaryArgument1Argument1["Class"].Token).Value, "Byte");
        }
    }
}

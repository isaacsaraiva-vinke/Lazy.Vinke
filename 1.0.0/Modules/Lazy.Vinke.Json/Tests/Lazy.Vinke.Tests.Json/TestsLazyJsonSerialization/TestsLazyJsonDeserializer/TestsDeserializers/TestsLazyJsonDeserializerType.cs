// TestsLazyJsonDeserializerType.cs
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
using System.ComponentModel.DataAnnotations;

namespace Lazy.Vinke.Tests.Json
{
    [TestClass]
    public class TestsLazyJsonDeserializerType
    {
        [TestMethod]
        public void Deserialize_Null_Single_Success()
        {
            // Arrange
            LazyJsonNull jsonToken = null;
            LazyJsonString jsonString = new LazyJsonString();
            LazyJsonObject jsonObject = new LazyJsonObject();

            // Act
            Object data1 = new LazyJsonDeserializerType().Deserialize(jsonToken, typeof(Type));
            Object data2 = new LazyJsonDeserializerType().Deserialize(jsonString, typeof(Type));
            Object data3 = new LazyJsonDeserializerType().Deserialize(jsonObject, null);
            Object data4 = new LazyJsonDeserializerType().Deserialize(jsonObject, typeof(String));
            Object data5 = new LazyJsonDeserializerType().Deserialize(jsonObject, typeof(DataTable));
            Object data6 = new LazyJsonDeserializerType().Deserialize(jsonObject, typeof(LazyJsonArray));

            // Assert
            Assert.IsNull(data1);
            Assert.IsNull(data2);
            Assert.IsNull(data3);
            Assert.IsNull(data4);
            Assert.IsNull(data5);
            Assert.IsNull(data6);
        }

        [TestMethod]
        public void Deserialize_Valued_Missing_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();

            // Act
            Object data = new LazyJsonDeserializerType().Deserialize(jsonObject, null);

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_Valued_Empty_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Assembly", new LazyJsonString("")));
            jsonObject.Add(new LazyJsonProperty("Namespace", new LazyJsonString("")));
            jsonObject.Add(new LazyJsonProperty("Class", new LazyJsonString("")));

            // Act
            Object data = new LazyJsonDeserializerType().Deserialize(jsonObject, null);

            // Assert
            Assert.IsNull(data);
        }

        [TestMethod]
        public void Deserialize_Valued_System_Success()
        {
            // Arrange
            Type type = typeof(Type);

            LazyJsonObject jsonObjectInt32 = new LazyJsonObject();
            jsonObjectInt32.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectInt32.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectInt32.Add(new LazyJsonProperty("Class", new LazyJsonString("Int32")));

            LazyJsonObject jsonObjectString = new LazyJsonObject();
            jsonObjectString.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectString.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectString.Add(new LazyJsonProperty("Class", new LazyJsonString("String")));

            LazyJsonObject jsonObjectDataTable = new LazyJsonObject();
            jsonObjectDataTable.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Data.Common")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Data")));
            jsonObjectDataTable.Add(new LazyJsonProperty("Class", new LazyJsonString("DataTable")));

            LazyJsonObject jsonObjectType = new LazyJsonObject();
            jsonObjectType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectType.Add(new LazyJsonProperty("Class", new LazyJsonString("Type")));

            // Act
            Object dataInt32 = new LazyJsonDeserializerType().Deserialize(jsonObjectInt32, typeof(Type));
            Object dataString = new LazyJsonDeserializerType().Deserialize(jsonObjectString, type);
            Object dataDataTable = new LazyJsonDeserializerType().Deserialize(jsonObjectDataTable, type);
            Object dataType = new LazyJsonDeserializerType().Deserialize(jsonObjectType, type);

            // Assert
            Assert.AreEqual(dataInt32, typeof(Int32));
            Assert.AreEqual(dataString, typeof(String));
            Assert.AreEqual(dataDataTable, typeof(DataTable));
            Assert.AreEqual(dataType, typeof(Type));
        }

        [TestMethod]
        public void Deserialize_Valued_LazyVinkeJson_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Assembly", new LazyJsonString("Lazy.Vinke.Json")));
            jsonObject.Add(new LazyJsonProperty("Namespace", new LazyJsonString("Lazy.Vinke.Json")));
            jsonObject.Add(new LazyJsonProperty("Class", new LazyJsonString("LazyJsonInteger")));

            // Act
            Object data = new LazyJsonDeserializerType().Deserialize(jsonObject, typeof(Type));

            // Assert
            Assert.AreEqual(data, typeof(LazyJsonInteger));
        }

        [TestMethod]
        public void Deserialize_Generic_List_Success()
        {
            // Arrange
            LazyJsonObject tokenListArgument0 = new LazyJsonObject();
            tokenListArgument0.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenListArgument0.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            tokenListArgument0.Add(new LazyJsonProperty("Class", new LazyJsonString("Int32")));
            LazyJsonArray tokenListArgumentTypes = new LazyJsonArray();
            tokenListArgumentTypes.Add(tokenListArgument0);

            LazyJsonObject tokenList = new LazyJsonObject();
            tokenList.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenList.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Collections.Generic")));
            tokenList.Add(new LazyJsonProperty("Class", new LazyJsonString("List`1")));
            tokenList.Add(new LazyJsonProperty("Arguments", tokenListArgumentTypes));

            // Act
            Object data = new LazyJsonDeserializerType().Deserialize(tokenList, typeof(Type));

            // Assert
            Assert.AreEqual(data, typeof(List<Int32>));
        }

        [TestMethod]
        public void Deserialize_Generic_ListNested_Success()
        {
            // Arrange
            LazyJsonObject tokenListArgument0Argument0 = new LazyJsonObject();
            tokenListArgument0Argument0.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenListArgument0Argument0.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            tokenListArgument0Argument0.Add(new LazyJsonProperty("Class", new LazyJsonString("String")));
            LazyJsonArray tokenListArgument0ArgumentTypes = new LazyJsonArray();
            tokenListArgument0ArgumentTypes.Add(tokenListArgument0Argument0);

            LazyJsonObject tokenListArgument0 = new LazyJsonObject();
            tokenListArgument0.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenListArgument0.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Collections.Generic")));
            tokenListArgument0.Add(new LazyJsonProperty("Class", new LazyJsonString("List`1")));
            tokenListArgument0.Add(new LazyJsonProperty("Arguments", tokenListArgument0ArgumentTypes));
            LazyJsonArray tokenListArgumentTypes = new LazyJsonArray();
            tokenListArgumentTypes.Add(tokenListArgument0);

            LazyJsonObject tokenList = new LazyJsonObject();
            tokenList.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenList.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Collections.Generic")));
            tokenList.Add(new LazyJsonProperty("Class", new LazyJsonString("List`1")));
            tokenList.Add(new LazyJsonProperty("Arguments", tokenListArgumentTypes));

            // Act
            Object data = new LazyJsonDeserializerType().Deserialize(tokenList, typeof(Type));

            // Assert
            Assert.AreEqual(data, typeof(List<List<String>>));
        }

        [TestMethod]
        public void Deserialize_Generic_Dictionary_Success()
        {
            // Arrange
            LazyJsonObject tokenDictionaryArgument0 = new LazyJsonObject();
            tokenDictionaryArgument0.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenDictionaryArgument0.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            tokenDictionaryArgument0.Add(new LazyJsonProperty("Class", new LazyJsonString("SByte")));
            LazyJsonObject tokenDictionaryArgument1 = new LazyJsonObject();
            tokenDictionaryArgument1.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Data.Common")));
            tokenDictionaryArgument1.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Data")));
            tokenDictionaryArgument1.Add(new LazyJsonProperty("Class", new LazyJsonString("DataTable")));
            LazyJsonArray tokenDictionaryArgumentTypes = new LazyJsonArray();
            tokenDictionaryArgumentTypes.Add(tokenDictionaryArgument0);
            tokenDictionaryArgumentTypes.Add(tokenDictionaryArgument1);

            LazyJsonObject tokenDictionary = new LazyJsonObject();
            tokenDictionary.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenDictionary.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Collections.Generic")));
            tokenDictionary.Add(new LazyJsonProperty("Class", new LazyJsonString("Dictionary`2")));
            tokenDictionary.Add(new LazyJsonProperty("Arguments", tokenDictionaryArgumentTypes));

            // Act
            Object data = new LazyJsonDeserializerType().Deserialize(tokenDictionary, typeof(Type));

            // Assert
            Assert.AreEqual(data, typeof(Dictionary<SByte, DataTable>));
        }

        [TestMethod]
        public void Deserialize_Generic_DictionaryNested_Success()
        {
            // Arrange
            LazyJsonObject tokenDictionaryArgument0Argument0 = new LazyJsonObject();
            tokenDictionaryArgument0Argument0.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenDictionaryArgument0Argument0.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            tokenDictionaryArgument0Argument0.Add(new LazyJsonProperty("Class", new LazyJsonString("Char")));
            LazyJsonObject tokenDictionaryArgument0Argument1 = new LazyJsonObject();
            tokenDictionaryArgument0Argument1.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenDictionaryArgument0Argument1.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            tokenDictionaryArgument0Argument1.Add(new LazyJsonProperty("Class", new LazyJsonString("Boolean")));
            LazyJsonArray tokenDictionaryArgument0ArgumentTypes = new LazyJsonArray();
            tokenDictionaryArgument0ArgumentTypes.Add(tokenDictionaryArgument0Argument0);
            tokenDictionaryArgument0ArgumentTypes.Add(tokenDictionaryArgument0Argument1);

            LazyJsonObject tokenDictionaryArgument0 = new LazyJsonObject();
            tokenDictionaryArgument0.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenDictionaryArgument0.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Collections.Generic")));
            tokenDictionaryArgument0.Add(new LazyJsonProperty("Class", new LazyJsonString("Dictionary`2")));
            tokenDictionaryArgument0.Add(new LazyJsonProperty("Arguments", tokenDictionaryArgument0ArgumentTypes));

            LazyJsonObject tokenDictionaryArgument1Argument0 = new LazyJsonObject();
            tokenDictionaryArgument1Argument0.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenDictionaryArgument1Argument0.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            tokenDictionaryArgument1Argument0.Add(new LazyJsonProperty("Class", new LazyJsonString("UInt32")));
            LazyJsonObject tokenDictionaryArgument1Argument1 = new LazyJsonObject();
            tokenDictionaryArgument1Argument1.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenDictionaryArgument1Argument1.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            tokenDictionaryArgument1Argument1.Add(new LazyJsonProperty("Class", new LazyJsonString("Double")));
            LazyJsonArray tokenDictionaryArgument1ArgumentTypes = new LazyJsonArray();
            tokenDictionaryArgument1ArgumentTypes.Add(tokenDictionaryArgument1Argument0);
            tokenDictionaryArgument1ArgumentTypes.Add(tokenDictionaryArgument1Argument1);

            LazyJsonObject tokenDictionaryArgument1 = new LazyJsonObject();
            tokenDictionaryArgument1.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenDictionaryArgument1.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Collections.Generic")));
            tokenDictionaryArgument1.Add(new LazyJsonProperty("Class", new LazyJsonString("Dictionary`2")));
            tokenDictionaryArgument1.Add(new LazyJsonProperty("Arguments", tokenDictionaryArgument1ArgumentTypes));

            LazyJsonArray tokenDictionaryArgumentTypes = new LazyJsonArray();
            tokenDictionaryArgumentTypes.Add(tokenDictionaryArgument0);
            tokenDictionaryArgumentTypes.Add(tokenDictionaryArgument1);

            LazyJsonObject tokenDictionary = new LazyJsonObject();
            tokenDictionary.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            tokenDictionary.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System.Collections.Generic")));
            tokenDictionary.Add(new LazyJsonProperty("Class", new LazyJsonString("Dictionary`2")));
            tokenDictionary.Add(new LazyJsonProperty("Arguments", tokenDictionaryArgumentTypes));

            // Act
            Object data = new LazyJsonDeserializerType().Deserialize(tokenDictionary, typeof(Type));

            // Assert
            Assert.AreEqual(data, typeof(Dictionary<Dictionary<Char, Boolean>, Dictionary<UInt32, Double>>));
        }
    }
}

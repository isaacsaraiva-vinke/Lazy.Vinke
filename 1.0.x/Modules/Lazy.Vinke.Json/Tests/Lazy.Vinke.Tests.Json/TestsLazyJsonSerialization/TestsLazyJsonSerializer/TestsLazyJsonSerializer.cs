// TestsLazyJsonSerializer.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 09

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
    public class TestsLazyJsonSerializer
    {
        [TestMethod]
        public void SerializeToken_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = LazyJsonSerializer.SerializeToken(null);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void SerializeToken_SelectSerializerType_KnownType_Success()
        {
            // Arrange
            Int32 index = -1;
            Decimal amount = 101.101m;
            Type type = typeof(String);

            // Act
            LazyJsonToken jsonTokenInteger = LazyJsonSerializer.SerializeToken(index);
            LazyJsonToken jsonTokenDecimal = LazyJsonSerializer.SerializeToken(amount);
            LazyJsonToken jsonTokenType = LazyJsonSerializer.SerializeToken(type);

            // Assert
            Assert.AreEqual(jsonTokenInteger.Type, LazyJsonType.Integer);
            Assert.AreEqual(jsonTokenDecimal.Type, LazyJsonType.Decimal);
            Assert.AreEqual(jsonTokenType.Type, LazyJsonType.Object);
        }

        [TestMethod]
        public void SerializeToken_SelectSerializerType_UnknownType_Success()
        {
            // Arrange
            SerializeToken_SelectSerializerType_UnknownType serializeToken_SelectSerializerType_UnknownType = new SerializeToken_SelectSerializerType_UnknownType();
            serializeToken_SelectSerializerType_UnknownType.Id = -1;
            serializeToken_SelectSerializerType_UnknownType.Code = "SerializeToken_SelectSerializerType_UnknownType";
            serializeToken_SelectSerializerType_UnknownType.Amount = 1.1m;
            serializeToken_SelectSerializerType_UnknownType.Tested = false;

            // Act
            LazyJsonObject jsonObject = (LazyJsonObject)LazyJsonSerializer.SerializeToken(serializeToken_SelectSerializerType_UnknownType);

            // Assert
            Assert.AreEqual(jsonObject.Count, 3);
            Assert.AreEqual(jsonObject[0].Name, "Id");
            Assert.AreEqual(jsonObject[0].Token.Type, LazyJsonType.Integer);
            Assert.AreEqual(jsonObject[1].Name, "InternalCode");
            Assert.AreEqual(jsonObject[1].Token.Type, LazyJsonType.String);
            Assert.AreEqual(jsonObject[2].Name, "Amount");
            Assert.AreEqual(jsonObject[2].Token.Type, LazyJsonType.Decimal);
        }

        [TestMethod]
        public void SerializeObject_Null_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonToken jsonToken = LazyJsonSerializer.SerializeObject(null);

            // Assert
            Assert.AreEqual(jsonToken.Type, LazyJsonType.Null);
        }

        [TestMethod]
        public void SerializeObject_Empty_Single_Success()
        {
            // Arrange

            // Act
            LazyJsonObject jsonObject = (LazyJsonObject)LazyJsonSerializer.SerializeObject(new SerializeObject_Empty_Single());

            // Assert
            Assert.AreEqual(jsonObject.Count, 0);
        }

        [TestMethod]
        public void SerializeObject_Simple_WithAttributes_Success()
        {
            // Arrange
            SerializeObject_Simple_WithAttributes serializeObject_Simple_WithAttributes = new SerializeObject_Simple_WithAttributes();
            serializeObject_Simple_WithAttributes.Id = 101;
            serializeObject_Simple_WithAttributes.InternalCode = "SerializeObject_Simple_WithAttributes_Success";
            serializeObject_Simple_WithAttributes.Amount = 101.101m;
            serializeObject_Simple_WithAttributes.Tested = true;

            // Act
            LazyJsonObject jsonObject = (LazyJsonObject)LazyJsonSerializer.SerializeObject(serializeObject_Simple_WithAttributes);

            // Assert
            Assert.AreEqual(jsonObject.Count, 3);
            Assert.AreEqual(jsonObject[0].Name, "Id");
            Assert.AreEqual(jsonObject[0].Token.Type, LazyJsonType.Integer);
            Assert.AreEqual(jsonObject[1].Name, "Code");
            Assert.AreEqual(jsonObject[1].Token.Type, LazyJsonType.String);
            Assert.AreEqual(jsonObject[2].Name, "Tested");
            Assert.AreEqual(jsonObject[2].Token.Type, LazyJsonType.Boolean);
        }

        [TestMethod]
        public void SerializeObject_PropertyAsObject_Simple_Success()
        {
            // Arrange
            SerializeObject_PropertyAsObject_Simple propertyAsObject_Decimal = new SerializeObject_PropertyAsObject_Simple();
            propertyAsObject_Decimal.SomeObject = 101.101m;

            // Act
            LazyJsonObject jsonObject = (LazyJsonObject)LazyJsonSerializer.SerializeObject(propertyAsObject_Decimal);

            // Assert
            LazyJsonObject jsonObjectType = (LazyJsonObject)((LazyJsonObject)jsonObject["SomeObject"].Token)["Type"].Token;
            LazyJsonToken jsonTokenValue = ((LazyJsonObject)jsonObject["SomeObject"].Token)["Value"].Token;

            Assert.AreEqual(((LazyJsonString)jsonObjectType["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectType["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectType["Class"].Token).Value, "Decimal");
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenValue).Value, 101.101m);
        }

        [TestMethod]
        public void SerializeObject_PropertyAsObject_Nested_Success()
        {
            // Arrange
            SerializeObject_PropertyAsObject_Nested netedPropertyAsObject_Decimal = new SerializeObject_PropertyAsObject_Nested();
            netedPropertyAsObject_Decimal.NestedObject = new SerializeObject_PropertyAsObject_Simple();
            netedPropertyAsObject_Decimal.NestedObject.SomeObject = 101.101m;

            // Act
            LazyJsonObject jsonObject = (LazyJsonObject)LazyJsonSerializer.SerializeObject(netedPropertyAsObject_Decimal);
            String jSon = LazyJsonWriter.Write(new LazyJson(jsonObject));
            // Assert
            LazyJsonObject jsonObjectType = (LazyJsonObject)((LazyJsonObject)((LazyJsonObject)jsonObject["NestedObject"].Token)["SomeObject"].Token)["Type"].Token;
            LazyJsonToken jsonTokenValue = ((LazyJsonObject)((LazyJsonObject)jsonObject["NestedObject"].Token)["SomeObject"].Token)["Value"].Token;

            Assert.AreEqual(((LazyJsonString)jsonObjectType["Assembly"].Token).Value, "System.Private.CoreLib");
            Assert.AreEqual(((LazyJsonString)jsonObjectType["Namespace"].Token).Value, "System");
            Assert.AreEqual(((LazyJsonString)jsonObjectType["Class"].Token).Value, "Decimal");
            Assert.AreEqual(((LazyJsonDecimal)jsonTokenValue).Value, 101.101m);
        }

        [TestMethod]
        public void SelectSerializerType_Null_Single_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(null);

            // Assert
            Assert.IsNull(jsonSerializerType);
        }

        [TestMethod]
        public void SelectSerializerType_SerializerClass_WithoutOptions_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(typeof(SelectSerializerType_SerializerClass_WithoutOptions));

            // Assert
            Assert.AreEqual(jsonSerializerType, typeof(LazyJsonSerializerDateTime));
        }

        [TestMethod]
        public void SelectSerializerType_SerializerClass_WithOptions_Success()
        {
            // Arrange
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsGlobal>().Add<LazyJsonSerializerInteger>(typeof(SelectSerializerType_SerializerClass_WithOptions));

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(typeof(SelectSerializerType_SerializerClass_WithOptions), jsonSerializerOptions);

            // Assert
            Assert.AreEqual(jsonSerializerType, typeof(LazyJsonSerializerString));
        }

        [TestMethod]
        public void SelectSerializerType_SerializerOptions_WithoutAttribute_Success()
        {
            // Arrange
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsGlobal>().Add<LazyJsonSerializerString>(typeof(SelectSerializerType_SerializerOptions_WithoutAttribute));

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(typeof(SelectSerializerType_SerializerOptions_WithoutAttribute), jsonSerializerOptions);

            // Assert
            Assert.AreEqual(jsonSerializerType, typeof(LazyJsonSerializerString));
        }

        [TestMethod]
        public void SelectSerializerType_SerializerOptions_WithAttribute_Success()
        {
            // Arrange
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsGlobal>().Add<LazyJsonSerializerString>(typeof(SelectSerializerType_SerializerOptions_WithAttribute));

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(typeof(SelectSerializerType_SerializerOptions_WithAttribute), jsonSerializerOptions);

            // Assert
            Assert.AreEqual(jsonSerializerType, typeof(LazyJsonSerializerDecimal));
        }

        [TestMethod]
        public void SelectSerializerType_SerializerBuiltIn_WithoutOptions_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(typeof(Int32));

            // Assert
            Assert.AreEqual(jsonSerializerType, typeof(LazyJsonSerializerInteger));
        }

        [TestMethod]
        public void SelectSerializerType_SerializerBuiltIn_WithOptions_Success()
        {
            // Arrange
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsGlobal>().Add<LazyJsonSerializerDecimal>(typeof(Int32));

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerType(typeof(Int32), jsonSerializerOptions);

            // Assert
            Assert.AreEqual(jsonSerializerType, typeof(LazyJsonSerializerDecimal));
        }

        [TestMethod]
        public void SelectSerializerTypeClass_Null_Single_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerTypeClass(null);

            // Assert
            Assert.IsNull(jsonSerializerType);
        }

        [TestMethod]
        public void SelectSerializerTypeClass_GetCustomAttributes_WithoutAttribute_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerTypeClass(typeof(SelectSerializerTypeClass_GetCustomAttributes_WithoutAttribute));

            // Assert
            Assert.IsNull(jsonSerializerType);
        }

        [TestMethod]
        public void SelectSerializerTypeClass_GetCustomAttributes_WithAttributeNull_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerTypeClass(typeof(SelectSerializerTypeClass_GetCustomAttributes_WithAttributeNull));

            // Assert
            Assert.IsNull(jsonSerializerType);
        }

        [TestMethod]
        public void SelectSerializerTypeClass_GetCustomAttributes_WithAttributeInvalid_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerTypeClass(typeof(SelectSerializerTypeClass_GetCustomAttributes_WithAttributeInvalid));

            // Assert
            Assert.IsNull(jsonSerializerType);
        }

        [TestMethod]
        public void SelectSerializerTypeClass_GetCustomAttributes_WithAttributeValid_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerTypeClass(typeof(SelectSerializerTypeClass_GetCustomAttributes_WithAttributeValid));

            // Assert
            Assert.AreEqual(jsonSerializerType, typeof(LazyJsonSerializerDecimal));
        }

        [TestMethod]
        public void SelectSerializerTypeOptions_Null_Single_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerTypeOptions(null);

            // Assert
            Assert.IsNull(jsonSerializerType);
        }

        [TestMethod]
        public void SelectSerializerTypeOptions_Options_Empty_Success()
        {
            // Arrange
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerTypeOptions(typeof(Int32), jsonSerializerOptions);

            // Assert
            Assert.IsNull(jsonSerializerType);
        }

        [TestMethod]
        public void SelectSerializerTypeOptions_Options_OptionsGlobalEmpty_Success()
        {
            // Arrange
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            jsonSerializerOptions.Item<LazyJsonSerializerOptionsGlobal>();

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerTypeOptions(typeof(Int32), jsonSerializerOptions);

            // Assert
            Assert.IsNull(jsonSerializerType);
        }

        [TestMethod]
        public void SelectSerializerTypeOptions_Options_OptionsGlobalValued_Success()
        {
            // Arrange
            LazyJsonSerializerOptions jsonSerializerOptions = new LazyJsonSerializerOptions();
            LazyJsonSerializerOptionsGlobal jsonSerializerOptionsGlobal = jsonSerializerOptions.Item<LazyJsonSerializerOptionsGlobal>();
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerInteger>(typeof(Int32));
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerInteger>(typeof(Int16));
            jsonSerializerOptionsGlobal.Add<LazyJsonSerializerDecimal>(typeof(Decimal));

            // Act
            Type jsonSerializerTypeInt32 = LazyJsonSerializer.SelectSerializerTypeOptions(typeof(Int32), jsonSerializerOptions);
            Type jsonSerializerTypeInt16 = LazyJsonSerializer.SelectSerializerTypeOptions(typeof(Int16), jsonSerializerOptions);
            Type jsonSerializerTypeDecimal = LazyJsonSerializer.SelectSerializerTypeOptions(typeof(Decimal), jsonSerializerOptions);

            // Assert
            Assert.AreEqual(jsonSerializerTypeInt32, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeInt16, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeDecimal, typeof(LazyJsonSerializerDecimal));
        }

        [TestMethod]
        public void SelectSerializerTypeBuiltIn_Null_Single_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerType = LazyJsonSerializer.SelectSerializerTypeBuiltIn(null);

            // Assert
            Assert.IsNull(jsonSerializerType);
        }

        [TestMethod]
        public void SelectSerializerTypeBuiltIn_AllTypes_Single_Success()
        {
            // Arrange

            // Act
            Type jsonSerializerTypeString = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(String));
            Type jsonSerializerTypeInt32 = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Int32));
            Type jsonSerializerTypeDecimal = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Decimal));
            Type jsonSerializerTypeDateTime = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(DateTime));
            Type jsonSerializerTypeBoolean = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Boolean));
            Type jsonSerializerTypeInt32Nullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<Int32>));
            Type jsonSerializerTypeDecimalNullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<Decimal>));
            Type jsonSerializerTypeDateTimeNullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<DateTime>));
            Type jsonSerializerTypeBooleanNullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<Boolean>));
            Type jsonSerializerTypeArray = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Object[]));
            Type jsonSerializerTypeInt16 = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Int16));
            Type jsonSerializerTypeInt64 = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Int64));
            Type jsonSerializerTypeDouble = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Double));
            Type jsonSerializerTypeSingle = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Single));
            Type jsonSerializerTypeChar = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Char));
            Type jsonSerializerTypeByte = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Byte));
            Type jsonSerializerTypeSByte = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(SByte));
            Type jsonSerializerTypeUInt32 = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(UInt32));
            Type jsonSerializerTypeUInt16 = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(UInt16));
            Type jsonSerializerTypeInt16Nullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<Int16>));
            Type jsonSerializerTypeInt64Nullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<Int64>));
            Type jsonSerializerTypeDoubleNullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<Double>));
            Type jsonSerializerTypeSingleNullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<Single>));
            Type jsonSerializerTypeCharNullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<Char>));
            Type jsonSerializerTypeByteNullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<Byte>));
            Type jsonSerializerTypeSByteNullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<SByte>));
            Type jsonSerializerTypeUInt32Nullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<UInt32>));
            Type jsonSerializerTypeUInt16Nullable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Nullable<UInt16>));
            Type jsonSerializerTypeDataTable = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(DataTable));
            Type jsonSerializerTypeList = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(List<Object>));
            Type jsonSerializerTypeDictionary = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Dictionary<Object, Object>));
            Type jsonSerializerTypeOfType = LazyJsonSerializer.SelectSerializerTypeBuiltIn(typeof(Type));

            // Assert
            Assert.AreEqual(jsonSerializerTypeString, typeof(LazyJsonSerializerString));
            Assert.AreEqual(jsonSerializerTypeInt32, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeDecimal, typeof(LazyJsonSerializerDecimal));
            Assert.AreEqual(jsonSerializerTypeDateTime, typeof(LazyJsonSerializerDateTime));
            Assert.AreEqual(jsonSerializerTypeBoolean, typeof(LazyJsonSerializerBoolean));
            Assert.AreEqual(jsonSerializerTypeInt32Nullable, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeDecimalNullable, typeof(LazyJsonSerializerDecimal));
            Assert.AreEqual(jsonSerializerTypeDateTimeNullable, typeof(LazyJsonSerializerDateTime));
            Assert.AreEqual(jsonSerializerTypeBooleanNullable, typeof(LazyJsonSerializerBoolean));
            Assert.AreEqual(jsonSerializerTypeArray, typeof(LazyJsonSerializerArray));
            Assert.AreEqual(jsonSerializerTypeInt16, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeInt64, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeDouble, typeof(LazyJsonSerializerDecimal));
            Assert.AreEqual(jsonSerializerTypeSingle, typeof(LazyJsonSerializerDecimal));
            Assert.AreEqual(jsonSerializerTypeChar, typeof(LazyJsonSerializerString));
            Assert.AreEqual(jsonSerializerTypeByte, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeSByte, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeUInt32, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeUInt16, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeInt16Nullable, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeInt64Nullable, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeDoubleNullable, typeof(LazyJsonSerializerDecimal));
            Assert.AreEqual(jsonSerializerTypeSingleNullable, typeof(LazyJsonSerializerDecimal));
            Assert.AreEqual(jsonSerializerTypeCharNullable, typeof(LazyJsonSerializerString));
            Assert.AreEqual(jsonSerializerTypeByteNullable, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeSByteNullable, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeUInt32Nullable, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeUInt16Nullable, typeof(LazyJsonSerializerInteger));
            Assert.AreEqual(jsonSerializerTypeDataTable, typeof(LazyJsonSerializerDataTable));
            Assert.AreEqual(jsonSerializerTypeList, typeof(LazyJsonSerializerList));
            Assert.AreEqual(jsonSerializerTypeDictionary, typeof(LazyJsonSerializerDictionary));
            Assert.AreEqual(jsonSerializerTypeOfType, typeof(LazyJsonSerializerType));
        }
    }
}

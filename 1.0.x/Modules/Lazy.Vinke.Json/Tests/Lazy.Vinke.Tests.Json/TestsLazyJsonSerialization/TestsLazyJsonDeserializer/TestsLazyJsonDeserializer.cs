// TestsLazyJsonDeserializer.cs
//
// This file is integrated part of "Lazy Vinke Tests Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 10

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

using Lazy.Vinke.Json;
using Lazy.Vinke.Json.Properties;
using Lazy.Vinke.Tests.Json.Properties;
using System.Data.Common;

namespace Lazy.Vinke.Tests.Json
{
    [TestClass]
    public class TestsLazyJsonDeserializer
    {
        [TestMethod]
        public void DeserializeProperty_Null_Single_Success()
        {
            // Arrange

            // Act
            String deserializedString = LazyJsonDeserializer.DeserializeProperty<String>(null);
            Nullable<Int32> deserializedIntegerNullable = LazyJsonDeserializer.DeserializeProperty<Nullable<Int32>>(null);
            Nullable<Boolean> deserializedBooleanNullable = LazyJsonDeserializer.DeserializeProperty<Nullable<Boolean>>(null);

            // Assert
            Assert.AreEqual(deserializedString, null);
            Assert.AreEqual(deserializedIntegerNullable, null);
            Assert.AreEqual(deserializedBooleanNullable, null);
        }

        [TestMethod]
        public void DeserializeProperty_SelectDeserializerType_knownType_Success()
        {
            // Arrange
            LazyJsonString jsonString = new LazyJsonString("DeserializeProperty_SelectDeserializerType_knownType_Success");
            LazyJsonInteger jsonInteger = new LazyJsonInteger(101);
            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(true);

            // Act
            String deserializedString = LazyJsonDeserializer.DeserializeProperty<String>(new LazyJsonProperty("StringProperty", jsonString));
            Int32 deserializedInteger = LazyJsonDeserializer.DeserializeProperty<Int32>(new LazyJsonProperty("IntegerProperty", jsonInteger));
            Boolean deserializedBoolean = LazyJsonDeserializer.DeserializeProperty<Boolean>(new LazyJsonProperty("BooleanProperty", jsonBoolean));

            // Assert
            Assert.AreEqual(deserializedString, "DeserializeProperty_SelectDeserializerType_knownType_Success");
            Assert.AreEqual(deserializedInteger, 101);
            Assert.AreEqual(deserializedBoolean, true);
        }

        [TestMethod]
        public void DeserializeProperty_SelectDeserializerType_UnknownType_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Id", new LazyJsonInteger(-1)));
            jsonObject.Add(new LazyJsonProperty("Code", new LazyJsonString("DeserializeProperty_SelectDeserializerType_UnknownType_Success")));
            jsonObject.Add(new LazyJsonProperty("Tested", new LazyJsonBoolean(false)));
            LazyJsonProperty jsonProperty = new LazyJsonProperty("Property", jsonObject);

            // Act
            DeserializeProperty_SelectDeserializerType_UnknownType deserializeProperty_SelectDeserializerType_UnknownType = LazyJsonDeserializer.DeserializeProperty<DeserializeProperty_SelectDeserializerType_UnknownType>(jsonProperty);

            // Assert
            Assert.AreEqual(deserializeProperty_SelectDeserializerType_UnknownType.Id, -1);
            Assert.AreEqual(deserializeProperty_SelectDeserializerType_UnknownType.InternalCode, "DeserializeProperty_SelectDeserializerType_UnknownType_Success");
            Assert.AreEqual(deserializeProperty_SelectDeserializerType_UnknownType.Tested, false);
        }

        [TestMethod]
        public void DeserializeToken_Null_Single_Success()
        {
            // Arrange

            // Act
            String deserializedString = LazyJsonDeserializer.DeserializeToken<String>(null);
            Nullable<Int32> deserializedIntegerNullable = LazyJsonDeserializer.DeserializeToken<Nullable<Int32>>(null);
            Nullable<Boolean> deserializedBooleanNullable = LazyJsonDeserializer.DeserializeToken<Nullable<Boolean>>(null);

            // Assert
            Assert.AreEqual(deserializedString, null);
            Assert.AreEqual(deserializedIntegerNullable, null);
            Assert.AreEqual(deserializedBooleanNullable, null);
        }

        [TestMethod]
        public void DeserializeToken_SelectDeserializerType_KnownType_Success()
        {
            // Arrange
            LazyJsonString jsonString = new LazyJsonString("DeserializeToken_SelectDeserializerType_KnownType_Success");
            LazyJsonInteger jsonInteger = new LazyJsonInteger(-1);
            LazyJsonBoolean jsonBoolean = new LazyJsonBoolean(false);
            LazyJsonObject jsonObjectType = new LazyJsonObject();
            jsonObjectType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectType.Add(new LazyJsonProperty("Class", new LazyJsonString("String")));

            // Act
            String deserializedString = LazyJsonDeserializer.DeserializeToken<String>(jsonString);
            Int32 deserializedInteger = LazyJsonDeserializer.DeserializeToken<Int32>(jsonInteger);
            Boolean deserializedBoolean = LazyJsonDeserializer.DeserializeToken<Boolean>(jsonBoolean);
            Type deserializedType = LazyJsonDeserializer.DeserializeToken<Type>(jsonObjectType);

            // Assert
            Assert.AreEqual(deserializedString, "DeserializeToken_SelectDeserializerType_KnownType_Success");
            Assert.AreEqual(deserializedInteger, -1);
            Assert.AreEqual(deserializedBoolean, false);
            Assert.AreEqual(deserializedType, typeof(String));
        }

        [TestMethod]
        public void DeserializeToken_SelectDeserializerType_UnknownType_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Id", new LazyJsonInteger(1)));
            jsonObject.Add(new LazyJsonProperty("InternalCode", new LazyJsonString("DeserializeToken_SelectDeserializerType_UnknownType_Success")));
            jsonObject.Add(new LazyJsonProperty("Amount", new LazyJsonDecimal(-101.101m)));

            // Act
            DeserializeToken_SelectDeserializerType_UnknownType deserializeToken_SelectDeserializerType_UnknownType = LazyJsonDeserializer.DeserializeToken<DeserializeToken_SelectDeserializerType_UnknownType>(jsonObject);

            // Assert
            Assert.AreEqual(deserializeToken_SelectDeserializerType_UnknownType.Id, 1);
            Assert.AreEqual(deserializeToken_SelectDeserializerType_UnknownType.Code, "DeserializeToken_SelectDeserializerType_UnknownType_Success");
            Assert.AreEqual(deserializeToken_SelectDeserializerType_UnknownType.Amount, -101.101m);
        }

        [TestMethod]
        public void DeserializeObject_Null_Single_Success()
        {
            // Arrange

            // Act
            DeserializeObject_Null_Single deserializeObject_Null_Single_1 = LazyJsonDeserializer.DeserializeTokenObject<DeserializeObject_Null_Single>(null);
            DeserializeObject_Null_Single deserializeObject_Null_Single_2 = (DeserializeObject_Null_Single)LazyJsonDeserializer.DeserializeTokenObject(new LazyJsonObject(), null);

            // Assert
            Assert.IsNull(deserializeObject_Null_Single_1);
            Assert.IsNull(deserializeObject_Null_Single_2);
        }

        [TestMethod]
        public void DeserializeObject_Empty_Single_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();

            // Act
            DeserializeObject_Empty_Single deserializeObject_Empty_Single = LazyJsonDeserializer.DeserializeTokenObject<DeserializeObject_Empty_Single>(jsonObject);

            // Assert
            Assert.IsNotNull(deserializeObject_Empty_Single);
        }

        [TestMethod]
        public void DeserializeObject_Simple_WithAttributes_Success()
        {
            // Arrange
            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("Id", new LazyJsonInteger(101)));
            jsonObject.Add(new LazyJsonProperty("Code", new LazyJsonString("SerializeObject_Simple_WithAttributes_Success")));
            jsonObject.Add(new LazyJsonProperty("Tested", new LazyJsonBoolean(true)));

            // Act
            DeserializeObject_Simple_WithAttributes deserializeObject_Simple_WithAttributes = LazyJsonDeserializer.DeserializeTokenObject<DeserializeObject_Simple_WithAttributes>(jsonObject);

            // Assert
            Assert.AreEqual(deserializeObject_Simple_WithAttributes.Id, 101);
            Assert.AreEqual(deserializeObject_Simple_WithAttributes.InternalCode, "SerializeObject_Simple_WithAttributes_Success");
            Assert.AreEqual(deserializeObject_Simple_WithAttributes.Tested, true);
        }

        [TestMethod]
        public void DeserializeObject_PropertyAsObject_Simple_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectType = new LazyJsonObject();
            jsonObjectType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectType.Add(new LazyJsonProperty("Class", new LazyJsonString("String")));

            LazyJsonString jsonStringValue = new LazyJsonString("Lazy.Vinke.Tests.Json");

            LazyJsonObject jsonObjectWrapped = new LazyJsonObject();
            jsonObjectWrapped.Add(new LazyJsonProperty("Type", jsonObjectType));
            jsonObjectWrapped.Add(new LazyJsonProperty("Value", jsonStringValue));

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("SomeObject", jsonObjectWrapped));

            // Act
            DeserializeObject_PropertyAsObject_Simple deserializeObject_PropertyAsObject_Simple = LazyJsonDeserializer.DeserializeTokenObject<DeserializeObject_PropertyAsObject_Simple>(jsonObject);

            // Assert
            Assert.AreEqual(deserializeObject_PropertyAsObject_Simple.SomeObject, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void DeserializeObject_PropertyAsObject_Nested_Success()
        {
            // Arrange
            LazyJsonObject jsonObjectType = new LazyJsonObject();
            jsonObjectType.Add(new LazyJsonProperty("Assembly", new LazyJsonString("System.Private.CoreLib")));
            jsonObjectType.Add(new LazyJsonProperty("Namespace", new LazyJsonString("System")));
            jsonObjectType.Add(new LazyJsonProperty("Class", new LazyJsonString("String")));

            LazyJsonString jsonStringValue = new LazyJsonString("Lazy.Vinke.Tests.Json");

            LazyJsonObject jsonObjectWrapped = new LazyJsonObject();
            jsonObjectWrapped.Add(new LazyJsonProperty("Type", jsonObjectType));
            jsonObjectWrapped.Add(new LazyJsonProperty("Value", jsonStringValue));

            LazyJsonObject jsonObjectNested = new LazyJsonObject();
            jsonObjectNested.Add(new LazyJsonProperty("SomeObject", jsonObjectWrapped));

            LazyJsonObject jsonObject = new LazyJsonObject();
            jsonObject.Add(new LazyJsonProperty("NestedObject", jsonObjectNested));

            // Act
            DeserializeObject_PropertyAsObject_Nested deserializeObject_PropertyAsObject_Nested = LazyJsonDeserializer.DeserializeTokenObject<DeserializeObject_PropertyAsObject_Nested>(jsonObject);

            // Assert
            Assert.AreEqual(deserializeObject_PropertyAsObject_Nested.NestedObject.SomeObject, "Lazy.Vinke.Tests.Json");
        }

        [TestMethod]
        public void SelectDeserializerType_Null_Single_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(null);

            // Assert
            Assert.IsNull(jsonDeserializerType);
        }

        [TestMethod]
        public void SelectDeserializerType_DeserializerClass_WithoutOptions_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(typeof(SelectDeserializerType_DeserializerClass_WithoutOptions));

            // Assert
            Assert.AreEqual(jsonDeserializerType, typeof(LazyJsonDeserializerDateTime));
        }

        [TestMethod]
        public void SelectDeserializerType_DeserializerClass_WithOptions_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsGlobal>().Add<LazyJsonDeserializerInteger>(typeof(SelectDeserializerType_DeserializerClass_WithOptions));

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(typeof(SelectDeserializerType_DeserializerClass_WithOptions), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual(jsonDeserializerType, typeof(LazyJsonDeserializerString));
        }

        [TestMethod]
        public void SelectDeserializerType_DeserializerOptions_WithoutAttribute_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsGlobal>().Add<LazyJsonDeserializerString>(typeof(SelectDeserializerType_DeserializerOptions_WithoutAttribute));

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(typeof(SelectDeserializerType_DeserializerOptions_WithoutAttribute), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual(jsonDeserializerType, typeof(LazyJsonDeserializerString));
        }

        [TestMethod]
        public void SelectDeserializerType_DeserializerOptions_WithAttribute_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsGlobal>().Add<LazyJsonDeserializerString>(typeof(SelectDeserializerType_DeserializerOptions_WithAttribute));

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(typeof(SelectDeserializerType_DeserializerOptions_WithAttribute), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual(jsonDeserializerType, typeof(LazyJsonDeserializerDecimal));
        }

        [TestMethod]
        public void SelectDeserializerType_DeserializerBuiltIn_WithoutOptions_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(typeof(Int32));

            // Assert
            Assert.AreEqual(jsonDeserializerType, typeof(LazyJsonDeserializerInteger));
        }

        [TestMethod]
        public void SelectDeserializerType_DeserializerBuiltIn_WithOptions_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsGlobal>().Add<LazyJsonDeserializerDecimal>(typeof(Int32));

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerType(typeof(Int32), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual(jsonDeserializerType, typeof(LazyJsonDeserializerDecimal));
        }

        [TestMethod]
        public void SelectDeserializerTypeClass_Null_Single_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerTypeClass(null);

            // Assert
            Assert.IsNull(jsonDeserializerType);
        }

        [TestMethod]
        public void SelectDeserializerTypeClass_GetCustomAttributes_WithoutAttribute_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerTypeClass(typeof(SelectDeserializerTypeClass_GetCustomAttributes_WithoutAttribute));

            // Assert
            Assert.IsNull(jsonDeserializerType);
        }

        [TestMethod]
        public void SelectDeserializerTypeClass_GetCustomAttributes_WithAttributeNull_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerTypeClass(typeof(SelectDeserializerTypeClass_GetCustomAttributes_WithAttributeNull));

            // Assert
            Assert.IsNull(jsonDeserializerType);
        }

        [TestMethod]
        public void SelectDeserializerTypeClass_GetCustomAttributes_WithAttributeInvalid_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerTypeClass(typeof(SelectDeserializerTypeClass_GetCustomAttributes_WithAttributeInvalid));

            // Assert
            Assert.IsNull(jsonDeserializerType);
        }

        [TestMethod]
        public void SelectDeserializerTypeClass_GetCustomAttributes_WithAttributeValid_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerTypeClass(typeof(SelectDeserializerTypeClass_GetCustomAttributes_WithAttributeValid));

            // Assert
            Assert.AreEqual(jsonDeserializerType, typeof(LazyJsonDeserializerDecimal));
        }

        [TestMethod]
        public void SelectDeserializerTypeOptions_Null_Single_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerTypeOptions(null);

            // Assert
            Assert.IsNull(jsonDeserializerType);
        }

        [TestMethod]
        public void SelectDeserializerTypeOptions_Options_Empty_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerTypeOptions(typeof(Int32), jsonDeserializerOptions);

            // Assert
            Assert.IsNull(jsonDeserializerType);
        }

        [TestMethod]
        public void SelectDeserializerTypeOptions_Options_OptionsGlobalEmpty_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsGlobal>();

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerTypeOptions(typeof(Int32), jsonDeserializerOptions);

            // Assert
            Assert.IsNull(jsonDeserializerType);
        }

        [TestMethod]
        public void SelectDeserializerTypeOptions_Options_OptionsGlobalValued_Success()
        {
            // Arrange
            LazyJsonDeserializerOptions jsonDeserializerOptions = new LazyJsonDeserializerOptions();
            LazyJsonDeserializerOptionsGlobal jsonDeserializerOptionsGlobal = jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsGlobal>();
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerInteger>(typeof(Int32));
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerInteger>(typeof(Int16));
            jsonDeserializerOptionsGlobal.Add<LazyJsonDeserializerDecimal>(typeof(Decimal));

            // Act
            Type jsonDeserializerTypeInt32 = LazyJsonDeserializer.SelectDeserializerTypeOptions(typeof(Int32), jsonDeserializerOptions);
            Type jsonDeserializerTypeInt16 = LazyJsonDeserializer.SelectDeserializerTypeOptions(typeof(Int16), jsonDeserializerOptions);
            Type jsonDeserializerTypeDecimal = LazyJsonDeserializer.SelectDeserializerTypeOptions(typeof(Decimal), jsonDeserializerOptions);

            // Assert
            Assert.AreEqual(jsonDeserializerTypeInt32, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeInt16, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeDecimal, typeof(LazyJsonDeserializerDecimal));
        }

        [TestMethod]
        public void SelectDeserializerTypeBuiltIn_Null_Single_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerType = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(null);

            // Assert
            Assert.IsNull(jsonDeserializerType);
        }

        [TestMethod]
        public void SelectDeserializerTypeBuiltIn_AllTypes_Single_Success()
        {
            // Arrange

            // Act
            Type jsonDeserializerTypeString = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(String));
            Type jsonDeserializerTypeInt32 = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Int32));
            Type jsonDeserializerTypeDecimal = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Decimal));
            Type jsonDeserializerTypeDateTime = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(DateTime));
            Type jsonDeserializerTypeBoolean = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Boolean));
            Type jsonDeserializerTypeInt32Nullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<Int32>));
            Type jsonDeserializerTypeDecimalNullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<Decimal>));
            Type jsonDeserializerTypeDateTimeNullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<DateTime>));
            Type jsonDeserializerTypeBooleanNullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<Boolean>));
            Type jsonDeserializerTypeArray = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Object[]));
            Type jsonDeserializerTypeInt16 = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Int16));
            Type jsonDeserializerTypeInt64 = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Int64));
            Type jsonDeserializerTypeDouble = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Double));
            Type jsonDeserializerTypeSingle = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Single));
            Type jsonDeserializerTypeChar = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Char));
            Type jsonDeserializerTypeByte = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Byte));
            Type jsonDeserializerTypeSByte = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(SByte));
            Type jsonDeserializerTypeUInt32 = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(UInt32));
            Type jsonDeserializerTypeUInt16 = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(UInt16));
            Type jsonDeserializerTypeInt16Nullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<Int16>));
            Type jsonDeserializerTypeInt64Nullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<Int64>));
            Type jsonDeserializerTypeDoubleNullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<Double>));
            Type jsonDeserializerTypeSingleNullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<Single>));
            Type jsonDeserializerTypeCharNullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<Char>));
            Type jsonDeserializerTypeByteNullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<Byte>));
            Type jsonDeserializerTypeSByteNullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<SByte>));
            Type jsonDeserializerTypeUInt32Nullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<UInt32>));
            Type jsonDeserializerTypeUInt16Nullable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Nullable<UInt16>));
            Type jsonDeserializerTypeDataTable = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(DataTable));
            Type jsonDeserializerTypeList = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(List<Object>));
            Type jsonDeserializerTypeDictionary = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Dictionary<Object, Object>));
            Type jsonDeserializerTypeObject = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Object));
            Type jsonDeserializerTypeOfType = LazyJsonDeserializer.SelectDeserializerTypeBuiltIn(typeof(Type));

            // Assert
            Assert.AreEqual(jsonDeserializerTypeString, typeof(LazyJsonDeserializerString));
            Assert.AreEqual(jsonDeserializerTypeInt32, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeDecimal, typeof(LazyJsonDeserializerDecimal));
            Assert.AreEqual(jsonDeserializerTypeDateTime, typeof(LazyJsonDeserializerDateTime));
            Assert.AreEqual(jsonDeserializerTypeBoolean, typeof(LazyJsonDeserializerBoolean));
            Assert.AreEqual(jsonDeserializerTypeInt32Nullable, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeDecimalNullable, typeof(LazyJsonDeserializerDecimal));
            Assert.AreEqual(jsonDeserializerTypeDateTimeNullable, typeof(LazyJsonDeserializerDateTime));
            Assert.AreEqual(jsonDeserializerTypeBooleanNullable, typeof(LazyJsonDeserializerBoolean));
            Assert.AreEqual(jsonDeserializerTypeArray, typeof(LazyJsonDeserializerArray));
            Assert.AreEqual(jsonDeserializerTypeInt16, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeInt64, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeDouble, typeof(LazyJsonDeserializerDecimal));
            Assert.AreEqual(jsonDeserializerTypeSingle, typeof(LazyJsonDeserializerDecimal));
            Assert.AreEqual(jsonDeserializerTypeChar, typeof(LazyJsonDeserializerString));
            Assert.AreEqual(jsonDeserializerTypeByte, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeSByte, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeUInt32, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeUInt16, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeInt16Nullable, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeInt64Nullable, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeDoubleNullable, typeof(LazyJsonDeserializerDecimal));
            Assert.AreEqual(jsonDeserializerTypeSingleNullable, typeof(LazyJsonDeserializerDecimal));
            Assert.AreEqual(jsonDeserializerTypeCharNullable, typeof(LazyJsonDeserializerString));
            Assert.AreEqual(jsonDeserializerTypeByteNullable, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeSByteNullable, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeUInt32Nullable, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeUInt16Nullable, typeof(LazyJsonDeserializerInteger));
            Assert.AreEqual(jsonDeserializerTypeDataTable, typeof(LazyJsonDeserializerDataTable));
            Assert.AreEqual(jsonDeserializerTypeList, typeof(LazyJsonDeserializerList));
            Assert.AreEqual(jsonDeserializerTypeDictionary, typeof(LazyJsonDeserializerDictionary));
            Assert.AreEqual(jsonDeserializerTypeOfType, typeof(LazyJsonDeserializerType));
        }

        [TestMethod]
        public void SelectDeserializePropertyEventHandler_KnownType_Single_Success()
        {
            // Arrange
            LazyJsonDeserializerBase jsonDeserializer = null;
            LazyJsonDeserializePropertyEventHandler jsonDeserializePropertyEventHandler = null;

            // Act
            LazyJsonDeserializer.SelectDeserializePropertyEventHandler(typeof(DataSet), out jsonDeserializer, out jsonDeserializePropertyEventHandler);

            // Assert
            //Assert.IsNotNull(jsonDeserializer);
            Assert.IsNotNull(jsonDeserializePropertyEventHandler);
            Assert.AreEqual(jsonDeserializePropertyEventHandler, jsonDeserializer.Deserialize);
        }

        [TestMethod]
        public void SelectDeserializePropertyEventHandler_UnknownType_Single_Success()
        {
            // Arrange
            LazyJsonDeserializerBase jsonDeserializer = null;
            LazyJsonDeserializePropertyEventHandler jsonDeserializePropertyEventHandler = null;

            // Act
            LazyJsonDeserializer.SelectDeserializePropertyEventHandler(typeof(DbDataAdapter), out jsonDeserializer, out jsonDeserializePropertyEventHandler);

            // Assert
            Assert.IsNull(jsonDeserializer);
            Assert.IsNotNull(jsonDeserializePropertyEventHandler);
            Assert.AreEqual(jsonDeserializePropertyEventHandler, LazyJsonDeserializer.DeserializePropertyObject);
        }

        [TestMethod]
        public void SelectDeserializeTokenEventHandler_KnownType_Single_Success()
        {
            // Arrange
            LazyJsonDeserializerBase jsonDeserializer = null;
            LazyJsonDeserializeTokenEventHandler jsonDeserializeTokenEventHandler = null;

            // Act
            LazyJsonDeserializer.SelectDeserializeTokenEventHandler(typeof(DataSet), out jsonDeserializer, out jsonDeserializeTokenEventHandler);

            // Assert
            //Assert.IsNotNull(jsonDeserializer);
            Assert.IsNotNull(jsonDeserializeTokenEventHandler);
            Assert.AreEqual(jsonDeserializeTokenEventHandler, jsonDeserializer.Deserialize);
        }

        [TestMethod]
        public void SelectDeserializeTokenEventHandler_UnknownType_Single_Success()
        {
            // Arrange
            LazyJsonDeserializerBase jsonDeserializer = null;
            LazyJsonDeserializeTokenEventHandler jsonDeserializeTokenEventHandler = null;

            // Act
            LazyJsonDeserializer.SelectDeserializeTokenEventHandler(typeof(ManifestResourceInfo), out jsonDeserializer, out jsonDeserializeTokenEventHandler);

            // Assert
            Assert.IsNull(jsonDeserializer);
            Assert.IsNotNull(jsonDeserializeTokenEventHandler);
            Assert.AreEqual(jsonDeserializeTokenEventHandler, LazyJsonDeserializer.DeserializeTokenObject);
        }
    }
}

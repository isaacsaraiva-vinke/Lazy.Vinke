// TestsSamplesLazyJsonDeserializer.cs
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

namespace Lazy.Vinke.Tests.Json
{
    public class DeserializeProperty_SelectDeserializerType_UnknownType
    {
        public Int32 Id { get; set; }

        [LazyJsonAttributePropertyRename("Code")]
        public String InternalCode { get; set; }

        [LazyJsonAttributePropertyIgnore()]
        public Decimal Amount { get; set; }

        [LazyJsonAttributeTypeDeserializer(typeof(LazyJsonDeserializerBoolean))]
        public Boolean Tested { get; set; }
    }

    public class DeserializeToken_SelectDeserializerType_UnknownType
    {
        public Int32 Id { get; set; }

        [LazyJsonAttributePropertyRename("InternalCode")]
        public String Code { get; set; }

        [LazyJsonAttributeTypeDeserializer(typeof(LazyJsonDeserializerDecimal))]
        public Decimal Amount { get; set; }

        [LazyJsonAttributePropertyIgnore()]
        public Boolean Tested { get; set; }
    }

    public class DeserializeObject_Null_Single
    {
    }

    public class DeserializeObject_Empty_Single
    {
    }

    public class DeserializeObject_Simple_WithAttributes
    {
        public Int32 Id { get; set; }

        [LazyJsonAttributePropertyRename("Code")]
        public String InternalCode { get; set; }

        [LazyJsonAttributePropertyIgnore()]
        public Decimal Amount { get; set; }

        [LazyJsonAttributeTypeDeserializer(typeof(LazyJsonDeserializerBoolean))]
        public Boolean Tested { get; set; }
    }

    public class DeserializeObject_PropertyAsObject_Simple
    {
        public Object SomeObject { get; set; }
    }

    public class DeserializeObject_PropertyAsObject_Nested
    {
        public DeserializeObject_PropertyAsObject_Simple NestedObject { get; set; }
    }

    [LazyJsonAttributeTypeDeserializer(typeof(LazyJsonDeserializerDateTime))]
    public class SelectDeserializerType_DeserializerClass_WithoutOptions
    {
    }

    [LazyJsonAttributeTypeDeserializer(typeof(LazyJsonDeserializerString))]
    public class SelectDeserializerType_DeserializerClass_WithOptions
    {
    }

    public class SelectDeserializerType_DeserializerOptions_WithoutAttribute
    {
    }

    [LazyJsonAttributeTypeDeserializer(typeof(LazyJsonDeserializerDecimal))]
    public class SelectDeserializerType_DeserializerOptions_WithAttribute
    {
    }

    public class SelectDeserializerTypeClass_GetCustomAttributes_WithoutAttribute
    {
    }

    [LazyJsonAttributeTypeDeserializer(null)]
    public class SelectDeserializerTypeClass_GetCustomAttributes_WithAttributeNull
    {
    }

    [LazyJsonAttributeTypeDeserializer(typeof(Int32))]
    public class SelectDeserializerTypeClass_GetCustomAttributes_WithAttributeInvalid
    {
    }

    [LazyJsonAttributeTypeDeserializer(typeof(LazyJsonDeserializerDecimal))]
    public class SelectDeserializerTypeClass_GetCustomAttributes_WithAttributeValid
    {
    }
}

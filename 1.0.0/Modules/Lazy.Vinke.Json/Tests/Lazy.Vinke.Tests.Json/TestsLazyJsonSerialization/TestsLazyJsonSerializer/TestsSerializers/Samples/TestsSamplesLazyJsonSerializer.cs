// TestsSamplesLazyJsonSerializer.cs
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
    public class SerializeToken_SelectSerializerType_UnknownType
    {
        public Int32 Id { get; set; }

        [LazyJsonAttributePropertyRename("InternalCode")]
        public String Code { get; set; }

        [LazyJsonAttributeTypeSerializer(typeof(LazyJsonSerializerDecimal))]
        public Decimal Amount { get; set; }

        [LazyJsonAttributePropertyIgnore()]
        public Boolean Tested { get; set; }
    }

    public class SerializeObject_Empty_Single
    {
    }

    public class SerializeObject_Simple_WithAttributes
    {
        public Int32 Id { get; set; }

        [LazyJsonAttributePropertyRename("Code")]
        public String InternalCode { get; set; }

        [LazyJsonAttributePropertyIgnore()]
        public Decimal Amount { get; set; }

        [LazyJsonAttributeTypeSerializer(typeof(LazyJsonSerializerBoolean))]
        public Boolean Tested { get; set; }
    }

    [LazyJsonAttributeTypeSerializer(typeof(LazyJsonSerializerDateTime))]
    public class SelectSerializerType_SerializerClass_WithoutOptions
    {
    }

    [LazyJsonAttributeTypeSerializer(typeof(LazyJsonSerializerString))]
    public class SelectSerializerType_SerializerClass_WithOptions
    {
    }

    public class SelectSerializerType_SerializerOptions_WithoutAttribute
    {
    }

    [LazyJsonAttributeTypeSerializer(typeof(LazyJsonSerializerDecimal))]
    public class SelectSerializerType_SerializerOptions_WithAttribute
    {
    }

    public class SelectSerializerTypeClass_GetCustomAttributes_WithoutAttribute
    {
    }

    [LazyJsonAttributeTypeSerializer(null)]
    public class SelectSerializerTypeClass_GetCustomAttributes_WithAttributeNull
    {
    }

    [LazyJsonAttributeTypeSerializer(typeof(Int32))]
    public class SelectSerializerTypeClass_GetCustomAttributes_WithAttributeInvalid
    {
    }

    [LazyJsonAttributeTypeSerializer(typeof(LazyJsonSerializerDecimal))]
    public class SelectSerializerTypeClass_GetCustomAttributes_WithAttributeValid
    {
    }
}

// LazyJsonSerializer.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 07

using System;
using System.IO;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Lazy.Vinke.Json
{
    public static class LazyJsonSerializer
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Serialize an object to the json
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <returns>The json</returns>
        public static String Serialize(Object data, LazyJsonSerializerOptions jsonSerializerOptions = null, LazyJsonWriterOptions jsonWriterOptions = null)
        {
            return LazyJsonWriter.Write(new LazyJson(SerializeToken(data, jsonSerializerOptions)), jsonWriterOptions);
        }

        /// <summary>
        /// Serialize an object to the json token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The json token</returns>
        public static LazyJsonToken SerializeToken(Object data, LazyJsonSerializerOptions jsonSerializerOptions = null)
        {
            if (data != null)
            {
                Type jsonSerializerType = SelectSerializerType(data.GetType(), jsonSerializerOptions);

                if (jsonSerializerType != null)
                {
                    return ((LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType)).Serialize(data, jsonSerializerOptions);
                }
                else
                {
                    return SerializeObject(data, jsonSerializerOptions);
                }
            }

            return new LazyJsonNull();
        }

        /// <summary>
        /// Serialize an object to the json token
        /// </summary>
        /// <param name="data">The object to be serialized</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The json token</returns>
        public static LazyJsonToken SerializeObject(Object data, LazyJsonSerializerOptions jsonSerializerOptions = null)
        {
            if (data != null)
            {
                LazyJsonObject jsonObject = new LazyJsonObject();
                List<String> alreadySerialized = new List<String>();
                PropertyInfo[] propertyInfoArray = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo propertyInfo in propertyInfoArray)
                {
                    if (propertyInfo.GetMethod == null)
                        continue;

                    if (alreadySerialized.Contains(propertyInfo.Name) == true)
                        continue;

                    if (propertyInfo.GetCustomAttribute<LazyJsonAttributePropertyIgnore>(false) != null)
                        continue;

                    alreadySerialized.Add(propertyInfo.Name);

                    Type jsonSerializerType = null;
                    String propertyName = propertyInfo.Name;
                    Object[] jsonAttributeArray = propertyInfo.GetCustomAttributes(typeof(LazyJsonAttributeBase), false);

                    foreach (Object jsonAttribute in jsonAttributeArray)
                    {
                        if (jsonAttribute is LazyJsonAttributePropertyRename)
                        {
                            propertyName = ((LazyJsonAttributePropertyRename)jsonAttribute).Name;
                        }
                        else if (jsonAttribute is LazyJsonAttributeTypeSerializer)
                        {
                            LazyJsonAttributeTypeSerializer jsonAttributeTypeSerializer = (LazyJsonAttributeTypeSerializer)jsonAttribute;

                            if (jsonAttributeTypeSerializer.Type != null && jsonAttributeTypeSerializer.Type.IsSubclassOf(typeof(LazyJsonSerializerBase)) == true)
                                jsonSerializerType = jsonAttributeTypeSerializer.Type;
                        }
                    }

                    if (jsonSerializerType != null)
                    {
                        jsonObject.Add(new LazyJsonProperty(propertyName, ((LazyJsonSerializerBase)Activator.CreateInstance(jsonSerializerType)).Serialize(propertyInfo.GetValue(data), jsonSerializerOptions)));
                    }
                    else
                    {
                        if (propertyInfo.PropertyType != typeof(Object))
                        {
                            jsonObject.Add(new LazyJsonProperty(propertyName, SerializeToken(propertyInfo.GetValue(data), jsonSerializerOptions)));
                        }
                        else
                        {
                            /* Wrap object exporting its type to be able to deserialize it */
                            /* Only if the property was declared as "Object" Type */

                            jsonObject.Add(new LazyJsonProperty(propertyName, new LazyJsonSerializerObject().Serialize(propertyInfo.GetValue(data), jsonSerializerOptions)));
                        }
                    }
                }

                return jsonObject;
            }

            return new LazyJsonNull();
        }

        /// <summary>
        /// Select serializer type
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The serializer type</returns>
        public static Type SelectSerializerType(Type dataType, LazyJsonSerializerOptions jsonSerializerOptions = null)
        {
            if (dataType != null)
            {
                Type jsonSerializerType = SelectSerializerTypeClass(dataType, jsonSerializerOptions);

                if (jsonSerializerType == null)
                {
                    jsonSerializerType = SelectSerializerTypeOptions(dataType, jsonSerializerOptions);

                    if (jsonSerializerType == null)
                    {
                        jsonSerializerType = SelectSerializerTypeBuiltIn(dataType, jsonSerializerOptions);
                    }
                }

                return jsonSerializerType;
            }

            return null;
        }

        /// <summary>
        /// Select serializer type defined on the class
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The serializer type</returns>
        public static Type SelectSerializerTypeClass(Type dataType, LazyJsonSerializerOptions jsonSerializerOptions = null)
        {
            if (dataType != null)
            {
                Object[] jsonAttributeTypeSerializerArray = null;

                /* Some types throws exceptions on GetCustomAttributes */
                try { jsonAttributeTypeSerializerArray = dataType.GetCustomAttributes(typeof(LazyJsonAttributeTypeSerializer), false); }
                catch { return null; }

                if (jsonAttributeTypeSerializerArray.Length > 0)
                {
                    LazyJsonAttributeTypeSerializer jsonAttributeTypeSerializer = (LazyJsonAttributeTypeSerializer)jsonAttributeTypeSerializerArray[0];

                    if (jsonAttributeTypeSerializer.Type != null && jsonAttributeTypeSerializer.Type.IsSubclassOf(typeof(LazyJsonSerializerBase)) == true)
                        return jsonAttributeTypeSerializer.Type;
                }
            }

            return null;
        }

        /// <summary>
        /// Select serializer type defined on the serializer options
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The serializer type</returns>
        public static Type SelectSerializerTypeOptions(Type dataType, LazyJsonSerializerOptions jsonSerializerOptions = null)
        {
            if (dataType != null && jsonSerializerOptions != null)
            {
                if (jsonSerializerOptions.Contains<LazyJsonSerializerOptionsGlobal>() == true)
                    return jsonSerializerOptions.Item<LazyJsonSerializerOptionsGlobal>().Get(dataType);
            }

            return null;
        }

        /// <summary>
        /// Select serializer type defined built in
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonSerializerOptions">The json serializer options</param>
        /// <returns>The serializer type</returns>
        public static Type SelectSerializerTypeBuiltIn(Type dataType, LazyJsonSerializerOptions jsonSerializerOptions = null)
        {
            if (dataType != null)
            {
                if (dataType == typeof(String)) return typeof(LazyJsonSerializerString);

                if (dataType == typeof(Int32)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Decimal)) return typeof(LazyJsonSerializerDecimal);
                if (dataType == typeof(DateTime)) return typeof(LazyJsonSerializerDateTime);
                if (dataType == typeof(Boolean)) return typeof(LazyJsonSerializerBoolean);
                if (dataType == typeof(Nullable<Int32>)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Nullable<Decimal>)) return typeof(LazyJsonSerializerDecimal);
                if (dataType == typeof(Nullable<DateTime>)) return typeof(LazyJsonSerializerDateTime);
                if (dataType == typeof(Nullable<Boolean>)) return typeof(LazyJsonSerializerBoolean);

                if (dataType.IsArray == true) return typeof(LazyJsonSerializerArray);

                if (dataType == typeof(Int16)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Int64)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Double)) return typeof(LazyJsonSerializerDecimal);
                if (dataType == typeof(Single)) return typeof(LazyJsonSerializerDecimal);
                if (dataType == typeof(Char)) return typeof(LazyJsonSerializerString);
                if (dataType == typeof(Byte)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(SByte)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(UInt32)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(UInt16)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Nullable<Int16>)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Nullable<Int64>)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Nullable<Double>)) return typeof(LazyJsonSerializerDecimal);
                if (dataType == typeof(Nullable<Single>)) return typeof(LazyJsonSerializerDecimal);
                if (dataType == typeof(Nullable<Char>)) return typeof(LazyJsonSerializerString);
                if (dataType == typeof(Nullable<Byte>)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Nullable<SByte>)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Nullable<UInt32>)) return typeof(LazyJsonSerializerInteger);
                if (dataType == typeof(Nullable<UInt16>)) return typeof(LazyJsonSerializerInteger);

                if (dataType == typeof(DataTable)) return typeof(LazyJsonSerializerDataTable);

                if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(List<>)) return typeof(LazyJsonSerializerList);
                if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(Dictionary<,>)) return typeof(LazyJsonSerializerDictionary);
                if (dataType.IsGenericType == true && dataType.GetGenericTypeDefinition() == typeof(Stack<>)) return typeof(LazyJsonSerializerStack);
                if (dataType.IsGenericType == true && dataType.IsAssignableTo(typeof(ITuple)) == true) return typeof(LazyJsonSerializerTuple);

                if (dataType.IsAssignableTo(typeof(Type)) == true) return typeof(LazyJsonSerializerType);
                if (dataType == typeof(Object)) return typeof(LazyJsonSerializerObject);

                /* Serializers retrieves the object type by calling the GetType() method */
                /* When the object is a "Type", this produces the same as "typeof(Object).GetType()", which always returns a "System.RuntimeType" */
                /* "System.RuntimeType" derives from "System.Type" */
            }

            return null;
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    #region EventHandlers

    public delegate LazyJsonToken LazyJsonSerializeTokenEventHandler(Object data, LazyJsonSerializerOptions jsonSerializerOptions = null);

    #endregion EventHandlers
}

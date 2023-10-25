// LazyJsonDeserializer.cs
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
    public static class LazyJsonDeserializer
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Deserialize the json to an object
        /// </summary>
        /// <typeparam name="T">The data type</typeparam>
        /// <param name="json">The json</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <returns>The object</returns>
        public static T Deserialize<T>(String json, LazyJsonDeserializerOptions jsonDeserializerOptions = null, LazyJsonReaderOptions jsonReaderOptions = null)
        {
            return (T)Deserialize(json, typeof(T), jsonDeserializerOptions, jsonReaderOptions);
        }

        /// <summary>
        /// Deserialize the json property to an object
        /// </summary>
        /// <typeparam name="T">The data type</typeparam>
        /// <param name="jsonProperty">The json property</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The object</returns>
        public static T DeserializeProperty<T>(LazyJsonProperty jsonProperty, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            return (T)DeserializeProperty(jsonProperty, typeof(T), jsonDeserializerOptions);
        }

        /// <summary>
        /// Deserialize the json token to an object
        /// </summary>
        /// <typeparam name="T">The data type</typeparam>
        /// <param name="jsonToken">The json token</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The object</returns>
        public static T DeserializeToken<T>(LazyJsonToken jsonToken, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            return (T)DeserializeToken(jsonToken, typeof(T), jsonDeserializerOptions);
        }

        /// <summary>
        /// Deserialize the json object to an object
        /// </summary>
        /// <typeparam name="T">The data type</typeparam>
        /// <param name="jsonObject">The json object</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The object</returns>
        public static T DeserializeObject<T>(LazyJsonObject jsonObject, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            return (T)DeserializeObject(jsonObject, typeof(T), jsonDeserializerOptions);
        }

        /// <summary>
        /// Deserialize the json to an object
        /// </summary>
        /// <param name="json">The json</param>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <returns>The object</returns>
        public static Object Deserialize(String json, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null, LazyJsonReaderOptions jsonReaderOptions = null)
        {
            return DeserializeToken(LazyJsonReader.Read(json, jsonReaderOptions).Root, dataType, jsonDeserializerOptions);
        }

        /// <summary>
        /// Deserialize the json property to an object
        /// </summary>
        /// <param name="jsonProperty">The json property</param>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The object</returns>
        public static Object DeserializeProperty(LazyJsonProperty jsonProperty, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (jsonProperty != null && jsonProperty.Token != null && jsonProperty.Token.Type != LazyJsonType.Null && dataType != null)
            {
                Type jsonDeserializerType = SelectDeserializerType(dataType, jsonDeserializerOptions);

                if (jsonDeserializerType != null)
                {
                    return ((LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType)).Deserialize(jsonProperty, dataType, jsonDeserializerOptions);
                }
                else
                {
                    return DeserializeObject(jsonProperty, dataType, jsonDeserializerOptions);
                }
            }

            return null;
        }

        /// <summary>
        /// Deserialize the json token to an object
        /// </summary>
        /// <param name="jsonToken">The json token</param>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The object</returns>
        public static Object DeserializeToken(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (jsonToken != null && jsonToken.Type != LazyJsonType.Null && dataType != null)
            {
                Type jsonDeserializerType = SelectDeserializerType(dataType, jsonDeserializerOptions);

                if (jsonDeserializerType != null)
                {
                    return ((LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType)).Deserialize(jsonToken, dataType, jsonDeserializerOptions);
                }
                else
                {
                    return DeserializeObject(jsonToken, dataType, jsonDeserializerOptions);
                }
            }

            return null;
        }

        /// <summary>
        /// Deserialize the json property to an object
        /// </summary>
        /// <param name="jsonProperty">The json property</param>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The object</returns>
        public static Object DeserializeObject(LazyJsonProperty jsonProperty, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (jsonProperty != null)
                return DeserializeObject(jsonProperty.Token, dataType, jsonDeserializerOptions);

            return null;
        }

        /// <summary>
        /// Deserialize the json token to an object
        /// </summary>
        /// <param name="jsonToken">The json token</param>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The object</returns>
        public static Object DeserializeObject(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (jsonToken != null && jsonToken.Type == LazyJsonType.Object && dataType != null)
            {
                LazyJsonObject jsonObject = (LazyJsonObject)jsonToken;

                Object data = Activator.CreateInstance(dataType);
                List<String> alreadyDeserialized = new List<String>();
                PropertyInfo[] propertyInfoArray = dataType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo propertyInfo in propertyInfoArray)
                {
                    if (propertyInfo.SetMethod == null)
                        continue;

                    if (alreadyDeserialized.Contains(propertyInfo.Name) == true)
                        continue;

                    if (propertyInfo.GetCustomAttribute<LazyJsonAttributePropertyIgnore>(false) != null)
                        continue;

                    alreadyDeserialized.Add(propertyInfo.Name);

                    Type jsonDeserializerType = null;
                    String propertyName = propertyInfo.Name;
                    Object[] jsonAttributeArray = propertyInfo.GetCustomAttributes(typeof(LazyJsonAttributeBase), false);

                    foreach (Object jsonAttribute in jsonAttributeArray)
                    {
                        if (jsonAttribute is LazyJsonAttributePropertyRename)
                        {
                            propertyName = ((LazyJsonAttributePropertyRename)jsonAttribute).Name;
                        }
                        else if (jsonAttribute is LazyJsonAttributeTypeDeserializer)
                        {
                            LazyJsonAttributeTypeDeserializer jsonAttributeTypeDeserializer = (LazyJsonAttributeTypeDeserializer)jsonAttribute;

                            if (jsonAttributeTypeDeserializer.Type != null && jsonAttributeTypeDeserializer.Type.IsSubclassOf(typeof(LazyJsonDeserializerBase)) == true)
                                jsonDeserializerType = jsonAttributeTypeDeserializer.Type;
                        }
                    }

                    LazyJsonProperty jsonProperty = jsonObject[propertyName];

                    if (jsonProperty != null)
                    {
                        if (jsonDeserializerType != null)
                        {
                            propertyInfo.SetValue(data, ((LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType)).Deserialize(jsonProperty, propertyInfo.PropertyType, jsonDeserializerOptions));
                        }
                        else
                        {
                            if (propertyInfo.PropertyType != typeof(Object))
                            {
                                propertyInfo.SetValue(data, DeserializeProperty(jsonProperty, propertyInfo.PropertyType, jsonDeserializerOptions));
                            }
                            else
                            {
                                /* Unwrap object importing its type that was previously exported by serializer */
                                /* Only if the property was declared as "Object" Type */

                                propertyInfo.SetValue(data, new LazyJsonDeserializerObject().Deserialize(jsonProperty, propertyInfo.PropertyType, jsonDeserializerOptions));
                            }
                        }
                    }
                }

                return data;
            }

            return null;
        }

        /// <summary>
        /// Select deserializer type
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The deserializer type</returns>
        public static Type SelectDeserializerType(Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (dataType != null)
            {
                Type jsonDeserializerType = SelectDeserializerTypeClass(dataType, jsonDeserializerOptions);

                if (jsonDeserializerType == null)
                {
                    jsonDeserializerType = SelectDeserializerTypeOptions(dataType, jsonDeserializerOptions);

                    if (jsonDeserializerType == null)
                    {
                        jsonDeserializerType = SelectDeserializerTypeBuiltIn(dataType, jsonDeserializerOptions);
                    }
                }

                return jsonDeserializerType;
            }

            return null;
        }

        /// <summary>
        /// Select deserializer type defined on the class
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The deserializer type</returns>
        public static Type SelectDeserializerTypeClass(Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (dataType != null)
            {
                Object[] jsonAttributeTypeDeserializerArray = null;

                /* Some types throws exceptions on GetCustomAttributes */
                try { jsonAttributeTypeDeserializerArray = dataType.GetCustomAttributes(typeof(LazyJsonAttributeTypeDeserializer), false); }
                catch { return null; }

                if (jsonAttributeTypeDeserializerArray.Length > 0)
                {
                    LazyJsonAttributeTypeDeserializer jsonAttributeTypeDeserializer = (LazyJsonAttributeTypeDeserializer)jsonAttributeTypeDeserializerArray[0];

                    if (jsonAttributeTypeDeserializer.Type != null && jsonAttributeTypeDeserializer.Type.IsSubclassOf(typeof(LazyJsonDeserializerBase)) == true)
                        return jsonAttributeTypeDeserializer.Type;
                }
            }

            return null;
        }

        /// <summary>
        /// Select deserializer type defined on the deserializer options
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The deserializer type</returns>
        public static Type SelectDeserializerTypeOptions(Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (dataType != null && jsonDeserializerOptions != null)
            {
                if (jsonDeserializerOptions.Contains<LazyJsonDeserializerOptionsGlobal>() == true)
                    return jsonDeserializerOptions.Item<LazyJsonDeserializerOptionsGlobal>().Get(dataType);
            }

            return null;
        }

        /// <summary>
        /// Select deserializer type defined built in
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        /// <returns>The deserializer type</returns>
        public static Type SelectDeserializerTypeBuiltIn(Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            if (dataType != null)
            {
                if (dataType == typeof(String)) return typeof(LazyJsonDeserializerString);

                if (dataType == typeof(Int32)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Decimal)) return typeof(LazyJsonDeserializerDecimal);
                if (dataType == typeof(DateTime)) return typeof(LazyJsonDeserializerDateTime);
                if (dataType == typeof(Boolean)) return typeof(LazyJsonDeserializerBoolean);
                if (dataType == typeof(Nullable<Int32>)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Nullable<Decimal>)) return typeof(LazyJsonDeserializerDecimal);
                if (dataType == typeof(Nullable<DateTime>)) return typeof(LazyJsonDeserializerDateTime);
                if (dataType == typeof(Nullable<Boolean>)) return typeof(LazyJsonDeserializerBoolean);

                if (dataType.IsArray == true) return typeof(LazyJsonDeserializerArray);

                if (dataType == typeof(Int16)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Int64)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Double)) return typeof(LazyJsonDeserializerDecimal);
                if (dataType == typeof(Single)) return typeof(LazyJsonDeserializerDecimal);
                if (dataType == typeof(Char)) return typeof(LazyJsonDeserializerString);
                if (dataType == typeof(Byte)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(SByte)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(UInt32)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(UInt16)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Nullable<Int16>)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Nullable<Int64>)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Nullable<Double>)) return typeof(LazyJsonDeserializerDecimal);
                if (dataType == typeof(Nullable<Single>)) return typeof(LazyJsonDeserializerDecimal);
                if (dataType == typeof(Nullable<Char>)) return typeof(LazyJsonDeserializerString);
                if (dataType == typeof(Nullable<Byte>)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Nullable<SByte>)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Nullable<UInt32>)) return typeof(LazyJsonDeserializerInteger);
                if (dataType == typeof(Nullable<UInt16>)) return typeof(LazyJsonDeserializerInteger);

                if (dataType == typeof(DataTable)) return typeof(LazyJsonDeserializerDataTable);

                if (dataType.IsGenericType == true)
                {
                    if (dataType.GetGenericTypeDefinition() == typeof(List<>)) return typeof(LazyJsonDeserializerList);
                    if (dataType.GetGenericTypeDefinition() == typeof(Dictionary<,>)) return typeof(LazyJsonDeserializerDictionary);
                    if (dataType.GetGenericTypeDefinition() == typeof(Queue<>)) return typeof(LazyJsonDeserializerQueue);
                    if (dataType.GetGenericTypeDefinition() == typeof(Stack<>)) return typeof(LazyJsonDeserializerStack);
                    if (dataType.IsAssignableTo(typeof(ITuple)) == true) return typeof(LazyJsonDeserializerTuple);
                }

                if (dataType == typeof(Type)) return typeof(LazyJsonDeserializerType);
                if (dataType == typeof(Object)) return typeof(LazyJsonDeserializerObject);
            }

            return null;
        }

        /// <summary>
        /// Select the json deserialize property event handler for data type
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializer">The json deserializer</param>
        /// <param name="jsonDeserializePropertyEventHandler">The json deserialize property event handler</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        public static void SelectDeserializePropertyEventHandler(Type dataType, out LazyJsonDeserializerBase jsonDeserializer, out LazyJsonDeserializePropertyEventHandler jsonDeserializePropertyEventHandler, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            Type jsonDeserializerType = SelectDeserializerType(dataType, jsonDeserializerOptions);

            if (jsonDeserializerType != null)
            {
                jsonDeserializer = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);
                jsonDeserializePropertyEventHandler = new LazyJsonDeserializePropertyEventHandler(jsonDeserializer.Deserialize);
            }
            else
            {
                jsonDeserializer = null;
                jsonDeserializePropertyEventHandler = new LazyJsonDeserializePropertyEventHandler(DeserializeObject);
            }
        }

        /// <summary>
        /// Select the json deserialize token event handler for data type
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <param name="jsonDeserializer">The json deserializer</param>
        /// <param name="jsonDeserializeTokenEventHandler">The json deserialize token event handler</param>
        /// <param name="jsonDeserializerOptions">The json deserializer options</param>
        public static void SelectDeserializeTokenEventHandler(Type dataType, out LazyJsonDeserializerBase jsonDeserializer, out LazyJsonDeserializeTokenEventHandler jsonDeserializeTokenEventHandler, LazyJsonDeserializerOptions jsonDeserializerOptions = null)
        {
            Type jsonDeserializerType = SelectDeserializerType(dataType, jsonDeserializerOptions);

            if (jsonDeserializerType != null)
            {
                jsonDeserializer = (LazyJsonDeserializerBase)Activator.CreateInstance(jsonDeserializerType);
                jsonDeserializeTokenEventHandler = new LazyJsonDeserializeTokenEventHandler(jsonDeserializer.Deserialize);
            }
            else
            {
                jsonDeserializer = null;
                jsonDeserializeTokenEventHandler = new LazyJsonDeserializeTokenEventHandler(DeserializeObject);
            }
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }

    #region EventHandlers

    public delegate Object LazyJsonDeserializePropertyEventHandler(LazyJsonProperty jsonProperty, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null);

    public delegate Object LazyJsonDeserializeTokenEventHandler(LazyJsonToken jsonToken, Type dataType, LazyJsonDeserializerOptions jsonDeserializerOptions = null);

    #endregion EventHandlers
}

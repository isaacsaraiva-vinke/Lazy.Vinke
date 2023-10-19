// LazyJsonReader.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, September 30

using System;
using System.IO;
using System.Data;
using System.Globalization;
using System.Collections.Generic;

using Lazy.Vinke.Json.Properties;

namespace Lazy.Vinke.Json
{
    public static class LazyJsonReader
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Read the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <returns>The lazy json</returns>
        public static LazyJson Read(String json, LazyJsonReaderOptions jsonReaderOptions = null)
        {
            Int32 line = 1;
            Int32 column = 1;
            Int32 index = 0;

            if (jsonReaderOptions == null)
                jsonReaderOptions = new LazyJsonReaderOptions();

            LazyJson lazyJson = new LazyJson();

            ReadRoot(json, jsonReaderOptions, lazyJson, ref line, ref column, ref index);

            return lazyJson;
        }

        /// <summary>
        /// Read the root node of the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="lazyJson">The lazy json</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadRoot(String json, LazyJsonReaderOptions jsonReaderOptions, LazyJson lazyJson, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            ReadDisposableContent(json, jsonReaderOptions, ref line, ref column, ref index);

            if (index < json.Length)
            {
                if (json[index] == '[')
                {
                    index++; column++;
                    LazyJsonArray jsonArray = null;
                    ReadArray(json, jsonReaderOptions, out jsonArray, ref line, ref column, ref index);
                    lazyJson.Root = jsonArray;
                }
                else if (json[index] == '{')
                {
                    index++; column++;
                    LazyJsonObject jsonObject = null;
                    ReadObject(json, jsonReaderOptions, out jsonObject, ref line, ref column, ref index);
                    lazyJson.Root = jsonObject;
                }
                else if (json[index] == '\"')
                {
                    index++; column++;
                    LazyJsonString jsonString = null;
                    ReadString(json, jsonReaderOptions, out jsonString, ref line, ref column, ref index);
                    lazyJson.Root = jsonString;
                }
                else if (json[index] == '-')
                {
                    index++; column++;
                    LazyJsonToken jsonToken = null;
                    ReadIntegerOrDecimalNegative(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);
                    lazyJson.Root = jsonToken;
                }
                else if (Char.IsDigit(json[index]) == true)
                {
                    LazyJsonToken jsonToken = null;
                    ReadIntegerOrDecimal(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);
                    lazyJson.Root = jsonToken;
                }
                else if (Char.IsLetter(json[index]) == true)
                {
                    LazyJsonToken jsonToken = null;
                    ReadNullOrBoolean(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);
                    lazyJson.Root = jsonToken;
                }
                else
                {
                    throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, LazyResourcesJson.LazyJsonCaptionTokenType, json[index]));
                }
            }
            else
            {
                throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenType, String.Empty));
            }

            ReadDisposableContent(json, jsonReaderOptions, ref line, ref column, ref index);

            if (index < json.Length)
            {
                throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, String.Empty, json[index]));
            }
        }

        /// <summary>
        /// Read array on the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonArray">The json array</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadArray(String json, LazyJsonReaderOptions jsonReaderOptions, out LazyJsonArray jsonArray, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            if (index < json.Length)
            {
                jsonArray = new LazyJsonArray();

                while (index < json.Length)
                {
                    ReadDisposableContent(json, jsonReaderOptions, ref line, ref column, ref index);

                    if (index < json.Length)
                    {
                        if (json[index] == '[')
                        {
                            index++; column++;
                            LazyJsonArray jsonArrayNested = null;
                            ReadArray(json, jsonReaderOptions, out jsonArrayNested, ref line, ref column, ref index);
                            jsonArray.Add(jsonArrayNested);
                        }
                        else if (json[index] == '{')
                        {
                            index++; column++;
                            LazyJsonObject jsonObject = null;
                            ReadObject(json, jsonReaderOptions, out jsonObject, ref line, ref column, ref index);
                            jsonArray.Add(jsonObject);
                        }
                        else if (json[index] == '\"')
                        {
                            index++; column++;
                            LazyJsonString jsonString = null;
                            ReadString(json, jsonReaderOptions, out jsonString, ref line, ref column, ref index);
                            jsonArray.Add(jsonString);
                        }
                        else if (json[index] == '-')
                        {
                            index++; column++;
                            LazyJsonToken jsonToken = null;
                            ReadIntegerOrDecimalNegative(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);
                            jsonArray.Add(jsonToken);
                        }
                        else if (Char.IsDigit(json[index]) == true)
                        {
                            LazyJsonToken jsonToken = null;
                            ReadIntegerOrDecimal(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);
                            jsonArray.Add(jsonToken);
                        }
                        else if (Char.IsLetter(json[index]) == true)
                        {
                            LazyJsonToken jsonToken = null;
                            ReadNullOrBoolean(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);
                            jsonArray.Add(jsonToken);
                        }
                        else if (json[index] == ']' && jsonArray.Length == 0)
                        {
                            index++; column++;
                            break;
                        }
                        else
                        {
                            throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, LazyResourcesJson.LazyJsonCaptionTokenType, json[index]));
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenType, String.Empty));
                    }

                    ReadDisposableContent(json, jsonReaderOptions, ref line, ref column, ref index);

                    if (index < json.Length)
                    {
                        if (json[index] == ',') { index++; column++; continue; }
                        if (json[index] == ']') { index++; column++; break; }

                        throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeArrayNextOrEnd, json[index]));
                    }
                    else
                    {
                        throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeArrayNextOrEnd, String.Empty));
                    }
                }
            }
            else
            {
                throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenType, String.Empty));
            }
        }

        /// <summary>
        /// Read object on the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonObject">The json object</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadObject(String json, LazyJsonReaderOptions jsonReaderOptions, out LazyJsonObject jsonObject, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            if (index < json.Length)
            {
                jsonObject = new LazyJsonObject();

                while (index < json.Length)
                {
                    ReadDisposableContent(json, jsonReaderOptions, ref line, ref column, ref index);

                    if (index < json.Length)
                    {
                        if (json[index] == '\"')
                        {
                            index++; column++;
                            LazyJsonProperty jsonProperty = null;
                            ReadProperty(json, jsonReaderOptions, out jsonProperty, ref line, ref column, ref index);

                            if (jsonObject[jsonProperty.Name] == null)
                            {
                                jsonObject.Add(jsonProperty);
                            }
                            else
                            {
                                throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionTokenObjectPropertyAlreadyExists, line, column, jsonProperty.Name));
                            }
                        }
                        else if (json[index] == '}' && jsonObject.Count == 0)
                        {
                            index++; column++;
                            break;
                        }
                        else
                        {
                            throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeObjectProperty, json[index]));
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeObjectProperty, String.Empty));
                    }

                    ReadDisposableContent(json, jsonReaderOptions, ref line, ref column, ref index);

                    if (index < json.Length)
                    {
                        if (json[index] == ',') { index++; column++; continue; }
                        if (json[index] == '}') { index++; column++; break; }

                        throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeObjectNextOrEnd, json[index]));
                    }
                    else
                    {
                        throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeObjectNextOrEnd, String.Empty));
                    }
                }
            }
            else
            {
                throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeObjectProperty, String.Empty));
            }
        }

        /// <summary>
        /// Read property on the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonProperty">The json property</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadProperty(String json, LazyJsonReaderOptions jsonReaderOptions, out LazyJsonProperty jsonProperty, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            LazyJsonString jsonStringPropertyName = null;
            ReadString(json, jsonReaderOptions, out jsonStringPropertyName, ref line, ref column, ref index);

            ReadDisposableContent(json, jsonReaderOptions, ref line, ref column, ref index);

            if (index < json.Length)
            {
                if (json[index] == ':')
                {
                    index++; column++;
                    ReadDisposableContent(json, jsonReaderOptions, ref line, ref column, ref index);

                    if (index < json.Length)
                    {
                        if (json[index] == '[')
                        {
                            index++; column++;
                            LazyJsonArray jsonArray = null;
                            ReadArray(json, jsonReaderOptions, out jsonArray, ref line, ref column, ref index);
                            jsonProperty = new LazyJsonProperty(jsonStringPropertyName.Value, jsonArray);
                        }
                        else if (json[index] == '{')
                        {
                            index++; column++;
                            LazyJsonObject jsonObject = null;
                            ReadObject(json, jsonReaderOptions, out jsonObject, ref line, ref column, ref index);
                            jsonProperty = new LazyJsonProperty(jsonStringPropertyName.Value, jsonObject);
                        }
                        else if (json[index] == '\"')
                        {
                            index++; column++;
                            LazyJsonString jsonString = null;
                            ReadString(json, jsonReaderOptions, out jsonString, ref line, ref column, ref index);
                            jsonProperty = new LazyJsonProperty(jsonStringPropertyName.Value, jsonString);
                        }
                        else if (json[index] == '-')
                        {
                            index++; column++;
                            LazyJsonToken jsonToken = null;
                            ReadIntegerOrDecimalNegative(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);
                            jsonProperty = new LazyJsonProperty(jsonStringPropertyName.Value, jsonToken);
                        }
                        else if (Char.IsDigit(json[index]) == true)
                        {
                            LazyJsonToken jsonToken = null;
                            ReadIntegerOrDecimal(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);
                            jsonProperty = new LazyJsonProperty(jsonStringPropertyName.Value, jsonToken);
                        }
                        else if (Char.IsLetter(json[index]) == true)
                        {
                            LazyJsonToken jsonToken = null;
                            ReadNullOrBoolean(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);
                            jsonProperty = new LazyJsonProperty(jsonStringPropertyName.Value, jsonToken);
                        }
                        else
                        {
                            throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, LazyResourcesJson.LazyJsonCaptionTokenType, json[index]));
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenType, String.Empty));
                    }
                }
                else
                {
                    throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeObjectPropertyValue, json[index]));
                }
            }
            else
            {
                throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeObjectPropertyValue, String.Empty));
            }
        }

        /// <summary>
        /// Read string on the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonString">The json string</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadString(String json, LazyJsonReaderOptions jsonReaderOptions, out LazyJsonString jsonString, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            Int32 startIndex = index;
            while (index < json.Length)
            {
                if (json[index] == '\"')
                {
                    break;
                }
                else
                {
                    switch (json[index])
                    {
                        case '\\': index += 2; column += 2; break;
                        case '\n': index++; column = 1; line++; break;
                        default: index++; column++; break;
                    }
                }
            }

            if (index < json.Length)
            {
                jsonString = new LazyJsonString(json.Substring(startIndex, index - startIndex));
                index++; column++;
            }
            else
            {
                throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeStringEnd, String.Empty));
            }
        }

        /// <summary>
        /// Read negative integer or decimal on the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonToken">The json token</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadIntegerOrDecimalNegative(String json, LazyJsonReaderOptions jsonReaderOptions, out LazyJsonToken jsonToken, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            if (index < json.Length)
            {
                if (Char.IsDigit(json[index]) == true)
                {
                    ReadIntegerOrDecimal(json, jsonReaderOptions, out jsonToken, ref line, ref column, ref index);

                    if (jsonToken.Type == LazyJsonType.Integer) { ((LazyJsonInteger)jsonToken).Value *= (-1); return; }
                    if (jsonToken.Type == LazyJsonType.Decimal) { ((LazyJsonDecimal)jsonToken).Value *= (-1); return; }
                }
                else
                {
                    throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeIntegerOrDecimal, json[index]));
                }
            }
            else
            {
                throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeIntegerOrDecimal, String.Empty));
            }
        }

        /// <summary>
        /// Read integer or decimal on the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonToken">The json token</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadIntegerOrDecimal(String json, LazyJsonReaderOptions jsonReaderOptions, out LazyJsonToken jsonToken, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            Int32 startIndex = index;
            while (index < json.Length && Char.IsDigit(json[index]) == true)
                index++;

            if (index < json.Length && json[index] == '.')
            {
                index++;
                while (index < json.Length && Char.IsDigit(json[index]) == true)
                    index++;
            }

            column += (index - startIndex);
            String token = json.Substring(startIndex, index - startIndex);

            if (token.Contains('.') == false)
            {
                jsonToken = new LazyJsonInteger(Convert.ToInt64(token));
            }
            else
            {
                if (token.Substring(token.IndexOf('.') + 1).Length > 0)
                {
                    jsonToken = new LazyJsonDecimal(Convert.ToDecimal(token, CultureInfo.InvariantCulture));
                }
                else
                {
                    throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionTokenDecimalMissingDecimalPlaces, line, column));
                }
            }
        }

        /// <summary>
        /// Read null or boolean on the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="jsonToken">The json token</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadNullOrBoolean(String json, LazyJsonReaderOptions jsonReaderOptions, out LazyJsonToken jsonToken, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            Int32 startIndex = index;
            while (index < json.Length && Char.IsLetter(json[index]) == true)
                index++;

            column += (index - startIndex);
            String token = json.Substring(startIndex, index - startIndex);

            switch (token.ToLower())
            {
                case "null": jsonToken = new LazyJsonNull(); break;
                case "true": jsonToken = new LazyJsonBoolean(true); break;
                case "false": jsonToken = new LazyJsonBoolean(false); break;
                default: throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedToken, line, column, LazyResourcesJson.LazyJsonCaptionTokenTypeNullOrBoolean, token));
            }
        }

        /// <summary>
        /// Read disposable content on the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadDisposableContent(String json, LazyJsonReaderOptions jsonReaderOptions, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            while (index < json.Length)
            {
                if (json[index] == ' ') { index++; column++; continue; }
                if (json[index] == '\r') { index++; column++; continue; }
                if (json[index] == '\n') { index++; column = 1; line++; continue; }
                if (json[index] == '\t') { index++; column++; continue; }

                if (json[index] == '/')
                {
                    index++; column++;
                    ReadComments(json, jsonReaderOptions, ref line, ref column, ref index);
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Read comments on the json
        /// </summary>
        /// <param name="json">The json to be read</param>
        /// <param name="jsonReaderOptions">The json reader options</param>
        /// <param name="line">The current line</param>
        /// <param name="column">The current column</param>
        /// <param name="index">The current index</param>
        private static void ReadComments(String json, LazyJsonReaderOptions jsonReaderOptions, ref Int32 line, ref Int32 column, ref Int32 index)
        {
            if (index < json.Length)
            {
                if (json[index] == '/')
                {
                    index++;

                    while (index < json.Length && json[index] != '\n')
                        index++;

                    index++; column = 1; line++;
                }
                else if (json[index] == '*')
                {
                    index++; column++;

                    Boolean inBlockCommentComplete = false;

                    while (index < (json.Length - 1))
                    {
                        if (json[index] == '*' && json[index + 1] == '/')
                        {
                            index += 2; column += 2;
                            inBlockCommentComplete = true;
                            break;
                        }
                        else if (json[index] == '\n')
                        {
                            index++; column = 1; line++;
                        }
                        else
                        {
                            index++; column++;
                        }
                    }

                    if (inBlockCommentComplete == false)
                    {
                        throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionCommentsInBlockEnd, String.Empty));
                    }
                }
                else
                {
                    throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedCharacter, line, column, LazyResourcesJson.LazyJsonCaptionCommentsInLineOrInBlock, json[index]));
                }
            }
            else
            {
                throw new Exception(String.Format(LazyResourcesJson.LazyJsonReaderExceptionUnexpectedEnd, line, column, LazyResourcesJson.LazyJsonCaptionCommentsInLineOrInBlock, String.Empty));
            }
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}

// LazyJsonWriter.cs
//
// This file is integrated part of "Lazy Vinke Json" solution
// Licensed under "Gnu General Public License Version 3"
//
// Created by Isaac Bezerra Saraiva
// Created on 2023, October 06

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace Lazy.Vinke.Json
{
    public static class LazyJsonWriter
    {
        #region Variables
        #endregion Variables

        #region Methods

        /// <summary>
        /// Write the json
        /// </summary>
        /// <param name="lazyJson">The lazy json</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <returns>The json</returns>
        public static String Write(LazyJson lazyJson, LazyJsonWriterOptions jsonWriterOptions = null)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (jsonWriterOptions == null)
                jsonWriterOptions = new LazyJsonWriterOptions();

            WriteRoot(stringBuilder, jsonWriterOptions, lazyJson.Root);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Write the root node of the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonToken">The json token</param>
        private static void WriteRoot(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonToken jsonToken)
        {
            Int32 indentLevel = 0;

            switch (jsonToken.Type)
            {
                case LazyJsonType.Null: stringBuilder.Append("null"); break;
                case LazyJsonType.Boolean: WriteBoolean(stringBuilder, jsonWriterOptions, (LazyJsonBoolean)jsonToken, "{0}"); break;
                case LazyJsonType.Integer: WriteInteger(stringBuilder, jsonWriterOptions, (LazyJsonInteger)jsonToken, "{0}"); break;
                case LazyJsonType.Decimal: WriteDecimal(stringBuilder, jsonWriterOptions, (LazyJsonDecimal)jsonToken, "{0}"); break;
                case LazyJsonType.String: WriteString(stringBuilder, jsonWriterOptions, (LazyJsonString)jsonToken, "{0}"); break;
                case LazyJsonType.Object: WriteObject(stringBuilder, jsonWriterOptions, (LazyJsonObject)jsonToken, ref indentLevel, false); break;
                case LazyJsonType.Array: WriteArray(stringBuilder, jsonWriterOptions, (LazyJsonArray)jsonToken, ref indentLevel, false); break;
            }
        }

        /// <summary>
        /// Write boolean on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonBoolean">The json boolean</param>
        /// <param name="format">The string format</param>
        private static void WriteBoolean(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonBoolean jsonBoolean, String format)
        {
            stringBuilder.Append(String.Format(format, jsonBoolean.Value == null ? "null" : jsonBoolean.Value.ToString().ToLower()));
        }

        /// <summary>
        /// Write integer on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonInteger">The json integer</param>
        /// <param name="format">The string format</param>
        private static void WriteInteger(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonInteger jsonInteger, String format)
        {
            stringBuilder.Append(String.Format(format, jsonInteger.Value == null ? "null" : jsonInteger.Value.ToString()));
        }

        /// <summary>
        /// Write decimal on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonDecimal">The json decimal</param>
        /// <param name="format">The string format</param>
        private static void WriteDecimal(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonDecimal jsonDecimal, String format)
        {
            stringBuilder.Append(String.Format(format, jsonDecimal.Value == null ? "null" : jsonDecimal.Value.ToString().Replace(',', '.')));
        }

        /// <summary>
        /// Write string on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonString">The json string</param>
        /// <param name="format">The string format</param>
        private static void WriteString(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonString jsonString, String format)
        {
            stringBuilder.Append(String.Format(format, jsonString.Value == null ? "null" : "\"" + jsonString.Value + "\""));
        }

        /// <summary>
        /// Write object on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonObject">The json object</param>
        /// <param name="indentLevel">The indent level</param>
        /// <param name="isArrayIndex">Set whether the token is being written on an array index</param>
        private static void WriteObject(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonObject jsonObject, ref Int32 indentLevel, Boolean isArrayIndex)
        {
            String openKey = "{";
            String closeKey = "}";

            if (jsonWriterOptions.Indent == true)
            {
                if (isArrayIndex == true)
                    openKey = String.Format("{0}{1}{2}", Environment.NewLine, String.Empty.PadRight(jsonWriterOptions.IndentSize * indentLevel), "{");

                if (jsonObject.Count > 0 || jsonWriterOptions.IndentEmptyObject == true)
                    closeKey = String.Format("{0}{1}{2}", Environment.NewLine, String.Empty.PadRight(jsonWriterOptions.IndentSize * indentLevel), "}");
            }

            indentLevel++;
            stringBuilder.Append(openKey);

            for (int i = 0; i < jsonObject.Count; i++)
            {
                WriteProperty(stringBuilder, jsonWriterOptions, jsonObject[i], ref indentLevel);
                stringBuilder.Append(",");
            }

            if (stringBuilder[stringBuilder.Length - 1] == ',')
                stringBuilder.Remove(stringBuilder.Length - 1, 1);

            stringBuilder.Append(closeKey);
            indentLevel--;
        }

        /// <summary>
        /// Write array on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonArray">The json array</param>
        /// <param name="indentLevel">The indent level</param>
        /// <param name="isArrayIndex">Set whether the token is being written on an array index</param>
        private static void WriteArray(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonArray jsonArray, ref Int32 indentLevel, Boolean isArrayIndex)
        {
            String format = "{0}";
            String openBracket = "[";
            String closeBracket = "]";

            if (jsonWriterOptions.Indent == true)
            {
                format = String.Format("{0}{1}{2}", Environment.NewLine, String.Empty.PadRight(jsonWriterOptions.IndentSize * (indentLevel + 1)), "{0}");

                if (isArrayIndex == true)
                    openBracket = String.Format("{0}{1}{2}", Environment.NewLine, String.Empty.PadRight(jsonWriterOptions.IndentSize * indentLevel), "[");

                if (jsonArray.Length > 0 || jsonWriterOptions.IndentEmptyArray == true)
                    closeBracket = String.Format("{0}{1}{2}", Environment.NewLine, String.Empty.PadRight(jsonWriterOptions.IndentSize * indentLevel), "]");
            }

            stringBuilder.Append(openBracket);

            for (int i = 0; i < jsonArray.Length; i++)
            {
                switch (jsonArray[i].Type)
                {
                    case LazyJsonType.Null: stringBuilder.Append(String.Format(format, "null")); break;
                    case LazyJsonType.Boolean: WriteBoolean(stringBuilder, jsonWriterOptions, (LazyJsonBoolean)jsonArray[i], format); break;
                    case LazyJsonType.Integer: WriteInteger(stringBuilder, jsonWriterOptions, (LazyJsonInteger)jsonArray[i], format); break;
                    case LazyJsonType.Decimal: WriteDecimal(stringBuilder, jsonWriterOptions, (LazyJsonDecimal)jsonArray[i], format); break;
                    case LazyJsonType.String: WriteString(stringBuilder, jsonWriterOptions, (LazyJsonString)jsonArray[i], format); break;
                    case LazyJsonType.Object: indentLevel++; WriteObject(stringBuilder, jsonWriterOptions, (LazyJsonObject)jsonArray[i], ref indentLevel, true); indentLevel--; break;
                    case LazyJsonType.Array: indentLevel++; WriteArray(stringBuilder, jsonWriterOptions, (LazyJsonArray)jsonArray[i], ref indentLevel, true); indentLevel--; break;
                }

                stringBuilder.Append(",");
            }

            if (stringBuilder[stringBuilder.Length - 1] == ',')
                stringBuilder.Remove(stringBuilder.Length - 1, 1);

            stringBuilder.Append(closeBracket);
        }

        /// <summary>
        /// Write property on the json
        /// </summary>
        /// <param name="stringBuilder">The string builder</param>
        /// <param name="jsonWriterOptions">The json writer options</param>
        /// <param name="jsonProperty">The json property</param>
        /// <param name="indentLevel">The indent level</param>
        private static void WriteProperty(StringBuilder stringBuilder, LazyJsonWriterOptions jsonWriterOptions, LazyJsonProperty jsonProperty, ref Int32 indentLevel)
        {
            String property = "\"{0}\":";

            if (jsonWriterOptions.Indent == true)
                property = Environment.NewLine + String.Empty.PadRight(jsonWriterOptions.IndentSize * indentLevel) + property + " ";

            property = String.Format(property, jsonProperty.Name);

            stringBuilder.Append(property);

            switch (jsonProperty.Token.Type)
            {
                case LazyJsonType.Null: stringBuilder.Append("null"); break;
                case LazyJsonType.Boolean: WriteBoolean(stringBuilder, jsonWriterOptions, (LazyJsonBoolean)jsonProperty.Token, "{0}"); break;
                case LazyJsonType.Integer: WriteInteger(stringBuilder, jsonWriterOptions, (LazyJsonInteger)jsonProperty.Token, "{0}"); break;
                case LazyJsonType.Decimal: WriteDecimal(stringBuilder, jsonWriterOptions, (LazyJsonDecimal)jsonProperty.Token, "{0}"); break;
                case LazyJsonType.String: WriteString(stringBuilder, jsonWriterOptions, (LazyJsonString)jsonProperty.Token, "{0}"); break;
                case LazyJsonType.Object: WriteObject(stringBuilder, jsonWriterOptions, (LazyJsonObject)jsonProperty.Token, ref indentLevel, false); break;
                case LazyJsonType.Array: WriteArray(stringBuilder, jsonWriterOptions, (LazyJsonArray)jsonProperty.Token, ref indentLevel, false); break;
            }
        }

        #endregion Methods

        #region Properties
        #endregion Properties
    }
}
